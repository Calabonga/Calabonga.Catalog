using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.Catalog2023.Web.Application.Services;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductPostUpdateRequest(ProductUpdateViewModel Model, ClaimsPrincipal User)
    : IRequest<OperationResult<ProductViewModel>>;

public class ProductPostUpdateRequestHandler
    : IRequestHandler<ProductPostUpdateRequest, OperationResult<ProductViewModel>>
{
    private readonly ITagCalculator _tagCalculator;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductPostUpdateRequestHandler(
        ITagCalculator tagCalculator,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _tagCalculator = tagCalculator;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductViewModel>> Handle(
        ProductPostUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ProductViewModel>();
        var repository = _unitOfWork.GetRepository<Product>();

        var entity = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Model.Id,
            disableTracking: false,
            ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (entity is null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Product), request.Model.Id.ToString()));
            return operation;
        }

        _mapper.Map(request.Model, entity,
            o => o.Items[nameof(ApplicationUser)] = request.User.Identity!.Name);

        if (!entity.Category!.Visible && entity.Visible)
        {
            entity.Visible = false;
            operation.AddError(new CatalogInvalidOperationException("Unable to enable Product", "Catalog is disabled"));
            return operation;
        }

        var calculation = await _tagCalculator.ProcessTagsAsync(
            request.Model.Tags.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries),
            entity,
            cancellationToken);

        if (!calculation.Competed)
        {
            operation.AddError(calculation.Exception!);
            return operation;
        }

        repository.Update(entity);

        await _unitOfWork.SaveChangesAsync();

        if (!_unitOfWork.LastSaveChangesResult.IsOk)
        {
            var exception = _unitOfWork.LastSaveChangesResult.Exception
                            ?? new CatalogDatabaseSaveException("Error saving entity Product");

            operation.AddError(exception);
            return operation;
        }

        var result = _mapper.Map<ProductViewModel>(entity);

        operation.Result = result;

        return operation;

    }
}
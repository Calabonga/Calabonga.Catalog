using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewPostCreateRequest(ReviewCreateViewModel Model, ClaimsPrincipal User)
    : IRequest<OperationResult<ReviewViewModel>>;

public class ReviewPostCreateRequestHandler
    : IRequestHandler<ReviewPostCreateRequest, OperationResult<ReviewViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewPostCreateRequestHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ReviewViewModel>> Handle(
        ReviewPostCreateRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ReviewViewModel>();

        var product = await _unitOfWork
            .GetRepository<Product>()
            .GetFirstOrDefaultAsync(predicate: x => x.Id == request.Model.ProductId);

        if (product == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Product), request.Model.ProductId.ToString()));
            return operation;
        }

        var repository = _unitOfWork.GetRepository<Review>();

        var entity = _mapper.Map<Review>(request.Model,
                    o => o.Items[nameof(ApplicationUser)] = request.User.Identity!.Name);

        await repository.InsertAsync(entity, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        if (!_unitOfWork.LastSaveChangesResult.IsOk)
        {
            var exception = _unitOfWork.LastSaveChangesResult.Exception
                            ?? new CatalogDatabaseSaveException("Error saving entity Review");

            operation.AddError(exception);
            return operation;
        }

        var result = _mapper.Map<ReviewViewModel>(entity);

        operation.Result = result;

        return operation;

    }
}
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductGetForUpdateRequest(Guid Id, ClaimsPrincipal User)
    : IRequest<OperationResult<ProductUpdateViewModel>>;

public class ProductGetForUpdateRequestHandler
    : IRequestHandler<ProductGetForUpdateRequest, OperationResult<ProductUpdateViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetForUpdateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductUpdateViewModel>> Handle(
        ProductGetForUpdateRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ProductUpdateViewModel>();

        var result = await _unitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(
            selector: ProductExpressions.ForEdit,
            predicate: x => x.Id == request.Id,
            ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (result is null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Product), request.Id.ToString()));
            return operation;
        }

        var items = await _unitOfWork.GetRepository<Category>()
            .GetAllAsync(
                selector: CategoryExpressions.Default,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (!items.Any())
        {
            operation.AddError("Does not contains any categories");
            return operation;
        }

        result.Categories = items.ToList();
        operation.Result = result;
        return operation;
    }
}
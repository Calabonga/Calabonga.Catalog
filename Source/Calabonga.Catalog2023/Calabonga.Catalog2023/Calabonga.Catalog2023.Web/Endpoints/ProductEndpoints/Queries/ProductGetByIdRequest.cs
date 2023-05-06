using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductGetByIdRequest(Guid ProductId, ClaimsPrincipal User)
    : IRequest<OperationResult<ProductViewModel>>;

public class ProductGetByIdRequestHandler
    : IRequestHandler<ProductGetByIdRequest, OperationResult<ProductViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetByIdRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductViewModel>> Handle
    (
        ProductGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ProductViewModel>();

        var item = await _unitOfWork.GetRepository<Product>()
            .GetFirstOrDefaultAsync(
                selector: ProductExpressions.Default,
                predicate: x => x.Id == request.ProductId,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item != null)
        {
            operation.Result = item;
            return operation;
        }

        operation.AddError(new CatalogNotFoundException(nameof(Product), request.ProductId.ToString()));
        return operation;
    }
}
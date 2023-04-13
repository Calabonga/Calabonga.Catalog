using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductGetAllRequest(ClaimsPrincipal User)
    : IRequest<OperationResult<List<ProductViewModel>>>;

public class ProductGetAllRequestHandler
    : IRequestHandler<ProductGetAllRequest, OperationResult<List<ProductViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetAllRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<ProductViewModel>>> Handle(
        ProductGetAllRequest request,
        CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.GetRepository<Product>()
            .GetAllAsync(
                selector: ProductExpressions.Default,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        return OperationResult.CreateResult(items.ToList());
    }
}
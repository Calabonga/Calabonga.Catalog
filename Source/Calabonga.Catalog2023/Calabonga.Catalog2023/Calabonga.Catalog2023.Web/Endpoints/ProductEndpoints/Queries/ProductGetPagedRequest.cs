using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductGetPagedRequest(int PageIndex, int PageSize, ClaimsPrincipal User)
    : IRequest<OperationResult<IPagedList<ProductViewModel>>>;

public class ProductGetPagedRequestHandler
    : IRequestHandler<ProductGetPagedRequest,
        OperationResult<IPagedList<ProductViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetPagedRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IPagedList<ProductViewModel>>> Handle
    (
        ProductGetPagedRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IPagedList<ProductViewModel>>();

        var result = await _unitOfWork.GetRepository<Product>()
            .GetPagedListAsync(
                selector: ProductExpressions.Default,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName),
                cancellationToken: cancellationToken);

        operation.Result = result;
        return operation;
    }
}


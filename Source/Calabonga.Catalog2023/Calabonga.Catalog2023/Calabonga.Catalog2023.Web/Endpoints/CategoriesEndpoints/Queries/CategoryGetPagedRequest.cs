using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;

public record CategoryGetPagedRequest(int PageIndex, int PageSize, ClaimsPrincipal User)
    : IRequest<OperationResult<IPagedList<CategoryViewModel>>>;

public class CategoryGetPagedRequestHandler
    : IRequestHandler<CategoryGetPagedRequest,
        OperationResult<IPagedList<CategoryViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryGetPagedRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IPagedList<CategoryViewModel>>> Handle
    (
        CategoryGetPagedRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IPagedList<CategoryViewModel>>();

        var result = await _unitOfWork.GetRepository<Category>()
            .GetPagedListAsync(
                selector: CategoryExpressions.Default,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName),
                cancellationToken: cancellationToken);

        operation.Result = result;
        return operation;
    }
}


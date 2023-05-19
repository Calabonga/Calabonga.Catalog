using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewGetPagedRequest(int PageIndex, int PageSize, ClaimsPrincipal User)
    : IRequest<OperationResult<IPagedList<ReviewViewModel>>>;

public class ReviewGetPagedRequestHandler
    : IRequestHandler<ReviewGetPagedRequest,
        OperationResult<IPagedList<ReviewViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewGetPagedRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IPagedList<ReviewViewModel>>> Handle
    (
        ReviewGetPagedRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IPagedList<ReviewViewModel>>();

        var result = await _unitOfWork.GetRepository<Review>()
            .GetPagedListAsync(
                selector: ReviewExpressions.Default,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName),
                cancellationToken: cancellationToken);

        operation.Result = result;
        return operation;
    }
}


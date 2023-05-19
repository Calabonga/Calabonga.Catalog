using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewGetForProductRequest(Guid ProductId, ClaimsPrincipal User)
    : IRequest<OperationResult<IEnumerable<ReviewViewModel>>>;

public class ReviewGetForProductHandler
    : IRequestHandler<ReviewGetForProductRequest,
        OperationResult<IEnumerable<ReviewViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewGetForProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IEnumerable<ReviewViewModel>>> Handle
    (
        ReviewGetForProductRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IEnumerable<ReviewViewModel>>();

        var result = await _unitOfWork.GetRepository<Review>()
            .GetAllAsync(
                selector: ReviewExpressions.Default,
                predicate: x => x.ProductId == request.ProductId,
                include: i => i.Include(x => x.Product),
                orderBy: o => o.OrderByDescending(x => x.CreatedAt),
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        operation.Result = result;
        return operation;
    }
}


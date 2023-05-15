using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewGetLastRequest(int TotalCount, ClaimsPrincipal User)
    : IRequest<OperationResult<IEnumerable<ReviewWithProductViewModel>>>;

public class ReviewGetLastRequestHandler
    : IRequestHandler<ReviewGetLastRequest,
        OperationResult<IEnumerable<ReviewWithProductViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewGetLastRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<IEnumerable<ReviewWithProductViewModel>>> Handle
    (
        ReviewGetLastRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IEnumerable<ReviewWithProductViewModel>>();

        var result = await _unitOfWork.GetRepository<Review>()
            .GetAll(
                selector: ReviewExpressions.WithProductName,
                include: i => i.Include(x => x.Product),
                orderBy: o => o.OrderByDescending(x => x.CreatedAt),
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName))
            .Take(request.TotalCount)
            .ToListAsync(cancellationToken);

        operation.Result = result;
        return operation;
    }
}


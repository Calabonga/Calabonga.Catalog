using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewGetByIdRequest(Guid ReviewId, ClaimsPrincipal User)
    : IRequest<OperationResult<ReviewViewModel>>;

public class ReviewGetByIdRequestHandler
    : IRequestHandler<ReviewGetByIdRequest, OperationResult<ReviewViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewGetByIdRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ReviewViewModel>> Handle
    (
        ReviewGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ReviewViewModel>();

        var item = await _unitOfWork.GetRepository<Review>()
            .GetFirstOrDefaultAsync(
                selector: ReviewExpressions.Default,
                predicate: x => x.Id == request.ReviewId,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item != null)
        {
            operation.Result = item;
            return operation;
        }

        operation.AddError(new CatalogNotFoundException(nameof(Review), request.ReviewId.ToString()));
        return operation;
    }
}
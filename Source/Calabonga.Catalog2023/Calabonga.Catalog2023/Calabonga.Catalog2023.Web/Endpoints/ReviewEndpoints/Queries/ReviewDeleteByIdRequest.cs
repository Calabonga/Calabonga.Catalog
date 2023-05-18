using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewDeleteByIdRequest(Guid ReviewId, ClaimsPrincipal User)
    : IRequest<OperationResult<ReviewViewModel>>;

public class ReviewDeleteByIdRequestHandler
    : IRequestHandler<ReviewDeleteByIdRequest, OperationResult<ReviewViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewDeleteByIdRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ReviewViewModel>> Handle
    (
        ReviewDeleteByIdRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ReviewViewModel>();
        var repository = _unitOfWork.GetRepository<Review>();
        var item = await repository
            .GetFirstOrDefaultAsync(
                selector: ReviewExpressions.Default,
                predicate: x => x.Id == request.ReviewId,
                disableTracking: false,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item != null)
        {
            repository.Delete(request.ReviewId);
            await _unitOfWork.SaveChangesAsync();
            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                var exception = _unitOfWork.LastSaveChangesResult.Exception
                                ?? new CatalogDatabaseSaveException(nameof(Review));
                operation.AddError(exception);
                return operation;
            }

            operation.Result = item;
            return operation;
        }

        operation.AddError(new CatalogNotFoundException(nameof(Review), request.ReviewId.ToString()));
        return operation;
    }
}
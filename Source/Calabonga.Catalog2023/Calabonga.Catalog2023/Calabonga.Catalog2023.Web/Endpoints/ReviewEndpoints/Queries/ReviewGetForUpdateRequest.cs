using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;
using Calabonga.PredicatesBuilder;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewGetForUpdateRequest(Guid Id, ClaimsPrincipal User)
    : IRequest<OperationResult<ReviewUpdateViewModel>>;

public class ReviewGetForUpdateRequestHandler
    : IRequestHandler<ReviewGetForUpdateRequest, OperationResult<ReviewUpdateViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewGetForUpdateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ReviewUpdateViewModel>> Handle(
        ReviewGetForUpdateRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ReviewUpdateViewModel>();

        var predicate = PredicateBuilder
            .True<Review>()
            .And(x => x.Id == request.Id);

        if (!request.User.IsInRole(AppData.SystemAdministratorRoleName))
        {
            predicate = predicate.And(x => x.UserName == request.User.Identity!.Name);
        }

        var entity = await _unitOfWork
            .GetRepository<Review>()
            .GetFirstOrDefaultAsync(
                predicate: predicate,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (entity == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Review), request.Id.ToString()));
            return operation;
        }

        var result = new ReviewUpdateViewModel
        {
            Content = entity.Content,
            UserName = entity.UserName,
            Id = entity.Id,
            Rating = entity.Rating
        };

        operation.Result = result;
        return operation;
    }
}
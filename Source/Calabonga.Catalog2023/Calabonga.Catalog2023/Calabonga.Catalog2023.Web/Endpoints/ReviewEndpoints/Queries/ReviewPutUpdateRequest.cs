using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries
{
    public record ReviewPutUpdateRequest(ReviewUpdateViewModel Model, ClaimsPrincipal User)
        : IRequest<OperationResult<ReviewViewModel>>;

    public class ReviewPutUpdateRequestHandler
        : IRequestHandler<ReviewPutUpdateRequest, OperationResult<ReviewViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewPutUpdateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<ReviewViewModel>> Handle(ReviewPutUpdateRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<ReviewViewModel>();

            var predicate = PredicateBuilder
                .True<Review>()
                .And(x => x.Id == request.Model.Id);

            if (!request.User.IsInRole(AppData.SystemAdministratorRoleName))
            {
                predicate = predicate.And(x => x.UserName == request.User.Identity!.Name);
            }

            var repository = _unitOfWork.GetRepository<Review>();

            var entity = await repository
                .GetFirstOrDefaultAsync(
                    predicate: predicate,
                    disableTracking: false,
                    ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

            if (entity == null)
            {
                operation.AddError(new CatalogNotFoundException(nameof(Review), request.Model.Id.ToString()));
                return operation;
            }

            _mapper.Map(request.Model, entity,
                o => o.Items[nameof(ApplicationUser)] = request.User.Identity!.Name);

            repository.Update(entity);

            await _unitOfWork.SaveChangesAsync();

            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                var exception = _unitOfWork.LastSaveChangesResult.Exception ?? new CatalogDatabaseSaveException(nameof(Review));
                operation.AddError(exception);
                return operation;
            }

            operation.Result = _mapper.Map<ReviewViewModel>(entity);
            return operation;
        }
    }
}

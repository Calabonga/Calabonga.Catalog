using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries
{
    public record CategoryGetByIdRequest(Guid CategoryId, ClaimsPrincipal User) : IRequest<OperationResult<CategoryViewModel>>;

    public class CategoryGetByIdRequestHandler
        : IRequestHandler<CategoryGetByIdRequest, OperationResult<CategoryViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryGetByIdRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<CategoryViewModel>> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<CategoryViewModel>();

            var predicate = PredicateBuilder.True<Category>();
            predicate = predicate.And(x => x.Id == request.CategoryId);

            if (!request.User.IsInRole(AppData.SystemAdministratorRoleName))
            {
                predicate = predicate.And(x => x.Visible);
            }

            var item = await _unitOfWork.GetRepository<Category>()
                .GetFirstOrDefaultAsync(
                    selector: CategoryExpressions.Default,
                    predicate: predicate);

            if (item != null)
            {
                operation.Result = item;
                return operation;
            }

            operation.AddError(new CatalogNotFoundException($"Item {request.CategoryId} not found"));
            return operation;
        }
    }
}

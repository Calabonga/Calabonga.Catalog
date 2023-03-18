using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.PredicatesBuilder;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries
{
    public record CategoryGetAllRequest(ClaimsPrincipal User) : IRequest<OperationResult<List<CategoryViewModel>>>;

    public class CategoryGetAllRequestHandler : IRequestHandler<CategoryGetAllRequest, OperationResult<List<CategoryViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryGetAllRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<CategoryViewModel>>> Handle(
            CategoryGetAllRequest request,
            CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<Category>();

            if (!request.User.IsInRole(AppData.SystemAdministratorRoleName))
            {
                predicate = predicate.And(x => x.Visible);
            }

            var items = await _unitOfWork.GetRepository<Category>()
                .GetAllAsync(
                    selector: CategoryExpressions.Default,
                    predicate: predicate,
                    include: i => i.Include(x => x.Products!));

            return OperationResult.CreateResult(items.ToList());
        }
    }


}

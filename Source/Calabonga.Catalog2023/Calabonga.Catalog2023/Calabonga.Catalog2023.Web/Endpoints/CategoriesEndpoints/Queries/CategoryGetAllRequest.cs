using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries
{
    public record CategoryGetAllRequest : IRequest<OperationResult<List<CategoryViewModel>>>;

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
            var items = await _unitOfWork.GetRepository<Category>()
                .GetAllAsync(
                    selector: s => new CategoryViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        ProductCount = s.Products!.Count
                    },
                    predicate: x => x.Visible,
                    include: i => i.Include(x => x.Products!));

            return OperationResult.CreateResult(items.ToList());
        }
    }


}

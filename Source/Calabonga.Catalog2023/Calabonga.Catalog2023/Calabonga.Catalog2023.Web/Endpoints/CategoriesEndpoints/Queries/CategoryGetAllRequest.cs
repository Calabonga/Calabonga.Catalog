using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;

public record CategoryGetAllRequest(ClaimsPrincipal User)
    : IRequest<OperationResult<List<CategoryViewModel>>>;

public class CategoryGetAllRequestHandler
    : IRequestHandler<CategoryGetAllRequest, OperationResult<List<CategoryViewModel>>>
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
                selector: CategoryExpressions.Default,
                include: i => i.Include(x => x.Products!), // Calabonga: refactor later (2023-03-25 11:09 CategoryGetAllRequest)
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        return OperationResult.CreateResult(items.ToList());
    }
}
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductGetForCreateRequest(ClaimsPrincipal User)
    : IRequest<OperationResult<ProductCreateViewModel>>;

public class ProductGetForCreateRequestHandler
    : IRequestHandler<ProductGetForCreateRequest, OperationResult<ProductCreateViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetForCreateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductCreateViewModel>> Handle(
        ProductGetForCreateRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ProductCreateViewModel>();

        var result = new ProductCreateViewModel();
        var items = await _unitOfWork.GetRepository<Category>()
            .GetAllAsync(
                selector: CategoryExpressions.Default,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (!items.Any())
        {
            operation.AddError("Does not contains any categories");
            return operation;
        }

        result.Categories = items.ToList();
        operation.Result = result;
        return operation;
    }
}
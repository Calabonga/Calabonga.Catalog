using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;

public record CategoryGetByIdForEditRequest(Guid CategoryId, ClaimsPrincipal User)
    : IRequest<OperationResult<CategoryUpdateViewModel>>;

public class CategoryGetByIdForEditRequestHandler
    : IRequestHandler<CategoryGetByIdForEditRequest, OperationResult<CategoryUpdateViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryGetByIdForEditRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CategoryUpdateViewModel>> Handle
    (
        CategoryGetByIdForEditRequest request,
        CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<CategoryUpdateViewModel>();

        var item = await _unitOfWork.GetRepository<Category>()
            .GetFirstOrDefaultAsync(
                selector: CategoryExpressions.ForEdit,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName)
            );

        if (item == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Category), request.CategoryId.ToString()));
            return operation;
        }

        operation.Result = item;
        return operation;
    }
};
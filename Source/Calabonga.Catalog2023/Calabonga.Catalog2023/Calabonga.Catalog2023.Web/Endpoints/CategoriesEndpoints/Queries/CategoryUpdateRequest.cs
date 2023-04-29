using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;

public record CategoryUpdateRequest(CategoryUpdateViewModel Model, ClaimsPrincipal User)
    : IRequest<OperationResult<CategoryViewModel>>;

public class CategoryUpdateRequestHandler
    : IRequestHandler<CategoryUpdateRequest, OperationResult<CategoryViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryUpdateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CategoryViewModel>> Handle(CategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<CategoryViewModel>();

        if (!request.User.IsInRole(AppData.SystemAdministratorRoleName))
        {
            operation.AddError(new CatalogAccessDeniedException());
            return operation;
        }

        var repository = _unitOfWork.GetRepository<Category>();
        var item = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Model.Id,
            ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Category), request.Model.Id.ToString()));
            return operation;
        }

        item.Visible = request.Model.Visible;
        item.Description = request.Model.Description;
        item.Name = request.Model.Name;

        repository.Update(item);

        await _unitOfWork.SaveChangesAsync();

        if (!_unitOfWork.LastSaveChangesResult.IsOk)
        {
            var exception = _unitOfWork.LastSaveChangesResult.Exception
                            ?? new CatalogDatabaseSaveException(nameof(Category));

            operation.AddError(exception);
            return operation;
        }

        operation.Result = new CategoryViewModel
        {
            Description = item.Description,
            Id = item.Id,
            Name = item.Name,
            ProductCount = 0
        };

        return operation;
    }
}
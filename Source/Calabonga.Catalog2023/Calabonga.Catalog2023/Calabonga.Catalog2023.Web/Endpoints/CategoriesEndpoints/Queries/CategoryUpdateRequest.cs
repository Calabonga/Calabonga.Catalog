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
    : IRequest<OperationResult<CategoryEditViewModel>>;

public class CategoryUpdateRequestHandler
    : IRequestHandler<CategoryUpdateRequest, OperationResult<CategoryEditViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryUpdateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CategoryEditViewModel>> Handle(CategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<CategoryEditViewModel>();

        if (!request.User.IsInRole(AppData.SystemAdministratorRoleName))
        {
            operation.AddError(new CatalogAccessDeniedException());
            return operation;
        }

        var repository = _unitOfWork.GetRepository<Category>();
        var item = await repository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Model.Id,
            ignoreAutoIncludes: true,
            ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Category), request.Model.Id.ToString()));
            return operation;
        }

        var currentState = item.Visible;
        var newState = request.Model.Visible;

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

        if (currentState && newState == false)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var products = productRepository
                .GetAll(
                    predicate: x => x.CategoryId == item.Id && x.Visible == true,
                    disableTracking: false,
                    ignoreAutoIncludes: true,
                    ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName))
                .ToList();

            if (products.Any())
            {
                products.ForEach(x => x.Visible = newState);
                productRepository.Update(products);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        if (!currentState && newState && request.Model.IsRestoreProducts)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            var products = productRepository
                .GetAll(
                    predicate: x => x.CategoryId == item.Id && x.Visible == false,
                    disableTracking: false,
                    ignoreAutoIncludes: true,
                    ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName))
                .ToList();

            if (products.Any())
            {
                products.ForEach(x => x.Visible = newState);
                productRepository.Update(products);
                await _unitOfWork.SaveChangesAsync();
            }


        }

        operation.Result = new CategoryEditViewModel
        {
            Description = item.Description,
            Id = item.Id,
            Name = item.Name,
            ProductCount = 0
        };

        return operation;
    }
}
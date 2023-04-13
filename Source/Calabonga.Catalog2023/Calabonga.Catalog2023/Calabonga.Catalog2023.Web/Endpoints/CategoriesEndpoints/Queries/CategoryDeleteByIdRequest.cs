using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;

public record CategoryDeleteByIdRequest(Guid CategoryId, ClaimsPrincipal User) : IRequest<OperationResult<Guid>>;

public class CategoryDeleteByIdRequestHandler : IRequestHandler<CategoryDeleteByIdRequest, OperationResult<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryDeleteByIdRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<Guid>> Handle(CategoryDeleteByIdRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<Guid>();

        var repository = _unitOfWork.GetRepository<Category>();

        var item = await repository
            .GetFirstOrDefaultAsync(predicate: x => x.Id == request.CategoryId,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Category), request.CategoryId.ToString()));
            return operation;
        }

        repository.Delete(item);

        await _unitOfWork.SaveChangesAsync();

        if (!_unitOfWork.LastSaveChangesResult.IsOk)
        {
            var exception = _unitOfWork.LastSaveChangesResult.Exception ?? new CatalogDatabaseSaveException(nameof(Category));
            operation.AddError(exception);
            return operation;
        }

        operation.Result = item.Id;
        return operation;
    }
}
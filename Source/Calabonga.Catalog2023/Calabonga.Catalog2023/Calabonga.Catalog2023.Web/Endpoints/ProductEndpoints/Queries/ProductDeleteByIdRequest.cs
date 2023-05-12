using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductDeleteByIdRequest(Guid ProductId, ClaimsPrincipal User) : IRequest<OperationResult<Guid>>;

public class ProductDeleteByIdRequestHandler : IRequestHandler<ProductDeleteByIdRequest, OperationResult<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductDeleteByIdRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<OperationResult<Guid>> Handle(ProductDeleteByIdRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<Guid>();

        var repository = _unitOfWork.GetRepository<Product>();

        var item = await repository
            .GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.ProductId,
                disableTracking: false,
                ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

        if (item == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Product), request.ProductId.ToString()));
            return operation;
        }

        repository.Delete(item);

        await _unitOfWork.SaveChangesAsync();

        if (!_unitOfWork.LastSaveChangesResult.IsOk)
        {
            var exception = _unitOfWork.LastSaveChangesResult.Exception ?? new CatalogDatabaseSaveException(nameof(Product));
            operation.AddError(exception);
            return operation;
        }

        operation.Result = item.Id;
        return operation;
    }
}
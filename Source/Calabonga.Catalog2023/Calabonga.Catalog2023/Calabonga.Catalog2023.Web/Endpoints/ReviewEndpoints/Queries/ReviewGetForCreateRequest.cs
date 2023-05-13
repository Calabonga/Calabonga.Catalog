using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public record ReviewGetForCreateRequest(Guid ProductId, ClaimsPrincipal User)
    : IRequest<OperationResult<ReviewCreateViewModel>>;

public class ReviewGetForCreateRequestHandler
    : IRequestHandler<ReviewGetForCreateRequest, OperationResult<ReviewCreateViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewGetForCreateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ReviewCreateViewModel>> Handle(
        ReviewGetForCreateRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<ReviewCreateViewModel>();
        var product = await _unitOfWork
            .GetRepository<Product>()
            .GetFirstOrDefaultAsync(predicate: x => x.Id == request.ProductId);

        if (product == null)
        {
            operation.AddError(new CatalogNotFoundException(nameof(Product), request.ProductId.ToString()));
            return operation;
        }

        var result = new ReviewCreateViewModel
        {
            UserName = request.User.Identity!.Name!,
            ProductId = request.ProductId
        };

        operation.Result = result;
        return operation;
    }
}
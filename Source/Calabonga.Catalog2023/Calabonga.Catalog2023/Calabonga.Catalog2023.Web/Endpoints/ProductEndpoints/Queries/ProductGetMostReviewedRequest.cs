using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public record ProductGetMostReviewedRequest(ClaimsPrincipal User, int Total = 10)
    : IRequest<OperationResult<List<ProductMostReviewedViewModel>>>;

public class ProductGetMostReviewedRequestHandler
    : IRequestHandler<ProductGetMostReviewedRequest, OperationResult<List<ProductMostReviewedViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetMostReviewedRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<ProductMostReviewedViewModel>>> Handle(
        ProductGetMostReviewedRequest request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Product>();

        var all = await repository.GetAllAsync(
            include: i => i.Include(x => x.Reviews!),
            predicate: x => x.Reviews != null,
            disableTracking: true,
            ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName)
        );

        if (request.Total is < 5 or > 30)
        {
            var operation = OperationResult.CreateResult<List<ProductMostReviewedViewModel>>();
            operation.AddError(new CatalogInvalidOperationException(
                    "Most rated",
                    "total not valid. Required more than 5 and less than 30."));
            return operation;
        }

        var items = all.GroupBy(x => new { Product = x, x.Reviews!.Count })
            .OrderByDescending(x => x.Key.Count)
            .Take(request.Total)
            .Select(x => new ProductMostReviewedViewModel
            {
                Title = x.Key.Product.Name,
                CategoryName = x.Key.Product.Category!.Name,
                TotalReview = x.Key.Count
            })
            .ToList();

        return OperationResult.CreateResult(items.ToList());
    }
}
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

public record ProductGetMostRatedRequest(ClaimsPrincipal User, int Total = 10)
    : IRequest<OperationResult<List<ProductMostRatedViewModel>>>;

public class ProductGetMostRatedRequestHandler
    : IRequestHandler<ProductGetMostRatedRequest, OperationResult<List<ProductMostRatedViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductGetMostRatedRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<List<ProductMostRatedViewModel>>> Handle(
        ProductGetMostRatedRequest request,
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
            var operation = OperationResult.CreateResult<List<ProductMostRatedViewModel>>();
            operation.AddError(new CatalogInvalidOperationException(
                    "Most reviewed",
                    "total not valid. Required more than 5 and less than 30."));
            return operation;
        }

        var items = all.GroupBy(x => new { Product = x, Rating = x.Reviews!.Sum(r => r.Rating) })
            .OrderByDescending(x => x.Key.Rating)
            .Take(request.Total)
            .Select(x => new ProductMostRatedViewModel
            {
                Title = x.Key.Product.Name,
                CategoryName = x.Key.Product.Category!.Name,
                TotalRating = x.Key.Rating
            })
            .ToList();

        return OperationResult.CreateResult(items.ToList());
    }
}
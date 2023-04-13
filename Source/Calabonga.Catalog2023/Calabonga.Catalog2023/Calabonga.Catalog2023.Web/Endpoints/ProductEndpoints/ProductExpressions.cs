using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using System.Linq.Expressions;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints;

/// <summary>
/// I'm using a projection because it as simple,
/// but you can use a mapper (like Automapper or Mapster)
/// </summary>
public static class ProductExpressions
{
    /// <summary>
    /// Projection from Category to CategoryViewModel
    /// </summary>
    public static Expression<Func<Product, ProductViewModel>> Default
        => x => new ProductViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CategoryId = x.CategoryId,
            CategoryName = x.Category!.Name,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy,
            Price = x.Price,
            Reviews = new List<ReviewViewModel>(),
            Tags = new List<TagViewModel>(),
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy,
            Visible = x.Visible
        };

    /// <summary>
    /// Projection from Category to CategoryUpdateViewModel
    /// </summary>
    public static Expression<Func<Product, ProductUpdateViewModel>> ForEdit =>
        x => new ProductUpdateViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CategoryId = x.CategoryId,
            Price = x.Price,
            Tags = string.Join(";", x.Tags!),
            Visible = x.Visible
        };
}
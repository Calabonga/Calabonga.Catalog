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
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy,
            Visible = x.Visible,

            Reviews = x.Reviews == null
                ? new List<ReviewForProductViewModel>()
                : x.Reviews!.Select(t => new ReviewForProductViewModel
                {
                    Id = t.Id,
                    ProductId = t.ProductId,
                    Content = t.Content,
                    CreatedAt = t.CreatedAt,
                    UserName = t.UserName,
                    Visible = t.Visible,
                    CreatedBy = t.CreatedBy,
                    Rating = t.Rating,
                    UpdatedAt = t.UpdatedAt,
                    UpdatedBy = t.UpdatedBy
                }).ToList(),

            Tags = x.Tags == null
                ? new List<TagViewModel>()
                : x.Tags!.Select(t => new TagViewModel { Id = t.Id, Name = t.Name }).ToList()

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
            Tags = string.Join(";", x.Tags!.Select(t => t.Name)),
            Visible = x.Visible
        };
}
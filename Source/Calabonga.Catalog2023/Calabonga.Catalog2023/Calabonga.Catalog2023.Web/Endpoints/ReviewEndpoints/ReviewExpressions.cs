using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using System.Linq.Expressions;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints;

/// <summary>
/// I'm using a projection because it as simple,
/// but you can use a mapper (like Automapper or Mapster)
/// </summary>
public static class ReviewExpressions
{
    /// <summary>
    /// Projection from Category to CategoryViewModel
    /// </summary>
    public static Expression<Func<Review, ReviewViewModel>> Default
        => x => new ReviewViewModel
        {
            Id = x.Id,
            ProductId = x.ProductId,
            Rating = x.Rating,
            Content = x.Content,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy,
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy,
            Visible = x.Visible,
            UserName = x.UserName
        };

    /// <summary>
    /// Projection from Category to CategoryViewModel
    /// </summary>
    public static Expression<Func<Review, ReviewWithProductViewModel>> WithProductName
        => x => new ReviewWithProductViewModel
        {
            Id = x.Id,
            ProductName = x.Product.Name,
            ProductId = x.ProductId,
            Rating = x.Rating,
            Content = x.Content,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy,
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy,
            Visible = x.Visible,
            UserName = x.UserName
        };
}
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using System.Linq.Expressions;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;

/// <summary>
/// I'm using a projection because it as simple,
/// but you can use a mapper (like Automapper or Mapster)
/// </summary>
public static class CategoryExpressions
{
    /// <summary>
    /// Projection from Category to CategoryViewModel
    /// </summary>
    public static Expression<Func<Category, CategoryViewModel>> Default
        => x => new CategoryViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ProductCount = x.Products!.Count
        };

    /// <summary>
    /// Projection from Category to CategoryUpdateViewModel
    /// </summary>
    public static Expression<Func<Category, CategoryUpdateViewModel>> ForEdit =>
        x => new CategoryUpdateViewModel
        {
            Id = x.Id,
            Visible = x.Visible,
            Description = x.Description,
            Name = x.Name
        };
}
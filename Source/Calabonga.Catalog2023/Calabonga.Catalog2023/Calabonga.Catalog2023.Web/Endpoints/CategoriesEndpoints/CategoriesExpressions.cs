using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using System.Linq.Expressions;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints
{
    public static class CategoryExpressions
    {
        public static Expression<Func<Category, CategoryViewModel>> Default => s => new CategoryViewModel
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            ProductCount = s.Products!.Count
        };
    }
}

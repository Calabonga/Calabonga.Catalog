using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

public class ProductCreateViewModel
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Category identifier
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    public int? Price { get; set; }

    /// <summary>
    /// Lined tags
    /// </summary>
    public string Tags { get; set; } = null!;

    /// <summary>
    /// Categories list for selection on the UI
    /// </summary>
    public List<CategoryViewModel> Categories { get; set; }
}
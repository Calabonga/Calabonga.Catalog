namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;

public class CategoryViewModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the catalog
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description for current catalog
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Total items in category
    /// </summary>
    public int ProductCount { get; set; }
}
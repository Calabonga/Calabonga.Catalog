namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;

public class CategoryCreateViewModel
{
    /// <summary>
    /// Name of the catalog
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description for current catalog
    /// </summary>
    public string? Description { get; set; }
}
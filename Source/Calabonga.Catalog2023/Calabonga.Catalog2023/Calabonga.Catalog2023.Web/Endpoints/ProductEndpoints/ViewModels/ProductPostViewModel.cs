namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

public class ProductPostViewModel
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
}
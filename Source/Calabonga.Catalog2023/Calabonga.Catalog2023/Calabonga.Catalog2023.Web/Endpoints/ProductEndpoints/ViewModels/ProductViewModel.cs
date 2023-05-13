namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

public class ProductViewModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// DateTime when entity created.
    /// It's never changed
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// User name who created entity.
    /// It's never changed
    /// </summary>
    public string CreatedBy { get; set; } = null!;

    /// <summary>
    /// Last date entity updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Author of last updated
    /// </summary>
    public string? UpdatedBy { get; set; }

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
    /// Category
    /// </summary>
    public string CategoryName { get; set; } = null!;

    /// <summary>
    /// Price
    /// </summary>
    public int? Price { get; set; }

    /// <summary>
    /// The collection of the reviews for current product
    /// </summary>
    public virtual List<ReviewForProductViewModel>? Reviews { get; set; }

    /// <summary>
    /// Lined tags
    /// </summary>
    public virtual List<TagViewModel>? Tags { get; set; }

    /// <summary>
    /// Hidden product should be visible = false
    /// </summary>
    public bool Visible { get; set; }
}
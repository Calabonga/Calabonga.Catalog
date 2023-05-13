namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;

public class ReviewViewModel
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
    /// Message from user
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// User name who post a review
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Rating for review (1-5)
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Product identifier
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Visibility
    /// </summary>
    public bool Visible { get; set; }
}
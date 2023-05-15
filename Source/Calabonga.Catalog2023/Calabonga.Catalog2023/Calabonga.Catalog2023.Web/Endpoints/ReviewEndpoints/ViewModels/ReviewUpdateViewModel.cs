namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;

public class ReviewUpdateViewModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

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
    /// Indicate that the entity visible
    /// </summary>
    public bool Visible { get; set; }
}
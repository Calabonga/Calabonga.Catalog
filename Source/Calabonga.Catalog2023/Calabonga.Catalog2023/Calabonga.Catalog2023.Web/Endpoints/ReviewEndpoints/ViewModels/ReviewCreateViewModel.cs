namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;

public class ReviewCreateViewModel
{
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
}
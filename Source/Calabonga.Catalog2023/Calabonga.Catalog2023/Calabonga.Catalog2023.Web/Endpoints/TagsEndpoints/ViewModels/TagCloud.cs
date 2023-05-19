using Calabonga.Catalog2023.Domain.Base;

namespace Calabonga.Catalog2023.Web.Endpoints.TagsEndpoints.ViewModels;

/// <summary>
/// ViewModel for Cloud calculation
/// </summary>
public class TagCloud : Identity
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Total items count with this tag
    /// </summary>
    public int TotalCount { get; set; }
}
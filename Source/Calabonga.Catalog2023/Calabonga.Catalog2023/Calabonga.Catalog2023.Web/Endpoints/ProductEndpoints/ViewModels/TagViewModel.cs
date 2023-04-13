namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

public class TagViewModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Tag name
    /// </summary>
    public string Name { get; set; } = null!;
}
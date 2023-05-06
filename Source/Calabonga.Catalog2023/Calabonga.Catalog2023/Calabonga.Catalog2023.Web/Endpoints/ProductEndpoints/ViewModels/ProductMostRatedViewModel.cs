namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;

public class ProductMostRatedViewModel
{
    public string Title { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public int TotalRating { get; set; }
}
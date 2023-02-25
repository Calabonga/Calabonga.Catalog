using Calabonga.Catalog2023.Domain.Base;

namespace Calabonga.Catalog2023.Domain
{
    /// <summary>
    /// Category for products
    /// </summary>
    public class Category : Identity, IPublished
    {
        /// <summary>
        /// Name of the catalog
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Description for current catalog
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Products collection
        /// </summary>
        public virtual List<Product>? Products { get; set; }

        /// <inheritdoc />
        public bool Visible { get; set; }
    }
}

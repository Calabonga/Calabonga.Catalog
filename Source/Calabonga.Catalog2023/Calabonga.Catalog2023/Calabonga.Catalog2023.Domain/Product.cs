using Calabonga.Catalog2023.Domain.Base;

namespace Calabonga.Catalog2023.Domain
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product : Auditable, IPublished
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
        /// Category
        /// </summary>
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// The collection of the reviews for current product
        /// </summary>
        public virtual List<Review>? Reviews { get; set; }

        /// <summary>
        /// Lined tags
        /// </summary>
        public virtual List<Tag>? Tags { get; set; }

        /// <inheritdoc />
        public bool Visible { get; set; }
    }
}

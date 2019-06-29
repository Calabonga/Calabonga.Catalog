using System;

namespace Calabonga.Catalog.Models
{
    /// <summary>
    /// Lined item for Product and Tag.
    /// For many-to-many relation
    /// </summary>
    public class ProductTag
    {
        /// <summary>
        /// Product identity
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Tag Identity
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public Tag Tag { get; set; }
    }
}

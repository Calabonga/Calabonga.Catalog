using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Models
{
    /// <summary>
    /// Category for products
    /// </summary>
    public class Category: Identity
    {
        /// <summary>
        /// Name of the catalog
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for current catalog
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Products collection
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

    }
}

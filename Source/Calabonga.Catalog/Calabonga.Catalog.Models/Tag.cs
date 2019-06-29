using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Models
{
    /// <summary>
    /// Tag entity for product
    /// </summary>
    public class Tag : Identity
    {
        /// <summary>
        /// Tag name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lined tags
        /// </summary>
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}

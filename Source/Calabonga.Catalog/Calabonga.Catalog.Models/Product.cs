﻿using System;
using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Models
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product : Auditable
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Category identifier
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// The collection of the reviews for current product
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

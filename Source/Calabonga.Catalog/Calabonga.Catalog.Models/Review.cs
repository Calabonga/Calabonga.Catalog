using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Models
{
    /// <summary>
    /// // Calabonga: update summary (2019-06-23 03:29 Review)
    /// </summary>
    public class Review : Auditable
    {
        /// <summary>
        /// Message from user
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// User name who post a review
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Rating for review (1-5)
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Product identifier
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public virtual Product Product { get; set; }
    }
}

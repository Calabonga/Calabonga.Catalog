using Calabonga.Catalog2023.Domain.Base;

namespace Calabonga.Catalog2023.Domain
{
    /// <summary>
    /// Review for product from User
    /// </summary>
    public class Review : Auditable, IPublished
    {
        /// <summary>
        /// Message from user
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// User name who post a review
        /// </summary>
        public string UserName { get; set; } = null!;

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
        public virtual Product Product { get; set; } = null!;

        /// <inheritdoc />
        public bool Visible { get; set; }
    }
}

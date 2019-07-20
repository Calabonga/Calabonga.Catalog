using System;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels
{
    /// <summary>
    /// ViewModel for Review updating
    /// </summary>
    public class ReviewUpdateViewModel : ViewModelBase, IPublished
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

        /// <inheritdoc />
        public bool Visible { get; set; }
    }
}
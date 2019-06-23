using System;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels
{
    /// <summary>
    /// ViewModel for Review UI viewing
    /// </summary>
    public class ReviewViewModel : ViewModelBase
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
        public virtual ProductViewModel Product { get; set; }
    }
}
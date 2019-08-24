using System;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels
{
    /// <summary>
    /// ViewModel for Review updating
    /// </summary>
    public class ReviewUpdateViewModel : ViewModelBase
    {
        /// <summary>
        /// Message from user
        /// </summary>
        public string Content { get; set; }

        
        /// <summary>
        /// Rating for review (1-5)
        /// </summary>
        public int Rating { get; set; }
    }
}
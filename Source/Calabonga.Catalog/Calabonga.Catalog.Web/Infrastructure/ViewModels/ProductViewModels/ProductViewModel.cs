using System;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels
{
    /// <summary>
    /// ViewModel for Product UI viewing
    /// </summary>
    public class ProductViewModel : ViewModelBase, IPublished
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
        /// Price
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string CategoryName { get; set; }

        /// <inheritdoc />
        public bool Visible { get; set; }
    }
}
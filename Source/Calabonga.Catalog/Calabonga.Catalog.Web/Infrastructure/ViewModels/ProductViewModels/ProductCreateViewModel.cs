using System;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels
{
    /// <summary>
    /// ViewModel for Product creation
    /// </summary>
    public class ProductCreateViewModel : IViewModel
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
    }
}
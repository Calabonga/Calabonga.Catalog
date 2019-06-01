using System.ComponentModel.DataAnnotations;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels
{
    /// <summary>
    /// ViewModel for Category creation
    /// </summary>
    public class CategoryCreateViewModel : IViewModel
    {
        /// <summary>
        /// Name of the catalog
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for current catalog
        /// </summary>
        public string Description { get; set; }
    }
}
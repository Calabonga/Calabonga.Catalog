using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels
{
    /// <summary>
    /// // Calabonga: update summary (2019-05-26 01:05 CategoriesController)
    /// </summary>
    public class CategoryUpdateViewModel : ViewModelBase, IValidatableObject, IPublished
    {
        /// <summary>
        /// Name of the catalog
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for current catalog
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        public bool Visible { get; set; }

        /// <summary>
        /// Products need should be enabled
        /// </summary>
        public bool VisibleProducts { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Наименование категории не должно быть пустым");
            }
            
            if (!string.IsNullOrEmpty(Name) && Name.Length < 5)
            {
                yield return new ValidationResult("Длина наименование категории должна быть не менее 5 символов");
            }
        }
    }
}
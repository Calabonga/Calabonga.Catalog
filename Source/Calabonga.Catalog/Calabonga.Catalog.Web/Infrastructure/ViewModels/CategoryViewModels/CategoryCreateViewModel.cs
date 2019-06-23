using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels
{
    /// <summary>
    /// ViewModel for Category creation
    /// </summary>
    public class CategoryCreateViewModel : IViewModel, IValidatableObject
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
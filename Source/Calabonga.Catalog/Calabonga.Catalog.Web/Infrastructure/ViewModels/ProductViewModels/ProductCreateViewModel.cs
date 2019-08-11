using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels
{
    /// <summary>
    /// ViewModel for entity with tags 
    /// </summary>
    public interface  ITagsHolder
    {
        /// <summary>
        /// Tag names for Product
        /// </summary>
        string TagsAsString { get; set; }
    }

    /// <summary>
    /// ViewModel for Product creation
    /// </summary>
    public class ProductCreateViewModel : ITagsHolder, IViewModel, IValidatableObject
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

        /// <inheritdoc />
        public string TagsAsString { get; set; }

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Наименование товара не должны быть пустым");
            }
            if (!string.IsNullOrEmpty(Name) && Name.Length <= 5)
            {
                yield return new ValidationResult("Наименование товара должно иметь не менее 5 символов");
            }
            if (!string.IsNullOrEmpty(Description) && Description.Length > 2048)
            {
                yield return new ValidationResult("Описание товара должно иметь не более 2048 символов");
            }
        }
    }
}
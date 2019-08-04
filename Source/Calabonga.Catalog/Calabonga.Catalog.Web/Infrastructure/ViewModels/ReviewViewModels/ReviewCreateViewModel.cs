using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Calabonga.Catalog.Core;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels
{
    /// <summary>
    /// ViewModel for Review creation
    /// </summary>
    public class ReviewCreateViewModel : IViewModel, IValidatableObject
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
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductId == Guid.Empty)
            {
                yield return new ValidationResult($"{ProductId}: {AppData.Messages.ProductIdentifierRequired}");
            }

            if (string.IsNullOrEmpty(UserName))
            {
                yield return new ValidationResult($"{ProductId} {AppData.Messages.UserNameRequired}");
            }

            if (!string.IsNullOrEmpty(UserName) && UserName.Length <= 5)
            {
                yield return new ValidationResult($"{ProductId} {AppData.Messages.UserNameMinLength}");
            }

            if (string.IsNullOrEmpty(Content))
            {
                yield return new ValidationResult($"{ProductId} {AppData.Messages.ReviewContentRequired}");
            }

            if (Rating < 1)
            {
                yield return new ValidationResult($"{ProductId} {AppData.Messages.RatingMinRequired}");
            }

            if (Rating > 5)
            {
                yield return new ValidationResult($"{ProductId} {AppData.Messages.RatingMaxRequired}");
            }
        }
    }
}
using System.Collections.Generic;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

namespace Calabonga.Catalog.Web.Infrastructure.Validations
{
    /// <summary>
    /// Entity Validator for Review
    /// </summary>
    public class ReviewValidator : EntityValidator<Review>
    {
        /// <inheritdoc />
        public override IEnumerable<ValidationResult> ValidateOnInsert(Review entity)
        {
            return base.ValidateOnInsert(entity);
        }

        /// <inheritdoc />
        public override IEnumerable<ValidationResult> ValidateOnInsertOrUpdate(Review entity)
        {
            return base.ValidateOnInsertOrUpdate(entity);
        }

        /// <inheritdoc />
        public override IEnumerable<ValidationResult> ValidateOnUpdate(Review entity)
        {
            return base.ValidateOnUpdate(entity);
        }
    }
}
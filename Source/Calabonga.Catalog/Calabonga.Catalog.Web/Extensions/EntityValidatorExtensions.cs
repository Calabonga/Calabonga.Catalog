using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

namespace Calabonga.Catalog.Web.Extensions
{
    /// <summary>
    /// Entity Validator Extensions
    /// </summary>
    public static class EntityValidatorExtensions
    {
        /// <summary>
        /// Returns validator from validation results
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ValidationContext GetResult(this List<ValidationResult> source)
        {
            return new ValidationContext(source);
        }

    }
}
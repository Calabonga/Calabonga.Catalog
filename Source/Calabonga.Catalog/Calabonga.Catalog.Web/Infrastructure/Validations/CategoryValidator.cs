using System.Collections.Generic;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

namespace Calabonga.Catalog.Web.Infrastructure.Validations
{
    /// <summary>
    /// Category validator
    /// </summary>
    public class CategoryValidator : EntityValidator<Category>
    {
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public CategoryValidator(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public override IEnumerable<ValidationResult> ValidateOnInsertOrUpdate(Category entity)
        {
            var category = _unitOfWork.GetRepository<Category>().GetFirstOrDefault(predicate: x => x.Name.ToLower().Equals(entity.Name.ToLower()));
            if (category != null)
            {
                yield return new ValidationResult($"Категория с именем  {entity.Name} уже существует");
            }
        }
    }
}
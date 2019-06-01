using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Validations.Base;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Catalog.Web.Infrastructure.Validations
{
    /// <summary>
    /// // Calabonga: update summary (2019-05-26 12:33 CategoriesController)
    /// </summary>
    public class CategoryValidator : EntityValidator<Category>
    {
        /// <inheritdoc />
        public CategoryValidator(IRepositoryFactory factory)
            : base(factory)
        {
        }
    }
}
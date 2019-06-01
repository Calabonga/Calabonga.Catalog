using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;
using Calabonga.Catalog.Web.Infrastructure.Managers.Base;
using Calabonga.Catalog.Web.Infrastructure.Validations.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;

namespace Calabonga.Catalog.Web.Infrastructure.Managers
{
    /// <summary>
    /// // Calabonga: update summary (2019-06-01 02:05 CategoriesController)
    /// </summary>
    public class CategoryManager: EntityManager<Category, CategoryCreateViewModel, CategoryUpdateViewModel>
    {
        /// <inheritdoc />
        public CategoryManager(IMapper mapper, IViewModelFactory<Category, CategoryCreateViewModel, CategoryUpdateViewModel> viewModelFactory, IEntityValidator<Category> validator) 
            : base(mapper, viewModelFactory, validator)
        {
        }
    }
}
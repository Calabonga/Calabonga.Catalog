using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

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
using AutoMapper;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Providers;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

namespace Calabonga.Catalog.Web.Infrastructure.Managers
{
    /// <summary>
    /// // Calabonga: update summary (2019-06-01 02:05 CategoriesController)
    /// </summary>
    public class CategoryManager : EntityManager<Category, CategoryCreateViewModel, CategoryUpdateViewModel>
    {
        private readonly IProductProvider _productProvider;
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public CategoryManager(
            IProductProvider productProvider,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IMapper mapper,
            IViewModelFactory<CategoryCreateViewModel, CategoryUpdateViewModel> viewModelFactory,
            IEntityValidator<Category> validator)
            : base(mapper, viewModelFactory, validator)
        {
            _productProvider = productProvider;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public override void OnEditBeforeMappings(CategoryUpdateViewModel model, Category entity)
        {
            if (entity.Visible && !model.Visible)
            {
                _productProvider.ChangeVisibilityByCategoryId(entity.Id, false);
            }

            if (model.Visible && model.VisibleProducts)
            {
                _productProvider.ChangeVisibilityByCategoryId(entity.Id, true);
            }
        }
    }
}
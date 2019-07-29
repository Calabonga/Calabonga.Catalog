using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog.Web.Infrastructure.Managers
{
    /// <summary>
    /// // Calabonga: update summary (2019-06-01 02:05 CategoriesController)
    /// </summary>
    public class CategoryManager: EntityManager<Category, CategoryCreateViewModel, CategoryUpdateViewModel>
    {
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public CategoryManager(
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IMapper mapper,
            IViewModelFactory<Category, CategoryCreateViewModel, CategoryUpdateViewModel> viewModelFactory,
            IEntityValidator<Category> validator) 
            : base(mapper, viewModelFactory, validator)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public override void OnEditBeforeMappings(CategoryUpdateViewModel model, Category entity)
        {
            if (entity.Visible && !model.Visible)
            {
                var products = _unitOfWork.GetRepository<Product>()
                    .GetAll()
                    .Where(x => x.CategoryId == entity.Id);

                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.Visible = false;
                        _unitOfWork.DbContext.Entry(product).State = EntityState.Modified;
                    }
                }
            }

            if (model.Visible && model.VisibleProducts)
            {
                var products = _unitOfWork.GetRepository<Product>()
                    .GetAll()
                    .Where(x => x.CategoryId == entity.Id);

                if (products.Any())
                {
                    foreach (var product in products)
                    {
                        product.Visible = true;
                        _unitOfWork.DbContext.Entry(product).State = EntityState.Modified;
                    }
                }
            }
        }
    }
}
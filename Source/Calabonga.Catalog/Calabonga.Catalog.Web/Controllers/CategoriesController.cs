using System;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Categories controller
    /// </summary>
    public class CategoriesController: WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Category, CategoryCreateViewModel, CategoryUpdateViewModel, CategoryViewModel, DefaultPagedListQueryParams>
    {
        /// <inheritdoc />
        public CategoriesController(IEntityManager<Category, CategoryCreateViewModel, CategoryUpdateViewModel> entityManager, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork) : base(entityManager, unitOfWork)
        {
        }
    }
}

using System;
using System.Linq;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Controller for entity Product with CRUD operations
    /// </summary>
    public class ProductsController : WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Product, ProductCreateViewModel, ProductUpdateViewModel, ProductViewModel, PagedListQueryParams>
    {
        /// <inheritdoc />
        public ProductsController(IEntityManager<Product, ProductCreateViewModel, ProductUpdateViewModel> entityManager, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork) 
            : base(entityManager, unitOfWork)
        {
        }

        /// <inheritdoc />
        protected override Func<IQueryable<Product>, IIncludableQueryable<Product, object>> GetIncludes()
        {
            return i => i.Include(x => x.Category);
        }
    }
}

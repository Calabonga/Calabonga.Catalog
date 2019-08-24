using System;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Controller for entity Product with CRUD operations
    /// </summary>
    public class ProductsController : WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Product, ProductCreateViewModel, ProductUpdateViewModel, ProductViewModel, DefaultPagedListQueryParams>
    {
        /// <inheritdoc />
        public ProductsController(IEntityManager<Product, ProductCreateViewModel, ProductUpdateViewModel> entityManager, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
            : base(entityManager, unitOfWork)
        {
        }

        /// <inheritdoc />
        protected override Func<IQueryable<Product>, IIncludableQueryable<Product, object>> GetIncludes()
        {
            return i => i
                .Include(x => x.Category)
                .Include(x => x.Reviews)
                .Include(x => x.ProductTags)
                .ThenInclude(x => x.Tag);
        }

        /// <inheritdoc />
        [AllowAnonymous]
        public override ActionResult<OperationResult<IPagedList<ProductViewModel>>> GetPaged(DefaultPagedListQueryParams queryParams, bool disabledDefaultIncludes = false)
        {
            return base.GetPaged(queryParams, disabledDefaultIncludes);
        }

        /// <inheritdoc />
        /// [Authorize(Roles = AppData.SystemAdministratorRoleName)]
        public override Task<ActionResult<OperationResult<ProductViewModel>>> DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }
    }
}

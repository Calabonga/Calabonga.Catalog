using System;
using System.Collections.Generic;
using System.Linq;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Controllers.Base;
using Calabonga.Catalog.Web.Infrastructure.Managers.Base;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.Settings;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Controller for entity Product with CRUD operations
    /// </summary>
    public class ProductsController : WritableController<Product, ProductCreateViewModel, ProductUpdateViewModel, ProductViewModel, PagedListQueryParams>
    {
        /// <inheritdoc />
        public ProductsController(IEntityManager<Product, ProductCreateViewModel, ProductUpdateViewModel> entityManager,
            IOptions<CurrentAppSettings> options,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork,
            IAccountService accountService)
            : base(entityManager, options, unitOfWork, accountService)
        {
        }

        /// <inheritdoc />
        protected override Func<IQueryable<Product>, IIncludableQueryable<Product, object>> GetIncludes()
        {
            return i => i.Include(x => x.Category);
        }
    }
}

using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Controllers.Base;
using Calabonga.Catalog.Web.Infrastructure.Managers.Base;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.Settings;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Categories controller
    /// </summary>
    public class CategoriesController: WritableController<Category, CategoryCreateViewModel, CategoryUpdateViewModel, CategoryViewModel, PagedListQueryParams>
    {
        /// <inheritdoc />
        public CategoriesController(IEntityManager<Category, CategoryCreateViewModel, CategoryUpdateViewModel> entityManager, IOptions<CurrentAppSettings> options, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, IAccountService accountService) 
            : base(entityManager, options, unitOfWork, accountService)
        {
        }
    }
}

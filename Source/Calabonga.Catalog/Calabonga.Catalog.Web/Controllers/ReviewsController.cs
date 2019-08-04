using System;
using Calabonga.Catalog.Core;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Controller for entity Review with CRUD operations
    /// </summary>
    public class ReviewsController : WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Review,
        ReviewCreateViewModel, ReviewUpdateViewModel, ReviewViewModel, DefaultPagedListQueryParams>
    {
        /// <inheritdoc />
        public ReviewsController(
            IEntityManager<Review, ReviewCreateViewModel, ReviewUpdateViewModel> entityManager,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
            : base(entityManager, unitOfWork)
        {
        }

        /// <inheritdoc />
        [Authorize(Roles = AppData.SystemAdministratorRoleName)]
        public override ActionResult<OperationResult<IPagedList<ReviewViewModel>>> GetPaged(DefaultPagedListQueryParams queryParams, bool disabledDefaultIncludes = false)
        {
            return base.GetPaged(queryParams, disabledDefaultIncludes);
        }
    }
}

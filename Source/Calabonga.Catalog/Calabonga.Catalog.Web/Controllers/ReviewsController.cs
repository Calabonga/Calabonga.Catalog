using System;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Controller for entity Review with CRUD operations
    /// </summary>
    public class ReviewsController : WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Review,
        ReviewCreateViewModel, ReviewUpdateViewModel, ReviewViewModel, PagedListQueryParams>
    {
        /// <inheritdoc />
        public ReviewsController(
            IEntityManager<Review, ReviewCreateViewModel, ReviewUpdateViewModel> entityManager,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
            : base(entityManager, unitOfWork)
        {
        }
    }
}

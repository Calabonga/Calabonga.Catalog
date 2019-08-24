using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.Catalog.Core;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;
using Calabonga.OperationResultsCore;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Controller for entity Review with CRUD operations
    /// </summary>
    [Authorize]
    public class ReviewsController : WritableController<ApplicationDbContext, ApplicationUser, ApplicationRole, Review,
        ReviewCreateViewModel, ReviewUpdateViewModel, ReviewViewModel, DefaultPagedListQueryParams>
    {
        private readonly IAccountService _accountService;

        /// <inheritdoc />
        public ReviewsController(
            IAccountService accountService,
            IEntityManager<Review, ReviewCreateViewModel, ReviewUpdateViewModel> entityManager,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
            : base(entityManager, unitOfWork)
        {
            _accountService = accountService;
        }

        /// <inheritdoc />
        [Authorize(Roles = AppData.SystemAdministratorRoleName)]
        public override ActionResult<OperationResult<IPagedList<ReviewViewModel>>> GetPaged(DefaultPagedListQueryParams queryParams, bool disabledDefaultIncludes = false)
        {
            return base.GetPaged(queryParams, disabledDefaultIncludes);
        }

        /// <summary>
        /// Returns reviews for product
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("get-reviews-by-product-id")]
        public ActionResult<OperationResult<List<ReviewViewModel>>> GetReviewForProductId(Guid productId)
        {
            var operation = OperationResult.CreateResult<List<ReviewViewModel>>();
            var items = UnitOfWork.GetRepository<Review>().GetAll().Where(x => x.ProductId == productId);
            if (!items.Any())
            {
                operation.AddInfo($"No reviews found for product with ID {productId}");
                return operation;
            }

            var mapped = CurrentMapper.Map<List<ReviewViewModel>>(items);
            operation.Result = mapped;
            return Ok(operation);
        }

        /// <inheritdoc />
        protected override PermissionValidationResult ValidateUserAccessRights(Review entity)
        {
            var userId = User.GetSubjectId();
            var isAdmin = User.IsInRole(AppData.SystemAdministratorRoleName);
            if (isAdmin || userId == entity.CreatedBy)
            {
                return new PermissionValidationResult();
            }

            return new UnauthorizedPermissionValidationResult();
        }

        /// <inheritdoc />
        protected override string GetUserNameForAuditInfo()
        {
            return User.GetSubjectId();
        }
    }
}

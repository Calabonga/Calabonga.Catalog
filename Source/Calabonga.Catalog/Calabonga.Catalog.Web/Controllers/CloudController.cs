using System.Collections.Generic;
using System.Linq;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CloudController: UnitOfWorkController<ApplicationUser, ApplicationRole>
    {
        private readonly ICloudService _cloudService;

        /// <inheritdoc />
        public CloudController(
            ICloudService cloudService,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
            : base(unitOfWork)
        {
            _cloudService = cloudService;
        }

        /// <summary>
        /// Returns cloud
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult<OperationResult<List<TagCloud>>> Get()
        {
            var operation = OperationResult.CreateResult<List<TagCloud>>();
            operation.Result = _cloudService.CreateCloud().ToList();
            return Ok(operation);
        }
    }
}

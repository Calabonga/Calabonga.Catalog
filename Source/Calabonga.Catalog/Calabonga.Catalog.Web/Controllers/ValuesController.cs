﻿using System.Collections.Generic;
using Calabonga.Catalog.Data;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;
using Calabonga.OperationResultsCore;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Demo purposes only
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : UnitOfWorkController<ApplicationDbContext, ApplicationUser, ApplicationRole>
    {
        /// <inheritdoc />
        public ValuesController(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Returns a collection.
        /// Demo purposes only
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<OperationResult<IEnumerable<string>>> Get()
        {
            var items = new string[] { "Calabonga", "Template", "For", "Micro", "Services" };
            return OperationResultSuccess<IEnumerable<string>>(items);
        }

        /// <summary>
        /// Return something by identifier
        /// Demo purposes only
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public ActionResult<OperationResult<string>> Get(int id)
        {
            return OperationResultInfo("MicroService", "Information");
        }
    }
}

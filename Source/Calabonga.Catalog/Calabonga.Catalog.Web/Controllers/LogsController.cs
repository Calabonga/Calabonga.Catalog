using AutoMapper;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Controllers;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Logs controller
    /// </summary>
    public class LogsController: ReadOnlyController<ApplicationDbContext, ApplicationUser, ApplicationRole, Log, LogViewModel, DefaultPagedListQueryParams>
    {
        /// <inheritdoc />
        public LogsController(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }
    }
}

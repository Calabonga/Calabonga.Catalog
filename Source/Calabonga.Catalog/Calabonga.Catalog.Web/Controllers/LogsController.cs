using AutoMapper;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Controllers.Base;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.Settings;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.LogViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Logs controller
    /// </summary>
    public class LogsController: ReadOnlyController<Log, LogViewModel, PagedListQueryParams>
    {
        /// <inheritdoc />
        public LogsController(IMapper mapper, IOptions<CurrentAppSettings> options, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, IAccountService accountService) 
            : base(mapper, options, unitOfWork, accountService)
        {
        }
    }
}

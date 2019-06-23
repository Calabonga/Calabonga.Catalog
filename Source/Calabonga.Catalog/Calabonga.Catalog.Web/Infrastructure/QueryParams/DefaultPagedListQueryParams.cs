using Calabonga.EntityFrameworkCore.UnitOfWork.Framework;

namespace Calabonga.Catalog.Web.Infrastructure.QueryParams
{
    /// <summary>
    /// // Calabonga: update summary (2019-06-23 01:51 DefaultPagedListQueryParams)
    /// </summary>
    public class DefaultPagedListQueryParams: PagedListQueryParams
    {
        /// <inheritdoc />
        public DefaultPagedListQueryParams()
        {
            PageSize = 10;
        }
    }
}

using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels
{
    /// <summary>
    /// ViewModel for Cloud calculation
    /// </summary>
    public class TagCloud: Identity
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Total items count with this tag
        /// </summary>
        public int TotalCount { get; set; }
    }
}

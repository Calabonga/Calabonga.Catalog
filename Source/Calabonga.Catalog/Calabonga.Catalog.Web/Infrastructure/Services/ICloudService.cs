using System.Collections.Generic;
using Calabonga.Catalog.Web.Infrastructure.ViewModels;

namespace Calabonga.Catalog.Web.Infrastructure.Services
{
    /// <summary>
    /// Cloud Service
    /// </summary>
    public interface ICloudService
    {
        /// <summary>
        /// // Calabonga: update summary (2019-08-17 02:56 CloudService)
        /// </summary>
        /// <returns></returns>
        IEnumerable<TagCloud> CreateCloud();
    }
}
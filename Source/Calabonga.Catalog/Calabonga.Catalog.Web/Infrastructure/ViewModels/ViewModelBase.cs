using System;
using Calabonga.Catalog.Models.Base;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;

namespace Calabonga.Catalog.Web.Infrastructure.ViewModels
{
    /// <summary>
    /// ViewModelBase for WritableController
    /// </summary>
    public class ViewModelBase : IViewModel, IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}

using Calabonga.Catalog2023.Domain.Base;

namespace Calabonga.Catalog2023.Web.Endpoints.EventItemsEndpoints.ViewModels
{
    public class EventItemUpdateViewModel : ViewModelBase
    {
        public string Logger { get; set; } = null!;

        public string Level { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
using Calabonga.Catalog2023.Domain.Base;

namespace Calabonga.Catalog2023.Domain
{
    /// <summary>
    /// EventItem entity for demo purposes only
    /// </summary>
    public class EventItem : Identity
    {
        public DateTime CreatedAt { get; set; }

        public string Logger { get; set; } = null!;

        public string Level { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string? ThreadId { get; set; }

        public string? ExceptionMessage { get; set; }
    }
}
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Catalog.Models
{
    public class Tag: Identity
    {
        public string Name { get; set; }
    }
}

namespace Calabonga.Catalog.Models
{
    /// <summary>
    /// Entity can be hide or publish
    /// </summary>
    public interface IPublished
    {
        /// <summary>
        /// Indicate that the entity visible
        /// </summary>
        bool Visible { get; set; }
    }
}

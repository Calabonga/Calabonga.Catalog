namespace Calabonga.Catalog2023.Domain
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

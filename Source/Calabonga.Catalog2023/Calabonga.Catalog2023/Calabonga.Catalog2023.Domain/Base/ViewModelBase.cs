namespace Calabonga.Catalog2023.Domain.Base
{
    /// <summary>
    /// ViewModelBase for 
    /// </summary>
    public class ViewModelBase : IViewModel, IHaveId
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
    }
}
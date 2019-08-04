using Calabonga.OperationResultsCore;

namespace Calabonga.Catalog.Web.Infrastructure.Services
{
    /// <summary>
    /// Service for Review
    /// </summary>
    public interface IReviewService
    {
        /// <summary>
        /// Update visibility for reviews
        /// </summary>
        /// <returns></returns>
        OperationResult<int> ChangeVisibilityByProductId();
    }
}
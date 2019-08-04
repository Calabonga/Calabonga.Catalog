using System;
using Calabonga.Catalog.Models;
using Calabonga.OperationResultsCore;

namespace Calabonga.Catalog.Web.Infrastructure.Providers
{
    /// <summary>
    /// Provider for product
    /// </summary>
    public interface IProductProvider
    {
        /// <summary>
        /// Changes visibility for products
        /// </summary>
        /// <returns></returns>
        OperationResult<int> ChangeVisibilityByCategoryId(Guid categoryId, bool visibility);

        /// <summary>
        /// Changes visibility for one product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="visibility"></param>
        /// <returns></returns>
        OperationResult<int> ChangeReviewVisibilityByProductId(Product product, bool visibility);
    }
}
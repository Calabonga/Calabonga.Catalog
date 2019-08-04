using System;
using System.Linq;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Exceptions;
using Calabonga.OperationResultsCore;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Catalog.Web.Infrastructure.Providers
{
    /// <summary>
    /// Provider for product
    /// </summary>
    public class ProductProvider : IProductProvider
    {
        private readonly IReviewService _reviewService;
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public ProductProvider(
            IReviewService reviewService,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
        {
            _reviewService = reviewService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Change visibility for products
        /// </summary>
        /// <returns></returns>
        public OperationResult<int> ChangeVisibilityByCategoryId(Guid categoryId, bool visibility)
        {
            var operation = OperationResult.CreateResult<int>();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var products = _unitOfWork.GetRepository<Product>()
                    .GetAll()
                    .Include(x => x.Reviews)
                    .Where(x => x.CategoryId == categoryId);
                
                if (!products.Any())
                {
                    operation.AddInfo("Products were not found");
                    return operation;
                }

                foreach (var product in products)
                {
                    var changeOperation = ChangeReviewVisibilityByProductId(product, visibility);
                    if (!changeOperation.Ok)
                    {
                        operation.Error = changeOperation.Error;
                        return operation;
                    }
                    product.Visible = visibility;
                    _unitOfWork.DbContext.Entry(product).State = EntityState.Modified;
                    _unitOfWork.SaveChanges();
                    if (!_unitOfWork.LastSaveChangesResult.IsOk)
                    {
                        operation.Result = 0;
                        operation.AddError(_unitOfWork.LastSaveChangesResult?.Exception);
                        return operation;
                    }
                    transaction.Commit();
                }

                operation.Result = products.Count();
                return operation;
            }
        }

        /// <inheritdoc />
        public OperationResult<int> ChangeReviewVisibilityByProductId(Product product, bool visibility)
        {
            var operation = OperationResult.CreateResult<int>();
            if (product == null)
            {
                operation.Error = new MicroserviceArgumentNullException(nameof(product));
                return operation;
            }

            if (!_unitOfWork.DbContext.Entry(product).Collection(x=>x.Reviews).IsLoaded)
            {
                _unitOfWork.DbContext.Entry(product).Collection(x=>x.Reviews).Load();
            }

            if (product.Reviews != null && product.Reviews.Any())
            {
                foreach (var review in product.Reviews)
                {
                    review.Visible = visibility;
                    _unitOfWork.DbContext.Entry(review).State = EntityState.Modified;
                }

                _unitOfWork.SaveChanges();
                if (!_unitOfWork.LastSaveChangesResult.IsOk)
                {
                    operation.Result = 0;
                    operation.AddError(_unitOfWork.LastSaveChangesResult?.Exception);
                    return operation;
                }
                operation.Result = product.Reviews.Count;
                return operation;
            }
            operation.AddInfo($"No review were found for product with id {product.Id}");
            return operation;
        }
    }
}

using Calabonga.Catalog.Data;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.OperationResultsCore;

namespace Calabonga.Catalog.Web.Infrastructure.Services
{
    /// <summary>
    /// Service for Review
    /// </summary>
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public ReviewService(IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Update visibility for reviews
        /// </summary>
        /// <returns></returns>
        public OperationResult<int> ChangeVisibilityByProductId()
        {
            var operation = OperationResult.CreateResult<int>();
            // TODO: implement
            return operation;
        }
    }
}

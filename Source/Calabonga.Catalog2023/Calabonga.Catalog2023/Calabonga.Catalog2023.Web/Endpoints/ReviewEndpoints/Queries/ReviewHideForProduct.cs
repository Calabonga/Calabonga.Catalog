using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;

public class ReviewHideForProduct
{
	public record Request(Guid ProductId, ClaimsPrincipal User) : IRequest<Unit>;

	public class Handler : IRequestHandler<Request, Unit>
	{
		private readonly IUnitOfWork _unitOfWork;

		public Handler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
		{
			var productRepository = _unitOfWork.GetRepository<Review>();
			var reviews = productRepository
				.GetAll(
					predicate: x => x.ProductId == request.ProductId && x.Visible == true,
					disableTracking: false,
					ignoreAutoIncludes: true,
					ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName))
				.ToList();

			if (!reviews.Any())
			{
				return Unit.Value;
			}

			reviews.ForEach(x => x.Visible = false);
			productRepository.Update(reviews);
			await _unitOfWork.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
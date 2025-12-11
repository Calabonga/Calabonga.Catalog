using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Notifications;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public class ProductsHideInCategory
{
	public record Request(Guid CategoryId, ClaimsPrincipal User) : IRequest<Unit>;

	public class Handler : IRequestHandler<Request, Unit>
	{
		private readonly IMediator _mediator;
		private readonly IUnitOfWork _unitOfWork;

		public Handler(
			IMediator mediator,
			IUnitOfWork unitOfWork)
		{
			_mediator = mediator;
			_unitOfWork = unitOfWork;
		}

		public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
		{
			var productRepository = _unitOfWork.GetRepository<Product>();
			var products = productRepository
				.GetAll(
					predicate: x => x.CategoryId == request.CategoryId && x.Visible == true,
					disableTracking: false,
					ignoreAutoIncludes: true,
					ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName))
				.ToList();

			if (!products.Any())
			{
				return Unit.Value;
			}

			products.ForEach(x => x.Visible = false);
			productRepository.Update(products);
			await _unitOfWork.SaveChangesAsync();

			foreach (var product in products)
			{
				await _mediator.Publish(new ProductHide.Notification(product.Id, request.User), cancellationToken);
			}

			return Unit.Value;
		}
	}
}
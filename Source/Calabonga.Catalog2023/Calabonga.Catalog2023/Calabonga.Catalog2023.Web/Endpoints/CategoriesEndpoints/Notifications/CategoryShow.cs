using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Notifications;

public class CategoryShow
{
	public record Notification(Guid CategoryId, ClaimsPrincipal User) : INotification;

	public class Handler : INotificationHandler<Notification>
	{
		private readonly IMediator _mediator;

		public Handler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task Handle(Notification notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(new ProductsShowInCategory.Request(notification.CategoryId, notification.User), cancellationToken);
		}
	}
}
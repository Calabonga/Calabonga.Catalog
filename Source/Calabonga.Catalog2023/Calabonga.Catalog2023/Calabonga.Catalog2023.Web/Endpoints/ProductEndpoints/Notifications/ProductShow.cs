using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Notifications;

public class ProductShow
{
	public record Notification(Guid ProductId, ClaimsPrincipal User) : INotification;

	public class Handler : INotificationHandler<Notification>
	{
		private readonly IMediator _mediator;

		public Handler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task Handle(Notification notification, CancellationToken cancellationToken)
		{
			await _mediator.Send(new ReviewShowForProduct.Request(notification.ProductId, notification.User), cancellationToken);
		}
	}
}
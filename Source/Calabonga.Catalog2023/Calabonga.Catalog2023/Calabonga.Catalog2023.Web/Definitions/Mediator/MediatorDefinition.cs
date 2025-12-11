using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using MediatR;
using MediatR.NotificationPublishers;

namespace Calabonga.Catalog2023.Web.Definitions.Mediator;

/// <summary>
/// Register Mediator as application definition
/// </summary>
public class MediatorDefinition : AppDefinition
{
	/// <summary>
	/// Configure services for current application
	/// </summary>
	/// <param name="services"></param>
	/// <param name="builder"></param>
	public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
	{
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblyContaining<Program>();
			cfg.NotificationPublisher = new TaskWhenAllPublisher();
		});
	}
}
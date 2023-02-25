using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application.Services;
using Calabonga.Catalog2023.Web.Definitions.Identity;

namespace Calabonga.Catalog2023.Web.Definitions.DependencyContainer
{
    /// <summary>
    /// Dependency container definition
    /// </summary>
    public class ContainerDefinition : AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ApplicationUserClaimsPrincipalFactory>();
        }
    }
}
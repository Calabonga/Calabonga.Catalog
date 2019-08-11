using Calabonga.Catalog.Data;
using Calabonga.Catalog.Web.Infrastructure.Auth;
using Calabonga.Catalog.Web.Infrastructure.Providers;
using Calabonga.Catalog.Web.Infrastructure.Services;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Catalog.Web.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Registrations for both points: API and Scheduler
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Register 
        /// </summary>
        /// <param name="services"></param>
        public static void Common(IServiceCollection services)
        {
            services.AddTransient<ApplicationUserStore>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<ApplicationClaimsPrincipalFactory>();

            // services
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProfileService, IdentityProfileService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<ICorsPolicyService, IdentityServerCorsPolicy>();
            services.AddTransient<IEmailService, EmailService>();

            // providers
            services.AddTransient<IProductProvider, ProductProvider>();

            // notifications
            Notifications(services);
        }
    }
}

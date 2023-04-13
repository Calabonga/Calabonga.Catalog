using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Web.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Calabonga.Catalog2023.Web.Definitions.Swagger
{
    /// <summary>
    /// Swagger definition for application
    /// </summary>
    public class SwaggerDefinition : AppDefinition
    {

        private const string AppVersion = "1.0.0";
        private const string SwaggerConfig = "/swagger/v1/swagger.json";

        public override void ConfigureApplication(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            var url = app.Services.GetRequiredService<IConfiguration>().GetValue<string>("AuthServer:Url");

            app.UseSwagger();
            app.UseSwaggerUI(settings =>
            {
                settings.SwaggerEndpoint(SwaggerConfig, $"{AppData.ServiceName} v.{AppVersion}");
                settings.DocumentTitle = $"{AppData.ServiceName}";
                settings.DefaultModelExpandDepth(0);
                settings.DefaultModelRendering(ModelRendering.Model);
                settings.DefaultModelsExpandDepth(0);
                settings.DocExpansion(DocExpansion.None);
                settings.OAuthScopeSeparator(" ");
                settings.OAuthClientId("client-id-code");
                settings.OAuthClientSecret("client-secret-code");
                settings.DisplayRequestDuration();
                settings.OAuthAppName(AppData.ServiceName);
                settings.OAuth2RedirectUrl($"{url}/swagger/oauth2-redirect.html");
            });
        }

        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppData.ServiceName,
                    Version = AppVersion,
                    Description = AppData.ServiceDescription
                });

                options.ResolveConflictingActions(x => x.First());

                options.TagActionsBy(api =>
                {
                    string tag;
                    if (api.ActionDescriptor is { } descriptor)
                    {
                        var attribute = descriptor.EndpointMetadata.OfType<FeatureGroupNameAttribute>().FirstOrDefault();
                        tag = attribute?.GroupName ?? descriptor.RouteValues["controller"] ?? "Untitled";
                    }
                    else
                    {
                        tag = api.RelativePath!;
                    }

                    var tags = new List<string>();
                    if (!string.IsNullOrEmpty(tag))
                    {
                        tags.Add(tag);
                    }
                    return tags;
                });

                var url = builder.Configuration.GetSection("AuthServer").GetValue<string>("Url");

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{url}/connect/token", UriKind.Absolute),
                            AuthorizationUrl = new Uri($"{url}/connect/authorize", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                            {
                            { "api", "Default scope" }
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        In = ParameterLocation.Cookie,
                        Type = SecuritySchemeType.OAuth2

                    },
                    new List<string>()
                }
                });
            });
        }
    }
}
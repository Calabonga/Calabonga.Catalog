using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application.Services;
using Calabonga.Catalog2023.Web.Definitions.Identity;
using Calabonga.Catalog2023.Web.Definitions.Mediator;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;

namespace Calabonga.Catalog2023.Web.Definitions.DependencyContainer;

/// <summary>
/// Dependency container definition
/// </summary>
public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<ITagCalculator, TagCalculator>();
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<ApplicationUserClaimsPrincipalFactory>();

        services.AddTransient<
            IPipelineBehavior<CategoryUpdateRequest, OperationResult<CategoryEditViewModel>>,
            CategoryUpdateRequestTransactionBehavior>();
    }
}
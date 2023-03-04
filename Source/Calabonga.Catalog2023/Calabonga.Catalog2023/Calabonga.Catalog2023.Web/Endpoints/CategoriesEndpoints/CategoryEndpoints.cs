using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;
public class CategoryEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/catalogs/get-all", GetAll);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    private Task GetAll([FromServices] IMediator mediator, HttpContext context)
    {
        return mediator.Send(new CategoryGetAllRequest(), context.RequestAborted);
    }
}
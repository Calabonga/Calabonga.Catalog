using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Definitions.OpenIddict;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;

public class CategoryEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/categories/get-all", GetAllCategories);
        app.MapPost("/api/categories/create", CreateCategory);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private async Task<IResult> CreateCategory(
        [FromServices] IMediator mediator,
        [FromBody] CategoryCreateViewModel createViewModel,
        HttpContext context)
    {
        var operation = await mediator.Send(new CategoryCreateRequest(createViewModel), context.RequestAborted);
        if (operation.Ok)
        {
            return TypedResults.Created($"/api/categories/{operation.Result!.Id}", operation.Result);
        }
        return TypedResults.BadRequest();
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    private Task GetAllCategories([FromServices] IMediator mediator, HttpContext context)
    {
        return mediator.Send(new CategoryGetAllRequest(), context.RequestAborted);
    }
}
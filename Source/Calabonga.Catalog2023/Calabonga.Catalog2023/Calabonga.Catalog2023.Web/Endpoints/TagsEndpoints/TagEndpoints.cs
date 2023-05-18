using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Endpoints.TagsEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.TagsEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog2023.Web.Endpoints.TagsEndpoints;

public class TagEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/tags/get-cloud", ReviewGetForCreate);

    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Tags")]
    private Task<OperationResult<IEnumerable<TagCloud>>> ReviewGetForCreate
    (
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new GetTagCloudRequest(), context.RequestAborted);
    }
}
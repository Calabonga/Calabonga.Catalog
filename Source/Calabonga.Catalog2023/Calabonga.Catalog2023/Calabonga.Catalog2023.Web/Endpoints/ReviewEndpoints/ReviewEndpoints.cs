using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Definitions.OpenIddict;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints;

public class ReviewEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapPost("/api/reviews/get-for-create", ReviewGetForCreate);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Reviews")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ReviewCreateViewModel>> ReviewGetForCreate(
        Guid productId,
        [FromServices] IMediator mediator, HttpContext context)
    {
        return mediator.Send(new ReviewGetForCreateRequest(productId, context.User), context.RequestAborted);
    }
}
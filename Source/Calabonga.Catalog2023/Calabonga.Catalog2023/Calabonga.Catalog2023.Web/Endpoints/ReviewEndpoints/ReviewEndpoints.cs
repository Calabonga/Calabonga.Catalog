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
        app.MapGet("/api/reviews/get-for-create", ReviewGetForCreate);
        app.MapPost("/api/reviews/create", ReviewPostCreate);
        app.MapGet("/api/reviews/{id:guid}", ReviewGetById);
        app.MapDelete("/api/reviews/{id:guid}", ReviewDeleteById);
        app.MapGet("/api/reviews/get-for-update", ReviewGetForUpdate);
        app.MapPut("/api/reviews/update", ReviewPostUpdate);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Reviews")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ReviewViewModel>> ReviewPostUpdate
    (
        ReviewUpdateViewModel model,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new ReviewPutUpdateRequest(model, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Reviews")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ReviewUpdateViewModel>> ReviewGetForUpdate(
        Guid id,
        [FromServices] IMediator mediator, HttpContext context)
    {
        return mediator.Send(new ReviewGetForUpdateRequest(id, context.User), context.RequestAborted);
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

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Reviews")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ReviewViewModel>> ReviewPostCreate
    (
        ReviewCreateViewModel model,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new ReviewPostCreateRequest(model, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Reviews")]
    private Task<OperationResult<ReviewViewModel>> ReviewGetById
    (
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new ReviewGetByIdRequest(id, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Reviews")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]

    private Task<OperationResult<ReviewViewModel>> ReviewDeleteById
    (
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new ReviewDeleteByIdRequest(id, context.User), context.RequestAborted);
    }
}
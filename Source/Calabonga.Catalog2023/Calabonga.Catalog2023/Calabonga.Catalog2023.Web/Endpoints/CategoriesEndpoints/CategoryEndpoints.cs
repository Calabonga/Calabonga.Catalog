using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Definitions.OpenIddict;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;

public class CategoryEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/categories/get-all", GetAllCategories);
        app.MapGet("/api/categories/get-paged/{pageIndex:int}", GetPagedCategories);
        app.MapGet("/api/categories/{id:guid}", GetByIdCategory);
        app.MapPost("/api/categories/create", CreateCategory);
        app.MapGet("/api/categories/edit/{id:guid}", CategoryCreateGetForEdit);
        app.MapPut("/api/categories/update", CategoryCreatePostAfterEdit);
        app.MapDelete("/api/categories/delete/{id:guid}", CategoryDeleteCategory);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    private Task<OperationResult<IPagedList<CategoryViewModel>>> GetPagedCategories(
        int pageIndex,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new CategoryGetPagedRequest(pageIndex, 5, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<Guid>> CategoryDeleteCategory(
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new CategoryDeleteByIdRequest(id, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<CategoryEditViewModel>> CategoryCreatePostAfterEdit(
        [FromBody] CategoryUpdateViewModel model,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new CategoryUpdateRequest(model, context.User), context.RequestAborted);
    }


    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<CategoryUpdateViewModel>> CategoryCreateGetForEdit(
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new CategoryGetByIdForEditRequest(id, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    private Task<OperationResult<CategoryViewModel>> GetByIdCategory(
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new CategoryGetByIdRequest(id, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<CategoryViewModel>> CreateCategory(
        [FromServices] IMediator mediator,
        [FromBody] CategoryCreateViewModel createViewModel,
        HttpContext context)
    {
        return mediator.Send(new CategoryCreateRequest(createViewModel), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    private Task<OperationResult<List<CategoryViewModel>>> GetAllCategories([FromServices] IMediator mediator, HttpContext context)
    {
        return mediator.Send(new CategoryGetAllRequest(context.User), context.RequestAborted);
    }
}
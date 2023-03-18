﻿using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Definitions.OpenIddict;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;

public class CategoryEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/categories/get-all", GetAllCategories);
        app.MapGet("/api/categories/{id:guid}", GetByIdCategory);
        app.MapPost("/api/categories/create", CreateCategory);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Categories")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
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
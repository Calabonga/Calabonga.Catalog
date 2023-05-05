using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.Catalog2023.Web.Application;
using Calabonga.Catalog2023.Web.Definitions.OpenIddict;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints;

public class ProductEndpoints : AppDefinition
{
    public override void ConfigureApplication(WebApplication app)
    {
        app.MapGet("/api/products/get-all", GetAllProducts);
        app.MapGet("/api/products/get-for-create", GetForCreateProduct);
        app.MapPost("/api/products/create", PostCreateProduct);
        app.MapGet("/api/products/get-paged/{pageIndex:int}", GetPagedProducts);
        app.MapGet("/api/products/{id:guid}", GetByIdProduct);
        app.MapGet("/api/products/edit/{id:guid}", ProductCreateGetForEdit);
        app.MapPut("/api/products/update", ProductCreatePostAfterEdit);
        app.MapDelete("/api/products/delete/{id:guid}", ProductDeleteProduct);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ProductCreateViewModel>> GetForCreateProduct(
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return mediator.Send(new ProductGetForCreateRequest(context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ProductViewModel>> PostCreateProduct(
        [FromServices] IMediator mediator,
        [FromBody] ProductPostViewModel createViewModel,
        HttpContext context)
    {
        return mediator.Send(new ProductPostCreateRequest(createViewModel, context.User), context.RequestAborted);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    private Task<OperationResult<IPagedList<ProductViewModel>>> GetPagedProducts(
        int pageIndex,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return Task.FromResult(OperationResult.CreateResult<IPagedList<ProductViewModel>>());
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<Guid>> ProductDeleteProduct(
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return Task.FromResult(OperationResult.CreateResult<Guid>());
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ProductViewModel>> ProductCreatePostAfterEdit(
        [FromBody] ProductUpdateViewModel model,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return Task.FromResult(OperationResult.CreateResult<ProductViewModel>());
    }


    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    private Task<OperationResult<ProductUpdateViewModel>> ProductCreateGetForEdit(
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return Task.FromResult(OperationResult.CreateResult<ProductUpdateViewModel>());
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    private Task<OperationResult<ProductViewModel>> GetByIdProduct(
        Guid id,
        [FromServices] IMediator mediator,
        HttpContext context)
    {
        return Task.FromResult(OperationResult.CreateResult<ProductViewModel>());
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Products")]
    private Task<OperationResult<List<ProductViewModel>>> GetAllProducts([FromServices] IMediator mediator, HttpContext context)
    {
        return mediator.Send(new ProductGetAllRequest(context.User), context.RequestAborted);
    }
}
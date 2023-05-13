using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;
using FluentValidation;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints;

public class ProductPostCreateRequestValidator : AbstractValidator<ProductPostCreateRequest>
{
    public ProductPostCreateRequestValidator()
    {
        RuleFor(x => x.Model.CategoryId).NotEmpty();
        RuleFor(x => x.Model.Name).NotEmpty().Length(5, 128);
        RuleFor(x => x.Model.Description).Length(10, 2048);
        RuleFor(x => x.Model.Price).GreaterThan(0);
        RuleFor(x => x.Model.Tags).NotEmpty();
    }
}
using Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints.Queries;
using FluentValidation;

namespace Calabonga.Catalog2023.Web.Endpoints.ReviewEndpoints;

public class ReviewPostCreateRequestValidator : AbstractValidator<ReviewCreateRequest>
{
    public ReviewPostCreateRequestValidator()
    {
        RuleFor(x => x.Model.ProductId).NotEmpty();
        RuleFor(x => x.Model.UserName).NotEmpty().Length(5, 128);
        RuleFor(x => x.Model.Content).Length(10, 2048);
        RuleFor(x => x.Model.Rating).GreaterThan(0).LessThan(6);
    }
}
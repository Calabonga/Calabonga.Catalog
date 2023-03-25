﻿using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.Queries;
using FluentValidation;

namespace Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints;

public class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequest>
{
    public CategoryUpdateRequestValidator()
    {
        RuleFor(x => x.Model.Id).NotEmpty();
        RuleFor(x => x.Model.Name).NotEmpty().Length(5, 50);
        RuleFor(x => x.Model.Description).Length(10, 1024);
    }
}
using Calabonga.Catalog2023.Web.Definitions.Mediator.Base;
using Calabonga.Catalog2023.Web.Endpoints.CategoriesEndpoints.ViewModels;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;

namespace Calabonga.Catalog2023.Web.Definitions.Mediator;

public class CategoryUpdateRequestTransactionBehavior :
    TransactionBehavior<IRequest<OperationResult<CategoryEditViewModel>>, OperationResult<CategoryEditViewModel>>
{
    public CategoryUpdateRequestTransactionBehavior(IUnitOfWork unitOfWork)
        : base(unitOfWork) { }
}
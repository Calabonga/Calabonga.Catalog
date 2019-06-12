using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;
using Calabonga.Catalog.Web.Infrastructure.Managers.Base;
using Calabonga.Catalog.Web.Infrastructure.Validations.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;

namespace Calabonga.Catalog.Web.Infrastructure.Managers
{
    /// <summary>
    /// Product entity manager
    /// <see cref="IViewModelFactory{TEntity,TCreateViewModel,TUpdateViewModel}"/> implementation
    /// </summary>
    public class ProductManager : EntityManager<Product, ProductCreateViewModel, ProductUpdateViewModel>
    {
        /// <inheritdoc />
        public ProductManager(IMapper mapper, IViewModelFactory<Product, ProductCreateViewModel, ProductUpdateViewModel> viewModelFactory,
            IEntityValidator<Product> validator)
            : base(mapper, viewModelFactory, validator)
        {
        }
    }
}
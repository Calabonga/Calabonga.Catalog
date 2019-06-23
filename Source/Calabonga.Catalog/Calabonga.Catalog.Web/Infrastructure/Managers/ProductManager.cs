using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

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
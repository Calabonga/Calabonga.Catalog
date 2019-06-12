using System;
using AutoMapper;
using Calabonga.Catalog.Core.Exceptions;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;

namespace Calabonga.Catalog.Web.Infrastructure.Factories
{
    /// <summary>
    /// ViewModel Factory for Product entity
    /// </summary>
    public class ProductViewModelFactory : ViewModelFactory<Product, ProductCreateViewModel, ProductUpdateViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;

        /// <inheritdoc />
        public ProductViewModelFactory(IMapper mapper, IRepositoryFactory factory)
        {
            _mapper = mapper;
            _repository = factory.GetRepository<Product>();
        }

        /// <inheritdoc />
        public override ProductCreateViewModel GenerateForCreate()
        {
            return new ProductCreateViewModel();
        }

        /// <inheritdoc />
        public override ProductUpdateViewModel GenerateForUpdate(Guid id)
        {
            var item = _repository.GetFirstOrDefault(predicate: x => x.Id == id);
            if (item == null)
            {
                throw new MicroserviceNotFoundException();
            }

            return _mapper.Map<ProductUpdateViewModel>(item);
        }
    }
}
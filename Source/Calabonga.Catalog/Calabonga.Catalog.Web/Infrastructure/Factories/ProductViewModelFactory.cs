using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ProductViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Exceptions;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.OperationResultsCore;

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
        public override Task<OperationResult<ProductCreateViewModel>> GenerateForCreateAsync()
        {
            return Task.FromResult(OperationResult.CreateResult(new ProductCreateViewModel()));
        }

        /// <inheritdoc />
        public override async Task<OperationResult<ProductUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var item = await _repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (item == null)
            {
                throw new MicroserviceNotFoundException();
            }

            var result = _mapper.Map<ProductUpdateViewModel>(item);
            return OperationResult.CreateResult(result);
        }
    }
}
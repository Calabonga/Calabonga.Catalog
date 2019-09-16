using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Catalog.Core.Exceptions;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.OperationResultsCore;

namespace Calabonga.Catalog.Web.Infrastructure.Factories
{
    /// <summary>
    /// // Calabonga: update summary (2019-05-26 12:36 CategoriesController)
    /// </summary>
    public class CategoryViewModelFactory : ViewModelFactory<Category, CategoryCreateViewModel, CategoryUpdateViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> _unitOfWork;

        /// <inheritdoc />
        public CategoryViewModelFactory(
            IMapper mapper,
            IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public override async Task<OperationResult<CategoryCreateViewModel>> GenerateForCreateAsync()
        {
            var result = new CategoryCreateViewModel
            {
                Name = "Категория по умолчанию"
            };

            return await Task.FromResult(OperationResult.CreateResult(result));
        }

        /// <inheritdoc />
        public override async Task<OperationResult<CategoryUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (category == null)
            {
                throw new MicroserviceArgumentNullException();
            }

            var mapped = _mapper.Map<CategoryUpdateViewModel>(category);
            return  OperationResult.CreateResult(mapped);
        }
    }
}
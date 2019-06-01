using System;
using AutoMapper;
using Calabonga.Catalog.Core.Exceptions;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.CategoryViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;

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
        public override CategoryCreateViewModel GenerateForCreate()
        {
            return new CategoryCreateViewModel
            {
                Name = "Категория по умолчанию"
            };
        }

        /// <inheritdoc />
        public override CategoryUpdateViewModel GenerateForUpdate(Guid id)
        {
            var category = _unitOfWork.GetRepository<Category>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (category == null)
            {
                throw new MicroserviceArgumentNullException();
            }

            return _mapper.Map<CategoryUpdateViewModel>(category);
        }
    }
}
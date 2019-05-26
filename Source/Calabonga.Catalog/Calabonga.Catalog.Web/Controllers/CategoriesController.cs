using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Catalog.Core.Exceptions;
using Calabonga.Catalog.Data;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Controllers.Base;
using Calabonga.Catalog.Web.Infrastructure.Factories.Base;
using Calabonga.Catalog.Web.Infrastructure.Managers.Base;
using Calabonga.Catalog.Web.Infrastructure.QueryParams;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.Settings;
using Calabonga.Catalog.Web.Infrastructure.Validations.Base;
using Calabonga.Catalog.Web.Infrastructure.ViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;

namespace Calabonga.Catalog.Web.Controllers
{
    /// <summary>
    /// Categories controller
    /// </summary>
    public class CategoriesController: WritableController<Category, CategoryCreateViewModel, CategoryUpdateViewModel, CategoryViewModel, PagedListQueryParams>
    {
        /// <inheritdoc />
        public CategoriesController(IEntityManager<Category, CategoryCreateViewModel, CategoryUpdateViewModel> entityManager, IOptions<CurrentAppSettings> options, IUnitOfWork<ApplicationDbContext, ApplicationUser, ApplicationRole> unitOfWork, IAccountService accountService) 
            : base(entityManager, options, unitOfWork, accountService)
        {
        }
    }

    /// <summary>
    /// // Calabonga: update summary (2019-05-26 01:05 CategoriesController)
    /// </summary>
    public class CategoryViewModel : ViewModelBase
    {
    }

    /// <summary>
    /// // Calabonga: update summary (2019-05-26 01:05 CategoriesController)
    /// </summary>
    public class CategoryUpdateViewModel : ViewModelBase
    {
        /// <summary>
        /// Name of the catalog
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description for current catalog
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// // Calabonga: update summary (2019-05-26 01:12 CategoriesController)
    /// </summary>
    public class CategoryCreateViewModel : IViewModel
    {
    }

    /// <summary>
    /// // Calabonga: update summary (2019-05-26 12:33 CategoriesController)
    /// </summary>
    public class CategoryValidator : EntityValidator<Category>
    {
        /// <inheritdoc />
        public CategoryValidator(IRepositoryFactory factory)
            : base(factory)
        {
        }
    }

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
            return new CategoryCreateViewModel();
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

using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Managers;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Validators;

namespace Calabonga.Catalog.Web.Infrastructure.Managers
{
    /// <summary>
    /// Review entity manager
    /// <see cref="IViewModelFactory{TEntity,TCreateViewModel,TUpdateViewModel}"/> implementation
    /// </summary>
    public class ReviewManager : EntityManager<Review, ReviewCreateViewModel, ReviewUpdateViewModel>
    {
        /// <inheritdoc />
        public ReviewManager(IMapper mapper, IViewModelFactory<Review, ReviewCreateViewModel, ReviewUpdateViewModel> viewModelFactory,
            IEntityValidator<Review> validator)
            : base(mapper, viewModelFactory, validator)
        {
        }
    }
}
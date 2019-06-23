using System;
using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Helpers;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Exceptions;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;

namespace Calabonga.Catalog.Web.Infrastructure.Factories
{
    /// <summary>
    /// ViewModel Factory for Review entity
    /// </summary>
    public class ReviewViewModelFactory : ViewModelFactory<Review, ReviewCreateViewModel, ReviewUpdateViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IRepository<Review> _repository;

        /// <inheritdoc />
        public ReviewViewModelFactory(IMapper mapper, IRepositoryFactory factory, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _repository = factory.GetRepository<Review>();
        }

        /// <inheritdoc />
        public override ReviewCreateViewModel GenerateForCreate()
        {
            var userName = "Аноним";
            var user = AsyncHelper.RunSync(async () => await _accountService.GetCurrentUserAsync());
            if (user != null)
            {
                userName = $"{user.LastName} {user.FirstName}";
            }

            return new ReviewCreateViewModel
            {
                UserName = userName
            };
        }

        /// <inheritdoc />
        public override ReviewUpdateViewModel GenerateForUpdate(Guid id)
        {
            var item = _repository.GetFirstOrDefault(predicate: x => x.Id == id);
            if (item == null)
            {
                throw new MicroserviceNotFoundException();
            }

            return _mapper.Map<ReviewUpdateViewModel>(item);
        }
    }
}
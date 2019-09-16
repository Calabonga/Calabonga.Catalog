using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Catalog.Models;
using Calabonga.Catalog.Web.Infrastructure.Helpers;
using Calabonga.Catalog.Web.Infrastructure.Services;
using Calabonga.Catalog.Web.Infrastructure.ViewModels.ReviewViewModels;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Exceptions;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Factories;
using Calabonga.OperationResultsCore;

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
        public override async Task<OperationResult<ReviewCreateViewModel>> GenerateForCreateAsync()
        {
            var userName = "Аноним";
            var user = await _accountService.GetCurrentUserAsync();
            if (user != null)
            {
                userName = $"{user.LastName} {user.FirstName}";
            }

            var result = new ReviewCreateViewModel
            {
                UserName = userName
            };

            return OperationResult.CreateResult(result);
        }

        /// <inheritdoc />
        public override async Task<OperationResult<ReviewUpdateViewModel>> GenerateForUpdateAsync(Guid id)
        {
            var item = await _repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (item == null)
            {
                throw new MicroserviceNotFoundException();
            }

            var result = _mapper.Map<ReviewUpdateViewModel>(item);
            return OperationResult.CreateResult(result);
        }
    }
}
using AutoMapper;
using Calabonga.Catalog2023.Domain;
using Calabonga.Catalog2023.Domain.Base;
using Calabonga.Catalog2023.Infrastructure;
using Calabonga.Catalog2023.Web.Application.Services;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Notifications;
using Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.ViewModels;
using Calabonga.Catalog2023.Web.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace Calabonga.Catalog2023.Web.Endpoints.ProductEndpoints.Queries;

public class ProductPostUpdate
{
	public record Request(ProductUpdateViewModel Model, ClaimsPrincipal User)
		: IRequest<OperationResult<ProductViewModel>>;

	public class Handler
		: IRequestHandler<Request, OperationResult<ProductViewModel>>
	{
		private readonly IMediator _mediator;
		private readonly ITagCalculator _tagCalculator;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public Handler(
			IMediator mediator,
			ITagCalculator tagCalculator,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_mediator = mediator;
			_tagCalculator = tagCalculator;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<OperationResult<ProductViewModel>> Handle(
			Request request,
			CancellationToken cancellationToken)
		{
			var operation = OperationResult.CreateResult<ProductViewModel>();
			var repository = _unitOfWork.GetRepository<Product>();

			var entity = await repository.GetFirstOrDefaultAsync(
				predicate: x => x.Id == request.Model.Id,
				disableTracking: false,
				ignoreQueryFilters: request.User.IsInRole(AppData.SystemAdministratorRoleName));

			if (entity is null)
			{
				operation.AddError(new CatalogNotFoundException(nameof(Product), request.Model.Id.ToString()));
				return operation;
			}

			var currentState = entity.Visible;
			var newState = request.Model.Visible;

			_mapper.Map(request.Model, entity,
				o => o.Items[nameof(ApplicationUser)] = request.User.Identity!.Name);

			if (!entity.Category!.Visible && entity.Visible)
			{
				entity.Visible = false;
				operation.AddError(new CatalogInvalidOperationException("Unable to set visibility for product", "catalog is disabled"));
				return operation;
			}

			if (currentState && newState == false)
			{
				await _mediator.Publish(new ProductHide.Notification(entity.Id, request.User), cancellationToken);
			}

			if (!currentState && newState)
			{
				await _mediator.Publish(new ProductShow.Notification(entity.Id, request.User), cancellationToken);
			}

			var calculation = await _tagCalculator.ProcessTagsAsync(
				request.Model.Tags.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries),
				entity,
				cancellationToken);

			if (!calculation.Competed)
			{
				operation.AddError(calculation.Exception!);
				return operation;
			}

			repository.Update(entity);

			await _unitOfWork.SaveChangesAsync();

			if (!_unitOfWork.LastSaveChangesResult.IsOk)
			{
				var exception = _unitOfWork.LastSaveChangesResult.Exception
								?? new CatalogDatabaseSaveException("Error saving entity Product");

				operation.AddError(exception);
				return operation;
			}

			var result = _mapper.Map<ProductViewModel>(entity);

			operation.Result = result;

			return operation;

		}
	}
}
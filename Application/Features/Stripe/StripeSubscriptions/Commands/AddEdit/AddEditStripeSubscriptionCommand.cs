using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Stripe;
using MediatR;

namespace Application.Features.Stripe.StripeSubscriptions.Commands.AddEdit
{
    public partial class AddEditStripeSubscriptionCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string StripeSubscriptionId { get; set; }

        public string SimtrixxUserId { get; set; }
    }

    internal class AddEditStripeSubscriptionCommandHandler : IRequestHandler<AddEditStripeSubscriptionCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditStripeSubscriptionCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditStripeSubscriptionCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var subscription = _mapper.Map<StripeSubscription>(command);
                await _unitOfWork.Repository<StripeSubscription>().AddAsync(subscription);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllStripeSubscriptionCacheKey);
                return await Result<int>.SuccessAsync(subscription.Id, "Subscription Saved");
            }
            else
            {
                var subscription = await _unitOfWork.Repository<StripeSubscription>().GetByIdAsync(command.Id);
                if (subscription != null)
                {
                    subscription.StripeSubscriptionId = command.StripeSubscriptionId;
                    await _unitOfWork.Repository<StripeSubscription>().UpdateAsync(subscription);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllStripeSubscriptionCacheKey);
                    return await Result<int>.SuccessAsync(subscription.Id, "Subscription Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Checkout Not Found!");
                }
            }
        }
    }
}

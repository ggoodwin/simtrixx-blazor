using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Stripe;
using MediatR;

namespace Application.Features.Stripe.StripeOrders.Commands.AddEdit
{
    public partial class AddEditStripeOrderCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string StripeOrderId { get; set; }
        public string Email { get; set; }

        public int LicenseId { get; set; }
        public int StripeSubscriptionId { get; set; }
        public int StripeCustomerId { get; set; }
        public string SimtrixxUserId { get; set; }
    }

    internal class AddEditStripeOrderCommandHandler : IRequestHandler<AddEditStripeOrderCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditStripeOrderCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditStripeOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var order = _mapper.Map<StripeOrder>(command);
                await _unitOfWork.Repository<StripeOrder>().AddAsync(order);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllStripeOrdersCacheKey);
                return await Result<int>.SuccessAsync(order.Id, "Order Saved");
            }
            else
            {
                var order = await _unitOfWork.Repository<StripeOrder>().GetByIdAsync(command.Id);
                if (order != null)
                {
                    order.StripeOrderId = command.StripeOrderId;
                    await _unitOfWork.Repository<StripeOrder>().UpdateAsync(order);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllStripeOrdersCacheKey);
                    return await Result<int>.SuccessAsync(order.Id, "Order Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Order Not Found!");
                }
            }
        }
    }
}

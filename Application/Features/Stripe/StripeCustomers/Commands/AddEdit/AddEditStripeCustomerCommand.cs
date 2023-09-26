using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Constants.Application;
using Common.Wrapper;
using Domain.Entities.Stripe;
using MediatR;

namespace Application.Features.Stripe.StripeCustomers.Commands.AddEdit
{
    public partial class AddEditStripeCustomerCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string StripeCustomerId { get; set; }

        public string SimtrixxUserId { get; set; }
    }

    internal class AddEditStripeCustomerCommandHandler : IRequestHandler<AddEditStripeCustomerCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditStripeCustomerCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddEditStripeCustomerCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var customer = _mapper.Map<StripeCustomer>(command);
                await _unitOfWork.Repository<StripeCustomer>().AddAsync(customer);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken,
                    ApplicationConstants.Cache.GetAllStripeCustomersCacheKey);
                return await Result<int>.SuccessAsync(customer.Id, "Customer Saved");
            }
            else
            {
                var customer = await _unitOfWork.Repository<StripeCustomer>().GetByIdAsync(command.Id);
                if (customer != null)
                {
                    customer.StripeCustomerId = command.StripeCustomerId;
                    await _unitOfWork.Repository<StripeCustomer>().UpdateAsync(customer);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllStripeCustomersCacheKey);
                    return await Result<int>.SuccessAsync(customer.Id, "Customer Updated");
                }
                else
                {
                    return await Result<int>.FailAsync("Customer Not Found!");
                }
            }
        }
    }
}

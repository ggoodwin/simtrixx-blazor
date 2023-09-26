using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities.Stripe;
using MediatR;

namespace Application.Features.Stripe.StripeCustomers.Queries.GetById
{
    public class GetStripeCustomerByIdQuery : IRequest<Result<GetStripeCustomerByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetStripeCustomerByIdQueryHandler : IRequestHandler<GetStripeCustomerByIdQuery, Result<GetStripeCustomerByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetStripeCustomerByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetStripeCustomerByIdResponse>> Handle(GetStripeCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var stripeCustomer = await _unitOfWork.Repository<StripeCustomer>().GetByIdAsync(query.Id);
            var mappedStripeCustomer = _mapper.Map<GetStripeCustomerByIdResponse>(stripeCustomer);
            return await Result<GetStripeCustomerByIdResponse>.SuccessAsync(mappedStripeCustomer);
        }
    }
}

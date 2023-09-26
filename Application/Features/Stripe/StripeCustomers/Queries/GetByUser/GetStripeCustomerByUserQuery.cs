using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using MediatR;

namespace Application.Features.Stripe.StripeCustomers.Queries.GetByUser
{
    public class GetStripeCustomerByUserQuery : IRequest<Result<GetStripeCustomerByUserResponse>>
    {
        public string UserId { get; set; }
    }

    internal class GetStripeCustomerByUserQueryHandler : IRequestHandler<GetStripeCustomerByUserQuery, Result<GetStripeCustomerByUserResponse>>
    {
        private readonly IStripeRepository _stripeRepository;
        private readonly IMapper _mapper;

        public GetStripeCustomerByUserQueryHandler(IStripeRepository stripeRepository, IMapper mapper)
        {
            _stripeRepository = stripeRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetStripeCustomerByUserResponse>> Handle(GetStripeCustomerByUserQuery query, CancellationToken cancellationToken)
        {
            var stripeCustomer = await _stripeRepository.GetCustomerByUserAsync(query.UserId);
            var mappedStripeCustomer = _mapper.Map<GetStripeCustomerByUserResponse>(stripeCustomer);
            return await Result<GetStripeCustomerByUserResponse>.SuccessAsync(mappedStripeCustomer);
        }
    }
}

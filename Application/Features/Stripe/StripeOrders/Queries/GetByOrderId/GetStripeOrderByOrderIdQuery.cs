using System.Linq.Expressions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Wrapper;
using Domain.Entities.Stripe;
using MediatR;

namespace Application.Features.Stripe.StripeOrders.Queries.GetByOrderId
{
    public class GetStripeOrderByOrderIdQuery : IRequest<Result<GetStripeOrderByOrderIdResponse>>
    {
        public string OrderId { get; set; }
    }

    internal class GetStripeOrderByOrderIdQueryHandler : IRequestHandler<GetStripeOrderByOrderIdQuery, Result<GetStripeOrderByOrderIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStripeRepository _stripeRepository;

        public GetStripeOrderByOrderIdQueryHandler(IMapper mapper, IStripeRepository stripeRepository)
        {
            _mapper = mapper;
            _stripeRepository = stripeRepository;
        }

        public async Task<Result<GetStripeOrderByOrderIdResponse>> Handle(GetStripeOrderByOrderIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _stripeRepository.GetOrderByOrderIdAsync(query.OrderId);
            if (order != null)
            {
                var mappedOrder = _mapper.Map<GetStripeOrderByOrderIdResponse>(order);
                return await Result<GetStripeOrderByOrderIdResponse>.SuccessAsync(mappedOrder);
            }
            else
            {
                return await Result<GetStripeOrderByOrderIdResponse>.FailAsync();
            }
        }
    }
}

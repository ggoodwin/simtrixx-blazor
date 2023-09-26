using Application.Features.Stripe.StripeCustomers.Commands.AddEdit;
using Application.Features.Stripe.StripeCustomers.Queries.GetById;
using Application.Features.Stripe.StripeCustomers.Queries.GetByUser;
using Application.Features.Stripe.StripeOrders.Commands.AddEdit;
using Application.Features.Stripe.StripeOrders.Queries.GetByOrderId;
using Application.Features.Stripe.StripeSubscriptions.Commands.AddEdit;
using AutoMapper;
using Domain.Entities.Stripe;

namespace Application.Mappings
{
    public class StripeProfile : Profile
    {
        public StripeProfile()
        {
            CreateMap<AddEditStripeCustomerCommand, StripeCustomer>().ReverseMap();
            CreateMap<GetStripeCustomerByIdResponse, StripeCustomer>().ReverseMap();
            CreateMap<GetStripeCustomerByUserResponse, StripeCustomer>().ReverseMap();

            CreateMap<AddEditStripeSubscriptionCommand, StripeSubscription>().ReverseMap();

            CreateMap<AddEditStripeOrderCommand, StripeOrder>().ReverseMap();
            CreateMap<GetStripeOrderByOrderIdResponse, StripeOrder>().ReverseMap();
        }
    }
}

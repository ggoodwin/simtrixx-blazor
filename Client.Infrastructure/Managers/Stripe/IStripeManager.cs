using Application.Features.Stripe.StripeCustomers.Commands.AddEdit;
using Application.Features.Stripe.StripeCustomers.Queries.GetById;
using Application.Features.Stripe.StripeCustomers.Queries.GetByUser;
using Application.Features.Stripe.StripeOrders.Commands.AddEdit;
using Application.Features.Stripe.StripeOrders.Queries.GetByOrderId;
using Application.Features.Stripe.StripeSubscriptions.Commands.AddEdit;
using Application.Responses.Stripe;
using Common.Wrapper;

namespace Client.Infrastructure.Managers.Stripe
{
    public interface IStripeManager : IManager
    {
        Task<string> CreateCheckoutSessionAsync();
        Task<StripeCheckoutResponse> GetCheckoutSessionAsync(string sessionId);
        Task<IResult<int>> SaveCustomerAsync(AddEditStripeCustomerCommand request);
        Task<IResult<int>> SaveSubscriptionAsync(AddEditStripeSubscriptionCommand request);
        Task<IResult<int>> SaveOrderAsync(AddEditStripeOrderCommand request);
        Task<IResult<GetStripeOrderByOrderIdResponse>> GetOrderByOrderId(GetStripeOrderByOrderIdQuery request);
        Task<string> CreateBillingSessionAsync(string customerId);
        Task<IResult<GetStripeCustomerByIdResponse>> GetLicenseByIdAsync(GetStripeCustomerByIdQuery request);
        Task<IResult<GetStripeCustomerByUserResponse>> GetCustomerByUserAsync(GetStripeCustomerByUserQuery request);
    }
}

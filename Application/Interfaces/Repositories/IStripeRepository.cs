using Domain.Entities.Stripe;

namespace Application.Interfaces.Repositories
{
    public interface IStripeRepository
    {
        Task<StripeOrder?> GetOrderByOrderIdAsync(string orderId);
        Task<StripeCustomer?> GetCustomerByUserAsync(string userId);
    }
}

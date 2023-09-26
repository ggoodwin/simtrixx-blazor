using Domain.Entities;

namespace Application.Features.Stripe.StripeOrders.Queries.GetByOrderId
{
    public class GetStripeOrderByOrderIdResponse
    {
        public int Id { get; set; }
        public int LicenseId { get; set; }
    }
}

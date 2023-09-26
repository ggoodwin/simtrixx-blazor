namespace Application.Responses.Stripe
{
    public class StripeOrderResponse
    {
        public string Id { get; set; }

        public int LicenseId { get; set; }
        public virtual Domain.Entities.License License { get; set; }
    }
}

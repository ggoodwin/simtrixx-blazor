using System.Text.Json.Serialization;

namespace Application.Responses.Stripe
{
    public class StripeCheckoutResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("customerId")]
        public string? CustomerId { get; set; }
        [JsonPropertyName("customerDetails")]
        public CustomerDetails? CustomerDetails { get; set; }
        [JsonPropertyName("subscriptionId")]
        public string? SubscriptionId { get; set; }
    }

    public class CustomerDetails
    {
        [JsonPropertyName("address")]
        public Address? Address { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }
    }
}

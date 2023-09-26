namespace Client.Infrastructure.Routes
{
    public class StripeEndpoints
    {
        public static string GetCheckoutSession(string sessionId)
        {
            return $"api/stripe/checkout-session/{sessionId}";
        }

        public static string GetCustomer(int id)
        {
            return $"api/stripe/get-customer/{id}";
        }

        public static string CreateBillingSession(string customerId)
        {
            return $"api/stripe/billing-portal/{customerId}";
        }

        public static string GetCustomerByUser(string userId)
        {
            return $"api/stripe/get-customer-by-user/{userId}";
        }

        public static string CreateCheckoutSession = "api/stripe/create-checkout-session";
        public static string SaveCustomer = "api/stripe/save-customer";
        public static string SaveSubscription = "api/stripe/save-subscription";
        public static string SaveOrder = "api/stripe/save-order";
        public static string GetOrderByOrderId = "api/stripe/get-by-order-id";
        
    }
}

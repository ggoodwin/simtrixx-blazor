using System.Data;

namespace Common.Constants.Application
{
    public static class ApplicationConstants
    {
        public static class SignalR
        {
            public const string HubUrl = "/signalRHub";
            public const string SendRegenerateTokens = "RegenerateTokensAsync";
            public const string ReceiveRegenerateTokens = "RegenerateTokens";
            public const string ReceiveMessage = "ReceiveMessage";
            public const string SendMessage = "SendMessageAsync";

            public const string OnConnect = "OnConnectAsync";
            public const string ConnectUser = "ConnectUser";
            public const string OnDisconnect = "OnDisconnectAsync";
            public const string DisconnectUser = "DisconnectUser";
            public const string OnChangeRolePermissions = "OnChangeRolePermissions";
            public const string LogoutUsersByRole = "LogoutUsersByRole";

            public const string PingRequest = "PingRequestAsync";
            public const string PingResponse = "PingResponseAsync";

        }
        public static class Cache
        {
            public const string GetAllSupportRepliesCacheKey = "all-reply-tickets";
            public const string GetAllLicensesCacheKey = "all-licenses";
            public const string GetAllContactRequestsCacheKey = "all-contact-requests";
            public const string GetAllDemoRequestsCacheKey = "all-demo-requests";

            public const string GetAllSupportTicketsByUserIdCacheKey = "all-support-tickets-by-user";
            public const string GetAllSupportTicketsCacheKey = "all-support-tickets";
            public const string GetAllSupportTicketsByStatusCacheKey = "all-support-tickets-by-status";

            public const string GetAllDepartmentsCacheKey = "all-departments";

            public const string GetAllStripeSubscriptionCacheKey = "all-stripe-subscriptions";
            public const string GetAllStripeCustomersCacheKey = "all-stripe-customers";
            public const string GetAllStripeOrdersCacheKey = "all-stripe-orders";

            public static string GetAllEntityExtendedAttributesByEntityIdCacheKey<TEntityId>(string entityFullName, TEntityId entityId)
            {
                return $"all-{entityFullName}-extended-attributes-{entityId}";
            }

            public static string GetAllEntityExtendedAttributesCacheKey(string entityFullName)
            {
                return $"all-{entityFullName}-extended-attributes";
            }
        }

        public static class MimeTypes
        {
            public const string OpenXml = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}

using Domain.Enums;

namespace Client.Infrastructure.Routes
{
    public class SupportTicketEndpoints
    {
        public static string GetById(int id)
        {
            return $"api/supportticket/{id}";
        }

        public static string GetByUser(string userId)
        {
            return $"api/supportticket/byuser/{userId}";
        }

        public static string GetByStatus(SupportStatus status)
        {
            return $"api/supportticket/bystatus/{status}";
        }

        public static string GetAll = "api/supportticket/all";
        public static string Save = "api/supportticket";
        public static string Delete = "api/supportticket";
    }
}

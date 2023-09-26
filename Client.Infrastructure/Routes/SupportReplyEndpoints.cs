namespace Client.Infrastructure.Routes
{
    public class SupportReplyEndpoints
    {
        public static string GetById(int id)
        {
            return $"api/supportreply/{id}";
        }

        public static string GetAll(int supportTicketId)
        {
            return $"api/supportreply/{supportTicketId}";
        }

        public static string Save = "api/supportreply";
        public static string Delete = "api/supportreply";
    }
}

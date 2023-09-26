namespace Client.Infrastructure.Routes
{
    public class ContactRequestEndpoints
    {
        public static string GetById(int id)
        {
            return $"api/contactrequest/{id}";
        }

        public static string GetAll = "api/contactrequest/all";
        public static string Save = "api/contactrequest";
        public static string Delete = "api/contactrequest";
    }
}

namespace Client.Infrastructure.Routes
{
    public class DemoRequestEndpoints
    {
        public static string GetById(int id)
        {
            return $"api/demorequest/{id}";
        }

        public static string GetAllDemoRequests = "api/demorequest/all";
        public static string Save = "api/demorequest";
        public static string Delete = "api/demorequest";
    }
}

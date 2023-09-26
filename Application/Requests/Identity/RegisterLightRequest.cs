using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Identity
{
    public class RegisterLightRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}

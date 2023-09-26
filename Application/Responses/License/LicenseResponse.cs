using System.ComponentModel.DataAnnotations;
using Domain.Entities.Identity;

namespace Application.Responses.License
{
    public class LicenseResponse
    {
        public int Id { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public SimtrixxUser User { get; set; }
    }
}

using Domain.Entities.Identity;

namespace Application.Features.Licenses.Queries.GetByUser
{
    public class GetLicensesByUserResponse
    {
        public int Id { get; set; }
        public DateTime Expiration { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
        public SimtrixxUser SimtrixxUser { get; set; }
    }
}

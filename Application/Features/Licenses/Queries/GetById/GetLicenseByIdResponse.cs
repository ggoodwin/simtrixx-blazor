using Domain.Entities.Identity;

namespace Application.Features.Licenses.Queries.GetById
{
    public class GetLicenseByIdResponse
    {
        public int Id { get; set; }
        public DateTime Expiration { get; set; }
        public string Key { get; set; }
        public string UserId { get; set; }
    }
}

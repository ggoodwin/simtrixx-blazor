using Domain.Entities.Identity;

namespace Application.Features.Licenses.Queries.GetAll
{
    public class GetAllLicensesResponse
    {
        public int Id { get; set; }
        public DateTime? Expiration { get; set; }
        public string Key { get; set; }
        public string SimtrixxUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}

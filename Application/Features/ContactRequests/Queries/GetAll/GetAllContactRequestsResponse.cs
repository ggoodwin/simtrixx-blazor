namespace Application.Features.ContactRequests.Queries.GetAll
{
    public class GetAllContactRequestsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string? Notes { get; set; }
        public bool Contacted { get; set; }
    }
}

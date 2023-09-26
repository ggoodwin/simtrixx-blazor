namespace Application.Features.ContactRequests.Queries.GetById
{
    public class GetContactRequestByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string? Notes { get; set; }
        public bool Contacted { get; set; }
    }
}

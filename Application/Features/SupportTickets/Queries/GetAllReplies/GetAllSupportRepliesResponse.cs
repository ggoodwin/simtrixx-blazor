namespace Application.Features.SupportTickets.Queries.GetAllReplies
{
    public class GetAllSupportRepliesResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int SupportTicketId { get; set; }
        public string SimtrixxUserId { get; set; }
        public string UserName { get; set; }
    }
}

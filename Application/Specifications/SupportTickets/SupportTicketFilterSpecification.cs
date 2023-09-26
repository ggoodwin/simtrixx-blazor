using Domain.Entities.Support;

namespace Application.Specifications.SupportTickets
{
    public class SupportTicketFilterSpecification : SimtrixxSpecification<SupportTicket>
    {
        public SupportTicketFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Subject.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}

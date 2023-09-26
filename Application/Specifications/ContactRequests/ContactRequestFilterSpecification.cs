using Domain.Entities;

namespace Application.Specifications.ContactRequests
{
    public class ContactRequestFilterSpecification : SimtrixxSpecification<ContactRequest>
    {
        public ContactRequestFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Email.Contains(searchString) || p.Notes.Contains(searchString) || p.Message.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}

using Domain.Entities;

namespace Application.Specifications.DemoRequests
{
    public class DemoRequestFilterSpecification : SimtrixxSpecification<DemoRequest>
    {
        public DemoRequestFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Email.Contains(searchString) || p.Notes.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}

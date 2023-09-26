using Domain.Entities;

namespace Application.Specifications.Licenses
{
    public class LicenseFilterSpecification : SimtrixxSpecification<License>
    {
        public LicenseFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.SimtrixxUser.FirstName.Contains(searchString) || p.SimtrixxUser.LastName.Contains(searchString) || p.SimtrixxUser.Email.Contains(searchString) || p.SimtrixxUser.UserName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}

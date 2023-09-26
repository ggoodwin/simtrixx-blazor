using Domain.Entities.Support;

namespace Application.Specifications.Departments
{
    public class DepartmentFilterSpecification : SimtrixxSpecification<SupportDepartment>
    {
        public DepartmentFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}

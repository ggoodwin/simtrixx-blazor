using Application.Features.Departments.Commands.AddEdit;
using Application.Features.Departments.Queries.GetAll;
using Common.Wrapper;

namespace Client.Infrastructure.Managers.Department
{
    public interface IDepartmentManager : IManager
    {
        Task<IResult<IEnumerable<GetAllDepartmentsResponse>>> GetAllAsync();
        Task<IResult<int>> SaveAsync(AddEditDepartmentCommand request);
        Task<IResult<int>> DeleteAsync(int id);
    }
}

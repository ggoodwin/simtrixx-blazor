using Application.Features.DemoRequests.Commands.AddEdit;
using Application.Features.DemoRequests.Queries.GetAll;
using Common.Wrapper;

namespace Client.Infrastructure.Managers.DemoRequest
{
    public interface IDemoRequestManager : IManager
    {
        Task<IResult<IEnumerable<GetAllDemoRequestsResponse>>> GetAllDemoRequestsAsync();
        Task<IResult<int>> SaveAsync(AddEditDemoRequestCommand request);
        Task<IResult<int>> DeleteAsync(int id);
    }
}

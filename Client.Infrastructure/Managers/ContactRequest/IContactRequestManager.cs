using Application.Features.ContactRequests.Commands.AddEdit;
using Application.Features.ContactRequests.Queries.GetAll;
using Common.Wrapper;

namespace Client.Infrastructure.Managers.ContactRequest
{
    public interface IContactRequestManager : IManager
    {
        Task<IResult<IEnumerable<GetAllContactRequestsResponse>>> GetAllContactRequestsAsync();
        Task<IResult<int>> SaveAsync(AddEditContactRequestCommand request);
        Task<IResult<int>> DeleteAsync(int id);
    }
}

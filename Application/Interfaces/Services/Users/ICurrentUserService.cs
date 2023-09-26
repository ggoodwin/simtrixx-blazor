using Application.Interfaces.Common;

namespace Application.Interfaces.Services.Users
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}

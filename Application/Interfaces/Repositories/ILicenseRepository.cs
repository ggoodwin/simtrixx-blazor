using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ILicenseRepository
    {
        Task<IEnumerable<License>> GetByUserAsync();
    }
}

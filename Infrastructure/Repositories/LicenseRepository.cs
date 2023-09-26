using System.Collections;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Users;
using Common.Wrapper;
using Domain.Entities;
using Infrastructure.Contexts;
using LazyCache;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly DataContext _dbContext;

        public LicenseRepository(DataContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<License>> GetByUserAsync()
        {
            return await _dbContext.Licenses.Where(l => l.SimtrixxUser.Id == _currentUserService.UserId).ToListAsync();
        }
    }
}

using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Users;
using Domain.Entities.Stripe;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StripeRepository : IStripeRepository
    {
        private readonly DataContext _dbContext;

        public StripeRepository(DataContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<StripeOrder?> GetOrderByOrderIdAsync(string orderId)
        {
            return await _dbContext.StripeOrder.FirstOrDefaultAsync(so => so != null && so.StripeOrderId == orderId);
        }

        public async Task<StripeCustomer?> GetCustomerByUserAsync(string userId)
        {
            return await _dbContext.StripeCustomer.FirstOrDefaultAsync(sc => sc.SimtrixxUserId == userId);
        }
    }
}

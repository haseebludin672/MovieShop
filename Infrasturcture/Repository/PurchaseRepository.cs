using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Purchase> GetPurchaseByUser(int movieId, int userId)
    {
        var purchasedByUser = await _dbContext.Purchases.Include(p => p.Movie)
            .FirstOrDefaultAsync(p => p.MovieId == movieId && p.UserId == userId);

        return purchasedByUser;
    }

    public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id)
    {
        var AllPurchases = await _dbContext.Purchases.Include(p => p.Movie)
            .Where(p => p.UserId == id).ToListAsync();
        return AllPurchases;
    }
}
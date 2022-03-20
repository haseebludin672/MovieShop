using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Favorite> GetFavoriteByUser(FavoriteRequestModel model)
    {
        var favorite =
            await _dbContext.Favorites.FirstOrDefaultAsync(f => f.MovieId == model.MovieId && f.UserId == model.UserId);
        return favorite;
    }

    public async Task<IEnumerable<Favorite>> GetAllFavoriteByUser(int id)
    {
        var favorites = await _dbContext.Favorites.Include(f => f.Movie)
            .Where(f => f.UserId == id).ToListAsync();
        return favorites;
    }
}
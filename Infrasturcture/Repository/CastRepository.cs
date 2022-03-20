using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : EfRepository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Cast> GetById(int id)
    {
        var castDetails = await _dbContext.Casts.Include(c => c.MovieCasts).ThenInclude(c => c.Movie)
            .FirstOrDefaultAsync(c => c.Id == id);
        return castDetails;
    }

    public CastDetailsModel GetCastDetails(int id)
    {
        throw new NotImplementedException();
    }
}
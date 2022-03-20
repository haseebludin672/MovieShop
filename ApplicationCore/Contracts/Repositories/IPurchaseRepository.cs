using ApplicationCore.Entities;
using ApplicationCore.Contracts.Repositories
    ;
namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository : IRepository<Purchase>
{
    Task<Purchase> GetPurchaseByUser(int movieId, int userId);
    Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int id);
}

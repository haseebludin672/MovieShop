using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories;
public interface IReviewRepository : IRepository<Review>
{
    Task<Review> GetReviewByUser(int movieId, int userId);
    Task<IEnumerable<Review>> GetAllReviewsByUser(int id);
}
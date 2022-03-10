using ApplicationCore.Contracts.Repositories;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
   
    }
}

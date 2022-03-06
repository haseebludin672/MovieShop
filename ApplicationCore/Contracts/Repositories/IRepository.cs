using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{// we are gonna create a generic repository interface where T is gonna be class of entities
    // Base repository is gonna have commonly used CRUD methods
    public interface IRepository <T> where T : class 
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookapp.Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Upsert(T entity);
    }
}
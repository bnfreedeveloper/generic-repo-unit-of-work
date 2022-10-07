using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookapp.Core.IRepository;
using bookapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bookapp.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(AppDbContext ctxt, ILogger logger)
        {
            context = ctxt;
            _logger = logger;
            dbSet = ctxt.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
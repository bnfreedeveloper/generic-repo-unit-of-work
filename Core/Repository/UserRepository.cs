using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookapp.Core.IRepository;
using bookapp.Data;
using bookapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bookapp.Core.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext ctxt, ILogger logger) : base(ctxt, logger)
        {
        }
        public override async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await base.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{typeof(UserRepository)} all method error");
                return new List<User>();
            }
        }
        public override async Task<bool> Upsert(User entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingUser == null)
                {
                    return await Add(entity);
                }
                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{typeof(UserRepository)} all method error");
                return false;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existUser = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (existUser != null)
                {
                    dbSet.Remove(existUser);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"{typeof(UserRepository)} all method error");
                return false;
            }

        }
    }
}
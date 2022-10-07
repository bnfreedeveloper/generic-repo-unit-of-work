using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookapp.Core.IConfiguration;
using bookapp.Core.IRepository;
using bookapp.Core.Repository;
using Microsoft.Extensions.Logging;

namespace bookapp.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users { get; set; }
        public UnitOfWork(AppDbContext ctx, ILoggerFactory log)
        {
            _context = ctx;
            _logger = log.CreateLogger("logs");

            Users = new UserRepository(_context, _logger);
        }

        public async Task CompleteChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
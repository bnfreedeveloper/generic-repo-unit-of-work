using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookapp.Core.IRepository;

namespace bookapp.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; set; }
        Task CompleteChangesAsync();
    }
}
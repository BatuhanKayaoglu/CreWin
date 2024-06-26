using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreWin.Infrastructure.IRepositories
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}

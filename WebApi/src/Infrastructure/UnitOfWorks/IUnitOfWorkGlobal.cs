using BL.UnitOfWorks;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public interface IUnitOfWorkGlobal : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}

using DAL.Data;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public class UnitOfWorkGlobal : UnitOfWorkBase<DALDbContext>, IUnitOfWorkGlobal
    {
        public UnitOfWorkGlobal(DALDbContext dbContext, IUserRepository userRepository) : base(dbContext)
        {
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; }
    }
}

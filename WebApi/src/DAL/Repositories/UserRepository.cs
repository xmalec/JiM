using DAL.Data;
using DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DALDbContext dbContext, ILogger<BaseRepository<User>> logger) : base(dbContext, logger)
        {
        }

        public User? GetUserByEmail(string email)
        {
            return Query().FirstOrDefault(x => email == x.Email);
        }
    }
}

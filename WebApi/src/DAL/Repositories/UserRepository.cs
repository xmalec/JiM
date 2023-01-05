using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DALDbContext dbContext) : base(dbContext)
        {
        }

        public User? GetUserByEmail(string email)
        {
            return Query().FirstOrDefault(x => email == x.Email);
        }
    }
}

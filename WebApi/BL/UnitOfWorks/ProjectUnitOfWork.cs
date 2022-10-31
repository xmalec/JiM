using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.UnitOfWorks
{
    public class ProjectUnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public ProjectUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            dbContext.SaveChanges();
        }
    }
}

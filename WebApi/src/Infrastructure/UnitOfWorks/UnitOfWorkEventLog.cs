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
    public class UnitOfWorkEventLog : UnitOfWorkBase<DALDbContext>, IUnitOfWorkEventLog
    {
        public UnitOfWorkEventLog(DALDbContext dbContext, IBaseRepository<EventLog> userRepository) : base(dbContext)
        {
            EventLogRepository = userRepository;
        }

        public IBaseRepository<EventLog> EventLogRepository { get; }
    }
}

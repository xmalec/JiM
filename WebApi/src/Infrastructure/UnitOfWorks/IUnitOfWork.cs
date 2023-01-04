using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persists all changes made within this unit of work.
        /// </summary>
        Task Commit();

        /// <summary>
        /// Roll backs the changes made in the current transaction of the UnitOfWork
        /// </summary>
        void Rollback();
    }
}

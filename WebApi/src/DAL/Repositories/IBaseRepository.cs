using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IBaseRepository<TEntity> : IRepository where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(int id);
        TEntity GetById(int id);
        IQueryable<TEntity> Query();
    }
}

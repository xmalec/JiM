using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly DALDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(DALDbContext dbContext)
        {
            context = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public TEntity Find(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            dbSet.Add(entity);
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet.AsQueryable();
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}

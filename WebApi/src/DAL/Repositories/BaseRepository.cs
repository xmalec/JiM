﻿using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        private readonly string entityName = typeof(TEntity).Name;

        public BaseRepository(DALDbContext dbContext)
        {
            context = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            await context.SaveChangesAsync();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public async Task Insert(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            dbSet.Add(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet.AsQueryable();
        }

        public async Task Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            var originalEntity = context.Find<TEntity>(entity.Id);
            entity.CreatedDate = originalEntity.CreatedDate;
            context.Entry(originalEntity).CurrentValues.SetValues(entity);
            await context.SaveChangesAsync();
        }
    }
}

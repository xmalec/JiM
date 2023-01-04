using BL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWorks
{
    public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork, IDisposable where TDbContext : DbContext
    {
        protected readonly TDbContext dbContext;
        private IDbContextTransaction? transaction;

        public UnitOfWorkBase(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            transaction = dbContext.Database.BeginTransaction();
        }

        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
            if (transaction != null) await transaction.CommitAsync();
            transaction = null;
        }

        public void Rollback()
        {
            transaction?.Rollback();
        }

        public void Dispose()
        {
            transaction?.Rollback();
            transaction?.Dispose();
            transaction = null;
        }

    }
}

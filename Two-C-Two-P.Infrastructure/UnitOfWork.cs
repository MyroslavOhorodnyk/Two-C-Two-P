using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Two_C_Two_P.Core.Enums;
using Two_C_Two_P.Core.Interfaces;
using Two_C_Two_P.Core.Interfaces.Repositories;
using Two_C_Two_P.Infrastructure.Models;
using Two_C_Two_P.Infrastructure.Repositories;

namespace Two_C_Two_P.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwoCTwoPContext _dbContext;
        private IDbContextTransaction _transaction;
        private ITransactionRepository _transactionRepository;

        public UnitOfWork(TwoCTwoPContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransactionRepository TransactionRepository
        {
            get { return _transactionRepository = _transactionRepository ?? new TransactionRepository(_dbContext); }
        }

        public async Task CommitAsync(bool useTransaction = false)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                if (useTransaction)
                {
                    _transaction = await _dbContext.Database.BeginTransactionAsync();
                }

                SetAuditValues();

                await _dbContext.SaveChangesAsync();

                if (useTransaction)
                {
                    await _transaction.CommitAsync();
                }
            });
        }

        public async Task RollbackAsync()
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await _dbContext.DisposeAsync();

                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            });
        }

        private void SetAuditValues()
        {
            var entities = _dbContext.ChangeTracker.Entries().Where(e =>
                e.Entity is IEntity<string> && (e.State == EntityState.Added ||
                                              e.State == EntityState.Detached ||
                                              e.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                var entity = (IEntity<string>)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedBy = "application";
                    entity.Status = (byte)EntityStatus.Active;
                    entity.CreatedDate = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entity.ModifiedBy = "application";
                    entity.ModifiedDate = DateTime.UtcNow;
                }
            }
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}

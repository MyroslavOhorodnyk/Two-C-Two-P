using System;
using Two_C_Two_P.Core.Interfaces;
using Two_C_Two_P.Core.Interfaces.Repositories;
using Two_C_Two_P.Infrastructure.Models;
using Two_C_Two_P.Infrastructure.Repositories;

namespace Two_C_Two_P.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwoCTwoPContext _dbContext;

        private ITransactionRepository _transactionRepository;

        public UnitOfWork(TwoCTwoPContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransactionRepository TransactionRepository
        {
            get { return _transactionRepository = _transactionRepository ?? new TransactionRepository(_dbContext); }
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

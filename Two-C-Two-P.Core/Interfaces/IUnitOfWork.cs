using System;
using System.Threading.Tasks;
using Two_C_Two_P.Core.Interfaces.Repositories;

namespace Two_C_Two_P.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }

        Task CommitAsync(bool useTransaction = false);

        Task RollbackAsync();
    }
}

using System;
using Two_C_Two_P.Core.Entities;

namespace Two_C_Two_P.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IAsyncRepository<Transaction, string>
    {
    }
}

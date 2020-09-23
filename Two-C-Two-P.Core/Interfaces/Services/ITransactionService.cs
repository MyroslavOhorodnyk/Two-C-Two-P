using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Two_C_Two_P.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TResult>> GetAsync<TResult>();

        Task<TResult> GetAsync<TResult>(Guid id);
    }
}

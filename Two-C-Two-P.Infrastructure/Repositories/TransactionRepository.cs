using System;
using Two_C_Two_P.Core.Entities;
using Two_C_Two_P.Core.Interfaces.Repositories;
using Two_C_Two_P.Infrastructure.Models;

namespace Two_C_Two_P.Infrastructure.Repositories
{
    public class TransactionRepository : EfRepository<Transaction, string>, ITransactionRepository
    {
        public TransactionRepository(TwoCTwoPContext context) : base(context)
        { }
    }
}

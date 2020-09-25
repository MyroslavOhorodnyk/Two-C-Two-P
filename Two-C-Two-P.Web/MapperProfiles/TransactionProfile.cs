using AutoMapper;
using Two_C_Two_P.Core.Entities;
using Two_C_Two_P.Web.DTOs.Transactions;

namespace Two_C_Two_P.Web.MapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, GetTransactionDto>();
        }
    }
}

using System;

namespace Two_C_Two_P.Web.DTOs.Transactions
{
    public class GetTransactionDto : BaseTransactionDto
    {
        public Guid Id { get; set; }
    }
}

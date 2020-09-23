using System;

namespace Two_C_Two_P.Web.DTOs.Transactions
{
    public class BaseTransactionDto
    {
        public decimal Ammount { get; set; }
        public string Currency { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}

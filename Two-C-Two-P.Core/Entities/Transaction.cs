using System;

namespace Two_C_Two_P.Core.Entities
{
    public class Transaction : EntityBase<Guid>
    {
        public decimal Ammount { get; set; }
        public string Currency { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}

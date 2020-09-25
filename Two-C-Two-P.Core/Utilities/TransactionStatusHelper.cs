using System.Collections.Generic;
using Two_C_Two_P.Core.Enums;

namespace Two_C_Two_P.Core.Utilities
{
    public class TransactionStatusHelper
    {
        static TransactionStatusHelper()
        {
            TransactionStatusMap = new Dictionary<string, TransactionStatus>
            {
                { "Approved", TransactionStatus.Approved },
                { "Failed", TransactionStatus.Rejected },
                { "Rejected", TransactionStatus.Rejected },
                { "Finished", TransactionStatus.Finished },
                { "Done", TransactionStatus.Finished }
            };
        }

        public static Dictionary<string, TransactionStatus> TransactionStatusMap { get; }
    }
}

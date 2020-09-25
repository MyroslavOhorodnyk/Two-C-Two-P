using System.Xml.Serialization;

namespace Two_C_Two_P.Infrastructure.FileReaders.Xml
{
    [XmlType(TypeName = "Transaction")]
    public class XmlTransactionObject
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        public string TransactionDate { get; set; }

        public string Status { get; set; }

        public PaymentDetails PaymentDetails { get; set; }
    }

    public class PaymentDetails
    {
        public string Amount { get; set; }

        public string CurrencyCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Two_C_Two_P.Core.Entities;
using Two_C_Two_P.Core.Enums;
using Two_C_Two_P.Core.Interfaces.FileReaders;
using Two_C_Two_P.Core.Models;
using Two_C_Two_P.Core.Utilities;

namespace Two_C_Two_P.Infrastructure.FileReaders.Xml
{
    public class XmlFileReader : IFileReader
    {
        public ParseResult ReadTransactions(Stream stream)
        {
            var result = new ParseResult();

            XmlSerializer xs = new XmlSerializer(typeof(List<XmlTransactionObject>), new XmlRootAttribute("Transactions"));

            var xmlTransactionObjects = new List<XmlTransactionObject>();

            using (StreamReader reader = new StreamReader(stream))
            {
                try
                {
                    xmlTransactionObjects = (List<XmlTransactionObject>)xs.Deserialize(reader);
                }
                catch (InvalidOperationException ex)
                {
                    result.Errors.Add($"Failed to Upload Xml document with Exception: {ex.Message}");
                    result.IsSucceeded = false;
                    return result;
                }
            }

            var objectNum = 0;
            foreach (var xmlTransaction in xmlTransactionObjects)
            {
                objectNum++;

                ParseTransaction(xmlTransaction, objectNum, result);
            }

            result.IsSucceeded = !result.Errors.Any();

            return result;
        }

        private void ParseTransaction(XmlTransactionObject xmlObject, int objectPosition, ParseResult parseResult)
        {
            bool isValid = true;

            var transaction = new Transaction();

            if (string.IsNullOrWhiteSpace(xmlObject.Id))
            {
                parseResult.Errors.Add($"Xml Transaction object № {objectPosition} has invalid or empty Id");
                isValid = false;
            }
            else
            {
                transaction.Id = xmlObject.Id;
            }

            var provider = new CultureInfo("en-GB");
            var style = NumberStyles.Number;
            if (Decimal.TryParse(xmlObject.PaymentDetails.Amount, style, provider, out decimal result))
            {
                transaction.Ammount = result;
            }
            else
            {
                parseResult.Errors.Add($"Unable to parse Amount '{xmlObject.PaymentDetails.Amount}' for Transaction №: '{objectPosition}' with Id: '{xmlObject.Id}' .");
                isValid = false;
            }

            string pattern = "[A-Z]{3}";
            Match m = Regex.Match(xmlObject.PaymentDetails.CurrencyCode, pattern);
            if (m.Success)
            {
                transaction.Currency = xmlObject.PaymentDetails.CurrencyCode;
            }
            else
            {
                parseResult.Errors.Add($"Incorect Currency format: '{xmlObject.PaymentDetails.CurrencyCode}' for Transaction №: '{objectPosition}' with Id: '{xmlObject.Id}' .");
                isValid = false;
            }

            if (DateTime.TryParse(xmlObject.TransactionDate, out DateTime date))
            {
                transaction.TransactionDate = date;
            }
            else
            {
                parseResult.Errors.Add($"Incorrect Date format: '{xmlObject.TransactionDate}' for Transaction №: '{objectPosition}' with Id '{xmlObject.Id}' .");
                isValid = false;
            }

            if (TransactionStatusHelper.TransactionStatusMap.TryGetValue(xmlObject.Status, out TransactionStatus status))
            {
                transaction.TransactionStatus = status.ToString();
            }
            else
            {
                parseResult.Errors.Add($"Incorect Transaction Status: '{xmlObject.Status}' for Transaction №: '{objectPosition}' with Id '{xmlObject.Id}' .");
                isValid = false;
            }

            if (isValid)
            {
                transaction.CreatedBy = "XmlFileUppload";
                transaction.ModifiedBy = "XmlFileUppload";
                parseResult.TransactionData.Add(transaction);
            }
        }
    }
}

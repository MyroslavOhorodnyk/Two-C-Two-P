using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Two_C_Two_P.Core.Entities;
using Two_C_Two_P.Core.Enums;
using Two_C_Two_P.Core.Interfaces.FileReaders;
using Two_C_Two_P.Core.Models;
using Two_C_Two_P.Core.Utilities;

namespace Two_C_Two_P.Infrastructure.FileReaders.Csv
{
    public class CsvFileReader : IFileReader
    {
        public ParseResult ReadTransactions(Stream stream)
        {
            var result = new ParseResult();

            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    int lineNumber = 0;
                    while (!reader.EndOfStream)
                    {
                        lineNumber++;

                        var line = reader.ReadLine();
                        var values = SplitRow(line);

                        ParseTransaction(values, lineNumber, result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Failed to Upload Csv document with Exception: {ex.Message}");
            }

            result.IsSucceeded = !result.Errors.Any();

            return result;
        }

        private void ParseTransaction(string[] values, int lineNumber, ParseResult parseResult)
        {
            bool isValid = true;

            if (!values.Any() || (values.Length == 1 && string.IsNullOrEmpty(values[0])))
            {
                parseResult.Warnings.Add($"Empty Row Occured - Line №: '{lineNumber}' . Skipping.");
                return;
            }
            else if (values.Count() < 5)
            {
                parseResult.Errors.Add($"Row doesn't contain all mandatory fields - Line №: '{lineNumber}' ");
                return;
            }
            else if (values.Count() > 5)
            {
                parseResult.Warnings.Add($"Row contains more fields than is expected - Line №: '{lineNumber}' . Trying to parse it anyway.");
            }

            var transaction = new Transaction();

            transaction.Id = values[0];

            var provider = new CultureInfo("en-GB");
            var style = NumberStyles.Number;
            if (Decimal.TryParse(values[1], style, provider, out decimal result))
            {
                transaction.Amount = result;
            }
            else
            {
                parseResult.Errors.Add($"Unable to parse Amount '{values[1]}' for Transaction №: '{lineNumber}' with Id: '{transaction.Id}' .");
                isValid = false;
            }

            string pattern = "[A-Z]{3}";
            Match m = Regex.Match(values[2], pattern);
            if (m.Success)
            {
                transaction.Currency = values[2];
            }
            else
            {
                parseResult.Errors.Add($"Incorect Currency format: '{values[2]}' for Transaction №: '{lineNumber}' with Id: '{transaction.Id}' .");
                isValid = false;
            }

            if (DateTime.TryParse(values[3], out DateTime date))
            {
                transaction.TransactionDate = date;
            }
            else
            {
                parseResult.Errors.Add($"Incorrect Date format: '{values[3]}' for Transaction №: '{lineNumber}' with Id '{transaction.Id}' .");
                isValid = false;
            }

            if (TransactionStatusHelper.TransactionStatusMap.TryGetValue(values[4], out TransactionStatus status))
            {
                transaction.TransactionStatus = status.ToString();
            }
            else
            {
                parseResult.Errors.Add($"Incorect Transaction Status: '{values[4]}' for Transaction №: '{lineNumber}' with Id '{transaction.Id}' .");
                isValid = false;
            }

            if (isValid)
            {
                transaction.CreatedBy = "CsvFileUpload";
                transaction.ModifiedBy = "CsvFileUpload";
                parseResult.TransactionData.Add(transaction);
            }
        }

        private string[] SplitRow(string line)
        {
            string[] separators = { "\",", ",\"" };
            char[] quotes = { '\"', ' ' };

            return line.Split(separators, StringSplitOptions.None)
                                         .Select(s => s.Trim(quotes).Replace("\\\"", "\""))
                                         .ToArray();
        }
    }
}

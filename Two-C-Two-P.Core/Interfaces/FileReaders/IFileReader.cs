using System.IO;
using Two_C_Two_P.Core.Models;

namespace Two_C_Two_P.Core.Interfaces.FileReaders
{
    public interface IFileReader
    {
        ParseResult ReadTransactions(Stream stream);
    }
}

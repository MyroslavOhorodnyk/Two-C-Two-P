using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Two_C_Two_P.Core.Entities;
using Two_C_Two_P.Core.Interfaces.FileReaders;
using Two_C_Two_P.Core.Interfaces.Services;
using Two_C_Two_P.Core.Models;
using Two_C_Two_P.Infrastructure.FileReaders.Csv;
using Two_C_Two_P.Infrastructure.FileReaders.Xml;
using Two_C_Two_P.Web.DTOs.Transactions;
using Two_C_Two_P.Web.DTOs.Upload;

namespace Two_C_Two_P.Web.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly string[] _allowedFileExtensions = { ".csv", ".xml", };

        private ITransactionService _transactionService { get; set; }
        private IUploadService _uploadService { get; set; }

        public TransactionController(ITransactionService transactionService, IUploadService uploadService)
        {
            _transactionService = transactionService;
            _uploadService = uploadService;
        }

        public async Task<ActionResult<IEnumerable<Transaction>>> GetAsync()
        {
            var result = await _transactionService.GetAsync<GetTransactionDto>();

            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<ActionResult<UploadResultDto>> UploadAsync([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("The specified file is null or empty.", nameof(file));
            }

            if (!_allowedFileExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                throw new ArgumentException("The specified file has invalid extension.", nameof(file));
            }

            byte[] bytes;
            await using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                bytes = memoryStream.ToArray();
            }

            IFileReader fileReader;
            if (Path.GetExtension(file.FileName) == ".csv")
            {
                fileReader = new CsvFileReader();
            }
            else
            {
                fileReader = new XmlFileReader();
            }

            var parseResult = await _uploadService.UploadAsync<ParseResult>(bytes, fileReader);

            var result = new UploadResultDto();
            if (parseResult.IsSucceeded)
            {
                result.Info.Add($"Uploaded {parseResult.TransactionData.Count()} Transactions");
            }
            else
            {
                result.Errors = parseResult.Errors.ToList();
            }

            result.Warnings = parseResult.Warnings.ToList();

            return Ok(result);
        }
    }
}

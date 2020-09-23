using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Two_C_Two_P.Core.Entities;
using Two_C_Two_P.Core.Interfaces.Services;
using Two_C_Two_P.Web.DTOs.Transactions;

namespace Two_C_Two_P.Web.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : Controller
    {
        private ITransactionService _transactionService { get; set; }

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<ActionResult<IEnumerable<Transaction>>> GetAsync()
        {
            var result = await _transactionService.GetAsync<GetTransactionDto>();

            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadAsync([FromForm] IFormFile file)
        {

            return Ok();
        }
    }
}

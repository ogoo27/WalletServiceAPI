using AutoMapper;
using E_WalletAPI.Models;
using E_WalletAPI.Models.DTO;
using E_WalletAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace E_WalletAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionServices _transactionServices;
        IMapper _mapper;

        public TransactionController(ITransactionServices transactionServices, IMapper mapper)
        {
            _transactionServices = transactionServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Creat_New_Transaction")]

        public IActionResult CreateNewTransaction([FromBody] TransactionRequestDTO transactionRequestDTO)
        {
            if(!ModelState.IsValid) return BadRequest(transactionRequestDTO);
            var transaction = _mapper.Map<TransactionModel>(transactionRequestDTO);
            return Ok(_transactionServices.CreateTransaction(transaction));
        }

        [HttpPost]
        [Route("Make_Deposit")]
        
        public IActionResult MakeDeposit(string WalletId, decimal Amount, string TransactionPin)
        {
            //if (!Regex.IsMatch(WalletId, @"^[0]$")) return BadRequest("Invalid Account number");
            return Ok(_transactionServices.MakeDeposit(WalletId, Amount, TransactionPin));
        }

        [HttpPost]
        [Route("make_withdrawal")]

        public IActionResult MakeWithdrawal(string WalletId, decimal Amount, string TransactionPin)
        {
            //if (!Regex.IsMatch(WalletId, @"^[0]$")) return BadRequest("Invalid Account number");
            return Ok(_transactionServices.MakeWithdrawal(WalletId, Amount, TransactionPin));
        }

        [HttpPost]
        [Route("make_fund_Transfer")]
        
        public IActionResult MakeFubdTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            //if (!Regex.IsMatch(WalletId, @"^[0]$")) return BadRequest("Invalid Account number");
            return Ok(_transactionServices.MakeFubdTransfer(FromAccount, ToAccount, Amount, TransactionPin));
        }

    }
}

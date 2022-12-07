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
    public class AccountsController : ControllerBase
    {
        private IAccountServices _accountServices;

        IMapper _mapper;

        public AccountsController(IAccountServices accountServices, IMapper mapper)
        {
            _accountServices = accountServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register_new_Wallet")]
        public IActionResult RegisterNewWallet([FromBody] RegisterNewAccountModel newAccount)
        {
            if(!ModelState.IsValid) return BadRequest(newAccount);
            var account = _mapper.Map<AccountModel>(newAccount);
            return Ok(_accountServices.Create(account, newAccount.Password, newAccount.ConfirmPassword));
        }

        [HttpGet]
        [Route("Get_all_Wallets")]
        public IActionResult GetAllWallets()
        {
            var accounts = _accountServices.GetAllAccounts();
            var cleanedAccounts = _mapper.Map<List<GetAccountModel>>(accounts);
            return Ok(cleanedAccounts);
        }


        [HttpPost]
        [Route("")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            //Lets map
            if(!ModelState.IsValid) return BadRequest(model);
            return Ok(_accountServices.Authenticate(model.AccountNumber, model.Password));
        }

        [HttpGet]
        [Route("Get_by_Wallet_Id")]
        public IActionResult GetByAccountNumber(string WalletId)
        {
            //if (!Regex.IsMatch(WalletId, @"^[0][1-9]/d{9}$|^[1-9]\d{9}$")) return BadRequest("Account number must be 10 Digits");
            var account = _accountServices.GetByWalletID(WalletId);
            var cleanedAccount = _mapper.Map<AccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpGet]
        [Route("Get_by_Wallet_Email")]
        public IActionResult GetByEmail(string Email)
        {
            var account = _accountServices.GetByEmail(Email);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }
        
        [HttpPut]
        [Route("Update_account")]
        public IActionResult UpdateWallet([FromBody] UpdateAccountModel model, string Password)
        {           
            if(!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<AccountModel>(model);
            _accountServices.Update(account, model.Password);
            return Ok();
        }
    }
}

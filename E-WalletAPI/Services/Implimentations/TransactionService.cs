
using E_WalletAPI.Data;
using E_WalletAPI.Models;
using E_WalletAPI.Services.Interfaces;
using E_WalletAPI.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Transactions;

namespace E_WalletAPI.Services.Implimentations
{
    public class TransactionService : ITransactionServices
    {
        private readonly E_walletDBContext _dbContext;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private static string _ourBankSettlementAccount;
        private readonly IAccountServices _accountServices;

        public TransactionService(E_walletDBContext dbContext, ILogger<TransactionService> logger, IOptions<AppSettings> settings, IAccountServices accountServices)
        {
            _dbContext = dbContext;
            _logger = logger;
            _settings = settings.Value;
            _ourBankSettlementAccount = _settings.OurBankSettlementAccount;
            _accountServices = accountServices;
            _accountServices = accountServices;
        }

        public Response CreateTransaction(TransactionModel transaction)
        {
            Response response = new Response();
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created Successfully";
            response.Data = null;
            return response;
        }

        public Response FindTransactionByDate(DateTime date)
        {
            Response response = new Response();
            var transaction = _dbContext.Transactions.Where(x => x.TransactionDate == date).ToList();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created Successfully";
            response.Data = transaction;
            return response;
        }

        public Response MakeDeposit(string WalletId, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            AccountModel sourceAccount;
            AccountModel destinationAccount;
            TransactionModel transaction = new TransactionModel();
            var authUser = _accountServices.Authenticate(WalletId, TransactionPin);
            if (authUser != null) throw new ApplicationException("Invalid Credentials");

            try
            {
                sourceAccount = _accountServices.GetByWalletID(_ourBankSettlementAccount);
                destinationAccount = _accountServices.GetByWalletID(WalletId);

                sourceAccount.AccountBalance -= Amount;
                destinationAccount.AccountBalance += Amount;

                if((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified && (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified)))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successful";
                    response.Data = null;

                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occured... => {ex.Message}");
            }

            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _ourBankSettlementAccount;
            transaction.TransactionDestinationAccount = WalletId;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"New transaction from => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} To Destination Account => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} on Date {transaction.TransactionDate} For Amount => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionAmount)} Transaction Type => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionType)} Transaction Status {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }

        public Response MakeFubdTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            AccountModel sourceAccount;
            AccountModel destinationAccount;
            TransactionModel transaction = new TransactionModel();
            var authUser = _accountServices.Authenticate(FromAccount, TransactionPin);
            if (authUser != null) throw new ApplicationException("Invalid Credentials");

            try
            {
                sourceAccount = _accountServices.GetByWalletID(FromAccount);
                destinationAccount = _accountServices.GetByWalletID(ToAccount);

                sourceAccount.AccountBalance -= Amount;
                destinationAccount.AccountBalance += Amount;

                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified && (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified)))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successful";
                    response.Data = null;

                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occured... => {ex.Message}");
            }

            transaction.TransactionType = TranType.Transfer;
            transaction.TransactionSourceAccount = FromAccount;
            transaction.TransactionDestinationAccount = ToAccount;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"New transaction from => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} To Destination Account => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} on Date {transaction.TransactionDate} For Amount => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionAmount)} Transaction Type => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionType)} Transaction Status {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }

        public Response MakeWithdrawal(string WalletId, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            AccountModel sourceAccount;
            AccountModel destinationAccount;
            TransactionModel transaction = new TransactionModel();
            var authUser = _accountServices.Authenticate(WalletId, TransactionPin);
            if (authUser != null) throw new ApplicationException("Invalid Credentials");

            try
            {
                sourceAccount = _accountServices.GetByWalletID(_ourBankSettlementAccount);
                destinationAccount = _accountServices.GetByWalletID(WalletId);

                //sourceAccount.AccountBalance -= Amount;
                destinationAccount.AccountBalance -= Amount;

                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified && (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified)))
                {
                    transaction.TransactionStatus = TranStatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction Successful";
                    response.Data = null;

                }
                else
                {
                    transaction.TransactionStatus = TranStatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"An error occured... => {ex.Message}");
            }

            transaction.TransactionType = TranType.Withdrawal;
            transaction.TransactionSourceAccount = WalletId;
            transaction.TransactionDestinationAccount = _ourBankSettlementAccount;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"New transaction from => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} To Destination Account => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} on Date {transaction.TransactionDate} For Amount => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionAmount)} Transaction Type => " +
                $"{JsonConvert.SerializeObject(transaction.TransactionType)} Transaction Status {JsonConvert.SerializeObject(transaction.TransactionStatus)}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }

        //public Response CreateTransaction(Transaction transaction)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

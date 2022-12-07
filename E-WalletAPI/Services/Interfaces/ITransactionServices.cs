using E_WalletAPI.Models;
using System.Transactions;

namespace E_WalletAPI.Services.Interfaces
{
    public interface ITransactionServices
    {
        Response CreateTransaction(TransactionModel transaction);
        Response FindTransactionByDate(DateTime date);

        Response MakeDeposit(string WalletId, decimal Amount, string TransactionPin);

        Response MakeWithdrawal(string WalletId, decimal Amount, string TransactionPin);

        Response MakeFubdTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin);

    }
}

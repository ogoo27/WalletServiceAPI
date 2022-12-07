using E_WalletAPI.Models;
using System.Collections.Concurrent;

namespace E_WalletAPI.Services.Interfaces
{
    public interface IAccountServices
    {
        AccountModel Authenticate(string WalletId, string Password);

        IEnumerable<AccountModel> GetAllAccounts();

        AccountModel Create(AccountModel accountModel, string Password, string ConfirmPassword);

        void Update(AccountModel accountModel, string Password = null);

        void Delete(int id);

        AccountModel GetByEmail(string Email);

        AccountModel GetByWalletID(string WalletId);
    }
}

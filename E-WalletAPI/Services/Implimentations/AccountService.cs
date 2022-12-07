using E_WalletAPI.Data;
using E_WalletAPI.Models;
using E_WalletAPI.Services.Interfaces;
using System.Text;


namespace E_WalletAPI.Services.Implimentations
{
    public class AccountService : IAccountServices
    {
        private E_walletDBContext _dbContext;

        public AccountService(E_walletDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AccountModel Authenticate(string WalletId, string Password)
        {
            //Checking if account Ecistes 
            var account = _dbContext.Accounts.Where(x => x.WalletIdGenerated == WalletId).SingleOrDefault();
            if (account == null)
                return null;

            //Verifying PinHash.
            if (!VerifyPasswordHash(Password, account.PasswordHash, account.PassWordSalt))
                return null;

            return account; 
        }



        private static bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Password");

            //Verifying password
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computePasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < computePasswordHash.Length; i++)
                {
                    if(computePasswordHash[i] != PasswordHash[i]) return false;
                }
            }
            return true;
            
        }

        public AccountModel Create(AccountModel accountModel, string Password, string ConfirmPassword)
        {
            //This is where we will create account
            if(_dbContext.Accounts.Any(x => x.Email == accountModel.Email)) throw new ApplicationException("user with Same Email already Registered");

            //Validate pin
            if (!Password.Equals(ConfirmPassword)) throw new ArgumentException("Password did not match", "Password");

            //Creating account is all validation passess
            byte[] PasswordHash, PasswordSalt;            
            CreatePasswordHash(Password, out PasswordSalt, out PasswordHash);

            accountModel.PasswordHash = PasswordHash;
            accountModel.PassWordSalt = PasswordSalt;

            //adding new account
            _dbContext.Accounts.Add(accountModel);
            _dbContext.SaveChanges();

            return accountModel;

        }

        public static void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }

        public void Delete(int Id)
        {
            var account = _dbContext.Accounts.Find(Id);
            if(account != null)
            {
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
        }


        public IEnumerable<AccountModel> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }


        public AccountModel GetById(Guid Id)
        {
            var account = _dbContext.Accounts.Where(x => x.WalletID == Id).FirstOrDefault();
            if (account != null) return null;
            return account;       
        }

        //Getting 
        public AccountModel GetByWalletID(string WalletId)
        {
            var account = _dbContext.Accounts.Where(x => x.WalletIdGenerated == WalletId).SingleOrDefault();
            if(account == null)
            {
                return null;
            }

            return account;
        }


        public void Update(AccountModel accountModel, string Password = null)
        {
            var accountToBeUpdated = _dbContext.Accounts.Where(x => x.Email == accountModel.Email).SingleOrDefault();
            if (accountToBeUpdated == null) throw new ApplicationException("Account does not exist");

            //Changing of Email. 
            if (!string.IsNullOrWhiteSpace(accountModel.Email))
            {
                if (_dbContext.Accounts.Any(x => x.Email == accountModel.Email)) throw new ApplicationException("This Email" + accountModel.Email + "already exists");

                accountToBeUpdated.Email = accountModel.Email;
            }

            //Changing of Phone number

            if (!string.IsNullOrWhiteSpace(accountModel.PhoneNumber))
            {
                if (_dbContext.Accounts.Any(x => x.Email == accountModel.Email)) throw new ApplicationException("This Phone Number" + accountModel.PhoneNumber + "already exists");

                accountToBeUpdated.PhoneNumber = accountModel.PhoneNumber;
            }


            //Chnaging of Passord
            if (!string.IsNullOrWhiteSpace(Password))
            {
                byte[] PasswordHash, PasswordSalt;
                CreatePasswordHash(Password, out PasswordHash, out PasswordSalt);
                accountToBeUpdated.PasswordHash = PasswordHash;
                accountToBeUpdated.PassWordSalt = PasswordSalt;              
                
            }
            accountToBeUpdated.DateLastUpdated = DateTime.Now;

            _dbContext.Accounts.Update(accountToBeUpdated);
            _dbContext.SaveChanges();
        }



        public AccountModel GetByEmail(string Email)
        {
            var account = _dbContext.Accounts.Where(x => x.Email == Email).FirstOrDefault();
            if (account != null)
            {
                return null;
            }

            return account;
        }
    }
}

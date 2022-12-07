using E_WalletAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_WalletAPI.Data
{
    public class E_walletDBContext : DbContext
    {
        public E_walletDBContext(DbContextOptions<E_walletDBContext> options) : base(options)
        {

        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
    }
}

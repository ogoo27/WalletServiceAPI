using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_WalletAPI.Models
{
    [Table("Accounts")]
    public class AccountModel
    {
        [Key]
        public Guid WalletID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string AccountName { get; set; }

        public string WalletIdGenerated { get; set; } = Guid.NewGuid().ToString();
        
        public String PhoneNumber { get; set; }

        public string Email { get; set; }
        public decimal AccountBalance { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PassWordSalt { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastUpdated { get; set; }


        public AccountModel()
        {
            AccountName = $"{FirstName} {LastName}";
            WalletID = Guid.NewGuid();
        }

       


    }
}

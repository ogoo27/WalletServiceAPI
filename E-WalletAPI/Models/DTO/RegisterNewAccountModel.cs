using System.ComponentModel.DataAnnotations;

namespace E_WalletAPI.Models.DTO
{
    public class RegisterNewAccountModel
    {
       
        public string FirstName { get; set; }

        public string LastName { get; set; }
        //public string AccountName { get; set; }

        //public string WalletIdGenerated { get; set; }

        public String PhoneNumber { get; set; }

        public string Email { get; set; }
        //public decimal AccountBalance { get; set; }

        //public byte[] PasswordHash { get; set; }

        
        public DateTime DateCreated { get; set; }

        public DateTime DateLastUpdated { get; set; }

        [Required]
        //[RegularExpression(@"^{0-9}/d{4}$", ErrorMessage = "Password Must be more than 4 Digits")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }

    }
}

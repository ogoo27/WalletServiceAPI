using System.ComponentModel.DataAnnotations;

namespace E_WalletAPI.Models.DTO
{
    public class UpdateAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String PhoneNumber { get; set; }

        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^{0-9}/d{4}$", ErrorMessage = "Password Must be more than 4 Digits")]
        public string Password { get; set; }

    }
        
}

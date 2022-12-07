using System.ComponentModel.DataAnnotations;

namespace E_WalletAPI.Models.DTO
{
    public class AuthenticateModel
    {
        [Required]
        [RegularExpression(@"^[0][1-9]/d{9}$|^[1-9]\d{9}$")]
        public string AccountNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

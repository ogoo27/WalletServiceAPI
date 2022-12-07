using System.ComponentModel.DataAnnotations;

namespace E_WalletAPI.Models.DTO
{
    public class GetAccountModel
    {
        [Key]
        public Guid WalletID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string AccountName { get; set; }

        public string WalletIdGenerated { get; set; }

        public String PhoneNumber { get; set; }

        public string Email { get; set; }
        public decimal AccountBalance { get; set; }      

        public DateTime DateCreated { get; set; }

        public DateTime DateLastUpdated { get; set; }

    }
}

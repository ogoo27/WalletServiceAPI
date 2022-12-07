namespace E_WalletAPI.Models.DTO
{
    public class TransactionRequestDTO
    {
        public decimal TransactionAmount { get; set; }
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public TranType TransactionType { get; set; }
        public string TransactionParticulars { get; set; }
        public DateTime TransactionDate { get; set; }


    }
}

namespace Admin.Models
{
    public class TransactionDetailsModel : BaseModel
    {
        public TransactionModel Transaction { get; set; }
        
        public decimal AmountToCapture { get; set; }
        public decimal AmountToReverse { get; set; }
    }
}
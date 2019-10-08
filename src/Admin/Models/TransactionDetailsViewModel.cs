namespace Admin.Models
{
    public class TransactionDetailsViewModel : BaseViewModel
    {
        public TransactionViewModel Transaction { get; set; }
        
        public decimal AmountToCapture { get; set; }
        public decimal AmountToReverse { get; set; }
    }
}
using System;

namespace Admin.Models
{
    public class TransactionModel : BaseModel
    {
        public int Id { get; set; }
        public int Step { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal CapturedAmount { get; set; }
        public PartyModel Payer { get; set; }
        public PartyModel Payee { get; set; }
        public bool IsFraud { get; set; }
        public bool IsFlaggedFraud { get; set; }
        public bool HasCapture { get; set; }
        public bool CanCapture { get; set; }
        public bool CanReverse { get; set; }
    }
}
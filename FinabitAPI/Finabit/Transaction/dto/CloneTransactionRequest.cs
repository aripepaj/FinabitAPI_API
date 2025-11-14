namespace FinabitAPI.Finabit.Transaction.dto
{
    public class CloneTransactionRequest
    {
        public int TransactionId { get; set; }
        public DateTime NewDate { get; set; }
        public string? Memo { get; set; }
        public bool? IncludePOSDetails { get; set; }
        public bool? ResetPaymentIDToZero { get; set; }
    }
}

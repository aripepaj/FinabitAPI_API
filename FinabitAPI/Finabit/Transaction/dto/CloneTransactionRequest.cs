namespace FinabitAPI.Finabit.Transaction.dto
{
    public sealed class CloneTransactionRequest
    {
        public int TransactionId { get; set; }
        public DateTime NewDate { get; set; } 
    }
}

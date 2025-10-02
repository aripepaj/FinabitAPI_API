namespace FinabitAPI.Models
{
    public sealed class CloneTransactionRequest
    {
        public int TransactionId { get; set; }
        public DateTime NewDate { get; set; } 
    }
}

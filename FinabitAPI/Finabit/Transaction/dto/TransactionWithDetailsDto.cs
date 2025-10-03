namespace FinabitAPI.Finabit.Transaction.dto
{
    public sealed class TransactionWithDetailsDto
    {
        public Transactions Header { get; set; }
        public List<TransactionDetail> Details { get; set; } = new();
    }
}

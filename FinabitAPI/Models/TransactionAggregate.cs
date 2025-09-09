namespace FinabitAPI.Models
{
    public class TransactionAggregate
    {
        public DateTime Data { get; set; }
        public decimal Value { get; set; }
        public int Rows { get; set; }
    }
}

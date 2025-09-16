namespace FinabitAPI.Models
{
    public class TransactionAggregate
    {
        public DateTime Data { get; set; }
        public string LocationName { get; set; }
        public decimal Value { get; set; }
        public decimal ValueWithoutVat { get; set; }
        public decimal CostValue { get; set; }
        public int Rows { get; set; }
    }
}

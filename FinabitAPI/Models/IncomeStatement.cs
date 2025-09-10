namespace FinabitAPI.Models
{
    public class IncomeStatement
    {
        public string Account { get; set; }
        public string AccountGroup { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}

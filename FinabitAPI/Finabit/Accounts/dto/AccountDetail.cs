namespace FinabitAPI.Finabit.Account.dto
{
    public class AccountDetail
    {
        public string? TransactionType { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Number { get; set; }
        public string? Name { get; set; }
        public string? Account { get; set; }
        public decimal DebitValue { get; set; }
        public decimal CreditValue { get; set; }
    }
}
namespace FinabitAPI.Finabit.Transaction.dto
{
    public class TransactionDetailDistinctDto
    {
        public int ID { get; set; }
        public int TransactionID { get; set; }
        public int DetailsType { get; set; }
        public string ItemID { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Account { get; set; } = "";
        public decimal Amount { get; set; }
        public string TransactionNo { get; set; } = "";
        public DateTime TransactionDate { get; set; }
        public int? PartnerID { get; set; }
        public string Memo { get; set; } = "";
    }
}

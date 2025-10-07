namespace FinabitAPI.BankJournal.dto
{
    public class JournalLineRequestDto
    {
        public int DepartmentID { get; set; }
        public string CashAccount { get; set; } = "";
        public DateTime? Date { get; set; }
        public int DetailsType { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public int? PartnerID { get; set; }
        public string? PartnerCode { get; set; }
        public string? AccountCode { get; set; }
        public int? PaymentID { get; set; }

        public string? TransactionNo { get; set; }
        public string? InvoiceNo { get; set; }
    }

}
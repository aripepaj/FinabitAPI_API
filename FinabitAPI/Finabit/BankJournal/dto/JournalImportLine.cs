namespace FinabitAPI.dto
{
    public class JournalImportLine
    {
        public string CashAccount { get; set; } = "";
        public int DetailsType { get; set; }   // 3,4,5,6
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public int? PartnerID { get; set; }
        public string? PartnerCode { get; set; }   // shifra furnitor/konsumator
        public string? AccountCode { get; set; }   // shifra llogari for 5/6
        public int? PaymentID { get; set; }
    }

    public class JournalImportRequest
    {
        public int DepartmentID { get; set; }
        public DateTime? Date { get; set; }
        public List<JournalImportLine> Lines { get; set; } = new();
    }

    public class JournalImportResponse
    {
        public bool Ok { get; set; }
        public List<JournalImportGroupResult> Results { get; set; } = new();
    }

    public class JournalImportGroupResult
    {
        public string Date { get; set; } = "";
        public string CashAccount { get; set; } = "";
        public int HeaderID { get; set; }
        public int InsertedLines { get; set; }
        public string Status { get; set; } = "ok"; // ok|error
        public string? Error { get; set; }
    }
}

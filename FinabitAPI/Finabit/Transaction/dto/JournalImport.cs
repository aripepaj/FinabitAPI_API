namespace FinabitAPI.Finabit.Transaction.dto
{
    public sealed class JournalImportLineDto
    {
        public DateTime Date { get; set; }                 
        public string CashAccount { get; set; } = "";      
        public int DetailsType { get; set; }              
        public decimal Amount { get; set; }                
        public string? Description { get; set; }          
        public int? PaymentID { get; set; }               
    }

    public sealed class JournalImportRequest
    {
        public int DepartmentID { get; set; }              
        public int JournalTypeID { get; set; } = 25;      
        public List<JournalImportLineDto> Lines { get; set; } = new();
    }

    public sealed class JournalImportGroupResult
    {
        public string Date { get; set; } = "";             
        public string CashAccount { get; set; } = "";
        public int HeaderID { get; set; }
        public int InsertedLines { get; set; }
        public string Status { get; set; } = "ok";        
        public string? Error { get; set; }
    }

    public sealed class JournalImportResponse
    {
        public bool Ok { get; set; }
        public List<JournalImportGroupResult> Results { get; set; } = new();
    }

    public sealed class CashAddLikePOSRequest
    {
        public int DepartmentID { get; set; }           // required
        public string? Reference { get; set; }          // "1" paradite | "2" pasdite | null -> fallback
        public DateTime? Date { get; set; }             // if null -> today; used for header date
        public int JournalTypeID { get; set; } = 25;    // 25=Arka, 24=Bank
        public string? CashAccount { get; set; }        // required for Bank; optional for Arka (falls back to employee/Options)
        public int DetailsType { get; set; }            // 3,4,5,6 (3/6 = out; 4/5 = in)
        public decimal Amount { get; set; }             // absolute; server applies +/- by DetailsType
        public string? Description { get; set; }        // goes to ItemID/ItemName
        public int? PaymentID { get; set; }             // optional link to existing transaction
    }

}

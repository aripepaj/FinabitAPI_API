using FinabitAPI.dto;

namespace FinabitAPI.BankJournal.dto
{
    public class MixedImportRequest
    {
        public int DepartmentID { get; set; }
        public int JournalTypeID { get; set; } // 24=Bank, 25=Cash
        public DateTime? Date { get; set; }
        public List<JournalImportLine> Lines { get; set; } = new();
    }
}
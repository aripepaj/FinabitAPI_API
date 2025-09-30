namespace FinabitAPI.Models
{
    public sealed class ItemFoundDetails
    {
        public int ID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public int? DepartmentID { get; set; }
        public string MatchedBarcode { get; set; }
    }

    public sealed class ItemExistenceResponse
    {
        public bool Exists { get; set; }
        public string ItemID { get; set; }       // echo request
        public string Name { get; set; }         // echo request
        public string Barcode { get; set; }      // echo request

        public int FoundID { get; set; }         // legacy convenience
        public ItemFoundDetails Details { get; set; } // null unless returnDetails=true
    }
}

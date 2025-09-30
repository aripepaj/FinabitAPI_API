namespace FinabitAPI.Models
{
    public sealed class ItemExistenceProbe
    {
        public int Index { get; set; } // required for round-trip ordering
        public string ItemID { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
    }

    public sealed class ItemExistenceBatchResponse
    {
        public int Index { get; set; }
        public string ItemID { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }

        public bool Exists { get; set; }
        public int FoundID { get; set; }
        public ItemFoundDetails Details { get; set; } 
    }

}

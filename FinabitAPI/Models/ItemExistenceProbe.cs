namespace FinabitAPI.Models
{
    public sealed class ItemExistenceProbe
    {
        public int Index { get; set; }            // to preserve caller order
        public string? ItemID { get; set; }
        public string? Name { get; set; }
        public string? Barcode { get; set; }
    }

    public sealed class ItemExistenceBatchResponse
    {
        public int Index { get; set; }
        public string? ItemID { get; set; }
        public string? Name { get; set; }
        public string? Barcode { get; set; }
        public bool Exists { get; set; }
        public int FoundID { get; set; }          // 0 if not found
    }

}

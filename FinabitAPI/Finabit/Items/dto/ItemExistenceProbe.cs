namespace FinabitAPI.Finabit.Items.dto
{
    // Models/ItemExistenceProbe.cs
    public sealed class ItemExistenceProbe
    {
        public int Index { get; set; }
        public string? ItemID { get; set; }   // nullable, NO [Required]
        public string? Name { get; set; }     // nullable
        public string? Barcode { get; set; }  // nullable
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

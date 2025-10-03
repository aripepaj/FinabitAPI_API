namespace FinabitAPI.Finabit.Items.dto
{
    public class ItemsFilter
    {
        public string? ItemName { get; set; }
        public string? ItemID { get; set; }
        public int? ItemGroupID { get; set; }
        public string? ItemGroup { get; set; }
        public string? Barcode { get; set; }
        public string? Category { get; set; }
        public string? Prodhuesi { get; set; }
        public bool? Active { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}

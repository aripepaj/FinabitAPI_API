namespace FinabitAPI.Finabit.Items.dto
{
    // Request DTO for creating a new item via API
    public class CreateItemRequest
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal VATValue { get; set; } = 18;
        public bool Taxable { get; set; } = true;
        public int DepartmentID { get; set; }
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public int ItemGroupID { get; set; }
        public string ItemGroup { get; set; }
        public string Barcode1 { get; set; }
    }

    // Response DTO for item creation
    public class CreateItemResponse
    {
        public string ItemID { get; set; }
        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
    }
}

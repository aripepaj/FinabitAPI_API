namespace FinabitAPI.Core.Global.dto
{
    public sealed class DistinctItemNameDto
    {
        public int DetailsType { get; set; }     // 1=item, 2=account (based on your proc)
        public string ItemID { get; set; }       // td.ItemID
        public string Description { get; set; }  // td.ItemName (raw description on the transaction)
        public string ItemName { get; set; }     // i.ItemName OR a.AccountDescription
        public decimal VatValue { get; set; }
    }
}

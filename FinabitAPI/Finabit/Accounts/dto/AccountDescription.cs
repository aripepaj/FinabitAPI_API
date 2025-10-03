namespace FinabitAPI.Finabit.Account.dto
{
    public sealed class AccountMatchDto
    {
        public int DetailsType { get; set; }
        public string ItemID { get; set; } = string.Empty;     
        public string Description { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;   
    }
}
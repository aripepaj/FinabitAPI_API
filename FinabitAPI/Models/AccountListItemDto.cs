namespace FinabitAPI.Models
{
    public sealed class AccountListItemDto
    {
        public string Account { get; set; } = "";
        public string AccountDescription { get; set; } = "";
        public string AccountDisplay { get; set; } = "";
        public int AccountSubGroupID { get; set; }
        public string AccountSubGroupName { get; set; } = "";
    }
}
namespace FinabitAPI.Models
{
    public sealed class AccountProbe
    {
        public int Index { get; set; }
        public string? AccountId { get; set; }
        public string? AccountName { get; set; }
    }

    public sealed class AccountProbeResult
    {
        public int Index { get; set; }
        public string? AccountId { get; set; }
        public string? AccountName { get; set; }
        public IReadOnlyList<AccountMatchDto> Results { get; set; } = Array.Empty<AccountMatchDto>();
    }
}

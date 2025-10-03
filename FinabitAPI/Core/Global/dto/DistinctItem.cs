namespace FinabitAPI.Core.Global.dto
{
    public sealed class DistinctItemProbe
    {
        public int Index { get; set; }
        public string? ItemId { get; set; }
        public string? ItemName { get; set; }
    }

    public sealed class DistinctItemProbeResult
    {
        public int Index { get; set; }
        public string? ItemId { get; set; }
        public string? ItemName { get; set; }
        public IReadOnlyList<DistinctItemNameDto> Results { get; set; } = Array.Empty<DistinctItemNameDto>();
    }

}

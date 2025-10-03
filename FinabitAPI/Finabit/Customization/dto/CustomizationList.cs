using System;
using System.ComponentModel.DataAnnotations;

namespace FinabitAPI.Finabit.Customization.dto
{
    /// <summary>
    /// Represents a saved configuration for different list views (table/pivot/chart)
    /// </summary>
    public class CustomizationList
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string User { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // 'table', 'pivot', 'chart'

        [Required]
        public string Config { get; set; } = string.Empty; // JSON string

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for creating/updating customization list
    /// </summary>
    public class CustomizationListDto
    {
        [Required]
        public string User { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Config { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for updating customization list
    /// </summary>
    public class CustomizationListUpdateDto
    {
        public string? Name { get; set; }
        public string? Config { get; set; }
    }
}

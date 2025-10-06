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

        [Required, StringLength(255)]
        public string User { get; set; } = string.Empty;

        [StringLength(255)]
        public string? StorageKey { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; } = string.Empty;

        // Renamed from Type -> Mode
        [Required, StringLength(50)]
        public string Mode { get; set; } = string.Empty; // 'table', 'pivot', 'chart'

        [StringLength(20)]
        public string? Device { get; set; } // 'mobile','desktop' or null

        // Renamed from Config -> Data
        [Required]
        public string Data { get; set; } = string.Empty; // JSON string

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for creating/updating customization list
    /// </summary>
    public class CustomizationListDto
    {
        [Required] public string User { get; set; } = string.Empty;
        public string? StorageKey { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Mode { get; set; } = string.Empty; // table|pivot|chart
        public string? Device { get; set; }
        [Required] public string Data { get; set; } = string.Empty;

        // Backward compatibility (optional incoming fields from old clients)
        public string? Type { get; set; } // if provided map to Mode
        public string? Config { get; set; } // if provided map to Data
    }

    /// <summary>
    /// DTO for updating customization list
    /// </summary>
    public class CustomizationListUpdateDto
    {
        public string? Name { get; set; }
        public string? Data { get; set; }
        public string? StorageKey { get; set; }
        public string? Device { get; set; }
        public string? Mode { get; set; }

        // Legacy support
        public string? Type { get; set; }
        public string? Config { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace FinabitAPI.Models
{
    /// <summary>
    /// Represents user preferences like last selected view names
    /// </summary>
    public class CustomizationProfilePreference
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string User { get; set; } = string.Empty;

        // New composite unique parts
        [StringLength(255)] public string? StorageKey { get; set; }
        [StringLength(50)] public string? Mode { get; set; } // table|pivot|chart
        [StringLength(20)] public string? Device { get; set; } // mobile|desktop|null

        // New field
        [StringLength(255)] public string? LastViewName { get; set; }

        // Legacy fields retained for backward compatibility
        [StringLength(255)] public string? PrefKey { get; set; }
        public string? PrefValue { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for creating/updating profile preference
    /// </summary>
    public class CustomizationProfilePreferenceDto
    {
        [Required] public string User { get; set; } = string.Empty;
        public string? StorageKey { get; set; }
        public string? Mode { get; set; }
        public string? Device { get; set; }
        public string? LastViewName { get; set; }

        // Legacy fields
        public string? PrefKey { get; set; }
        public string? PrefValue { get; set; }
    }
}

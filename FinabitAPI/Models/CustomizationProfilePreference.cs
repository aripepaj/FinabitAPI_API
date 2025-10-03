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

        [Required]
        [StringLength(255)]
        public string User { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PrefKey { get; set; } = string.Empty;

        [Required]
        public string PrefValue { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for creating/updating profile preference
    /// </summary>
    public class CustomizationProfilePreferenceDto
    {
        [Required]
        public string User { get; set; } = string.Empty;

        [Required]
        public string PrefKey { get; set; } = string.Empty;

        [Required]
        public string PrefValue { get; set; } = string.Empty;
    }
}

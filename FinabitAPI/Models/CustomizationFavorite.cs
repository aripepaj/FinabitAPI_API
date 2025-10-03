using System;
using System.ComponentModel.DataAnnotations;

namespace FinabitAPI.Models
{
    /// <summary>
    /// Represents a user's bookmarked/favorite item
    /// </summary>
    public class CustomizationFavorite
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string User { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string ItemId { get; set; } = string.Empty;

        // Supabase schema does not enforce item_type uniqueness; keep for backward compatibility.
        [StringLength(100)]
        public string? ItemType { get; set; }

        // Renamed/added fields
        [StringLength(255)]
        public string Label { get; set; } = string.Empty;

        [StringLength(500)]
        public string Path { get; set; } = string.Empty;

        public string? Icon { get; set; } // JSON (was Metadata/Icon separation)

        // Legacy metadata field kept if needed
        public string? Metadata { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow; // renamed from CreatedAt
    }

    /// <summary>
    /// DTO for creating customization favorite
    /// </summary>
    public class CustomizationFavoriteDto
    {
        [Required] public string User { get; set; } = string.Empty;
        [Required] public string ItemId { get; set; } = string.Empty;
        public string? ItemType { get; set; }
        [Required] public string Label { get; set; } = string.Empty;
        [Required] public string Path { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Metadata { get; set; } // keep for backward compatibility / merging
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace FinabitAPI.Finabit.Customization.dto
{
    /// <summary>
    /// Represents a user's bookmarked/favorite item
    /// </summary>
    public class CustomizationFavorite
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string User { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ItemId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ItemType { get; set; } = string.Empty;

        public string? Metadata { get; set; } // JSON string

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO for creating customization favorite
    /// </summary>
    public class CustomizationFavoriteDto
    {
        [Required]
        public string User { get; set; } = string.Empty;

        [Required]
        public string ItemId { get; set; } = string.Empty;

        [Required]
        public string ItemType { get; set; } = string.Empty;

        public string? Metadata { get; set; }
    }
}

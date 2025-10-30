using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinabitAPI.Clinic.Medical
{
    /// <summary>
    /// Lookup table for module types (e.g., Ankesat, Ekzaminimet, ...)
    /// </summary>
    [Table("tblCLMedicalModuleTypes")]
    public class MedicalModuleType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// Strongly-typed constants for default module type names.
    /// Keep in sync with the seed in ClinicDbContext.
    /// </summary>
    public static class MedicalModuleTypes
    {
        public const string Ankesat = "Ankesat";
        public const string Ekzaminimet = "Ekzaminimet";
        public const string Diagnozat = "Diagnozat";
        public const string Terapite = "Terapitë";
        public const string Analizat = "Analizat";
        public const string Keshillat = "Këshillat";
        public const string Kontrollat = "Kontrollat";
        public const string Verejtjet = "Vërejtjet";
    }

    /// <summary>
    /// Unified medical record header shared by all modules.
    /// Holds the common fields while details are stored in MedicalFieldValues.
    /// </summary>
    [Table("tblCLMedicalRecords")]
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Related patient ID (tblCLPatients.Id)
        /// </summary>
        [Required]
        public int PatientID { get; set; }

        /// <summary>
        /// Optional code shown in UI
        /// </summary>
        [MaxLength(50)]
        public string? Kodi { get; set; }

        /// <summary>
        /// Title/subject of the record
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Titulli { get; set; } = string.Empty;

        /// <summary>
        /// Owning department (null for all)
        /// </summary>
        public int? Departamenti { get; set; }

        /// <summary>
        /// Redundant string name of the module type for quick filters (kept for convenience)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ModuleType { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to lookup table (authoritative type id)
        /// </summary>
        public int ModuleTypeId { get; set; }

        [ForeignKey(nameof(ModuleTypeId))]
        public MedicalModuleType? ModuleTypeRef { get; set; }

        /// <summary>
        /// Optional date of the record (visit date, exam date, etc.)
        /// </summary>
        public DateTime? Data { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        [MaxLength(20)]
        public string? Priority { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }

    /// <summary>
    /// Configurable field definitions per module/department for medical records.
    /// </summary>
    [Table("tblCLMedicalFieldDefinitions")]
    public class MedicalFieldDefinition
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Module identifier string (e.g., MedicalModuleTypes.Ankesat)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ModuleType { get; set; } = string.Empty;

        /// <summary>
        /// Optional department constraint
        /// </summary>
        public int? DepartmentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FieldKey { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FieldType { get; set; } = "text"; // text, number, date, dropdown, checkbox, textarea

        public string? FieldOptions { get; set; } // JSON

        public bool IsEnabled { get; set; } = true;
        public bool IsRequired { get; set; } = false;
        public int DisplayOrder { get; set; }
        public string? ValidationRules { get; set; } // JSON
        public string? HelpText { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    /// <summary>
    /// Values for dynamic medical fields per record.
    /// </summary>
    [Table("tblCLMedicalFieldValues")]
    public class MedicalFieldValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MedicalRecordID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModuleType { get; set; } = string.Empty;

        [Required]
        public int FieldDefinitionID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FieldKey { get; set; } = string.Empty;

        public string? FieldValue { get; set; }
        public decimal? NumericValue { get; set; }
        public DateTime? DateValue { get; set; }
        public bool? BooleanValue { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(FieldDefinitionID))]
        public MedicalFieldDefinition? FieldDefinition { get; set; }
    }

    /// <summary>
    /// Types of Anamnesis (templates/stickers grouping)
    /// </summary>
    [Table("tblCLAnamnesisTypes")]
    public class AnamnesisType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// A saved Anamnesis template header. Its fields are defined via MedicalFieldDefinitions where ModuleType = Anamnesis.
    /// </summary>
    [Table("tblCLAnamnesisDefinitions")]
    public class AnamnesisDefinition
    {
        [Key]
        public int Id { get; set; }

        public int TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public AnamnesisType? Type { get; set; }

        [MaxLength(50)]
        public string? Kodi { get; set; }

        [Required]
        [MaxLength(300)]
        public string Titulli { get; set; } = string.Empty;

        public int? DepartmentID { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsEnabled { get; set; } = true;

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

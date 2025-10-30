//-- =============================================
//-- Author:		Generated  
//-- Create date: 13.10.25 
//-- Description:	Dynamic field definition system for patient registration
//-- =============================================
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinabitAPI.Clinic.Patient
{
    /// <summary>
    /// Defines configurable fields for patient registration per department
    /// </summary>
    [Table("tblCLPatientFieldDefinitions")]
    public class PatientFieldDefinition
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Department this field definition applies to (null = all departments)
        /// </summary>
        public int? DepartmentID { get; set; }

        /// <summary>
        /// Internal field key (unique identifier)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FieldKey { get; set; } = string.Empty;

        /// <summary>
        /// Display name in UI (e.g., "Nr. personal", "Alergjitë")
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Field type: text, number, date, dropdown, checkbox, textarea
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FieldType { get; set; } = "text";

        /// <summary>
        /// JSON options for dropdown/checkbox lists
        /// </summary>
        public string? FieldOptions { get; set; }

        /// <summary>
        /// Whether field is enabled for this department
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Whether field is required
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// Display order in form
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Field validation rules (JSON)
        /// </summary>
        public string? ValidationRules { get; set; }

        /// <summary>
        /// Help text or placeholder
        /// </summary>
        [MaxLength(500)]
        public string? HelpText { get; set; }

        /// <summary>
        /// CSS class or styling info
        /// </summary>
        [MaxLength(100)]
        public string? CssClass { get; set; }

        /// <summary>
        /// Field width (1-12 for bootstrap grid)
        /// </summary>
        public int Width { get; set; } = 12;

        /// <summary>
        /// Whether field is visible (hidden fields for internal use)
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Whether field is readonly
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }

    /// <summary>
    /// Stores actual field values for each patient
    /// </summary>
    [Table("tblCLPatientFieldValues")]
    public class PatientFieldValue
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Patient ID this value belongs to
        /// </summary>
        [Required]
        public int PatientID { get; set; }

        /// <summary>
        /// Field definition ID
        /// </summary>
        [Required]
        public int FieldDefinitionID { get; set; }

        /// <summary>
        /// Field key for quick lookup
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FieldKey { get; set; } = string.Empty;

        /// <summary>
        /// The actual field value (stored as string, typed by FieldType)
        /// </summary>
        public string? FieldValue { get; set; }

        /// <summary>
        /// For numeric/date fields - typed value
        /// </summary>
        public decimal? NumericValue { get; set; }

        /// <summary>
        /// For date fields
        /// </summary>
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// For boolean fields
        /// </summary>
        public bool? BooleanValue { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Navigation property to field definition
        /// </summary>
        [ForeignKey("FieldDefinitionID")]
        public PatientFieldDefinition FieldDefinition { get; set; } = null!;
    }

    /// <summary>
    /// DTO for field configuration management
    /// </summary>
    public class PatientFieldConfigurationDto
    {
        public int Id { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string FieldKey { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public string? FieldOptions { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public string? ValidationRules { get; set; }
        public string? HelpText { get; set; }
        public string? CssClass { get; set; }
        public int Width { get; set; }
        public bool IsVisible { get; set; }
        public bool IsReadOnly { get; set; }
    }

    /// <summary>
    /// DTO for patient data with dynamic fields
    /// </summary>
    public class PatientWithFieldsDto
    {
        public int Id { get; set; }
        public string? MedicalRecordNumber { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // HGuest data
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone1 { get; set; }
        public string? Email { get; set; }

        /// <summary>
        /// Dynamic field values
        /// </summary>
        public Dictionary<string, object?> DynamicFields { get; set; } = new();

        /// <summary>
        /// Field definitions for this department
        /// </summary>
        public List<PatientFieldConfigurationDto> FieldDefinitions { get; set; } = new();
    }
}
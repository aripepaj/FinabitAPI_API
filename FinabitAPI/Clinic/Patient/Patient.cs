using FinabitAPI.Hotel.HGuests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinabitAPI.Clinic.Patient
{
    /// <summary>
    /// Patient entity with 1-to-1 relationship to HGuest
    /// Patient.Id is a foreign key to HGuest.ID (shared primary key)
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Patient ID - Foreign Key to HGuest.ID (shared primary key)
        /// </summary>
        [Key]
        [ForeignKey("HGuest")]
        public int Id { get; set; }

        /// <summary>
        /// Medical Record Number
        /// </summary>
        public string? MedicalRecordNumber { get; set; }

        /// <summary>
        /// Patient's blood type
        /// </summary>
        public string? BloodType { get; set; }

        /// <summary>
        /// Known allergies
        /// </summary>
        public string? Allergies { get; set; }

        /// <summary>
        /// Emergency contact name
        /// </summary>
        public string? EmergencyContactName { get; set; }

        /// <summary>
        /// Emergency contact phone
        /// </summary>
        public string? EmergencyContactPhone { get; set; }

        /// <summary>
        /// Notes about the patient
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Date when patient record was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Date when patient record was last updated
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        // Note: HGuest data will be loaded separately via service layer
        // to avoid EF Core mapping conflicts with existing Hotel module;
    }
}
//-- =============================================
//-- Author:		Generated
//-- Create date: 13.10.25 
//-- Description:	CRUD Repository for Patient using EF Core
//-- =============================================
using Microsoft.EntityFrameworkCore;
using FinabitAPI.Hotel.HGuests;

namespace FinabitAPI.Clinic.Patient
{
    public class PatientRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly ClinicDbContext _context;

        public PatientRepository(ClinicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Insert

        public async Task<Patient> InsertAsync(Patient patient)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                patient.CreatedDate = DateTime.UtcNow;
                
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                return patient;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error inserting patient: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Update

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                patient.UpdatedDate = DateTime.UtcNow;
                
                _context.Patients.Update(patient);
                await _context.SaveChangesAsync();

                return patient;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error updating patient: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                var patient = await _context.Patients.FindAsync(id);
                if (patient == null)
                {
                    ErrorID = 1;
                    ErrorDescription = "Patient not found";
                    return false;
                }

                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error deleting patient: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Select

        public async Task<Patient?> GetByIdAsync(int id)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (patient == null)
                {
                    ErrorID = 1;
                    ErrorDescription = "Patient not found";
                }

                return patient;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error retrieving patient: {ex.Message}";
                throw;
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                return await _context.Patients.ToListAsync();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error retrieving patients: {ex.Message}";
                throw;
            }
        }

        public async Task<List<Patient>> GetByMedicalRecordNumberAsync(string medicalRecordNumber)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                return await _context.Patients
                    .Where(p => p.MedicalRecordNumber == medicalRecordNumber)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error retrieving patients by medical record number: {ex.Message}";
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                return await _context.Patients.AnyAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error checking patient existence: {ex.Message}";
                throw;
            }
        }

        /// <summary>
        /// Get HGuest data for a patient using raw SQL to avoid EF Core mapping conflicts
        /// </summary>
        public async Task<HGuestData?> GetHGuestDataAsync(int guestId)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = "";

                var sql = @"
                    SELECT ID, Name, Surname, Phone1, Email 
                    FROM tblHGuest 
                    WHERE ID = {0}";

                var result = await _context.Database
                    .SqlQueryRaw<HGuestData>(sql, guestId)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Error retrieving HGuest data: {ex.Message}";
                throw;
            }
        }

        #endregion
    }

    /// <summary>
    /// Simple class to hold HGuest data without EF Core mapping conflicts
    /// </summary>
    public class HGuestData
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone1 { get; set; }
        public string? Email { get; set; }
    }
}
//-- =============================================
//-- Author:		Generated  
//-- Create date: 13.10.25
//-- Description:	Service Layer for Patient
//-- =============================================
using System;
using System.Collections.Generic;

namespace FinabitAPI.Clinic.Patient
{
    public class PatientService
    {
        public PatientRepository GlobalPatient;
        private readonly ClinicDbContext _context;

        public PatientService(ClinicDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            GlobalPatient = new PatientRepository(_context);
        }

        public int ErrorID { get; set; } = 0;
        public string ErrorDescription { get; set; } = "";

        #region Insert

        public async Task<Patient> InsertAsync(Patient patient)
        {
            try
            {
                var result = await GlobalPatient.InsertAsync(patient);
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error inserting patient: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Update

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            try
            {
                var result = await GlobalPatient.UpdateAsync(patient);
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error updating patient: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await GlobalPatient.DeleteAsync(id);
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error deleting patient: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Select

        public async Task<Patient?> GetByIdAsync(int id)
        {
            try
            {
                var result = await GlobalPatient.GetByIdAsync(id);
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error retrieving patient: {ex.Message}";
                throw;
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            try
            {
                var result = await GlobalPatient.GetAllAsync();
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error retrieving patients: {ex.Message}";
                throw;
            }
        }

        public async Task<List<Patient>> GetByMedicalRecordNumberAsync(string medicalRecordNumber)
        {
            try
            {
                var result = await GlobalPatient.GetByMedicalRecordNumberAsync(medicalRecordNumber);
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error retrieving patients by medical record number: {ex.Message}";
                throw;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                var result = await GlobalPatient.ExistsAsync(id);
                ErrorID = GlobalPatient.ErrorID;
                ErrorDescription = GlobalPatient.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error checking patient existence: {ex.Message}";
                throw;
            }
        }

        #endregion

        #region Business Logic Methods

        /// <summary>
        /// Get patients with their HGuest information formatted for display
        /// </summary>
        public async Task<List<object>> GetPatientsWithHGuestInfoAsync()
        {
            try
            {
                var patients = await GetAllAsync();
                var result = new List<object>();

                foreach (var patient in patients)
                {
                    var hguestData = await GlobalPatient.GetHGuestDataAsync(patient.Id);
                    
                    result.Add(new
                    {
                        patient.Id,
                        patient.MedicalRecordNumber,
                        patient.BloodType,
                        patient.Allergies,
                        patient.EmergencyContactName,
                        patient.EmergencyContactPhone,
                        patient.Notes,
                        patient.CreatedDate,
                        patient.UpdatedDate,
                        HGuest = hguestData != null ? new
                        {
                            hguestData.Name,
                            hguestData.Surname,
                            hguestData.Phone1,
                            hguestData.Email
                        } : null
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error getting patients with HGuest info: {ex.Message}";
                throw;
            }
        }

        /// <summary>
        /// Get a specific patient with HGuest information formatted for display
        /// </summary>
        public async Task<object?> GetPatientWithHGuestInfoAsync(int id)
        {
            try
            {
                var patient = await GetByIdAsync(id);
                
                if (patient == null)
                    return null;

                var hguestData = await GlobalPatient.GetHGuestDataAsync(patient.Id);

                return new
                {
                    patient.Id,
                    patient.MedicalRecordNumber,
                    patient.BloodType,
                    patient.Allergies,
                    patient.EmergencyContactName,
                    patient.EmergencyContactPhone,
                    patient.Notes,
                    patient.CreatedDate,
                    patient.UpdatedDate,
                    HGuest = hguestData != null ? new
                    {
                        hguestData.Name,
                        hguestData.Surname,
                        hguestData.Phone1,
                        hguestData.Email
                    } : null
                };
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = $"Service error getting patient with HGuest info: {ex.Message}";
                throw;
            }
        }

        #endregion
    }
}
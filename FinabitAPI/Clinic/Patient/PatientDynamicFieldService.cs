//-- =============================================
//-- Author:		Generated  
//-- Create date: 13.10.25 
//-- Description:	Service for managing dynamic patient fields
//-- =============================================
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FinabitAPI.Clinic.Patient
{
    /// <summary>
    /// Service for managing dynamic patient field configuration and values
    /// </summary>
    public class PatientDynamicFieldService
    {
        private readonly ClinicDbContext _context;

        public int ErrorID { get; private set; }
        public string ErrorDescription { get; private set; } = string.Empty;

        public PatientDynamicFieldService(ClinicDbContext context)
        {
            _context = context;
        }

        #region Field Definition Management

        /// <summary>
        /// Get all field definitions for a department
        /// </summary>
        public async Task<List<PatientFieldConfigurationDto>> GetFieldDefinitionsForDepartmentAsync(int? departmentId = null)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = string.Empty;

                var query = _context.PatientFieldDefinitions.AsQueryable();

                // Get fields for specific department or global fields (null department)
                if (departmentId.HasValue)
                {
                    query = query.Where(f => f.DepartmentID == departmentId || f.DepartmentID == null);
                }
                else
                {
                    query = query.Where(f => f.DepartmentID == null);
                }

                var definitions = await query
                    .OrderBy(f => f.DisplayOrder)
                    .ThenBy(f => f.DisplayName)
                    .Select(f => new PatientFieldConfigurationDto
                    {
                        Id = f.Id,
                        DepartmentID = f.DepartmentID,
                        FieldKey = f.FieldKey,
                        DisplayName = f.DisplayName,
                        FieldType = f.FieldType,
                        FieldOptions = f.FieldOptions,
                        IsEnabled = f.IsEnabled,
                        IsRequired = f.IsRequired,
                        DisplayOrder = f.DisplayOrder,
                        ValidationRules = f.ValidationRules,
                        HelpText = f.HelpText,
                        CssClass = f.CssClass,
                        Width = f.Width,
                        IsVisible = f.IsVisible,
                        IsReadOnly = f.IsReadOnly
                    })
                    .ToListAsync();

                return definitions;
            }
            catch (Exception ex)
            {
                ErrorID = 1;
                ErrorDescription = $"Error retrieving field definitions: {ex.Message}";
                return new List<PatientFieldConfigurationDto>();
            }
        }

        /// <summary>
        /// Create or update field definition
        /// </summary>
        public async Task<PatientFieldDefinition?> SaveFieldDefinitionAsync(PatientFieldDefinition fieldDef)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = string.Empty;

                if (fieldDef.Id == 0)
                {
                    // Create new
                    fieldDef.CreatedDate = DateTime.UtcNow;
                    _context.PatientFieldDefinitions.Add(fieldDef);
                }
                else
                {
                    // Update existing
                    var existing = await _context.PatientFieldDefinitions.FindAsync(fieldDef.Id);
                    if (existing == null)
                    {
                        ErrorID = 2;
                        ErrorDescription = "Field definition not found";
                        return null;
                    }

                    existing.DisplayName = fieldDef.DisplayName;
                    existing.FieldType = fieldDef.FieldType;
                    existing.FieldOptions = fieldDef.FieldOptions;
                    existing.IsEnabled = fieldDef.IsEnabled;
                    existing.IsRequired = fieldDef.IsRequired;
                    existing.DisplayOrder = fieldDef.DisplayOrder;
                    existing.ValidationRules = fieldDef.ValidationRules;
                    existing.HelpText = fieldDef.HelpText;
                    existing.CssClass = fieldDef.CssClass;
                    existing.Width = fieldDef.Width;
                    existing.IsVisible = fieldDef.IsVisible;
                    existing.IsReadOnly = fieldDef.IsReadOnly;
                    existing.UpdatedDate = DateTime.UtcNow;
                    existing.UpdatedBy = fieldDef.UpdatedBy;

                    fieldDef = existing;
                }

                await _context.SaveChangesAsync();
                return fieldDef;
            }
            catch (Exception ex)
            {
                ErrorID = 3;
                ErrorDescription = $"Error saving field definition: {ex.Message}";
                return null;
            }
        }

        /// <summary>
        /// Delete field definition and all related values
        /// </summary>
        public async Task<bool> DeleteFieldDefinitionAsync(int fieldDefId)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = string.Empty;

                var fieldDef = await _context.PatientFieldDefinitions.FindAsync(fieldDefId);
                if (fieldDef == null)
                {
                    ErrorID = 4;
                    ErrorDescription = "Field definition not found";
                    return false;
                }

                // Delete all field values for this definition (cascade should handle this)
                _context.PatientFieldDefinitions.Remove(fieldDef);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                ErrorID = 5;
                ErrorDescription = $"Error deleting field definition: {ex.Message}";
                return false;
            }
        }

        #endregion

        #region Field Values Management

        /// <summary>
        /// Get patient with all dynamic field values
        /// </summary>
        public async Task<PatientWithFieldsDto?> GetPatientWithDynamicFieldsAsync(int patientId, int? departmentId = null)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = string.Empty;

                // Get patient basic data (will need to also fetch HGuest data via raw SQL)
                var patient = await _context.Patients.FindAsync(patientId);
                if (patient == null)
                {
                    ErrorID = 6;
                    ErrorDescription = "Patient not found";
                    return null;
                }

                // Get HGuest data via raw SQL (to avoid EF mapping conflicts)
                var hguestData = await GetHGuestDataAsync(patientId);

                // Get field definitions
                var fieldDefinitions = await GetFieldDefinitionsForDepartmentAsync(departmentId);

                // Get field values for this patient
                var fieldValues = await _context.PatientFieldValues
                    .Where(v => v.PatientID == patientId)
                    .ToDictionaryAsync(v => v.FieldKey, v => v);

                // Build dynamic fields dictionary
                var dynamicFields = new Dictionary<string, object?>();
                foreach (var fieldDef in fieldDefinitions.Where(f => f.IsEnabled))
                {
                    if (fieldValues.TryGetValue(fieldDef.FieldKey, out var value))
                    {
                        // Convert value based on field type
                        dynamicFields[fieldDef.FieldKey] = ConvertFieldValue(value, fieldDef.FieldType);
                    }
                    else
                    {
                        dynamicFields[fieldDef.FieldKey] = null;
                    }
                }

                return new PatientWithFieldsDto
                {
                    Id = patient.Id,
                    MedicalRecordNumber = patient.MedicalRecordNumber,
                    BloodType = patient.BloodType,
                    Allergies = patient.Allergies,
                    EmergencyContactName = patient.EmergencyContactName,
                    EmergencyContactPhone = patient.EmergencyContactPhone,
                    Notes = patient.Notes,
                    CreatedDate = patient.CreatedDate,
                    UpdatedDate = patient.UpdatedDate,
                    
                    // HGuest data
                    Name = hguestData?.Name,
                    Surname = hguestData?.Surname,
                    Phone1 = hguestData?.Phone1,
                    Email = hguestData?.Email,
                    
                    DynamicFields = dynamicFields,
                    FieldDefinitions = fieldDefinitions
                };
            }
            catch (Exception ex)
            {
                ErrorID = 7;
                ErrorDescription = $"Error retrieving patient with dynamic fields: {ex.Message}";
                return null;
            }
        }

        /// <summary>
        /// Save dynamic field values for a patient
        /// </summary>
        public async Task<bool> SavePatientDynamicFieldsAsync(int patientId, Dictionary<string, object?> fieldValues)
        {
            try
            {
                ErrorID = 0;
                ErrorDescription = string.Empty;

                foreach (var kvp in fieldValues)
                {
                    var fieldKey = kvp.Key;
                    var value = kvp.Value;

                    // Get or create field value record
                    var existingValue = await _context.PatientFieldValues
                        .FirstOrDefaultAsync(v => v.PatientID == patientId && v.FieldKey == fieldKey);

                    // Get field definition for type information
                    var fieldDef = await _context.PatientFieldDefinitions
                        .FirstOrDefaultAsync(f => f.FieldKey == fieldKey);

                    if (fieldDef == null) continue; // Skip unknown fields

                    if (existingValue == null)
                    {
                        // Create new value
                        existingValue = new PatientFieldValue
                        {
                            PatientID = patientId,
                            FieldDefinitionID = fieldDef.Id,
                            FieldKey = fieldKey,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PatientFieldValues.Add(existingValue);
                    }

                    // Set typed values based on field type
                    SetTypedValue(existingValue, value, fieldDef.FieldType);
                    existingValue.UpdatedDate = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ErrorID = 8;
                ErrorDescription = $"Error saving patient dynamic fields: {ex.Message}";
                return false;
            }
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Get HGuest data via raw SQL to avoid EF mapping conflicts
        /// </summary>
        private async Task<HGuestData?> GetHGuestDataAsync(int patientId)
        {
            try
            {
                var sql = @"
                    SELECT ID, Name, Surname, Phone1, Email 
                    FROM tblHGuest 
                    WHERE ID = {0}";

                var result = await _context.Database
                    .SqlQueryRaw<HGuestData>(sql, patientId)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert field value based on type
        /// </summary>
        private object? ConvertFieldValue(PatientFieldValue fieldValue, string fieldType)
        {
            return fieldType.ToLower() switch
            {
                "number" or "decimal" => fieldValue.NumericValue,
                "date" or "datetime" => fieldValue.DateValue,
                "checkbox" or "boolean" => fieldValue.BooleanValue,
                _ => fieldValue.FieldValue
            };
        }

        /// <summary>
        /// Set typed value on field value entity
        /// </summary>
        private void SetTypedValue(PatientFieldValue fieldValue, object? value, string fieldType)
        {
            // Clear all typed values first
            fieldValue.FieldValue = null;
            fieldValue.NumericValue = null;
            fieldValue.DateValue = null;
            fieldValue.BooleanValue = null;

            if (value == null) return;

            switch (fieldType.ToLower())
            {
                case "number":
                case "decimal":
                    if (decimal.TryParse(value.ToString(), out var decimalVal))
                    {
                        fieldValue.NumericValue = decimalVal;
                        fieldValue.FieldValue = decimalVal.ToString();
                    }
                    break;

                case "date":
                case "datetime":
                    if (DateTime.TryParse(value.ToString(), out var dateVal))
                    {
                        fieldValue.DateValue = dateVal;
                        fieldValue.FieldValue = dateVal.ToString("yyyy-MM-dd");
                    }
                    break;

                case "checkbox":
                case "boolean":
                    if (bool.TryParse(value.ToString(), out var boolVal))
                    {
                        fieldValue.BooleanValue = boolVal;
                        fieldValue.FieldValue = boolVal.ToString();
                    }
                    break;

                default:
                    fieldValue.FieldValue = value.ToString();
                    break;
            }
        }

        #endregion
    }
}
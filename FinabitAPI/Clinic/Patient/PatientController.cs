//-- =============================================
//-- Author:		Generated
//-- Create date: 13.10.25 
//-- Description:	Controller for Patient CRUD operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Clinic.Patient
{
    [ApiController]
    [Route("api/clinic/patients")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly PatientDynamicFieldService _dynamicFieldService;

        public PatientController(PatientService patientService, PatientDynamicFieldService dynamicFieldService)
        {
            _patientService = patientService;
            _dynamicFieldService = dynamicFieldService;
        }

        #region CRUD Operations

        /// <summary>
        /// Insert a new patient
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<object>> Insert([FromBody] Patient patient)
        {
            try
            {
                var result = await _patientService.InsertAsync(patient);

                if (_patientService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Patient inserted successfully", 
                        data = result 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _patientService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while inserting the patient", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Update an existing patient
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> Update(int id, [FromBody] Patient patient)
        {
            try
            {
                if (id != patient.Id)
                {
                    return BadRequest(new { 
                        success = false, 
                        message = "Patient ID mismatch" 
                    });
                }

                var exists = await _patientService.ExistsAsync(id);
                if (!exists)
                {
                    return NotFound(new { 
                        success = false, 
                        message = $"Patient with ID {id} not found" 
                    });
                }

                var result = await _patientService.UpdateAsync(patient);

                if (_patientService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Patient updated successfully", 
                        data = result 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _patientService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while updating the patient", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Delete a patient by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            try
            {
                var exists = await _patientService.ExistsAsync(id);
                if (!exists)
                {
                    return NotFound(new { 
                        success = false, 
                        message = $"Patient with ID {id} not found" 
                    });
                }

                var result = await _patientService.DeleteAsync(id);

                if (_patientService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Patient deleted successfully" 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _patientService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while deleting the patient", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Get all patients with their linked HGuest information
        /// </summary>
        /// <returns>List of patients including HGuest data (Name, Surname, Phone1, Email)</returns>
        [HttpGet]
        public async Task<ActionResult<object>> GetPatients()
        {
            try
            {
                var patients = await _patientService.GetPatientsWithHGuestInfoAsync();

                return Ok(new { 
                    success = true, 
                    data = patients 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving patients", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Get a specific patient by ID with linked HGuest information
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Patient with HGuest data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPatient(int id)
        {
            try
            {
                var patient = await _patientService.GetPatientWithHGuestInfoAsync(id);

                if (patient == null)
                {
                    return NotFound(new { 
                        success = false, 
                        message = $"Patient with ID {id} not found" 
                    });
                }

                return Ok(new { 
                    success = true, 
                    data = patient 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving the patient", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Get patients by medical record number
        /// </summary>
        [HttpGet("by-medical-record/{medicalRecordNumber}")]
        public async Task<ActionResult<object>> GetPatientsByMedicalRecord(string medicalRecordNumber)
        {
            try
            {
                var patients = await _patientService.GetByMedicalRecordNumberAsync(medicalRecordNumber);

                return Ok(new { 
                    success = true, 
                    data = patients 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while retrieving patients by medical record number", 
                    error = ex.Message 
                });
            }
        }

        #endregion

        #region Dynamic Fields Operations

        /// <summary>
        /// Get patient with dynamic fields for a specific department
        /// </summary>
        [HttpGet("{id}/with-fields")]
        public async Task<ActionResult<object>> GetPatientWithFields(int id, [FromQuery] int? departmentId = null)
        {
            try
            {
                var patient = await _dynamicFieldService.GetPatientWithDynamicFieldsAsync(id, departmentId);

                if (patient == null)
                {
                    return NotFound(new { 
                        success = false, 
                        message = _dynamicFieldService.ErrorDescription ?? $"Patient with ID {id} not found" 
                    });
                }

                return Ok(new { 
                    success = true, 
                    data = patient 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving patient with dynamic fields", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Save patient dynamic field values
        /// </summary>
        [HttpPost("{id}/dynamic-fields")]
        public async Task<ActionResult<object>> SavePatientDynamicFields(int id, [FromBody] SaveDynamicFieldsRequest request)
        {
            try
            {
                var result = await _dynamicFieldService.SavePatientDynamicFieldsAsync(id, request.FieldValues);

                if (_dynamicFieldService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Patient dynamic fields saved successfully" 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _dynamicFieldService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error saving patient dynamic fields", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Get all patients with dynamic fields for a department
        /// </summary>
        [HttpGet("with-fields")]
        public async Task<ActionResult<object>> GetPatientsWithFields([FromQuery] int? departmentId = null)
        {
            try
            {
                var patients = await _patientService.GetAllAsync();
                if (patients == null || patients.Count == 0)
                {
                    return Ok(new { 
                        success = true, 
                        data = new List<object>() 
                    });
                }

                var patientsWithFields = new List<PatientWithFieldsDto>();

                foreach (var patient in patients)
                {
                    var patientWithFields = await _dynamicFieldService.GetPatientWithDynamicFieldsAsync(patient.Id, departmentId);
                    if (patientWithFields != null)
                    {
                        patientsWithFields.Add(patientWithFields);
                    }
                }

                return Ok(new { 
                    success = true, 
                    data = patientsWithFields 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving patients with dynamic fields", 
                    error = ex.Message 
                });
            }
        }

        #endregion
    }

    /// <summary>
    /// Request model for saving dynamic fields
    /// </summary>
    public class SaveDynamicFieldsRequest
    {
        public Dictionary<string, object?> FieldValues { get; set; } = new();
    }
}
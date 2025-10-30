//-- =============================================
//-- Author:		Generated  
//-- Create date: 13.10.25 
//-- Description:	Controller for patient dynamic field configuration
//-- =============================================
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Clinic.Patient
{
    /// <summary>
    /// Controller for managing patient dynamic field configuration
    /// </summary>
    [ApiController]
    [Route("api/clinic/patient-fields")]
    public class PatientFieldConfigController : ControllerBase
    {
        private readonly PatientDynamicFieldService _fieldService;

        public PatientFieldConfigController(PatientDynamicFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        /// <summary>
        /// Get field configuration for a department
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<object>> GetFieldConfiguration([FromQuery] int? departmentId = null)
        {
            try
            {
                var fields = await _fieldService.GetFieldDefinitionsForDepartmentAsync(departmentId);
                
                return Ok(new { 
                    success = true, 
                    data = fields 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error retrieving field configuration", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Create or update field definition
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<object>> SaveFieldDefinition([FromBody] PatientFieldDefinition fieldDef)
        {
            try
            {
                if (string.IsNullOrEmpty(fieldDef.FieldKey) || string.IsNullOrEmpty(fieldDef.DisplayName))
                {
                    return BadRequest(new { 
                        success = false, 
                        message = "FieldKey and DisplayName are required" 
                    });
                }

                var result = await _fieldService.SaveFieldDefinitionAsync(fieldDef);

                if (_fieldService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Field definition saved successfully", 
                        data = result 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _fieldService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error saving field definition", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Update field definition (enable/disable, required, etc.)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> UpdateFieldDefinition(int id, [FromBody] UpdateFieldDefinitionRequest request)
        {
            try
            {
                // Create a field definition with the updates
                var fieldDef = new PatientFieldDefinition
                {
                    Id = id,
                    DepartmentID = request.DepartmentID,
                    FieldKey = request.FieldKey,
                    DisplayName = request.DisplayName,
                    FieldType = request.FieldType,
                    FieldOptions = request.FieldOptions,
                    IsEnabled = request.IsEnabled,
                    IsRequired = request.IsRequired,
                    DisplayOrder = request.DisplayOrder,
                    ValidationRules = request.ValidationRules,
                    HelpText = request.HelpText,
                    CssClass = request.CssClass,
                    Width = request.Width,
                    IsVisible = request.IsVisible,
                    IsReadOnly = request.IsReadOnly,
                    UpdatedBy = request.UpdatedBy
                };

                var result = await _fieldService.SaveFieldDefinitionAsync(fieldDef);

                if (_fieldService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Field definition updated successfully", 
                        data = result 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _fieldService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error updating field definition", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Delete field definition
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteFieldDefinition(int id)
        {
            try
            {
                var result = await _fieldService.DeleteFieldDefinitionAsync(id);

                if (_fieldService.ErrorID == 0)
                {
                    return Ok(new { 
                        success = true, 
                        message = "Field definition deleted successfully" 
                    });
                }
                else
                {
                    return BadRequest(new { 
                        success = false, 
                        message = _fieldService.ErrorDescription 
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error deleting field definition", 
                    error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Initialize default field definitions for a department
        /// </summary>
        [HttpPost("initialize/{departmentId}")]
        public async Task<ActionResult<object>> InitializeDefaultFields(int departmentId)
        {
            try
            {
                // Create default field definitions based on your image
                var defaultFields = new[]
                {
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "nr_personal",
                        DisplayName = "Nr. personal",
                        FieldType = "text",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 1,
                        Width = 6,
                        CreatedDate = DateTime.UtcNow
                    },
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "alergjite",
                        DisplayName = "Alergjitë",
                        FieldType = "textarea",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 2,
                        Width = 12,
                        CreatedDate = DateTime.UtcNow
                    },
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "grupi_gjakut",
                        DisplayName = "Grupi i gjakut",
                        FieldType = "dropdown",
                        FieldOptions = """["A+","A-","B+","B-","AB+","AB-","O+","O-"]""",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 3,
                        Width = 6,
                        CreatedDate = DateTime.UtcNow
                    },
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "tensioni_gjakut",
                        DisplayName = "Tensioni i gjakut",
                        FieldType = "text",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 4,
                        Width = 6,
                        CreatedDate = DateTime.UtcNow
                    },
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "pulsacionet",
                        DisplayName = "Pulsacionet",
                        FieldType = "number",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 5,
                        Width = 6,
                        CreatedDate = DateTime.UtcNow
                    },
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "historiku_semundjeve",
                        DisplayName = "Historiku i sëmundjeve kardiake në familje",
                        FieldType = "textarea",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 6,
                        Width = 12,
                        CreatedDate = DateTime.UtcNow
                    },
                    new PatientFieldDefinition
                    {
                        DepartmentID = departmentId,
                        FieldKey = "medikamentet",
                        DisplayName = "Medikamentet e përdorura",
                        FieldType = "textarea",
                        IsEnabled = true,
                        IsRequired = false,
                        DisplayOrder = 7,
                        Width = 12,
                        CreatedDate = DateTime.UtcNow
                    }
                };

                var savedFields = new List<PatientFieldDefinition>();
                
                foreach (var field in defaultFields)
                {
                    var saved = await _fieldService.SaveFieldDefinitionAsync(field);
                    if (saved != null)
                    {
                        savedFields.Add(saved);
                    }
                }

                return Ok(new { 
                    success = true, 
                    message = "Default field definitions initialized successfully", 
                    data = savedFields 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = "Error initializing default fields", 
                    error = ex.Message 
                });
            }
        }
    }

    /// <summary>
    /// Request model for updating field definitions
    /// </summary>
    public class UpdateFieldDefinitionRequest
    {
        public int? DepartmentID { get; set; }
        public string FieldKey { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string FieldType { get; set; } = "text";
        public string? FieldOptions { get; set; }
        public bool IsEnabled { get; set; } = true;
        public bool IsRequired { get; set; } = false;
        public int DisplayOrder { get; set; }
        public string? ValidationRules { get; set; }
        public string? HelpText { get; set; }
        public string? CssClass { get; set; }
        public int Width { get; set; } = 12;
        public bool IsVisible { get; set; } = true;
        public bool IsReadOnly { get; set; } = false;
        public int? UpdatedBy { get; set; }
    }
}
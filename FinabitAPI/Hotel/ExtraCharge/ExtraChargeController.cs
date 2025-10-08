using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace FinabitAPI.Hotel.ExtraCharge
{
    // Use ExtraChargeEntity alias to avoid namespace conflicts
    using ExtraChargeEntity = global::ExtraCharge;

    [ApiController]
    [Route("api/[controller]")]
    public class ExtraChargeController : ControllerBase
    {
        private readonly ExtraChargeService _extraChargeService;
        private readonly ILogger<ExtraChargeController> _logger;

        public ExtraChargeController(ExtraChargeService extraChargeService, ILogger<ExtraChargeController> logger)
        {
            _extraChargeService = extraChargeService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new extra charge
        /// </summary>
        /// <param name="extraCharge">Extra charge data</param>
        /// <returns>Created extra charge with result status</returns>
        [HttpPost]
        public IActionResult CreateExtraCharge([FromBody] ExtraChargeEntity extraCharge)
        {
            try
            {
                if (extraCharge == null)
                {
                    _logger.LogWarning("Extra charge data is null");
                    return BadRequest("Extra charge data is required");
                }

                if (string.IsNullOrEmpty(extraCharge.ExtraChargeName))
                {
                    _logger.LogWarning("Extra charge name is required");
                    return BadRequest("Extra charge name is required");
                }

                _logger.LogInformation("Creating extra charge with name: {ExtraChargeName}", extraCharge.ExtraChargeName);
                
                _extraChargeService.Insert(extraCharge);

                if (extraCharge.ErrorID == 0)
                {
                    _logger.LogInformation("Extra charge created successfully");
                    return Ok(new { message = "Extra charge created successfully", extraCharge = extraCharge });
                }
                else
                {
                    _logger.LogWarning("Failed to create extra charge. ErrorID: {ErrorID}, Error: {ErrorDescription}", 
                        extraCharge.ErrorID, extraCharge.ErrorDescription);
                    return BadRequest(new { message = "Failed to create extra charge", error = extraCharge.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating extra charge");
                return StatusCode(500, "Internal server error occurred while creating extra charge");
            }
        }

        /// <summary>
        /// Updates an existing extra charge
        /// </summary>
        /// <param name="id">Extra charge ID</param>
        /// <param name="extraCharge">Updated extra charge data</param>
        /// <returns>Updated extra charge with result status</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateExtraCharge(int id, [FromBody] ExtraChargeEntity extraCharge)
        {
            try
            {
                if (extraCharge == null)
                {
                    _logger.LogWarning("Extra charge data is null");
                    return BadRequest("Extra charge data is required");
                }

                if (id != extraCharge.ExtraChargeID)
                {
                    _logger.LogWarning("ID mismatch: URL ID {UrlId} vs Body ID {BodyId}", id, extraCharge.ExtraChargeID);
                    return BadRequest("ID in URL must match ID in request body");
                }

                if (string.IsNullOrEmpty(extraCharge.ExtraChargeName))
                {
                    _logger.LogWarning("Extra charge name is required");
                    return BadRequest("Extra charge name is required");
                }

                _logger.LogInformation("Updating extra charge with ID: {ExtraChargeID}", id);
                
                _extraChargeService.Update(extraCharge);

                if (extraCharge.ErrorID == 0)
                {
                    _logger.LogInformation("Extra charge updated successfully");
                    return Ok(new { message = "Extra charge updated successfully", extraCharge = extraCharge });
                }
                else
                {
                    _logger.LogWarning("Failed to update extra charge. ErrorID: {ErrorID}, Error: {ErrorDescription}", 
                        extraCharge.ErrorID, extraCharge.ErrorDescription);
                    return BadRequest(new { message = "Failed to update extra charge", error = extraCharge.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating extra charge with ID: {ExtraChargeID}", id);
                return StatusCode(500, "Internal server error occurred while updating extra charge");
            }
        }

        /// <summary>
        /// Deletes an extra charge by ID
        /// </summary>
        /// <param name="id">Extra charge ID to delete</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteExtraCharge(int id)
        {
            try
            {
                _logger.LogInformation("Deleting extra charge with ID: {ExtraChargeID}", id);
                
                var extraCharge = new ExtraChargeEntity { ExtraChargeID = id };
                _extraChargeService.Delete(extraCharge);

                if (extraCharge.ErrorID == 0)
                {
                    _logger.LogInformation("Extra charge deleted successfully");
                    return Ok(new { message = "Extra charge deleted successfully" });
                }
                else
                {
                    _logger.LogWarning("Failed to delete extra charge. ErrorID: {ErrorID}, Error: {ErrorDescription}", 
                        extraCharge.ErrorID, extraCharge.ErrorDescription);
                    return BadRequest(new { message = "Failed to delete extra charge", error = extraCharge.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting extra charge with ID: {ExtraChargeID}", id);
                return StatusCode(500, "Internal server error occurred while deleting extra charge");
            }
        }

        /// <summary>
        /// Gets all extra charges
        /// </summary>
        /// <returns>List of all extra charges</returns>
        [HttpGet]
        public IActionResult GetAllExtraCharges()
        {
            try
            {
                _logger.LogInformation("Getting all extra charges");
                
                var extraCharges = _extraChargeService.SelectAll();
                
                _logger.LogInformation("Retrieved {Count} extra charges", extraCharges.Count);
                return Ok(extraCharges);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all extra charges");
                return StatusCode(500, "Internal server error occurred while getting extra charges");
            }
        }

        /// <summary>
        /// Gets extra charges as DataTable format
        /// </summary>
        /// <returns>Extra charges in DataTable JSON format</returns>
        [HttpGet("table")]
        public IActionResult GetExtraChargesTable()
        {
            try
            {
                _logger.LogInformation("Getting extra charges as DataTable");
                
                var dataTable = _extraChargeService.SelectAllTable();
                
                // Convert DataTable to JSON for proper API response
                var json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                var extraChargeList = JsonConvert.DeserializeObject(json);
                
                _logger.LogInformation("Retrieved extra charges DataTable with {Count} rows", dataTable.Rows.Count);
                return Ok(extraChargeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting extra charges DataTable");
                return StatusCode(500, "Internal server error occurred while getting extra charges table");
            }
        }

        /// <summary>
        /// Gets a specific extra charge by ID
        /// </summary>
        /// <param name="id">Extra charge ID</param>
        /// <returns>Extra charge details</returns>
        [HttpGet("{id}")]
        public IActionResult GetExtraChargeById(int id)
        {
            try
            {
                _logger.LogInformation("Getting extra charge by ID: {ExtraChargeID}", id);
                
                var extraCharge = new ExtraChargeEntity { ExtraChargeID = id };
                var result = _extraChargeService.SelectByID(extraCharge);
                
                if (result != null && result.ExtraChargeID > 0)
                {
                    _logger.LogInformation("Extra charge found with ID: {ExtraChargeID}", id);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Extra charge not found with ID: {ExtraChargeID}", id);
                    return NotFound(new { message = $"Extra charge with ID {id} not found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting extra charge by ID: {ExtraChargeID}", id);
                return StatusCode(500, "Internal server error occurred while getting extra charge");
            }
        }
    }
}

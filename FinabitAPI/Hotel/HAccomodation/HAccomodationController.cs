using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace FinabitAPI.Hotel.HAccomodation
{
    // Use HAccomodationEntity alias to avoid namespace conflicts
    using HAccomodationEntity = global::HAccomodation;

    [ApiController]
    [Route("api/[controller]")]
    public class HAccomodationController : ControllerBase
    {
        private readonly HAccomodationService _accommodationService;
        private readonly ILogger<HAccomodationController> _logger;

        public HAccomodationController(HAccomodationService accommodationService, ILogger<HAccomodationController> logger)
        {
            _accommodationService = accommodationService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new accommodation
        /// </summary>
        /// <param name="accommodation">Accommodation data</param>
        /// <returns>Created accommodation with result status</returns>
        [HttpPost]
        public IActionResult CreateAccommodation([FromBody] HAccomodationEntity accommodation)
        {
            try
            {
                if (accommodation == null)
                {
                    _logger.LogWarning("Accommodation data is null");
                    return BadRequest("Accommodation data is required");
                }

                if (string.IsNullOrEmpty(accommodation.AccomodationName))
                {
                    _logger.LogWarning("Accommodation name is required");
                    return BadRequest("Accommodation name is required");
                }

                _logger.LogInformation("Creating accommodation with name: {AccomodationName}", accommodation.AccomodationName);
                
                _accommodationService.Insert(accommodation);

                if (accommodation.ErrorID == 0)
                {
                    _logger.LogInformation("Accommodation created successfully");
                    return Ok(new { message = "Accommodation created successfully", accommodation = accommodation });
                }
                else
                {
                    _logger.LogWarning("Failed to create accommodation. ErrorID: {ErrorID}, Error: {ErrorDescription}", 
                        accommodation.ErrorID, accommodation.ErrorDescription);
                    return BadRequest(new { message = "Failed to create accommodation", error = accommodation.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating accommodation");
                return StatusCode(500, "Internal server error occurred while creating accommodation");
            }
        }

        /// <summary>
        /// Updates an existing accommodation
        /// </summary>
        /// <param name="id">Accommodation ID</param>
        /// <param name="accommodation">Updated accommodation data</param>
        /// <returns>Updated accommodation with result status</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAccommodation(int id, [FromBody] HAccomodationEntity accommodation)
        {
            try
            {
                if (accommodation == null)
                {
                    _logger.LogWarning("Accommodation data is null");
                    return BadRequest("Accommodation data is required");
                }

                if (id != accommodation.ID)
                {
                    _logger.LogWarning("ID mismatch: URL ID {UrlId} vs Body ID {BodyId}", id, accommodation.ID);
                    return BadRequest("ID in URL must match ID in request body");
                }

                if (string.IsNullOrEmpty(accommodation.AccomodationName))
                {
                    _logger.LogWarning("Accommodation name is required");
                    return BadRequest("Accommodation name is required");
                }

                _logger.LogInformation("Updating accommodation with ID: {ID}", id);
                
                _accommodationService.Update(accommodation);

                if (accommodation.ErrorID == 0)
                {
                    _logger.LogInformation("Accommodation updated successfully");
                    return Ok(new { message = "Accommodation updated successfully", accommodation = accommodation });
                }
                else
                {
                    _logger.LogWarning("Failed to update accommodation. ErrorID: {ErrorID}, Error: {ErrorDescription}", 
                        accommodation.ErrorID, accommodation.ErrorDescription);
                    return BadRequest(new { message = "Failed to update accommodation", error = accommodation.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating accommodation with ID: {ID}", id);
                return StatusCode(500, "Internal server error occurred while updating accommodation");
            }
        }

        /// <summary>
        /// Deletes an accommodation by ID
        /// </summary>
        /// <param name="id">Accommodation ID to delete</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAccommodation(int id)
        {
            try
            {
                _logger.LogInformation("Deleting accommodation with ID: {ID}", id);
                
                var accommodation = new HAccomodationEntity { ID = id };
                _accommodationService.Delete(accommodation);

                if (accommodation.ErrorID == 0)
                {
                    _logger.LogInformation("Accommodation deleted successfully");
                    return Ok(new { message = "Accommodation deleted successfully" });
                }
                else
                {
                    _logger.LogWarning("Failed to delete accommodation. ErrorID: {ErrorID}, Error: {ErrorDescription}", 
                        accommodation.ErrorID, accommodation.ErrorDescription);
                    return BadRequest(new { message = "Failed to delete accommodation", error = accommodation.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting accommodation with ID: {ID}", id);
                return StatusCode(500, "Internal server error occurred while deleting accommodation");
            }
        }

        /// <summary>
        /// Gets all accommodations
        /// </summary>
        /// <returns>List of all accommodations</returns>
        [HttpGet]
        public IActionResult GetAllAccommodations()
        {
            try
            {
                _logger.LogInformation("Getting all accommodations");
                
                var accommodations = _accommodationService.SelectAll();
                
                _logger.LogInformation("Retrieved {Count} accommodations", accommodations.Count);
                return Ok(accommodations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all accommodations");
                return StatusCode(500, "Internal server error occurred while getting accommodations");
            }
        }

        /// <summary>
        /// Gets accommodations as DataTable format
        /// </summary>
        /// <returns>Accommodations in DataTable JSON format</returns>
        [HttpGet("table")]
        public IActionResult GetAccommodationsTable()
        {
            try
            {
                _logger.LogInformation("Getting accommodations as DataTable");
                
                var dataTable = _accommodationService.SelectAllTable();
                
                // Convert DataTable to JSON for proper API response
                var json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                var accommodationList = JsonConvert.DeserializeObject(json);
                
                _logger.LogInformation("Retrieved accommodations DataTable with {Count} rows", dataTable.Rows.Count);
                return Ok(accommodationList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting accommodations DataTable");
                return StatusCode(500, "Internal server error occurred while getting accommodations table");
            }
        }

        /// <summary>
        /// Gets a specific accommodation by ID
        /// </summary>
        /// <param name="id">Accommodation ID</param>
        /// <returns>Accommodation details</returns>
        [HttpGet("{id}")]
        public IActionResult GetAccommodationById(int id)
        {
            try
            {
                _logger.LogInformation("Getting accommodation by ID: {ID}", id);
                
                var accommodation = new HAccomodationEntity { ID = id };
                var result = _accommodationService.SelectByID(accommodation);
                
                if (result != null && result.ID > 0)
                {
                    _logger.LogInformation("Accommodation found with ID: {ID}", id);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Accommodation not found with ID: {ID}", id);
                    return NotFound(new { message = $"Accommodation with ID {id} not found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting accommodation by ID: {ID}", id);
                return StatusCode(500, "Internal server error occurred while getting accommodation");
            }
        }
    }
}

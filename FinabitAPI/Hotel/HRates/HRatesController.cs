using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace FinabitAPI.Hotel.HRates
{
    // Use HRatesEntity alias to avoid namespace conflicts
    using HRatesEntity = global::HRates;

    [ApiController]
    [Route("api/[controller]")]
    public class HRatesController : ControllerBase
    {
        private readonly HRatesService _hRatesService;
        private readonly ILogger<HRatesController> _logger;

        public HRatesController(HRatesService hRatesService, ILogger<HRatesController> logger)
        {
            _hRatesService = hRatesService;
            _logger = logger;
        }

        #region Insert

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] HRatesEntity hRates)
        {
            try
            {
                _logger.LogInformation("HRatesController.Insert called for RateName: {RateName}", hRates.RateName);
                
                if (hRates == null)
                {
                    _logger.LogWarning("HRatesController.Insert received null HRates object");
                    return BadRequest("HRates object is required");
                }

                _hRatesService.Insert(hRates);

                if (hRates.ErrorID == 0)
                {
                    _logger.LogInformation("HRatesController.Insert successful for RateName: {RateName}", hRates.RateName);
                    return Ok(new { success = true, message = "HRates inserted successfully", data = hRates });
                }
                else
                {
                    _logger.LogWarning("HRatesController.Insert failed with ErrorID: {ErrorID}", hRates.ErrorID);
                    return BadRequest(new { success = false, message = hRates.ErrorDescription, errorId = hRates.ErrorID });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesController.Insert");
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion

        #region Update

        [HttpPut("update")]
        public IActionResult Update([FromBody] HRatesEntity hRates)
        {
            try
            {
                _logger.LogInformation("HRatesController.Update called for ID: {ID}", hRates.ID);
                
                if (hRates == null)
                {
                    _logger.LogWarning("HRatesController.Update received null HRates object");
                    return BadRequest("HRates object is required");
                }

                if (hRates.ID <= 0)
                {
                    _logger.LogWarning("HRatesController.Update received invalid ID: {ID}", hRates.ID);
                    return BadRequest("Valid ID is required for update");
                }

                _hRatesService.Update(hRates);

                if (hRates.ErrorID == 0)
                {
                    _logger.LogInformation("HRatesController.Update successful for ID: {ID}", hRates.ID);
                    return Ok(new { success = true, message = "HRates updated successfully", data = hRates });
                }
                else
                {
                    _logger.LogWarning("HRatesController.Update failed with ErrorID: {ErrorID}", hRates.ErrorID);
                    return BadRequest(new { success = false, message = hRates.ErrorDescription, errorId = hRates.ErrorID });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesController.Update for ID: {ID}", hRates.ID);
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion

        #region Delete

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("HRatesController.Delete called for ID: {ID}", id);
                
                if (id <= 0)
                {
                    _logger.LogWarning("HRatesController.Delete received invalid ID: {ID}", id);
                    return BadRequest("Valid ID is required for delete");
                }

                var hRates = new HRatesEntity { ID = id };
                _hRatesService.Delete(hRates);

                if (hRates.ErrorID == 0)
                {
                    _logger.LogInformation("HRatesController.Delete successful for ID: {ID}", id);
                    return Ok(new { success = true, message = "HRates deleted successfully" });
                }
                else
                {
                    _logger.LogWarning("HRatesController.Delete failed with ErrorID: {ErrorID}", hRates.ErrorID);
                    return BadRequest(new { success = false, message = hRates.ErrorDescription, errorId = hRates.ErrorID });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesController.Delete for ID: {ID}", id);
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion

        #region SelectAll

        [HttpGet("list")]
        public IActionResult SelectAll()
        {
            try
            {
                _logger.LogInformation("HRatesController.SelectAll called");
                var result = _hRatesService.SelectAll();
                _logger.LogInformation("HRatesController.SelectAll returned {Count} records", result.Count);
                return Ok(new { success = true, data = result, count = result.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesController.SelectAll");
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion

        #region SelectAllTable

        [HttpGet("table")]
        public IActionResult SelectAllTable()
        {
            try
            {
                _logger.LogInformation("HRatesController.SelectAllTable called");
                var dataTable = _hRatesService.SelectAllTable();
                var json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                _logger.LogInformation("HRatesController.SelectAllTable returned {Count} records", dataTable.Rows.Count);
                return Ok(new { success = true, data = json, count = dataTable.Rows.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesController.SelectAllTable");
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion

        #region SelectByID

        [HttpGet("details/{id}")]
        public IActionResult SelectByID(int id)
        {
            try
            {
                _logger.LogInformation("HRatesController.SelectByID called for ID: {ID}", id);
                
                if (id <= 0)
                {
                    _logger.LogWarning("HRatesController.SelectByID received invalid ID: {ID}", id);
                    return BadRequest("Valid ID is required");
                }

                var hRates = new HRatesEntity { ID = id };
                var result = _hRatesService.SelectByID(hRates);

                if (result != null)
                {
                    _logger.LogInformation("HRatesController.SelectByID found record for ID: {ID}", id);
                    return Ok(new { success = true, data = result });
                }
                else
                {
                    _logger.LogWarning("HRatesController.SelectByID no record found for ID: {ID}", id);
                    return NotFound(new { success = false, message = "HRates not found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesController.SelectByID for ID: {ID}", id);
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion
    }
}

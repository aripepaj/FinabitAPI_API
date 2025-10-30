using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace FinabitAPI.Hotel.HPansion
{
    // Use HPansionEntity alias to avoid namespace conflicts
    using HPansionEntity = global::HPansion;

    [ApiController]
    [Route("api/[controller]")]
    public class HPansionController : ControllerBase
    {
        private readonly HPansionService _hPansionService;
        private readonly ILogger<HPansionController> _logger;

        public HPansionController(HPansionService hPansionService, ILogger<HPansionController> logger)
        {
            _hPansionService = hPansionService;
            _logger = logger;
        }

        #region Insert

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] HPansionEntity hPansion)
        {
            try
            {
                _logger.LogInformation("HPansionController.Insert called for PansionName: {PansionName}", hPansion.PansionName);
                
                if (hPansion == null)
                {
                    _logger.LogWarning("HPansionController.Insert received null HPansion object");
                    return BadRequest("HPansion object is required");
                }

                _hPansionService.Insert(hPansion);

                if (hPansion.ErrorID == 0)
                {
                    _logger.LogInformation("HPansionController.Insert successful for PansionName: {PansionName}", hPansion.PansionName);
                    return Ok(new { success = true, message = "HPansion inserted successfully", data = hPansion });
                }
                else
                {
                    _logger.LogWarning("HPansionController.Insert failed with ErrorID: {ErrorID}", hPansion.ErrorID);
                    return BadRequest(new { success = false, message = hPansion.ErrorDescription, errorId = hPansion.ErrorID });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionController.Insert");
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion

        #region Update

        [HttpPut("update")]
        public IActionResult Update([FromBody] HPansionEntity hPansion)
        {
            try
            {
                _logger.LogInformation("HPansionController.Update called for ID: {ID}", hPansion.ID);
                
                if (hPansion == null)
                {
                    _logger.LogWarning("HPansionController.Update received null HPansion object");
                    return BadRequest("HPansion object is required");
                }

                if (hPansion.ID <= 0)
                {
                    _logger.LogWarning("HPansionController.Update received invalid ID: {ID}", hPansion.ID);
                    return BadRequest("Valid ID is required for update");
                }

                _hPansionService.Update(hPansion);

                if (hPansion.ErrorID == 0)
                {
                    _logger.LogInformation("HPansionController.Update successful for ID: {ID}", hPansion.ID);
                    return Ok(new { success = true, message = "HPansion updated successfully", data = hPansion });
                }
                else
                {
                    _logger.LogWarning("HPansionController.Update failed with ErrorID: {ErrorID}", hPansion.ErrorID);
                    return BadRequest(new { success = false, message = hPansion.ErrorDescription, errorId = hPansion.ErrorID });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionController.Update for ID: {ID}", hPansion.ID);
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
                _logger.LogInformation("HPansionController.Delete called for ID: {ID}", id);
                
                if (id <= 0)
                {
                    _logger.LogWarning("HPansionController.Delete received invalid ID: {ID}", id);
                    return BadRequest("Valid ID is required for delete");
                }

                var hPansion = new HPansionEntity { ID = id };
                _hPansionService.Delete(hPansion);

                if (hPansion.ErrorID == 0)
                {
                    _logger.LogInformation("HPansionController.Delete successful for ID: {ID}", id);
                    return Ok(new { success = true, message = "HPansion deleted successfully" });
                }
                else
                {
                    _logger.LogWarning("HPansionController.Delete failed with ErrorID: {ErrorID}", hPansion.ErrorID);
                    return BadRequest(new { success = false, message = hPansion.ErrorDescription, errorId = hPansion.ErrorID });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionController.Delete for ID: {ID}", id);
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
                _logger.LogInformation("HPansionController.SelectAll called");
                var result = _hPansionService.SelectAll();
                _logger.LogInformation("HPansionController.SelectAll returned {Count} records", result.Count);
                return Ok(new { success = true, data = result, count = result.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionController.SelectAll");
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
                _logger.LogInformation("HPansionController.SelectAllTable called");
                var dataTable = _hPansionService.SelectAllTable();
                var json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                _logger.LogInformation("HPansionController.SelectAllTable returned {Count} records", dataTable.Rows.Count);
                return Ok(new { success = true, data = json, count = dataTable.Rows.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionController.SelectAllTable");
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
                _logger.LogInformation("HPansionController.SelectByID called for ID: {ID}", id);
                
                if (id <= 0)
                {
                    _logger.LogWarning("HPansionController.SelectByID received invalid ID: {ID}", id);
                    return BadRequest("Valid ID is required");
                }

                var hPansion = new HPansionEntity { ID = id };
                var result = _hPansionService.SelectByID(hPansion);

                if (result != null)
                {
                    _logger.LogInformation("HPansionController.SelectByID found record for ID: {ID}", id);
                    return Ok(new { success = true, data = result });
                }
                else
                {
                    _logger.LogWarning("HPansionController.SelectByID no record found for ID: {ID}", id);
                    return NotFound(new { success = false, message = "HPansion not found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionController.SelectByID for ID: {ID}", id);
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        #endregion
    }
}

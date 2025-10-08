using Microsoft.AspNetCore.Mvc;
using System.Data;
using FinabitAPI.Finabit.VATPercent;
using VATPercentEntity = VATPercent;

namespace FinabitAPI.Finabit.VATPercent
{
    [Route("api/[controller]")]
    [ApiController]
    public class VATPercentController : ControllerBase
    {
        private readonly VATPercentService _service;
        private readonly ILogger<VATPercentController> _logger;

        public VATPercentController(VATPercentService service, ILogger<VATPercentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        private object ConvertDataTableToJson(DataTable dataTable)
        {
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow row in dataTable.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }
                rows.Add(dict);
            }
            return rows;
        }

        /// <summary>
        /// Insert a new VAT percent
        /// </summary>
        /// <param name="vatPercent">VAT percent object</param>
        /// <returns>Success or error result</returns>
        [HttpPost]
        public IActionResult Insert([FromBody] VATPercentEntity vatPercent)
        {
            try
            {
                if (vatPercent == null)
                {
                    return BadRequest(new { message = "VAT percent data is required" });
                }

                _service.Insert(vatPercent);

                if (vatPercent.ErrorID == 0)
                {
                    return Ok(new { message = "VAT percent inserted successfully", vatPercent });
                }
                else
                {
                    return BadRequest(new { message = "Failed to insert VAT percent", error = vatPercent.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting VAT percent");
                return StatusCode(500, new { message = "An error occurred while inserting VAT percent", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing VAT percent
        /// </summary>
        /// <param name="vatPercent">VAT percent object with updated data</param>
        /// <returns>Success or error result</returns>
        [HttpPut]
        public IActionResult Update([FromBody] VATPercentEntity vatPercent)
        {
            try
            {
                if (vatPercent == null)
                {
                    return BadRequest(new { message = "VAT percent data is required" });
                }

                if (vatPercent.VATID <= 0)
                {
                    return BadRequest(new { message = "Valid VAT ID is required" });
                }

                _service.Update(vatPercent);

                if (vatPercent.ErrorID == 0)
                {
                    return Ok(new { message = "VAT percent updated successfully", vatPercent });
                }
                else
                {
                    return BadRequest(new { message = "Failed to update VAT percent", error = vatPercent.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating VAT percent");
                return StatusCode(500, new { message = "An error occurred while updating VAT percent", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a VAT percent
        /// </summary>
        /// <param name="id">VAT ID to delete</param>
        /// <returns>Success or error result</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Valid VAT ID is required" });
                }

                var vatPercent = new VATPercentEntity { VATID = id };
                _service.Delete(vatPercent);

                if (vatPercent.ErrorID == 0)
                {
                    return Ok(new { message = "VAT percent deleted successfully" });
                }
                else
                {
                    return BadRequest(new { message = "Failed to delete VAT percent", error = vatPercent.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting VAT percent with ID: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while deleting VAT percent", error = ex.Message });
            }
        }

        /// <summary>
        /// Get all VAT percents
        /// </summary>
        /// <returns>List of VAT percents</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _service.SelectAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all VAT percents");
                return StatusCode(500, new { message = "An error occurred while getting VAT percents", error = ex.Message });
            }
        }

        /// <summary>
        /// Get all VAT percents as table format
        /// </summary>
        /// <returns>VAT percents as JSON table</returns>
        [HttpGet("table")]
        public IActionResult GetAllTable()
        {
            try
            {
                var dataTable = _service.SelectAllTable();
                var json = ConvertDataTableToJson(dataTable);
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting VAT percents table");
                return StatusCode(500, new { message = "An error occurred while getting VAT percents table", error = ex.Message });
            }
        }

        /// <summary>
        /// Get VAT percent by ID
        /// </summary>
        /// <param name="id">VAT ID</param>
        /// <returns>VAT percent object</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Valid VAT ID is required" });
                }

                var vatPercent = new VATPercentEntity { VATID = id };
                var result = _service.SelectByID(vatPercent);

                if (result != null && result.VATID > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(new { message = "VAT percent not found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting VAT percent by ID: {Id}", id);
                return StatusCode(500, new { message = "An error occurred while getting VAT percent", error = ex.Message });
            }
        }
    }
}
using System.Data;
using FinabitAPI.Finabit.SystemData;
using FinabitAPI.Finabit.SystemInfo.dto;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Finabit.SystemData
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemDataController : ControllerBase
    {
        private readonly SystemDataService _service;
        private readonly ILogger<SystemDataController> _logger;

        public SystemDataController(SystemDataService service, ILogger<SystemDataController> logger)
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
        /// Get system data configuration
        /// </summary>
        /// <returns>System data object</returns>
        [HttpGet]
        public IActionResult GetSystemData()
        {
            try
            {
                var result = _service.ListSystemData();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system data");
                return StatusCode(
                    500,
                    new
                    {
                        message = "An error occurred while getting system data",
                        error = ex.Message,
                    }
                );
            }
        }

        /// <summary>
        /// Get system data for transaction processing
        /// </summary>
        /// <returns>System data object for transactions</returns>
        [HttpGet("transaction")]
        public IActionResult GetSystemDataForTransaction()
        {
            try
            {
                var result = _service.ListSystemDataForTran();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system data for transaction");
                return StatusCode(
                    500,
                    new
                    {
                        message = "An error occurred while getting system data for transaction",
                        error = ex.Message,
                    }
                );
            }
        }

        /// <summary>
        /// Get system data as table format
        /// </summary>
        /// <returns>System data as JSON table</returns>
        [HttpGet("table")]
        public IActionResult GetSystemDataTable()
        {
            try
            {
                var dataTable = _service.ListSystemDataTable();
                var json = ConvertDataTableToJson(dataTable);
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system data table");
                return StatusCode(
                    500,
                    new
                    {
                        message = "An error occurred while getting system data table",
                        error = ex.Message,
                    }
                );
            }
        }

        [HttpGet("getAll")]
        public ActionResult<SystemBaseInfo> GetBasicSystemInfo()
        {
            var info = _service.getAllData();

            if (info == null)
                return NotFound();

            return Ok(info);
        }
    }
}

//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Hotel Agencies REST API operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HAgencies
{
    [ApiController]
    [Route("api/[controller]")]
    public class HAgenciesController : ControllerBase
    {
        private readonly DBAccess _dbAccess;

        public HAgenciesController(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        /// <summary>
        /// Helper method to convert DataTable to JSON-serializable format
        /// </summary>
        private object ConvertDataTableToJson(DataTable dataTable)
        {
            var rows = new List<Dictionary<string, object?>>();
            foreach (DataRow row in dataTable.Rows)
            {
                var dict = new Dictionary<string, object?>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }
                rows.Add(dict);
            }
            return rows;
        }

        #region Insert

        /// <summary>
        /// Insert a new agency
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var service = new HAgenciesService(_dbAccess);
                service.Insert(hAgency);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, errorId = service.ErrorID, id = hAgency.ID });
                }
                else
                {
                    return BadRequest(new { success = false, errorId = service.ErrorID, errorDescription = service.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update an existing agency
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var service = new HAgenciesService(_dbAccess);
                service.Update(hAgency);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, errorId = service.ErrorID });
                }
                else
                {
                    return BadRequest(new { success = false, errorId = service.ErrorID, errorDescription = service.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete an agency
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var service = new HAgenciesService(_dbAccess);
                service.Delete(hAgency);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, errorId = service.ErrorID });
                }
                else
                {
                    return BadRequest(new { success = false, errorId = service.ErrorID, errorDescription = service.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region SelectAll

        /// <summary>
        /// Get all agencies
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<FinabitAPI.Hotel.HAgencies.HAgencies>> SelectAll()
        {
            try
            {
                var service = new HAgenciesService(_dbAccess);
                var agencies = service.SelectAll();

                if (service.ErrorID == 0)
                {
                    return Ok(agencies);
                }
                else
                {
                    return BadRequest(new { success = false, errorId = service.ErrorID, errorDescription = service.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region SelectAllTable

        /// <summary>
        /// Get all agencies as DataTable
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable()
        {
            try
            {
                var service = new HAgenciesService(_dbAccess);
                var table = service.SelectAllTable();

                if (service.ErrorID == 0)
                {
                    return Ok(ConvertDataTableToJson(table));
                }
                else
                {
                    return BadRequest(new { success = false, errorId = service.ErrorID, errorDescription = service.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region SelectByID

        /// <summary>
        /// Get agency by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<FinabitAPI.Hotel.HAgencies.HAgencies> SelectByID([FromBody] FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var service = new HAgenciesService(_dbAccess);
                var result = service.SelectByID(hAgency);

                if (service.ErrorID == 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { success = false, errorId = service.ErrorID, errorDescription = service.ErrorDescription });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}

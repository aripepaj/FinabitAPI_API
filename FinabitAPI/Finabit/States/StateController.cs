//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for State REST API operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Finabit.States
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly DBAccess _dbAccess;

        public StateController(DBAccess dbAccess)
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
        /// Insert a new state
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] State state)
        {
            try
            {
                var service = new StateService(_dbAccess);
                service.Insert(state);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, errorId = service.ErrorID, id = state.ID });
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
        /// Update an existing state
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] State state)
        {
            try
            {
                var service = new StateService(_dbAccess);
                service.Update(state);

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
        /// Delete a state
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] State state)
        {
            try
            {
                var service = new StateService(_dbAccess);
                service.Delete(state);

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
        /// Get all states
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<State>> SelectAll()
        {
            try
            {
                var service = new StateService(_dbAccess);
                var states = service.SelectAll();

                if (service.ErrorID == 0)
                {
                    return Ok(states);
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
        /// Get all states as DataTable
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable()
        {
            try
            {
                var service = new StateService(_dbAccess);
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
        /// Get state by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<State> SelectByID([FromBody] State state)
        {
            try
            {
                var service = new StateService(_dbAccess);
                var result = service.SelectByID(state);

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

//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Hotel Event Details REST API operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.EventDetails
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventDetailsController : ControllerBase
    {
        private readonly DBAccess _dbAccess;

        public EventDetailsController(DBAccess dbAccess)
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
        /// Insert a new event detail
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                service.Insert(eventDetail);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, errorId = service.ErrorID, id = eventDetail.ID });
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
        /// Update an existing event detail
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                service.Update(eventDetail);

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
        /// Delete an event detail
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                service.Delete(eventDetail);

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
        /// Get all event details
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<FinabitAPI.Hotel.EventDetails.EventsDetails>> SelectAll()
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                var eventDetails = service.SelectAll();

                if (service.ErrorID == 0)
                {
                    return Ok(eventDetails);
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
        /// Get all event details as DataTable
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable()
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
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
        /// Get event detail by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<FinabitAPI.Hotel.EventDetails.EventsDetails> SelectByID([FromBody] FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                var result = service.SelectByID(eventDetail);

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

        #region SelectByIDTable

        /// <summary>
        /// Get event details by Event ID as DataTable
        /// </summary>
        [HttpGet("SelectByIDTable/{id}")]
        public ActionResult<object> SelectByIDTable(int id)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                var table = service.SelectByIDTable(id);

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

        #region SelectAllByEventID

        /// <summary>
        /// Get all event details by Event ID
        /// </summary>
        [HttpGet("SelectAllByEventID/{id}")]
        public ActionResult<List<FinabitAPI.Hotel.EventDetails.EventsDetails>> SelectAllByEventID(int id)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                var eventDetails = service.SelectAllByEventID(id);

                if (service.ErrorID == 0)
                {
                    return Ok(eventDetails);
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

        #region GetAcomodationValueForEvent

        /// <summary>
        /// Get accommodation value for event
        /// </summary>
        [HttpGet("GetAcomodationValueForEvent/{id}")]
        public ActionResult<object> GetAcomodationValueForEvent(int id)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                var table = service.GetAcomodationValueForEvent(id);

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

        #region GetEventProfitability

        /// <summary>
        /// Get event profitability data
        /// </summary>
        [HttpPost("GetEventProfitability")]
        public ActionResult<object> GetEventProfitability([FromBody] EventProfitabilityRequest request)
        {
            try
            {
                var service = new EventDetailsService(_dbAccess);
                var table = service.GetEventProfitability(request.Transactions);

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
    }

    /// <summary>
    /// Request model for GetEventProfitability operation
    /// </summary>
    public class EventProfitabilityRequest
    {
        public DataTable Transactions { get; set; } = new DataTable();
    }
}

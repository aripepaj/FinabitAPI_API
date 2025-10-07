//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Hotel Extra Charge Details CRUD operations and management
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Hotel.Reservations;
using System.Data;

namespace FinabitAPI.Hotel.ExtraChargeDetails
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraChargeDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public ExtraChargeDetailsController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
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

        #region CRUD Operations

        /// <summary>
        /// Insert a new extra charge detail
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.Insert(extraChargeDetail);

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

        /// <summary>
        /// Update an existing extra charge detail
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.Update(extraChargeDetail);

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

        /// <summary>
        /// Delete an extra charge detail
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.Delete(extraChargeDetail);

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

        /// <summary>
        /// Get all extra charge details
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<ExtraChargeDetails>> SelectAll()
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                var result = service.SelectAll();

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

        /// <summary>
        /// Get extra charge details as table data by reservation ID
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable([FromQuery] int reservationID)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                var table = service.SelectAllTable(reservationID);

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

        /// <summary>
        /// Get extra charge detail by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<ExtraChargeDetails> SelectByID([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                var result = service.SelectByID(extraChargeDetail);

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

        #region Check-In Operations

        /// <summary>
        /// Get extra charge details for check-in process
        /// </summary>
        [HttpGet("SelectExDetailsCheckIN")]
        public ActionResult<object> SelectExDetailsCheckIN()
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                var table = service.SelectExDetailsCheckIN();

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

        #region Business Logic Operations

        /// <summary>
        /// Insert accommodation charges for a reservation
        /// </summary>
        [HttpPost("InsertAccomodationInExtraCharge")]
        public ActionResult<object> InsertAccomodationInExtraCharge([FromBody] HReservation reservation)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.InsertAccomodationInExtraCharge(reservation);

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

        #region Folio Management Operations

        /// <summary>
        /// Get extra charge details by folio ID with options
        /// </summary>
        [HttpGet("SelectByFolioID")]
        public ActionResult<object> SelectByFolioID(
            [FromQuery] int folioID, 
            [FromQuery] int reservationId, 
            [FromQuery] bool forInformationInvoice = false)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                var table = service.SelectByFolioID(folioID, reservationId, forInformationInvoice);

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

        /// <summary>
        /// Update folio ID for extra charge detail
        /// </summary>
        [HttpPut("UpdateFolioID")]
        public ActionResult<object> UpdateFolioID([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.UpdateFolioID(extraChargeDetail);

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

        /// <summary>
        /// Bulk update folio IDs for room charges
        /// </summary>
        [HttpPut("UpdateFolioIDRooms")]
        public ActionResult<object> UpdateFolioIDRooms([FromBody] UpdateFolioIDRoomsRequest request)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.UpdateFolioIDRooms(request.DataTable, request.NewReservationId, request.NewHFolioID);

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

        /// <summary>
        /// Update folio ID without transaction
        /// </summary>
        [HttpPut("UpdateFolioIDNeTransaction")]
        public ActionResult<object> UpdateFolioIDNeTransaction([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.UpdateFolioIDNeTransaction(extraChargeDetail);

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

        /// <summary>
        /// Update both folio ID and reservation ID
        /// </summary>
        [HttpPut("UpdateFolioIDAndReservationID")]
        public ActionResult<object> UpdateFolioIDAndReservationID([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.UpdateFolioIDAndReservationID(extraChargeDetail);

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

        /// <summary>
        /// Update both folio ID and reservation ID without transaction
        /// </summary>
        [HttpPut("UpdateFolioIDAndReservationIdNeTransaction")]
        public ActionResult<object> UpdateFolioIDAndReservationIdNeTransaction([FromBody] ExtraChargeDetails extraChargeDetail)
        {
            try
            {
                var service = new ExtraChargeDetailsService(_dbAccess);
                service.UpdateFolioIDAndReservationIdNeTransaction(extraChargeDetail);

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
    }

    /// <summary>
    /// Request model for UpdateFolioIDRooms operation
    /// </summary>
    public class UpdateFolioIDRoomsRequest
    {
        public DataTable DataTable { get; set; } = new DataTable();
        public int NewReservationId { get; set; }
        public int NewHFolioID { get; set; }
    }
}

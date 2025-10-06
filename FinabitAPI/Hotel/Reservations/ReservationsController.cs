using Finabit_API.Models;
using FinabitAPI.Service;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinabitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public ReservationsController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
        }

        #region CRUD Operations

        /// <summary>
        /// Insert a new hotel reservation
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<int> Insert([FromBody] HReservation reservation)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                int result = service.Insert(reservation);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, id = reservation.ID, errorId = service.ErrorID });
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
        /// Update an existing hotel reservation
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<int> Update([FromBody] HReservation reservation)
        {
            try
            {
                var service = new ReservationsService( _dbAccess);
                int result = service.Update(reservation);

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
        /// Update reservation status
        /// </summary>
        [HttpPut("UpdateStatus")]
        public ActionResult<int> UpdateStatus([FromBody] HReservation reservation)
        {
            try
            {
                var service = new ReservationsService( _dbAccess);
                int result = service.UpdateStatus(reservation);

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
        /// Delete a hotel reservation
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<int> Delete([FromBody] HReservation reservation)
        {
            try
            {
                var service = new ReservationsService( _dbAccess);
                int result = service.Delete(reservation);

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

        #region Select Methods

        /// <summary>
        /// Get all reservations
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<HReservation>> SelectAll()
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var reservations = service.SelectAll();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservations with filtering options
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<DataTable> SelectAllTable(
            [FromQuery] string reservationStatusId = "",
            [FromQuery] string dateFilteringModel = "",
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var from = fromDate ?? DateTime.Now.AddMonths(-1);
                var to = toDate ?? DateTime.Now;

                var service = new ReservationsService(_dbAccess);
                var dataTable = service.SelectAllTable(reservationStatusId, dateFilteringModel, from, to);
                return Ok(dataTable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation by ID
        /// </summary>
        [HttpGet("SelectByID/{id}")]
        public ActionResult<HReservation> SelectByID(int id)
        {
            try
            {
                var reservation = new HReservation { ID = id };
                var service = new ReservationsService(_dbAccess);
                var result = service.SelectByID(reservation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation by booking ID
        /// </summary>
        [HttpGet("SelectByBookingID/{subBookingId}")]
        public ActionResult<HReservation> SelectByBookingID(string subBookingId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.SelectByBookingID(subBookingId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation data table by ID
        /// </summary>
        [HttpGet("SelectDataTableByID/{id}")]
        public ActionResult<DataTable> SelectDataTableByID(int id)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.SelectDataTableByID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Gantt Chart Methods

        /// <summary>
        /// Get all reservations for Gantt chart
        /// </summary>
        [HttpGet("SelectAllForGantt")]
        public ActionResult<List<HReservation>> SelectAllForGantt()
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var reservations = service.SelectAllForGantt();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservations table for Gantt chart with filters
        /// </summary>
        [HttpGet("SelectAllTableForGantt")]
        public ActionResult<DataTable> SelectAllTableForGantt(
            [FromQuery] string floorId = "",
            [FromQuery] string roomTypeId = "",
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var from = fromDate ?? DateTime.Now;
                var to = toDate ?? DateTime.Now.AddMonths(1);

                var service = new ReservationsService(_dbAccess);
                var result = service.SelectAllTableForGantt(floorId, roomTypeId, from, to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Room Related Methods

        /// <summary>
        /// Get reservations by room ID
        /// </summary>
        [HttpGet("GetReservationsByRoomID/{roomId}")]
        public ActionResult<List<HReservation>> GetReservationsByRoomID(int roomId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var reservations = service.GetReservationsByRoomID(roomId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Search for available rooms
        /// </summary>
        [HttpGet("SelectAllTableForRoomSearcher")]
        public ActionResult<DataTable> SelectAllTableForRoomSearcher(
            [FromQuery] string floorId = "",
            [FromQuery] string roomTypeId = "",
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            try
            {
                var from = fromDate ?? DateTime.Now;
                var to = toDate ?? DateTime.Now.AddDays(1);

                var service = new ReservationsService(_dbAccess);
                var result = service.SelectAllTableForRoomSearcher(floorId, roomTypeId, from, to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation status for a specific room on a specific date
        /// </summary>
        [HttpGet("GetReservationStatusForRoom")]
        public ActionResult<int> GetReservationStatusForRoom([FromQuery] int roomId, [FromQuery] DateTime date)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var status = service.GetReservationStatusForRoom(roomId, date);
                return Ok(new { roomId, date, status });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation for a specific room number
        /// </summary>
        [HttpGet("GetReservationForRoom/{roomNo}")]
        public ActionResult<DataTable> GetReservationForRoom(string roomNo)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GetReservationForRoom(roomNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Transaction Related Methods

        /// <summary>
        /// Update transaction ID for a reservation
        /// </summary>
        [HttpPut("UpdateTranID")]
        public ActionResult UpdateTranID([FromQuery] int id, [FromQuery] int tranId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                service.UpdateTranID(id, tranId);
                return Ok(new { success = true, message = "Transaction ID updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get transaction ID for a reservation
        /// </summary>
        [HttpGet("GetTransactionIDForReservation/{reservationId}")]
        public ActionResult<int> GetTransactionIDForReservation(int reservationId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var transactionId = service.GetTransactionIDForReservation(reservationId);
                return Ok(new { reservationId, transactionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Delete transaction details for a reservation
        /// </summary>
        [HttpDelete("DeleteTransactionDetailsForReservation/{transactionId}")]
        public ActionResult DeleteTransactionDetailsForReservation(int transactionId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                service.DeleteTransactionDetailsForReservation(transactionId);
                return Ok(new { success = true, message = "Transaction details deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Delete transactions for a reservation
        /// </summary>
        [HttpDelete("DeleteTransactionsForReservation/{reservationId}")]
        public ActionResult DeleteTransactionsForReservation(int reservationId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                service.DeleteTransactionsForReservation(reservationId);
                return Ok(new { success = true, message = "Transactions deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Card and POS Methods

        /// <summary>
        /// Get reservation from card PIN
        /// </summary>
        [HttpGet("GetReservationFromCard/{pin}")]
        public ActionResult<DataTable> GetReservationFromCard(string pin)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GetReservationFromCard(pin);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation sales from POS
        /// </summary>
        [HttpGet("GETReservationSalesFromPOS")]
        public ActionResult<DataTable> GETReservationSalesFromPOS(
            [FromQuery] int hReservationId,
            [FromQuery] int hFolioId,
            [FromQuery] bool isGrouped = false)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GETReservationSalesFromPOS(hReservationId, hFolioId, isGrouped);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Pricing Methods

        /// <summary>
        /// Get price information for room booking
        /// </summary>
        [HttpGet("GetPrice")]
        public ActionResult<DataTable> GetPrice(
            [FromQuery] int tariffId,
            [FromQuery] int roomTypeId,
            [FromQuery] string source = "",
            [FromQuery] string season = "",
            [FromQuery] DateTime? date = null)
        {
            try
            {
                var bookingDate = date ?? DateTime.Now;
                var service = new ReservationsService(_dbAccess);
                var result = service.GetPrice(tariffId, roomTypeId, source, season, bookingDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Guest and Report Methods

        /// <summary>
        /// Get guest list
        /// </summary>
        [HttpGet("GetGuestList")]
        public ActionResult<DataTable> GetGuestList()
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GetGuestList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get daily hotel report
        /// </summary>
        [HttpGet("GetHDailyReport")]
        public ActionResult<DataTable> GetHDailyReport([FromQuery] DateTime? date = null)
        {
            try
            {
                var reportDate = date ?? DateTime.Now;
                var service = new ReservationsService(_dbAccess);
                var result = service.GetHDailyReport(reportDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservation data for check-in print
        /// </summary>
        [HttpGet("SelectResByIDForCheckInPrint/{resId}")]
        public ActionResult<DataTable> SelectResByIDForCheckInPrint(int resId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.SelectResByIDForCheckInPrint(resId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Group and Event Methods

        /// <summary>
        /// Get group reservation schema
        /// </summary>
        [HttpGet("GetGroupReservationSchema")]
        public ActionResult<DataTable> GetGroupReservationSchema()
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GetGroupReservationSchema();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get last group ID
        /// </summary>
        [HttpGet("GetLastGroupID")]
        public ActionResult<int> GetLastGroupID()
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GetLastGroupID();
                return Ok(new { lastGroupId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get active events
        /// </summary>
        [HttpGet("GetActiveEvents")]
        public ActionResult<DataTable> GetActiveEvents()
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.GetActiveEvents();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update event detail ID for reservations
        /// </summary>
        [HttpPut("UpdateEventDetailID")]
        public ActionResult UpdateEventDetailID([FromBody] DataTable eventDetails)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                service.UpdateEventDetailID(eventDetails);
                return Ok(new { success = true, message = "Event details updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get reservations table for specific event
        /// </summary>
        [HttpGet("SelectAllTableForEvents/{eventId}")]
        public ActionResult<DataTable> SelectAllTableForEvents(int eventId)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                var result = service.SelectAllTableForEvents(eventId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update accommodation value for an event
        /// </summary>
        [HttpPut("AzhuroVlerenEAkomodimit")]
        public ActionResult AzhuroVlerenEAkomodimit([FromQuery] int eventDetailId, [FromQuery] decimal value)
        {
            try
            {
                var service = new ReservationsService(_dbAccess);
                service.AzhuroVlerenEAkomodimit(eventDetailId, value);
                return Ok(new { success = true, message = "Accommodation value updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #endregion
    }
}

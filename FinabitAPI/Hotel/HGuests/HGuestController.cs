//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Hotel Guest CRUD operations and reports
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;
using System.Data;

namespace FinabitAPI.Hotel.HGuests
{
    [ApiController]
    [Route("api/[controller]")]
    public class HGuestController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public HGuestController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
        }

        #region CRUD Operations

        /// <summary>
        /// Insert a new hotel guest
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] HGuest guest)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                service.Insert(guest);

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
        /// Update an existing hotel guest
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] HGuest guest)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                service.Update(guest);

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
        /// Delete a hotel guest
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] HGuest guest)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                service.Delete(guest);

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
        /// Get all hotel guests
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<HGuest>> SelectAll()
        {
            try
            {
                var service = new HGuestService(_dbAccess);
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
        /// Get all hotel guests as table data
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable()
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.SelectAllTable();

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get hotel guest by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<HGuest> SelectByID([FromBody] HGuest guest)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var result = service.SelectByID(guest);

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

        #region Hotel Reports

        /// <summary>
        /// Get in-house guest list for a specific date
        /// </summary>
        [HttpGet("HInHouseList")]
        public ActionResult<object> HInHouseList([FromQuery] DateTime date, [FromQuery] bool include = false)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.HInHouseList(date, include);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get arrival list for a date range
        /// </summary>
        [HttpGet("HArrivalList")]
        public ActionResult<object> HArrivalList(
            [FromQuery] DateTime fromDate, 
            [FromQuery] DateTime toDate, 
            [FromQuery] string origin = "", 
            [FromQuery] string roomType = "", 
            [FromQuery] bool include = false)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.HArrivalList(fromDate, toDate, origin, roomType, include);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get departure list for a date range
        /// </summary>
        [HttpGet("HDepartureList")]
        public ActionResult<object> HDepartureList(
            [FromQuery] DateTime fromDate, 
            [FromQuery] DateTime toDate, 
            [FromQuery] string roomType = "", 
            [FromQuery] string origin = "", 
            [FromQuery] bool include = false)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.HDepartureList(fromDate, toDate, roomType, origin, include);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get reservation list for a date range
        /// </summary>
        [HttpGet("HReservationList")]
        public ActionResult<object> HReservationList(
            [FromQuery] DateTime fromDate, 
            [FromQuery] DateTime toDate, 
            [FromQuery] string originID = "")
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.HReservationList(fromDate, toDate, originID);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get client realisation list for a date range
        /// </summary>
        [HttpGet("HClientRealisationList")]
        public ActionResult<object> HClientRealisationList(
            [FromQuery] DateTime fromDate, 
            [FromQuery] DateTime toDate, 
            [FromQuery] string originID = "")
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.HClientRealisationList(fromDate, toDate, originID);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get client realisation list grouped for a date range
        /// </summary>
        [HttpGet("HClientRealisationList_Group")]
        public ActionResult<object> HClientRealisationList_Group(
            [FromQuery] DateTime fromDate, 
            [FromQuery] DateTime toDate, 
            [FromQuery] string originID = "")
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.HClientRealisationList_Group(fromDate, toDate, originID);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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

        #region Financial Reports

        /// <summary>
        /// Get extra charge list for a date range
        /// </summary>
        [HttpGet("ExtraChargeList")]
        public ActionResult<object> ExtraChargeList(
            [FromQuery] DateTime fromDate, 
            [FromQuery] DateTime toDate, 
            [FromQuery] string origin = "", 
            [FromQuery] string roomType = "")
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.ExtraChargeList(fromDate, toDate, origin, roomType);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get client balance by date
        /// </summary>
        [HttpGet("ClientBalanceByDate")]
        public ActionResult<object> ClientBalanceByDate([FromQuery] string fromDate)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.ClientBalanceByDate(fromDate);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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

        #region Room Management Reports

        /// <summary>
        /// Get month room statuses for a date range
        /// </summary>
        [HttpGet("MonthRoomStatuses")]
        public ActionResult<object> MonthRoomStatuses([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.MonthRoomStatuses(fromDate, toDate);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get room type statuses for a date range
        /// </summary>
        [HttpGet("RoomTypeStatuses")]
        public ActionResult<object> RoomTypeStatuses([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.RoomTypeStatuses(fromDate, toDate);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get room type statuses per period
        /// </summary>
        [HttpGet("RoomTypeStatuses_PerPeriod")]
        public ActionResult<object> RoomTypeStatuses_PerPeriod([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.RoomTypeStatuses_PerPeriod(fromDate, toDate);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get free rooms per period
        /// </summary>
        [HttpGet("GetFreeRooms_PerPeriod")]
        public ActionResult<object> GetFreeRooms_PerPeriod([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.GetFreeRooms_PerPeriod(fromDate, toDate);

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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

        #region Search Operations

        /// <summary>
        /// Search guests by name
        /// </summary>
        [HttpGet("SelectAllTableBySearch")]
        public ActionResult<object> SelectAllTableBySearch([FromQuery] string name)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var table = service.SelectAllTableBySearch(name ?? "");

                if (service.ErrorID == 0)
                {
                    return Ok(table);
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
        /// Get guest by full name
        /// </summary>
        [HttpGet("SelectByFullName")]
        public ActionResult<HGuest> SelectByFullName([FromQuery] string fullName)
        {
            try
            {
                var service = new HGuestService(_dbAccess);
                var result = service.SelectByFullName(fullName ?? "");

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

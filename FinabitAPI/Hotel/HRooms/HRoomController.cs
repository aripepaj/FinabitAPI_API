//-- =============================================
//-- Author:		Generated
//-- Create date: 06.10.25 
//-- Description:	Controller for Hotel Rooms CRUD operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;

namespace FinabitAPI.Hotel.HRooms
{
    [ApiController]
    [Route("api/[controller]")]
    public class HRoomController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public HRoomController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
        }

        #region CRUD Operations

        /// <summary>
        /// Insert a new hotel room
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<int> Insert([FromBody] HRoom room)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                int result = service.Insert(room);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, id = room.ID, errorId = service.ErrorID });
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
        /// Update an existing hotel room
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<int> Update([FromBody] HRoom room)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                int result = service.Update(room);

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
        /// Delete a hotel room
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<int> Delete([FromBody] HRoom room)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                int result = service.Delete(room);

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
        /// Get all hotel rooms for a specific date and department
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<HRoom>> SelectAll([FromQuery] DateTime date, [FromQuery] int departmentId)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var rooms = service.SelectAll(date, departmentId);
                
                if (service.ErrorID == 0)
                {
                    return Ok(rooms);
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
        /// Get all hotel rooms for a specific date (alternative version)
        /// </summary>
        [HttpGet("SelectAll_3")]
        public ActionResult<List<HRoom>> SelectAll_3([FromQuery] DateTime date)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var rooms = service.SelectAll_3(date);
                
                if (service.ErrorID == 0)
                {
                    return Ok(rooms);
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
        /// Get room by name and date
        /// </summary>
        [HttpGet("SelectByName_Date")]
        public ActionResult<HRoom> SelectByName_Date([FromQuery] string roomName, [FromQuery] DateTime date)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var room = service.SelectByName_Date(roomName, date);
                
                if (service.ErrorID == 0)
                {
                    return Ok(room);
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
        /// Get all rooms as DataTable for a specific date
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable([FromQuery] DateTime date)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var table = service.SelectAllTable(date);
                
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
        /// Get room history as DataTable by room ID
        /// </summary>
        [HttpGet("SelectAllTableHistory")]
        public ActionResult<object> SelectAllTableHistory([FromQuery] int roomId)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var table = service.SelectAllTableHistory(roomId);
                
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
        /// Get room by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<HRoom> SelectByID([FromBody] HRoom room)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var result = service.SelectByID(room);
                
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

        #region Update Operations

        /// <summary>
        /// Update room position
        /// </summary>
        [HttpPut("UpdatePosition")]
        public ActionResult<int> UpdatePosition([FromQuery] int roomId, [FromQuery] int positionX, [FromQuery] int positionY)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                int result = service.UpdatePosition(roomId, positionX, positionY);
                
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
        /// Set room out of order status
        /// </summary>
        [HttpPut("SetOutOfOrder")]
        public ActionResult<int> SetOutOfOrder([FromQuery] int roomId, [FromQuery] int outOfOrder)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                int result = service.SetOutOfOrder(roomId, outOfOrder);
                
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

        #region Business Logic Operations

        /// <summary>
        /// Get reservation for a specific room and date
        /// </summary>
        [HttpGet("GetReservationForRoom")]
        public ActionResult<int> GetReservationForRoom([FromQuery] int roomId, [FromQuery] DateTime date)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var reservationId = service.GetReservationForRoom(roomId, date);
                
                if (service.ErrorID == 0)
                {
                    return Ok(new { reservationId = reservationId });
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
        /// Get all information for invoice between date range
        /// </summary>
        [HttpGet("SelectAllInformationInvoice")]
        public ActionResult<object> SelectAllInformationInvoice([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var table = service.SelectAllInformationInvoice(fromDate, toDate);
                
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
        /// Get free rooms by selected type for date range
        /// </summary>
        [HttpGet("GetFreeRoomsBySelectedType")]
        public ActionResult<object> GetFreeRoomsBySelectedType([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] int type)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var table = service.GetFreeRoomsBySelectedType(fromDate, toDate, type);
                
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
        /// Get free room by type and date range
        /// </summary>
        [HttpGet("GetFreeRoomByType")]
        public ActionResult<HRoom> GetFreeRoomByType([FromQuery] string roomTypeCode, [FromQuery] DateTime checkInDate, [FromQuery] DateTime checkOutDate)
        {
            try
            {
                var service = new HRoomService(_dbAccess);
                var room = service.GetFreeRoomByType(roomTypeCode, checkInDate, checkOutDate);
                
                if (service.ErrorID == 0)
                {
                    return Ok(room);
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
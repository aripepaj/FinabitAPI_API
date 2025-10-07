//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Hotel Banquets CRUD operations and management
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;
using System.Data;

namespace FinabitAPI.Hotel.HBanquets
{
    [ApiController]
    [Route("api/[controller]")]
    public class HBanquetsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public HBanquetsController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
        }

        #region CRUD Operations

        /// <summary>
        /// Insert a new banquet
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] HBanquets banquet)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                service.Insert(banquet);

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
        /// Update an existing banquet
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] HBanquets banquet)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                service.Update(banquet);

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
        /// Delete a banquet
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] HBanquets banquet)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                service.Delete(banquet);

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
        /// Get all banquets
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<HBanquets>> SelectAll()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
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
        /// Get all banquets as table data with date filtering
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable([FromQuery] string fromDate = "", [FromQuery] string toDate = "")
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var table = service.SelectAllTable(fromDate, toDate);

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
        /// Get banquet by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<HBanquets> SelectByID([FromBody] HBanquets banquet)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.SelectByID(banquet);

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

        #region Lookup Operations

        /// <summary>
        /// Get available banquet rooms for lookup
        /// </summary>
        [HttpGet("BanquetsRoomLookup")]
        public ActionResult<List<HBanquets>> BanquetsRoomLookup()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.BanquetsRoomLookup();

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
        /// Get payment types for lookup
        /// </summary>
        [HttpGet("PaymentLookup")]
        public ActionResult<List<HBanquets>> PaymentLookup()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.PaymentLookup();

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
        /// Get status options for lookup
        /// </summary>
        [HttpGet("StatusLookup")]
        public ActionResult<List<HBanquets>> StatusLookup()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.StatusLookup();

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

        #region Reporting & Analytics

        /// <summary>
        /// Get banquet data optimized for charts
        /// </summary>
        [HttpGet("SelectAllForChart")]
        public ActionResult<List<HBanquets>> SelectAllForChart()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.SelectAllForChart();

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
        /// Get event details by ID
        /// </summary>
        [HttpGet("GetEvent")]
        public ActionResult<object> GetEvent([FromQuery] int id)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var table = service.GetEvent(id);

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
        /// Get banquet details by ID as table data
        /// </summary>
        [HttpPost("SelectByIDTable")]
        public ActionResult<object> SelectByIDTable([FromBody] HBanquets banquet)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var table = service.SelectByIDTable(banquet);

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
        /// Get date and time information for banquet
        /// </summary>
        [HttpPost("SelectDateAndTime")]
        public ActionResult<object> SelectDateAndTime([FromBody] HBanquets banquet)
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var table = service.SelectDateAndTime(banquet);

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

        #region Utility Operations

        /// <summary>
        /// Get the latest parent ID for hierarchical banquet records
        /// </summary>
        [HttpGet("GetLatestParentID")]
        public ActionResult<int> GetLatestParentID()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.GetLatestParentID();

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
        /// Get the latest banquet ID
        /// </summary>
        [HttpGet("GetLatestID")]
        public ActionResult<int> GetLatestID()
        {
            try
            {
                var service = new HBanquetsService(_dbAccess);
                var result = service.GetLatestID();

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

//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Hotel Folios CRUD operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;
using System.Data;

namespace FinabitAPI.Hotel.HFolios
{
    [ApiController]
    [Route("api/[controller]")]
    public class HFoliosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public HFoliosController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
        }

        #region CRUD Operations

        /// <summary>
        /// Insert a new hotel folio
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<int> Insert([FromBody] HFolios folio)
        {
            try
            {
                var service = new HFoliosService(_dbAccess);
                int result = service.Insert(folio);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, id = folio.ID, errorId = service.ErrorID });
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
        /// Update an existing hotel folio
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<int> Update([FromBody] HFolios folio)
        {
            try
            {
                var service = new HFoliosService(_dbAccess);
                int result = service.Update(folio);

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
        /// Delete a hotel folio
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<int> Delete([FromBody] HFolios folio)
        {
            try
            {
                var service = new HFoliosService(_dbAccess);
                int result = service.Delete(folio);

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
        /// Get all hotel folios
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<HFolios>> SelectAll()
        {
            try
            {
                var service = new HFoliosService(_dbAccess);
                var folios = service.SelectAll();

                if (service.ErrorID == 0)
                {
                    return Ok(folios);
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
        /// Get folio by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult SelectByID([FromBody] HFolios folio)
        {
            try
            {
                var service = new HFoliosService(_dbAccess);
                var dt = service.SelectByID(folio);
 var rows = dt.AsEnumerable()
        .Select(r => dt.Columns.Cast<DataColumn>()
            .ToDictionary(c => c.ColumnName, c => r[c]));
                if (service.ErrorID == 0)
                {
                    return Ok(rows);
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
        /// Get folios by reservation ID
        /// </summary>
        [HttpGet("SelectByReservationId")]
        public ActionResult<List<HFolios>> SelectByReservationId([FromBody] HFolios folio)
        {
            try
            {
                var service = new HFoliosService(_dbAccess);
                var folios = service.SelectByReservationId(folio);

                if (service.ErrorID == 0)
                {
                    return Ok(folios);
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

//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Controller for Form Configuration REST API operations
//-- =============================================
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FinabitAPI.Utilis;
using FinabitAPI.Core.User.dto;

namespace FinabitAPI.Core.FormConfiguration
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormConfigurationController : ControllerBase
    {
        private readonly DBAccess _dbAccess;

        public FormConfigurationController(DBAccess dbAccess)
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
        /// Insert a new form configuration
        /// </summary>
        [HttpPost("Insert")]
        public ActionResult<object> Insert([FromBody] FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                service.Insert(formConfiguration);

                if (service.ErrorID == 0)
                {
                    return Ok(new { success = true, errorId = service.ErrorID, id = formConfiguration.ID });
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
        /// Update an existing form configuration
        /// </summary>
        [HttpPut("Update")]
        public ActionResult<object> Update([FromBody] FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                service.Update(formConfiguration);

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
        /// Delete a form configuration
        /// </summary>
        [HttpDelete("Delete")]
        public ActionResult<object> Delete([FromBody] FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                service.Delete(formConfiguration);

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
        /// Get all form configurations
        /// </summary>
        [HttpGet("SelectAll")]
        public ActionResult<List<FinabitAPI.Core.FormConfiguration.FormConfiguration>> SelectAll()
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                var formConfigurations = service.SelectAll();

                if (service.ErrorID == 0)
                {
                    return Ok(formConfigurations);
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
        /// Get all form configurations as DataTable
        /// </summary>
        [HttpGet("SelectAllTable")]
        public ActionResult<object> SelectAllTable()
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
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
        /// Get form configuration by ID
        /// </summary>
        [HttpPost("SelectByID")]
        public ActionResult<FinabitAPI.Core.FormConfiguration.FormConfiguration> SelectByID([FromBody] FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                var result = service.SelectByID(formConfiguration);

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

        #region GetUserRight

        /// <summary>
        /// Get user rights for a specific form
        /// </summary>
        [HttpGet("GetUserRight/{formName}")]
        public ActionResult<FinabitAPI.Core.FormConfiguration.FormConfiguration> GetUserRight(string formName)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                var result = service.GetUserRight(formName);

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

        #region GetUserRightForAdmin

        /// <summary>
        /// Get admin user rights for a specific form
        /// </summary>
        [HttpGet("GetUserRightForAdmin/{formName}/{userID}/{roleID}")]
        public ActionResult<FinabitAPI.Core.FormConfiguration.FormConfiguration> GetUserRightForAdmin(string formName, int userID, int roleID)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                var result = service.GetUserRightForAdmin(formName, userID, roleID);

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

        #region GetUserRight_ALL

        /// <summary>
        /// Get all user rights
        /// </summary>
        [HttpGet("GetUserRight_ALL")]
        public ActionResult<List<FinabitAPI.Core.FormConfiguration.FormConfiguration>> GetUserRight_ALL()
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                var result = service.GetUserRight_ALL();

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

        #region GetAuthorizationName

        /// <summary>
        /// Get authorization for user authentication
        /// </summary>
        [HttpPost("GetAuthorizationName")]
        public ActionResult<Users> GetAuthorizationName([FromBody] AuthorizationRequest request)
        {
            try
            {
                var service = new FormConfigurationService(_dbAccess);
                var result = service.GetAuthorizationName(request.FormName, request.UserID, request.Password);

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

    /// <summary>
    /// Request model for GetAuthorizationName operation
    /// </summary>
    public class AuthorizationRequest
    {
        public string FormName { get; set; } = string.Empty;
        public int UserID { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

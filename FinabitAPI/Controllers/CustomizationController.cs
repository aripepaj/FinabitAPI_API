using FinabitAPI.Models;
using FinabitAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Controllers
{
    /// <summary>
    /// Unified controller for all customization features: Lists, Favorites, and Profile Preferences
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomizationController : ControllerBase
    {
        private readonly ICustomizationRepository _repo;
        private readonly ILogger<CustomizationController> _logger;

        public CustomizationController(ICustomizationRepository repo, ILogger<CustomizationController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        #region Customization Lists

        /// <summary>
        /// Get a customization list by ID
        /// </summary>
        [HttpGet("lists/{id}")]
        [ProducesResponseType(typeof(CustomizationList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetListById(int id, CancellationToken ct)
        {
            try
            {
                var list = await _repo.GetListByIdAsync(id, ct);
                if (list == null)
                    return NotFound(new { message = "Customization list not found" });

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customization list {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get all customization lists for a user, optionally filtered by type
        /// </summary>
        [HttpGet("lists/user/{user}")]
        [ProducesResponseType(typeof(IReadOnlyList<CustomizationList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListsByUser(string user, [FromQuery] string? type = null, CancellationToken ct = default)
        {
            try
            {
                var lists = await _repo.GetListsByUserAsync(user, type, ct);
                return Ok(lists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customization lists for user {User}", user);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get a specific customization list by user, name, and type
        /// </summary>
        [HttpGet("lists/user/{user}/name/{name}/type/{type}")]
        [ProducesResponseType(typeof(CustomizationList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetListByUserNameType(string user, string name, string type, CancellationToken ct)
        {
            try
            {
                var list = await _repo.GetListByUserNameTypeAsync(user, name, type, ct);
                if (list == null)
                    return NotFound(new { message = "Customization list not found" });

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customization list for user {User}, name {Name}, type {Type}", user, name, type);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Create a new customization list
        /// </summary>
        [HttpPost("lists")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateList([FromBody] CustomizationListDto dto, CancellationToken ct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validate type
                if (dto.Type != "table" && dto.Type != "pivot" && dto.Type != "chart")
                    return BadRequest(new { message = "Type must be 'table', 'pivot', or 'chart'" });

                var id = await _repo.CreateListAsync(dto, ct);
                return CreatedAtAction(nameof(GetListById), new { id }, new { id, message = "Customization list created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customization list");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Update an existing customization list
        /// </summary>
        [HttpPut("lists/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateList(int id, [FromBody] CustomizationListUpdateDto dto, CancellationToken ct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var success = await _repo.UpdateListAsync(id, dto, ct);
                if (!success)
                    return NotFound(new { message = "Customization list not found or no changes made" });

                return Ok(new { message = "Customization list updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customization list {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete a customization list
        /// </summary>
        [HttpDelete("lists/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteList(int id, CancellationToken ct)
        {
            try
            {
                var success = await _repo.DeleteListAsync(id, ct);
                if (!success)
                    return NotFound(new { message = "Customization list not found" });

                return Ok(new { message = "Customization list deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customization list {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        #endregion

        #region Customization Favorites

        /// <summary>
        /// Get a favorite by ID
        /// </summary>
        [HttpGet("favorites/{id}")]
        [ProducesResponseType(typeof(CustomizationFavorite), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFavoriteById(int id, CancellationToken ct)
        {
            try
            {
                var favorite = await _repo.GetFavoriteByIdAsync(id, ct);
                if (favorite == null)
                    return NotFound(new { message = "Favorite not found" });

                return Ok(favorite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorite {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get all favorites for a user, optionally filtered by item type
        /// </summary>
        [HttpGet("favorites/user/{user}")]
        [ProducesResponseType(typeof(IReadOnlyList<CustomizationFavorite>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoritesByUser(string user, [FromQuery] string? itemType = null, CancellationToken ct = default)
        {
            try
            {
                var favorites = await _repo.GetFavoritesByUserAsync(user, itemType, ct);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorites for user {User}", user);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get a specific favorite by user, item ID, and item type
        /// </summary>
        [HttpGet("favorites/user/{user}/item/{itemId}/type/{itemType}")]
        [ProducesResponseType(typeof(CustomizationFavorite), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFavoriteByUserItem(string user, string itemId, string itemType, CancellationToken ct)
        {
            try
            {
                var favorite = await _repo.GetFavoriteByUserItemAsync(user, itemId, itemType, ct);
                if (favorite == null)
                    return NotFound(new { message = "Favorite not found" });

                return Ok(favorite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorite for user {User}, item {ItemId}, type {ItemType}", user, itemId, itemType);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Check if an item is favorited (HEAD request)
        /// </summary>
        [HttpHead("favorites/user/{user}/item/{itemId}/type/{itemType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckFavorite(string user, string itemId, string itemType, CancellationToken ct)
        {
            try
            {
                var favorite = await _repo.GetFavoriteByUserItemAsync(user, itemId, itemType, ct);
                if (favorite == null)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking favorite for user {User}, item {ItemId}, type {ItemType}", user, itemId, itemType);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a new favorite
        /// </summary>
        [HttpPost("favorites")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFavorite([FromBody] CustomizationFavoriteDto dto, CancellationToken ct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = await _repo.CreateFavoriteAsync(dto, ct);
                return CreatedAtAction(nameof(GetFavoriteById), new { id }, new { id, message = "Favorite created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating favorite");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete a favorite by ID
        /// </summary>
        [HttpDelete("favorites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFavorite(int id, CancellationToken ct)
        {
            try
            {
                var success = await _repo.DeleteFavoriteAsync(id, ct);
                if (!success)
                    return NotFound(new { message = "Favorite not found" });

                return Ok(new { message = "Favorite deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting favorite {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete a favorite by user, item ID, and item type
        /// </summary>
        [HttpDelete("favorites/user/{user}/item/{itemId}/type/{itemType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFavoriteByUserItem(string user, string itemId, string itemType, CancellationToken ct)
        {
            try
            {
                var success = await _repo.DeleteFavoriteByUserItemAsync(user, itemId, itemType, ct);
                if (!success)
                    return NotFound(new { message = "Favorite not found" });

                return Ok(new { message = "Favorite deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting favorite for user {User}, item {ItemId}, type {ItemType}", user, itemId, itemType);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        #endregion

        #region Customization Profile Preferences

        /// <summary>
        /// Get a preference by ID
        /// </summary>
        [HttpGet("preferences/{id}")]
        [ProducesResponseType(typeof(CustomizationProfilePreference), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPreferenceById(int id, CancellationToken ct)
        {
            try
            {
                var preference = await _repo.GetPreferenceByIdAsync(id, ct);
                if (preference == null)
                    return NotFound(new { message = "Preference not found" });

                return Ok(preference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving preference {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get all preferences for a user
        /// </summary>
        [HttpGet("preferences/user/{user}")]
        [ProducesResponseType(typeof(IReadOnlyList<CustomizationProfilePreference>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPreferencesByUser(string user, CancellationToken ct)
        {
            try
            {
                var preferences = await _repo.GetPreferencesByUserAsync(user, ct);
                return Ok(preferences);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving preferences for user {User}", user);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get a specific preference by user and key
        /// </summary>
        [HttpGet("preferences/user/{user}/key/{prefKey}")]
        [ProducesResponseType(typeof(CustomizationProfilePreference), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPreferenceByUserKey(string user, string prefKey, CancellationToken ct)
        {
            try
            {
                var preference = await _repo.GetPreferenceByUserKeyAsync(user, prefKey, ct);
                if (preference == null)
                    return NotFound(new { message = "Preference not found" });

                return Ok(preference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving preference for user {User}, key {PrefKey}", user, prefKey);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Create or update a preference (upsert)
        /// </summary>
        [HttpPost("preferences")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpsertPreference([FromBody] CustomizationProfilePreferenceDto dto, CancellationToken ct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var id = await _repo.UpsertPreferenceAsync(dto, ct);
                return Ok(new { id, message = "Preference saved successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error upserting preference");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Update a preference (alias for upsert)
        /// </summary>
        [HttpPut("preferences")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePreference([FromBody] CustomizationProfilePreferenceDto dto, CancellationToken ct)
        {
            return await UpsertPreference(dto, ct);
        }

        /// <summary>
        /// Batch upsert preferences
        /// </summary>
        [HttpPost("preferences/batch")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BatchUpsertPreferences([FromBody] List<CustomizationProfilePreferenceDto> dtos, CancellationToken ct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ids = new List<int>();
                foreach (var dto in dtos)
                {
                    var id = await _repo.UpsertPreferenceAsync(dto, ct);
                    ids.Add(id);
                }

                return Ok(new { ids, count = ids.Count, message = "Preferences saved successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error batch upserting preferences");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Delete a preference by ID
        /// </summary>
        [HttpDelete("preferences/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePreference(int id, CancellationToken ct)
        {
            try
            {
                var success = await _repo.DeletePreferenceAsync(id, ct);
                if (!success)
                    return NotFound(new { message = "Preference not found" });

                return Ok(new { message = "Preference deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting preference {Id}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        #endregion
    }
}

using FinabitAPI.Finabit.Customization.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Finabit.Customization
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
        /// Get all customization lists for a user, optionally filtered by mode/storage/device (legacy 'type' still accepted)
        /// </summary>
        [HttpGet("lists/user/{user}")]
        [ProducesResponseType(typeof(IReadOnlyList<CustomizationList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListsByUser(string user, [FromQuery] string? mode = null, [FromQuery] string? storageKey = null, [FromQuery] string? device = null, [FromQuery] string? type = null, CancellationToken ct = default)
        {
            try
            {
                if (string.IsNullOrEmpty(mode) && !string.IsNullOrEmpty(type)) mode = type; // backward compatibility
                var lists = await _repo.GetListsByUserAsync(user, mode, storageKey, device, ct);
                return Ok(lists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customization lists for user {User}", user);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get a specific customization list by user, name, and mode (legacy 'type' route kept)
        /// </summary>
        [HttpGet("lists/user/{user}/name/{name}/mode/{mode}")]
        [HttpGet("lists/user/{user}/name/{name}/type/{mode}")] // backward compatibility
        [ProducesResponseType(typeof(CustomizationList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetListByUserNameType(string user, string name, string mode, [FromQuery] string? storageKey, [FromQuery] string? device, CancellationToken ct)
        {
            try
            {
                var list = await _repo.GetListByCompositeAsync(user, name, mode, storageKey, device, ct);
                if (list == null)
                    return NotFound(new { message = "Customization list not found" });
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customization list for user {User}, name {Name}, mode {Mode}", user, name, mode);
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

                var modeValue = !string.IsNullOrEmpty(dto.Mode) ? dto.Mode : dto.Type;
                if (modeValue != "table" && modeValue != "pivot" && modeValue != "chart")
                    return BadRequest(new { message = "Mode must be 'table', 'pivot', or 'chart'" });

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
        /// Get all favorites for a user
        /// </summary>
        [HttpGet("favorites/user/{user}")]
        [ProducesResponseType(typeof(IReadOnlyList<CustomizationFavorite>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoritesByUser(string user, CancellationToken ct = default)
        {
            try
            {
                var favorites = await _repo.GetFavoritesByUserAsync(user, ct);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorites for user {User}", user);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Get a specific favorite by user and item ID (legacy route with itemType still works)
        /// </summary>
        [HttpGet("favorites/user/{user}/item/{itemId}")]
        [HttpGet("favorites/user/{user}/item/{itemId}/type/{itemType}")]
        [ProducesResponseType(typeof(CustomizationFavorite), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFavoriteByUserItem(string user, string itemId, string? itemType, CancellationToken ct)
        {
            try
            {
                var favorite = await _repo.GetFavoriteByUserItemAsync(user, itemId, ct);
                if (favorite == null)
                    return NotFound(new { message = "Favorite not found" });
                return Ok(favorite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorite for user {User}, item {ItemId}", user, itemId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Check if an item is favorited (HEAD request)
        /// </summary>
        [HttpHead("favorites/user/{user}/item/{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckFavorite(string user, string itemId, CancellationToken ct)
        {
            try
            {
                var favorite = await _repo.GetFavoriteByUserItemAsync(user, itemId, ct);
                if (favorite == null) return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking favorite for user {User}, item {ItemId}", user, itemId);
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
        [HttpDelete("favorites/user/{user}/item/{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFavoriteByUserItem(string user, string itemId, CancellationToken ct)
        {
            try
            {
                var success = await _repo.DeleteFavoriteByUserItemAsync(user, itemId, ct);
                if (!success)
                    return NotFound(new { message = "Favorite not found" });
                return Ok(new { message = "Favorite deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting favorite for user {User}, item {ItemId}", user, itemId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        #endregion

    // Removed customization profile preferences endpoints region
    }
}

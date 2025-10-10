using FinabitAPI.Core.User.dto;

namespace FinabitAPI.Core.User
{
    public class UsersService
    {
        private readonly FinabitAPI.User.UsersRepository _repository;
        private readonly ILogger<UsersService> _logger;

        public UsersService(FinabitAPI.User.UsersRepository repository, ILogger<UsersService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        #region GetLoginUser

        public Users GetLoginUser(string userName, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to get login user for username: {UserName}", userName);
                
                var user = _repository.GetLoginUser(userName, password);
                
                if (user != null && user.ID > 0)
                {
                    _logger.LogInformation("User login successful for username: {UserName}, UserID: {UserID}", userName, user.ID);
                }
                else
                {
                    _logger.LogWarning("User login failed for username: {UserName}", userName);
                }
                
                return user ?? new Users(); // Return empty user if null
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting login user for username: {UserName}", userName);
                throw;
            }
        }

        #endregion

        #region ValidateUser

        public bool ValidateUser(string userName, string password)
        {
            try
            {
                _logger.LogInformation("Validating user credentials for username: {UserName}", userName);
                
                var user = GetLoginUser(userName, password);
                
                bool isValid = user != null && user.ID > 0 && user.HasConnections && user.Status;
                
                if (isValid)
                {
                    _logger.LogInformation("User validation successful for username: {UserName}", userName);
                }
                else
                {
                    _logger.LogWarning("User validation failed for username: {UserName}. User ID: {UserID}, HasConnections: {HasConnections}, Status: {Status}", 
                        userName, user?.ID, user?.HasConnections, user?.Status);
                }
                
                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while validating user for username: {UserName}", userName);
                return false;
            }
        }

        #endregion

        #region GetUserById

        public Users? GetUserById(int userId)
        {
            try
            {
                _logger.LogInformation("Getting user by ID: {UserID}", userId);
                
                // Note: This would require a new method in UsersRepository
                // For now, we'll return null and log that it's not implemented
                _logger.LogWarning("GetUserById method not implemented in UsersRepository for UserID: {UserID}", userId);
                
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID: {UserID}", userId);
                throw;
            }
        }

        #endregion

        #region CheckUserStatus

        public bool CheckUserStatus(string userName)
        {
            try
            {
                _logger.LogInformation("Checking user status for username: {UserName}", userName);
                
                // This is a lightweight check that could use a different repository method
                // For now, we'll use the existing GetLoginUser with a dummy password check
                var user = _repository.GetLoginUser(userName, ""); // Empty password for status check only
                
                bool isActive = user != null && user.Status && user.HasConnections;
                
                if (isActive)
                {
                    _logger.LogInformation("User is active for username: {UserName}", userName);
                }
                else
                {
                    _logger.LogWarning("User is not active for username: {UserName}. Status: {Status}, HasConnections: {HasConnections}", 
                        userName, user?.Status, user?.HasConnections);
                }
                
                return isActive;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking user status for username: {UserName}", userName);
                return false;
            }
        }

        #endregion
    }
}

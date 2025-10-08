using Microsoft.Extensions.Logging;
using FinabitAPI.Core.Server;
using ServerEntity = Server;

namespace FinabitAPI.Core.Server
{
    public class ServerService
    {
        private readonly ServerRepository _repository;
        private readonly ILogger<ServerService> _logger;

        public ServerService(ServerRepository repository, ILogger<ServerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public DateTime GetServerTime()
        {
            try
            {
                _logger.LogInformation("Getting server time");
                return _repository.GetServerTime();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting server time");
                throw;
            }
        }

        public List<ServerEntity> ListServers(string connection)
        {
            try
            {
                _logger.LogInformation("Getting list of servers with connection: {Connection}", connection);
                return ServerRepository.ListSevers(connection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting server list");
                throw;
            }
        }
    }
}
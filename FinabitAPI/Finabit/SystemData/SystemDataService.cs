using System.Data;
using FinabitAPI.Finabit.SystemData;
using FinabitAPI.Finabit.SystemInfo.dto;
using Microsoft.Extensions.Logging;

namespace FinabitAPI.Finabit.SystemData
{
    public class SystemDataService
    {
        private readonly SystemDataRepository _repository;
        private readonly ILogger<SystemDataService> _logger;

        public SystemDataService(SystemDataRepository repository, ILogger<SystemDataService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public FinabitAPI.Finabit.SystemData.SystemData ListSystemData()
        {
            try
            {
                _logger.LogInformation("Getting system data");
                return _repository.ListSystemData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system data");
                throw;
            }
        }

        public FinabitAPI.Finabit.SystemData.SystemData ListSystemDataForTran()
        {
            try
            {
                _logger.LogInformation("Getting system data for transaction");
                return _repository.ListSystemDataForTran();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system data for transaction");
                throw;
            }
        }

        public DataTable ListSystemDataTable()
        {
            try
            {
                _logger.LogInformation("Getting system data as DataTable");
                return _repository.ListSystemDataTable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system data table");
                throw;
            }
        }

        public SystemBaseInfo getAllData()
        {
            try
            {
                _logger.LogInformation("Getting system base info");
                return _repository.GetSystemBasicInfo();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting system base info");
                throw;
            }
        }
    }
}

using System.Data;

namespace FinabitAPI.Hotel.ExtraCharge
{
    // Use ExtraChargeEntity alias to avoid namespace conflicts
    using ExtraChargeEntity = global::ExtraCharge;

    public class ExtraChargeService
    {
        private readonly ExtraChargeRepository _repository;
        private readonly ILogger<ExtraChargeService> _logger;

        public ExtraChargeService(ExtraChargeRepository repository, ILogger<ExtraChargeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        #region Insert

        public void Insert(ExtraChargeEntity extraCharge)
        {
            try
            {
                _logger.LogInformation("Inserting new extra charge with name: {ExtraChargeName}", extraCharge.ExtraChargeName);
                _repository.Insert(extraCharge);
                _logger.LogInformation("Extra charge inserted successfully with ErrorID: {ErrorID}", extraCharge.ErrorID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting extra charge");
                throw;
            }
        }

        #endregion

        #region Update

        public void Update(ExtraChargeEntity extraCharge)
        {
            try
            {
                _logger.LogInformation("Updating extra charge with ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                _repository.Update(extraCharge);
                _logger.LogInformation("Extra charge updated successfully with ErrorID: {ErrorID}", extraCharge.ErrorID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating extra charge with ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                throw;
            }
        }

        #endregion

        #region Delete

        public void Delete(ExtraChargeEntity extraCharge)
        {
            try
            {
                _logger.LogInformation("Deleting extra charge with ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                _repository.Delete(extraCharge);
                _logger.LogInformation("Extra charge deleted successfully with ErrorID: {ErrorID}", extraCharge.ErrorID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting extra charge with ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                throw;
            }
        }

        #endregion

        #region SelectAll

        public List<ExtraChargeEntity> SelectAll()
        {
            try
            {
                _logger.LogInformation("Getting all extra charges");
                var extraCharges = _repository.SelectAll();
                _logger.LogInformation("Retrieved {Count} extra charges", extraCharges.Count);
                return extraCharges;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all extra charges");
                throw;
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                _logger.LogInformation("Getting all extra charges as DataTable");
                var dataTable = _repository.SelectAllTable();
                _logger.LogInformation("Retrieved extra charges DataTable with {Count} rows", dataTable.Rows.Count);
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting extra charges DataTable");
                throw;
            }
        }

        #endregion

        #region SelectByID

        public ExtraChargeEntity? SelectByID(ExtraChargeEntity extraCharge)
        {
            try
            {
                _logger.LogInformation("Getting extra charge by ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                var result = _repository.SelectByID(extraCharge);
                
                if (result != null && result.ExtraChargeID > 0)
                {
                    _logger.LogInformation("Extra charge found with ID: {ExtraChargeID}", result.ExtraChargeID);
                }
                else
                {
                    _logger.LogWarning("Extra charge not found with ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting extra charge by ID: {ExtraChargeID}", extraCharge.ExtraChargeID);
                throw;
            }
        }

        #endregion
    }
}

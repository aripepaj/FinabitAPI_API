using System.Data;

namespace FinabitAPI.Hotel.HAccomodation
{
    // Use HAccomodationEntity alias to avoid namespace conflicts
    using HAccomodationEntity = global::HAccomodation;

    public class HAccomodationService
    {
        private readonly HAccomodationRepository _repository;
        private readonly ILogger<HAccomodationService> _logger;

        public HAccomodationService(HAccomodationRepository repository, ILogger<HAccomodationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        #region Insert

        public void Insert(HAccomodationEntity accommodation)
        {
            try
            {
                _logger.LogInformation("Inserting new accommodation with name: {AccomodationName}", accommodation.AccomodationName);
                _repository.Insert(accommodation);
                _logger.LogInformation("Accommodation inserted successfully with ErrorID: {ErrorID}", accommodation.ErrorID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting accommodation");
                throw;
            }
        }

        #endregion

        #region Update

        public void Update(HAccomodationEntity accommodation)
        {
            try
            {
                _logger.LogInformation("Updating accommodation with ID: {ID}", accommodation.ID);
                _repository.Update(accommodation);
                _logger.LogInformation("Accommodation updated successfully with ErrorID: {ErrorID}", accommodation.ErrorID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating accommodation with ID: {ID}", accommodation.ID);
                throw;
            }
        }

        #endregion

        #region Delete

        public void Delete(HAccomodationEntity accommodation)
        {
            try
            {
                _logger.LogInformation("Deleting accommodation with ID: {ID}", accommodation.ID);
                _repository.Delete(accommodation);
                _logger.LogInformation("Accommodation deleted successfully with ErrorID: {ErrorID}", accommodation.ErrorID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting accommodation with ID: {ID}", accommodation.ID);
                throw;
            }
        }

        #endregion

        #region SelectAll

        public List<HAccomodationEntity> SelectAll()
        {
            try
            {
                _logger.LogInformation("Getting all accommodations");
                var accommodations = _repository.SelectAll();
                _logger.LogInformation("Retrieved {Count} accommodations", accommodations.Count);
                return accommodations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all accommodations");
                throw;
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                _logger.LogInformation("Getting all accommodations as DataTable");
                var dataTable = _repository.SelectAllTable();
                _logger.LogInformation("Retrieved accommodations DataTable with {Count} rows", dataTable.Rows.Count);
                return dataTable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting accommodations DataTable");
                throw;
            }
        }

        #endregion

        #region SelectByID

        public HAccomodationEntity? SelectByID(HAccomodationEntity accommodation)
        {
            try
            {
                _logger.LogInformation("Getting accommodation by ID: {ID}", accommodation.ID);
                var result = _repository.SelectByID(accommodation);
                
                if (result != null)
                {
                    _logger.LogInformation("Accommodation found with ID: {ID}", result.ID);
                }
                else
                {
                    _logger.LogWarning("Accommodation not found with ID: {ID}", accommodation.ID);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting accommodation by ID: {ID}", accommodation.ID);
                throw;
            }
        }

        #endregion
    }
}

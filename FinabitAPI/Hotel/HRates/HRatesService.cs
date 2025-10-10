using System.Data;
using Microsoft.Extensions.Logging;

namespace FinabitAPI.Hotel.HRates
{
    // Use HRatesEntity alias to avoid namespace conflicts
    using HRatesEntity = global::HRates;

    public class HRatesService
    {
        private readonly HRatesRepository _hRatesRepository;
        private readonly ILogger<HRatesService> _logger;

        public HRatesService(HRatesRepository hRatesRepository, ILogger<HRatesService> logger)
        {
            _hRatesRepository = hRatesRepository;
            _logger = logger;
        }

        #region Insert

        public void Insert(HRatesEntity hRates)
        {
            try
            {
                _logger.LogInformation("HRatesService.Insert called for RateName: {RateName}", hRates.RateName);
                _hRatesRepository.Insert(hRates);
                
                if (hRates.ErrorID == 0)
                {
                    _logger.LogInformation("HRatesService.Insert successful for RateName: {RateName}", hRates.RateName);
                }
                else
                {
                    _logger.LogWarning("HRatesService.Insert failed with ErrorID: {ErrorID}, Description: {ErrorDescription}", 
                        hRates.ErrorID, hRates.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesService.Insert for RateName: {RateName}", hRates.RateName);
                hRates.ErrorID = -1;
                hRates.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(HRatesEntity hRates)
        {
            try
            {
                _logger.LogInformation("HRatesService.Update called for ID: {ID}, RateName: {RateName}", hRates.ID, hRates.RateName);
                _hRatesRepository.Update(hRates);
                
                if (hRates.ErrorID == 0)
                {
                    _logger.LogInformation("HRatesService.Update successful for ID: {ID}", hRates.ID);
                }
                else
                {
                    _logger.LogWarning("HRatesService.Update failed with ErrorID: {ErrorID}, Description: {ErrorDescription}", 
                        hRates.ErrorID, hRates.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesService.Update for ID: {ID}", hRates.ID);
                hRates.ErrorID = -1;
                hRates.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(HRatesEntity hRates)
        {
            try
            {
                _logger.LogInformation("HRatesService.Delete called for ID: {ID}", hRates.ID);
                _hRatesRepository.Delete(hRates);
                
                if (hRates.ErrorID == 0)
                {
                    _logger.LogInformation("HRatesService.Delete successful for ID: {ID}", hRates.ID);
                }
                else
                {
                    _logger.LogWarning("HRatesService.Delete failed with ErrorID: {ErrorID}, Description: {ErrorDescription}", 
                        hRates.ErrorID, hRates.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesService.Delete for ID: {ID}", hRates.ID);
                hRates.ErrorID = -1;
                hRates.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<HRatesEntity> SelectAll()
        {
            try
            {
                _logger.LogInformation("HRatesService.SelectAll called");
                var result = _hRatesRepository.SelectAll();
                _logger.LogInformation("HRatesService.SelectAll returned {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesService.SelectAll");
                return new List<HRatesEntity>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                _logger.LogInformation("HRatesService.SelectAllTable called");
                var result = _hRatesRepository.SelectAllTable();
                _logger.LogInformation("HRatesService.SelectAllTable returned {Count} records", result.Rows.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesService.SelectAllTable");
                return new DataTable();
            }
        }

        #endregion

        #region SelectByID

        public HRatesEntity? SelectByID(HRatesEntity hRates)
        {
            try
            {
                _logger.LogInformation("HRatesService.SelectByID called for ID: {ID}", hRates.ID);
                var result = _hRatesRepository.SelectByID(hRates);
                
                if (result != null)
                {
                    _logger.LogInformation("HRatesService.SelectByID found record for ID: {ID}", hRates.ID);
                }
                else
                {
                    _logger.LogWarning("HRatesService.SelectByID no record found for ID: {ID}", hRates.ID);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HRatesService.SelectByID for ID: {ID}", hRates.ID);
                return null;
            }
        }

        #endregion
    }
}

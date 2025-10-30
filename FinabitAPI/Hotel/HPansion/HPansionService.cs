using System.Data;
using Microsoft.Extensions.Logging;

namespace FinabitAPI.Hotel.HPansion
{
    // Use HPansionEntity alias to avoid namespace conflicts
    using HPansionEntity = global::HPansion;

    public class HPansionService
    {
        private readonly HPansionRepository _hPansionRepository;
        private readonly ILogger<HPansionService> _logger;

        public HPansionService(HPansionRepository hPansionRepository, ILogger<HPansionService> logger)
        {
            _hPansionRepository = hPansionRepository;
            _logger = logger;
        }

        #region Insert

        public void Insert(HPansionEntity hPansion)
        {
            try
            {
                _logger.LogInformation("HPansionService.Insert called for PansionName: {PansionName}", hPansion.PansionName);
                _hPansionRepository.Insert(hPansion);
                
                if (hPansion.ErrorID == 0)
                {
                    _logger.LogInformation("HPansionService.Insert successful for PansionName: {PansionName}", hPansion.PansionName);
                }
                else
                {
                    _logger.LogWarning("HPansionService.Insert failed with ErrorID: {ErrorID}, Description: {ErrorDescription}", 
                        hPansion.ErrorID, hPansion.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionService.Insert for PansionName: {PansionName}", hPansion.PansionName);
                hPansion.ErrorID = -1;
                hPansion.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(HPansionEntity hPansion)
        {
            try
            {
                _logger.LogInformation("HPansionService.Update called for ID: {ID}, PansionName: {PansionName}", hPansion.ID, hPansion.PansionName);
                _hPansionRepository.Update(hPansion);
                
                if (hPansion.ErrorID == 0)
                {
                    _logger.LogInformation("HPansionService.Update successful for ID: {ID}", hPansion.ID);
                }
                else
                {
                    _logger.LogWarning("HPansionService.Update failed with ErrorID: {ErrorID}, Description: {ErrorDescription}", 
                        hPansion.ErrorID, hPansion.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionService.Update for ID: {ID}", hPansion.ID);
                hPansion.ErrorID = -1;
                hPansion.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(HPansionEntity hPansion)
        {
            try
            {
                _logger.LogInformation("HPansionService.Delete called for ID: {ID}", hPansion.ID);
                _hPansionRepository.Delete(hPansion);
                
                if (hPansion.ErrorID == 0)
                {
                    _logger.LogInformation("HPansionService.Delete successful for ID: {ID}", hPansion.ID);
                }
                else
                {
                    _logger.LogWarning("HPansionService.Delete failed with ErrorID: {ErrorID}, Description: {ErrorDescription}", 
                        hPansion.ErrorID, hPansion.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionService.Delete for ID: {ID}", hPansion.ID);
                hPansion.ErrorID = -1;
                hPansion.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<HPansionEntity> SelectAll()
        {
            try
            {
                _logger.LogInformation("HPansionService.SelectAll called");
                var result = _hPansionRepository.SelectAll();
                _logger.LogInformation("HPansionService.SelectAll returned {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionService.SelectAll");
                return new List<HPansionEntity>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                _logger.LogInformation("HPansionService.SelectAllTable called");
                var result = _hPansionRepository.SelectAllTable();
                _logger.LogInformation("HPansionService.SelectAllTable returned {Count} records", result.Rows.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionService.SelectAllTable");
                return new DataTable();
            }
        }

        #endregion

        #region SelectByID

        public HPansionEntity? SelectByID(HPansionEntity hPansion)
        {
            try
            {
                _logger.LogInformation("HPansionService.SelectByID called for ID: {ID}", hPansion.ID);
                var result = _hPansionRepository.SelectByID(hPansion);
                
                if (result != null)
                {
                    _logger.LogInformation("HPansionService.SelectByID found record for ID: {ID}", hPansion.ID);
                }
                else
                {
                    _logger.LogWarning("HPansionService.SelectByID no record found for ID: {ID}", hPansion.ID);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HPansionService.SelectByID for ID: {ID}", hPansion.ID);
                return null;
            }
        }

        #endregion
    }
}

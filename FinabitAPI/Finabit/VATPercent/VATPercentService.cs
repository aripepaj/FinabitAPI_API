using System.Data;
using Microsoft.Extensions.Logging;
using FinabitAPI.Finabit.VATPercent;
using VATPercentEntity = VATPercent;

namespace FinabitAPI.Finabit.VATPercent
{
    public class VATPercentService
    {
        private readonly VATPercentRepository _repository;
        private readonly ILogger<VATPercentService> _logger;

        public VATPercentService(VATPercentRepository repository, ILogger<VATPercentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void Insert(VATPercentEntity cls)
        {
            try
            {
                _logger.LogInformation("Inserting VAT percent: {VATName}", cls.VATName);
                _repository.Insert(cls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting VAT percent: {VATName}", cls.VATName);
                throw;
            }
        }

        public void Update(VATPercentEntity cls)
        {
            try
            {
                _logger.LogInformation("Updating VAT percent with ID: {VATID}", cls.VATID);
                _repository.Update(cls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating VAT percent with ID: {VATID}", cls.VATID);
                throw;
            }
        }

        public void Delete(VATPercentEntity cls)
        {
            try
            {
                _logger.LogInformation("Deleting VAT percent with ID: {VATID}", cls.VATID);
                _repository.Delete(cls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting VAT percent with ID: {VATID}", cls.VATID);
                throw;
            }
        }

        public List<VATPercentEntity> SelectAll()
        {
            try
            {
                _logger.LogInformation("Getting all VAT percents");
                return _repository.SelectAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all VAT percents");
                throw;
            }
        }

        public DataTable SelectAllTable()
        {
            try
            {
                _logger.LogInformation("Getting all VAT percents as DataTable");
                return _repository.SelectAllTable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting VAT percents as DataTable");
                throw;
            }
        }

        public VATPercentEntity SelectByID(VATPercentEntity cls)
        {
            try
            {
                _logger.LogInformation("Getting VAT percent by ID: {VATID}", cls.VATID);
                return _repository.SelectByID(cls);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting VAT percent by ID: {VATID}", cls.VATID);
                throw;
            }
        }
    }
}
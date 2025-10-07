//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Service for Hotel Agencies business logic operations
//-- =============================================
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HAgencies
{
    public class HAgenciesService
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public HAgenciesService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var repository = new HAgenciesRepository(_dbAccess);
                repository.Insert(hAgency);
                
                ErrorID = hAgency.ErrorID;
                ErrorDescription = hAgency.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var repository = new HAgenciesRepository(_dbAccess);
                repository.Update(hAgency);
                
                ErrorID = hAgency.ErrorID;
                ErrorDescription = hAgency.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var repository = new HAgenciesRepository(_dbAccess);
                repository.Delete(hAgency);
                
                ErrorID = hAgency.ErrorID;
                ErrorDescription = hAgency.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<FinabitAPI.Hotel.HAgencies.HAgencies> SelectAll()
        {
            try
            {
                var repository = new HAgenciesRepository(_dbAccess);
                var result = repository.SelectAll();
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<FinabitAPI.Hotel.HAgencies.HAgencies>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                var repository = new HAgenciesRepository(_dbAccess);
                var result = repository.SelectAllTable();
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        #endregion

        #region SelectByID

        public FinabitAPI.Hotel.HAgencies.HAgencies SelectByID(FinabitAPI.Hotel.HAgencies.HAgencies hAgency)
        {
            try
            {
                var repository = new HAgenciesRepository(_dbAccess);
                var result = repository.SelectByID(hAgency);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new FinabitAPI.Hotel.HAgencies.HAgencies();
            }
        }

        #endregion
    }
}

//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Service for Form Configuration business logic operations
//-- =============================================
using System.Data;
using FinabitAPI.Utilis;
using FinabitAPI.Core.User.dto;

namespace FinabitAPI.Core.FormConfiguration
{
    public class FormConfigurationService
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public FormConfigurationService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                repository.Insert(formConfiguration);
                
                ErrorID = formConfiguration.ErrorID;
                ErrorDescription = formConfiguration.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                repository.Update(formConfiguration);
                
                ErrorID = formConfiguration.ErrorID;
                ErrorDescription = formConfiguration.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                repository.Delete(formConfiguration);
                
                ErrorID = formConfiguration.ErrorID;
                ErrorDescription = formConfiguration.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<FinabitAPI.Core.FormConfiguration.FormConfiguration> SelectAll()
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                var result = repository.SelectAll();
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<FinabitAPI.Core.FormConfiguration.FormConfiguration>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
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

        public FinabitAPI.Core.FormConfiguration.FormConfiguration SelectByID(FinabitAPI.Core.FormConfiguration.FormConfiguration formConfiguration)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                var result = repository.SelectByID(formConfiguration);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new FinabitAPI.Core.FormConfiguration.FormConfiguration();
            }
        }

        #endregion

        #region GetUserRight

        public FinabitAPI.Core.FormConfiguration.FormConfiguration GetUserRight(string formName)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                var result = repository.GetUserRight(formName);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new FinabitAPI.Core.FormConfiguration.FormConfiguration();
            }
        }

        #endregion

        #region GetUserRightForAdmin

        public FinabitAPI.Core.FormConfiguration.FormConfiguration GetUserRightForAdmin(string formName, int userID, int roleID)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                var result = repository.GetUserRightForAdmin(formName, userID, roleID);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new FinabitAPI.Core.FormConfiguration.FormConfiguration();
            }
        }

        #endregion

        #region GetUserRight_ALL

        public List<FinabitAPI.Core.FormConfiguration.FormConfiguration> GetUserRight_ALL()
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                var result = repository.GetUserRight_ALL();
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<FinabitAPI.Core.FormConfiguration.FormConfiguration>();
            }
        }

        #endregion

        #region GetAuthorizationName

        public Users GetAuthorizationName(string formName, int userID, string password)
        {
            try
            {
                var repository = new FormConfigurationRepository(_dbAccess);
                var result = repository.GetAuthorizationName(formName, userID, password);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new Users();
            }
        }

        #endregion
    }
}

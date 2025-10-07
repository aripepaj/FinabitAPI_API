//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Service for State business logic operations
//-- =============================================
using System.Data;
using FinabitAPI.Core.Global.dto;
using FinabitAPI.Utilis;

namespace FinabitAPI.Finabit.States
{
    public class StateService
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public StateService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(State state)
        {
            try
            {
                var repository = new StateRepository(_dbAccess);
                repository.Insert(state);
                
                ErrorID = state.ErrorID;
                ErrorDescription = state.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(State state)
        {
            try
            {
                var repository = new StateRepository(_dbAccess);
                repository.Update(state);
                
                ErrorID = state.ErrorID;
                ErrorDescription = state.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(State state)
        {
            try
            {
                var repository = new StateRepository(_dbAccess);
                repository.Delete(state);
                
                ErrorID = state.ErrorID;
                ErrorDescription = state.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<State> SelectAll()
        {
            try
            {
                var repository = new StateRepository(_dbAccess);
                var result = repository.SelectAll();
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<State>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                var repository = new StateRepository(_dbAccess);
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

        public State SelectByID(State state)
        {
            try
            {
                var repository = new StateRepository(_dbAccess);
                var result = repository.SelectByID(state);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new State();
            }
        }

        #endregion
    }
}

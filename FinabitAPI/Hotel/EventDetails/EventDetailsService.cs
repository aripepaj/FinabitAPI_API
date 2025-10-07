//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Service for Hotel Event Details business logic operations
//-- =============================================
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.EventDetails
{
    public class EventDetailsService
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public EventDetailsService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                repository.Insert(eventDetail);
                
                ErrorID = eventDetail.ErrorID;
                ErrorDescription = eventDetail.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                repository.Update(eventDetail);
                
                ErrorID = eventDetail.ErrorID;
                ErrorDescription = eventDetail.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                repository.Delete(eventDetail);
                
                ErrorID = eventDetail.ErrorID;
                ErrorDescription = eventDetail.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<FinabitAPI.Hotel.EventDetails.EventsDetails> SelectAll()
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                var result = repository.SelectAll();
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<FinabitAPI.Hotel.EventDetails.EventsDetails>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
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

        public FinabitAPI.Hotel.EventDetails.EventsDetails SelectByID(FinabitAPI.Hotel.EventDetails.EventsDetails eventDetail)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                var result = repository.SelectByID(eventDetail);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new FinabitAPI.Hotel.EventDetails.EventsDetails();
            }
        }

        #endregion

        #region SelectByIDTable

        public DataTable SelectByIDTable(int ID)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                var result = repository.SelectByIDTable(ID);
                
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

        #region SelectAllByEventID

        public List<FinabitAPI.Hotel.EventDetails.EventsDetails> SelectAllByEventID(int ID)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                var result = repository.SelectAllByEventID(ID);
                
                ErrorID = repository.ErrorID;
                ErrorDescription = repository.ErrorDescription;
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<FinabitAPI.Hotel.EventDetails.EventsDetails>();
            }
        }

        #endregion

        #region GetAcomodationValueForEvent

        public DataTable GetAcomodationValueForEvent(int ID)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                var result = repository.GetAcomodationValueForEvent(ID);
                
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

        #region GetEventProfitability

        public DataTable GetEventProfitability(DataTable transactions)
        {
            try
            {
                var repository = new EventDetailsRepository(_dbAccess);
                var result = repository.GetEventProfitability(transactions);
                
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
    }
}

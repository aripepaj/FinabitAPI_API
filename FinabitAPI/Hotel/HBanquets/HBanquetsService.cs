//-- =============================================
//-- Author:		Generated  
//-- Create date: 07.10.25
//-- Description:	Service Layer for HBanquets
//-- =============================================
using System;
using System.Collections.Generic;
using System.Data;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HBanquets
{
    public class HBanquetsService
    {
        public HBanquetsRepository GlobalHBanquets;
        private readonly DBAccess _dbAccess;

        public HBanquetsService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            GlobalHBanquets = new HBanquetsRepository(_dbAccess);
        }

        public int ErrorID { get; set; } = 0;
        public string ErrorDescription { get; set; } = "";

        #region Insert

        public void Insert(HBanquets cls)
        {
            try
            {
                GlobalHBanquets.Insert(cls);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(HBanquets cls)
        {
            try
            {
                GlobalHBanquets.Update(cls);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(HBanquets cls)
        {
            try
            {
                GlobalHBanquets.Delete(cls);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<HBanquets> SelectAll()
        {
            try
            {
                var result = GlobalHBanquets.SelectAll();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HBanquets>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable(string fromDate, string toDate)
        {
            try
            {
                var result = GlobalHBanquets.SelectAllTable(fromDate, toDate);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
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

        public HBanquets SelectByID(HBanquets cls)
        {
            try
            {
                var result = GlobalHBanquets.SelectByID(cls);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new HBanquets();
            }
        }

        #endregion

        public List<HBanquets> BanquetsRoomLookup()
        {
            try
            {
                var result = GlobalHBanquets.BanquetsRoomLookup();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HBanquets>();
            }
        }

        public List<HBanquets> PaymentLookup()
        {
            try
            {
                var result = GlobalHBanquets.PaymentLookup();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HBanquets>();
            }
        }

        public List<HBanquets> StatusLookup()
        {
            try
            {
                var result = GlobalHBanquets.StatusLookup();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HBanquets>();
            }
        }

        #region SelectAllforChart

        public List<HBanquets> SelectAllForChart()
        {
            try
            {
                var result = GlobalHBanquets.SelectAllForChart();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HBanquets>();
            }
        }

        #endregion

        public DataTable GetEvent(int ID)
        {
            try
            {
                var result = GlobalHBanquets.GetEvent(ID);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable SelectByIDTable(HBanquets cls)
        {
            try
            {
                var result = GlobalHBanquets.SelectByIDTable(cls);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable SelectDateAndTime(HBanquets cls)
        {
            try
            {
                var result = GlobalHBanquets.SelectDateAndTime(cls);
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public int GetLatestParentID()
        {
            try
            {
                var result = GlobalHBanquets.GetLatestParentID();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return 0;
            }
        }

        public int GetLatestID()
        {
            try
            {
                var result = GlobalHBanquets.GetLatestID();
                ErrorID = GlobalHBanquets.ErrorID;
                ErrorDescription = GlobalHBanquets.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return 0;
            }
        }
    }
}

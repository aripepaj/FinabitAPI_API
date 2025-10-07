//-- =============================================
//-- Author:		Generated  
//-- Create date: 07.10.25
//-- Description:	Service Layer for HGuest
//-- =============================================
using System;
using System.Collections.Generic;
using System.Data;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HGuests
{
    public class HGuestService
    {
        public HGuestRepository GlobalHGuest;
        private readonly DBAccess _dbAccess;

        public HGuestService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            GlobalHGuest = new HGuestRepository(_dbAccess);
        }

        public int ErrorID { get; set; } = 0;
        public string ErrorDescription { get; set; } = "";

        #region Insert

        public void Insert(HGuest cls)
        {
            try
            {
                GlobalHGuest.Insert(cls);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(HGuest cls)
        {
            try
            {
                GlobalHGuest.Update(cls);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(HGuest cls)
        {
            try
            {
                GlobalHGuest.Delete(cls);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<HGuest> SelectAll()
        {
            try
            {
                var result = GlobalHGuest.SelectAll();
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HGuest>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable()
        {
            try
            {
                var result = GlobalHGuest.SelectAllTable();
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        public HGuest SelectByID(HGuest cls)
        {
            try
            {
                var result = GlobalHGuest.SelectByID(cls);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new HGuest();
            }
        }

        #endregion

        #region HInHouseList

        public DataTable HInHouseList(DateTime Date, bool Include)
        {
            try
            {
                var result = GlobalHGuest.HInHouseList(Date, Include);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        #region HArrivalListByDate

        public DataTable HArrivalList(DateTime FromDate, DateTime ToDate, string Origin, string RoomType, bool Include)
        {
            try
            {
                var result = GlobalHGuest.HArrivalList(FromDate, ToDate, Origin, RoomType, Include);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        #region HDepartureListByDate

        public DataTable HDepartureList(DateTime FromDate, DateTime ToDate, string RoomType, string Origin, bool Include)
        {
            try
            {
                var result = GlobalHGuest.HDepartureList(FromDate, ToDate, RoomType, Origin, Include);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        #region HReservationListByDate

        public DataTable HReservationList(DateTime FromDate, DateTime ToDate, string OriginID)
        {
            try
            {
                var result = GlobalHGuest.HReservationList(FromDate, ToDate, OriginID);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        #region HClientRealisationList

        public DataTable HClientRealisationList(DateTime FromDate, DateTime ToDate, string OriginID)
        {
            try
            {
                var result = GlobalHGuest.HClientRealisationList(FromDate, ToDate, OriginID);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        #region ExtraChargeListByDate

        public DataTable ExtraChargeList(DateTime FromDate, DateTime ToDate, string Origin, string RoomType)
        {
            try
            {
                var result = GlobalHGuest.ExtraChargeListByDate(FromDate, ToDate, Origin, RoomType);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable ClientBalanceByDate(string FromDate)
        {
            try
            {
                var result = GlobalHGuest.ClientBalancByDate(FromDate);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        public DataTable MonthRoomStatuses(string FromDate, string ToDate)
        {
            try
            {
                var result = GlobalHGuest.MonthRoomStatuses(FromDate, ToDate);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable RoomTypeStatuses(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var result = GlobalHGuest.RoomTypeStatuses(FromDate, ToDate);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable RoomTypeStatuses_PerPeriod(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var result = GlobalHGuest.RoomTypeStatuses_PerPeriod(FromDate, ToDate);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable GetFreeRooms_PerPeriod(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                var result = GlobalHGuest.GetFreeRooms_PerPeriod(FromDate, ToDate);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        #region HClientRealisationList_Group

        public DataTable HClientRealisationList_Group(DateTime FromDate, DateTime ToDate, string OriginID)
        {
            try
            {
                var result = GlobalHGuest.HClientRealisationList_Group(FromDate, ToDate, OriginID);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        #region SelectAllTableBySearch

        public DataTable SelectAllTableBySearch(string Name)
        {
            try
            {
                var result = GlobalHGuest.SelectAllTableBySearch(Name);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
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

        public HGuest SelectByFullName(string FullName)
        {
            try
            {
                var result = GlobalHGuest.SelectByFullName(FullName);
                ErrorID = GlobalHGuest.ErrorID;
                ErrorDescription = GlobalHGuest.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new HGuest();
            }
        }
    }
}

//-- =============================================
//-- Author:		KOS Code Generator
//-- Create date: 03.10.25 
//-- Description:	Service layer for Hotel Reservations CRUD operations
//-- =============================================
using System;
using System.Collections.Generic;
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.Reservations
{
    public class ReservationsService
    {
        public ReservationsRepository GlobalReservation;
        private readonly DBAccess _dbAccess;

        public ReservationsService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            GlobalReservation = new ReservationsRepository(_dbAccess);
        }

      

        public int ErrorID
        {
            get { return GlobalReservation.ErrorID; }
            set { GlobalReservation.ErrorID = value; }
        }

        public string ErrorDescription
        {
            get { return GlobalReservation.ErrorDescription; }
            set { GlobalReservation.ErrorDescription = value; }
        }

        #region Insert

        public int Insert(HReservation cls)
        {
            GlobalReservation.Insert(cls);
            return GlobalReservation.ErrorID;
        }

        #endregion

        #region Update

        public int Update(HReservation cls)
        {
            GlobalReservation.Update(cls);
            return GlobalReservation.ErrorID;
        }

        #endregion

        #region UpdateStatus

        public int UpdateStatus(HReservation cls)
        {
            GlobalReservation.UpdateStatus(cls);
            return GlobalReservation.ErrorID;
        }

        #endregion

        #region Delete

        public int Delete(HReservation cls)
        {
            GlobalReservation.Delete(cls);
            return GlobalReservation.ErrorID;
        }

        #endregion

        #region Select Methods

        public List<HReservation> SelectAll()
        {
            return GlobalReservation.SelectAll();
        }

        public DataTable SelectAllTable(string ReservationStatusID, string DateFilteringModel, DateTime FromDate, DateTime ToDate)
        {
            return GlobalReservation.SelectAllTable(ReservationStatusID, DateFilteringModel, FromDate, ToDate);
        }

        public HReservation SelectByID(HReservation cls)
        {
            return GlobalReservation.SelectByID(cls);
        }

        public HReservation SelectByBookingID(string SubBookingID)
        {
            return GlobalReservation.SelectByBookingID(SubBookingID);
        }

        public DataTable SelectDataTableByID(int ID)
        {
            return GlobalReservation.SelectDataTableByID(ID);
        }

        #endregion

        #region Gantt Chart Methods

        public List<HReservation> SelectAllForGantt()
        {
            return GlobalReservation.SelectAllForGantt();
        }

        public DataTable SelectAllTableForGantt(string FloorID, string RoomTypeID, DateTime FromDate, DateTime ToDate)
        {
            return GlobalReservation.SelectAllTableForGantt(FloorID, RoomTypeID, FromDate, ToDate);
        }

        #endregion

        #region Room Related Methods

        public List<HReservation> GetReservationsByRoomID(int RoomID)
        {
            return GlobalReservation.GetReservationsByRoomID(RoomID);
        }

        public DataTable SelectAllTableForRoomSearcher(string FloorID, string RoomTypeID, DateTime FromDate, DateTime ToDate)
        {
            return GlobalReservation.SelectAllTableForRoomSearcher(FloorID, RoomTypeID, FromDate, ToDate);
        }

        public int GetReservationStatusForRoom(int RoomID, DateTime Date)
        {
            return GlobalReservation.GetReservationStatusForRoom(RoomID, Date);
        }

        public DataTable GetReservationForRoom(string RoomNo)
        {
            return GlobalReservation.GetReservationForRoom(RoomNo);
        }

        #endregion

        #region Transaction Related Methods

        public void UpdateTranID(int ID, int TranID)
        {
            GlobalReservation.UpdateTranID(ID, TranID);
        }

        public int GetTransactionIDForReservation(int reservationID)
        {
            return GlobalReservation.GetTransactionIDForReservation(reservationID);
        }

        public void DeleteTransactionDetailsForReservation(int TransactionID)
        {
            GlobalReservation.DeleteTransactionDetailsForReservation(TransactionID);
        }

        public void DeleteTransactionsForReservation(int reservationID)
        {
            GlobalReservation.DeleteTransactionsForReservation(reservationID);
        }

        #endregion

        #region Card and POS Methods

        public DataTable GetReservationFromCard(string PIN)
        {
            return GlobalReservation.GetReservationFromCard(PIN);
        }

        public DataTable GETReservationSalesFromPOS(int HReservationID, int HFolioID, bool isGrouped)
        {
            return GlobalReservation.GETReservationSalesFromPOS(HReservationID, HFolioID, isGrouped);
        }

        #endregion

        #region Pricing Methods

        public DataTable GetPrice(int TarrifID, int RoomTypeID, string Source, string Season, DateTime date)
        {
            return GlobalReservation.GetPrice(TarrifID, RoomTypeID, Source, Season, date);
        }

        #endregion

        #region Guest and Report Methods

        public DataTable GetGuestList()
        {
            return GlobalReservation.GetGuestList();
        }

        public DataTable GetHDailyReport(DateTime Date)
        {
            return GlobalReservation.GetHDailyReport(Date);
        }

        public DataTable SelectResByIDForCheckInPrint(int resId)
        {
            return GlobalReservation.SelectResByIDForCheckInPrint(resId);
        }

        #endregion

        #region Group and Event Methods

        public DataTable GetGroupReservationSchema()
        {
            return GlobalReservation.GetGroupReservationSchema();
        }

        public int GetLastGroupID()
        {
            return GlobalReservation.GetLastGroupID();
        }

        public DataTable GetActiveEvents()
        {
            return GlobalReservation.GetActiveEvents();
        }

        public void UpdateEventDetailID(DataTable dt)
        {
            GlobalReservation.UpdateEventDetailID(dt);
        }

        public DataTable SelectAllTableForEvents(int EventID)
        {
            return GlobalReservation.SelectAllTableForEvents(EventID);
        }

        public void AzhuroVlerenEAkomodimit(int EventDetailID, decimal Value)
        {
            GlobalReservation.AzhuroVlerenEAkomodimit(EventDetailID, Value);
        }

        #endregion

        #region Connection Management

        public void CloseGlobalConnection()
        {
            GlobalReservation.CloseGlobalConnection();
        }

        public void Dispose()
        {
            GlobalReservation.Dispose();
        }

        #endregion
    }
}
//-- =============================================
//-- Author:		Generated
//-- Create date: 06.10.25 
//-- Description:	Service layer for Hotel Rooms CRUD operations
//-- =============================================
using System;
using System.Collections.Generic;
using System.Data;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;

namespace FinabitAPI.Hotel.HRooms
{
    public class HRoomService
    {
        public HRoomRepository GlobalHRoom;
        private readonly DBAccess _dbAccess;

        public HRoomService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            GlobalHRoom = new HRoomRepository(_dbAccess);
        }

        public int ErrorID { get; set; } = 0;
        public string ErrorDescription { get; set; } = "";

        #region Insert

        public int Insert(HRoom cls)
        {
            GlobalHRoom.Insert(cls);
            ErrorID = cls.ErrorID;
            ErrorDescription = cls.ErrorDescription ?? "";
            return ErrorID;
        }

        #endregion

        #region Update

        public int Update(HRoom cls)
        {
            GlobalHRoom.Update(cls);
            ErrorID = cls.ErrorID;
            ErrorDescription = cls.ErrorDescription ?? "";
            return ErrorID;
        }

        #endregion

        #region Delete

        public int Delete(HRoom cls)
        {
            GlobalHRoom.Delete(cls);
            ErrorID = cls.ErrorID;
            ErrorDescription = cls.ErrorDescription ?? "";
            return ErrorID;
        }

        #endregion

        #region Select Methods

        public List<HRoom> SelectAll(DateTime date, int departmentId)
        {
            try
            {
                var rooms = GlobalHRoom.SelectAll(date, departmentId);
                ErrorID = 0;
                ErrorDescription = "";
                return rooms;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HRoom>();
            }
        }

        public List<HRoom> SelectAll_3(DateTime date)
        {
            try
            {
                var rooms = GlobalHRoom.SelectAll_3(date);
                ErrorID = 0;
                ErrorDescription = "";
                return rooms;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HRoom>();
            }
        }

        public HRoom SelectByName_Date(string roomName, DateTime date)
        {
            try
            {
                var room = GlobalHRoom.SelectByName_Date(roomName, date);
                ErrorID = 0;
                ErrorDescription = "";
                return room;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new HRoom();
            }
        }

        public DataTable SelectAllTable(DateTime date)
        {
            try
            {
                var table = GlobalHRoom.SelectAllTable(date);
                ErrorID = 0;
                ErrorDescription = "";
                return table;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable SelectAllTableHistory(int roomId)
        {
            try
            {
                var table = GlobalHRoom.SelectAllTableHistory(roomId);
                ErrorID = 0;
                ErrorDescription = "";
                return table;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public HRoom SelectByID(HRoom cls)
        {
            try
            {
                var room = GlobalHRoom.SelectByID(cls);
                ErrorID = 0;
                ErrorDescription = "";
                return room;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new HRoom();
            }
        }

        #endregion

        #region Update Methods

        public int UpdatePosition(int roomId, int positionX, int positionY)
        {
            try
            {
                GlobalHRoom.UpdatePosition(roomId, positionX, positionY);
                ErrorID = GlobalHRoom.ErrorID;
                ErrorDescription = GlobalHRoom.ErrorDescription;
                return ErrorID;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return ErrorID;
            }
        }

        public int SetOutOfOrder(int roomId, int outOfOrder)
        {
            try
            {
                GlobalHRoom.SetOutOfOrder(roomId, outOfOrder);
                ErrorID = GlobalHRoom.ErrorID;
                ErrorDescription = GlobalHRoom.ErrorDescription;
                return ErrorID;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return ErrorID;
            }
        }

        #endregion

        #region Business Logic Methods

        public int GetReservationForRoom(int roomId, DateTime date)
        {
            try
            {
                var reservationId = GlobalHRoom.GetReservationForRoom(roomId, date);
                ErrorID = 0;
                ErrorDescription = "";
                return reservationId;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return 0;
            }
        }

        public DataTable SelectAllInformationInvoice(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var table = GlobalHRoom.SelectAllInformationInvoice(fromDate, toDate);
                ErrorID = 0;
                ErrorDescription = "";
                return table;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable GetFreeRoomsBySelectedType(DateTime fromDate, DateTime toDate, int type)
        {
            try
            {
                var table = GlobalHRoom.GetFreeRoomsBySelectedType(fromDate, toDate, type);
                ErrorID = 0;
                ErrorDescription = "";
                return table;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public HRoom GetFreeRoomByType(string roomTypeCode, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                var room = GlobalHRoom.GetFreeRoomByType(roomTypeCode, checkInDate, checkOutDate);
                ErrorID = 0;
                ErrorDescription = "";
                return room;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new HRoom();
            }
        }

        #endregion
    }
}
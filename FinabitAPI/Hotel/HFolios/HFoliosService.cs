//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Service layer for Hotel Folios CRUD operations
//-- =============================================
using System;
using System.Collections.Generic;
using System.Data;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;

namespace FinabitAPI.Hotel.HFolios
{
    public class HFoliosService
    {
        public HFoliosRepository GlobalHFolios;
        private readonly DBAccess _dbAccess;

        public HFoliosService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            GlobalHFolios = new HFoliosRepository(_dbAccess);
        }

        public int ErrorID { get; set; } = 0;
        public string ErrorDescription { get; set; } = "";

        #region Insert

        public int Insert(HFolios cls)
        {
            GlobalHFolios.Insert(cls);
            ErrorID = cls.ErrorID;
            ErrorDescription = cls.ErrorDescription ?? "";
            return ErrorID;
        }

        #endregion

        #region Update

        public int Update(HFolios cls)
        {
            GlobalHFolios.Update(cls);
            ErrorID = cls.ErrorID;
            ErrorDescription = cls.ErrorDescription ?? "";
            return ErrorID;
        }

        #endregion

        #region Delete

        public int Delete(HFolios cls)
        {
            GlobalHFolios.Delete(cls);
            ErrorID = cls.ErrorID;
            ErrorDescription = cls.ErrorDescription ?? "";
            return ErrorID;
        }

        #endregion

        #region Select Methods

        public List<HFolios> SelectAll()
        {
            try
            {
                var folios = GlobalHFolios.SelectAll();
                ErrorID = 0;
                ErrorDescription = "";
                return folios;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<HFolios>();
            }
        }

        public DataTable SelectByID(HFolios cls)
        {
            try
            {
                var folio = GlobalHFolios.SelectByID(cls);
                ErrorID = 0;
                ErrorDescription = "";
                return folio;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public DataTable SelectByReservationId(HFolios cls)
        {
            try
            {
                var folios = GlobalHFolios.SelectByReservationID(cls);
                ErrorID = 0;
                ErrorDescription = "";
                return folios;
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

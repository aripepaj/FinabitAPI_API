//-- =============================================
//-- Author:		Generated  
//-- Create date: 07.10.25
//-- Description:	Service Layer for ExtraChargeDetails
//-- =============================================
using System;
using System.Collections.Generic;
using System.Data;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;
using FinabitAPI.Hotel.Reservations;

namespace FinabitAPI.Hotel.ExtraChargeDetails
{
    public class ExtraChargeDetailsService
    {
        public ExtraChargeDetailsRepository GlobalExtraChargeDetails;
        private readonly DBAccess _dbAccess;

        public ExtraChargeDetailsService(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
            GlobalExtraChargeDetails = new ExtraChargeDetailsRepository(_dbAccess);
        }

        public int ErrorID { get; set; } = 0;
        public string ErrorDescription { get; set; } = "";

        #region Insert

        public void Insert(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.Insert(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Update

        public void Update(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.Update(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region Delete

        public void Delete(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.Delete(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region SelectAll

        public List<ExtraChargeDetails> SelectAll()
        {
            try
            {
                var result = GlobalExtraChargeDetails.SelectAll();
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new List<ExtraChargeDetails>();
            }
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable(int reservationID)
        {
            try
            {
                var result = GlobalExtraChargeDetails.SelectAllTable(reservationID);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
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

        public DataTable SelectExDetailsCheckIN()
        {
            try
            {
                var result = GlobalExtraChargeDetails.SelectExDetailsCheckIN();
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public ExtraChargeDetails SelectByID(ExtraChargeDetails cls)
        {
            try
            {
                var result = GlobalExtraChargeDetails.SelectByID(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new ExtraChargeDetails();
            }
        }

        public void InsertAccomodationInExtraCharge(HReservation cls)
        {
            try
            {
                GlobalExtraChargeDetails.InsertAccomodationInExtraCharge(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        public DataTable SelectByFolioID(int folioID, int reservationId, bool forInformationInvoice)
        {
            try
            {
                var result = GlobalExtraChargeDetails.SelectByFolioID(folioID, reservationId, forInformationInvoice);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
                return result;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                return new DataTable();
            }
        }

        public void UpdateFolioID(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.UpdateFolioID(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        public void UpdateFolioIDRooms(DataTable dt, int newReservationId, int newHFolioID)
        {
            try
            {
                GlobalExtraChargeDetails.UpdateFolioIDRooms(dt, newReservationId, newHFolioID);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        public void UpdateFolioIDNeTransaction(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.UpdateFolioIDNeTransaction(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        public void UpdateFolioIDAndReservationID(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.UpdateFolioIDAndReservationID(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }

        public void UpdateFolioIDAndReservationIdNeTransaction(ExtraChargeDetails cls)
        {
            try
            {
                GlobalExtraChargeDetails.UpdateFolioIDAndReservationIdNeTransaction(cls);
                ErrorID = GlobalExtraChargeDetails.ErrorID;
                ErrorDescription = GlobalExtraChargeDetails.ErrorDescription;
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
            }
        }
    }
}

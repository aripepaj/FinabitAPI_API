//-- =============================================
//-- Author:		KOS Code Generator
//-- Create date: 03.10.25 
//-- Description:	CRUD for Table tblHReservation
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Utilis;

namespace FinabitAPI.Hotel.Reservations
{
    public class ReservationsRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;
        private bool _ownsConnection;

        private SqlConnection? cnnGlobal;
        private SqlTransaction? tranGlobal;

        public ReservationsRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
           
        }

        #region Connection Management

        public void OpenGlobalConnection()
        {
            if (cnnGlobal != null && cnnGlobal.State == ConnectionState.Open)
                return;

            if (_dbAccess == null)
                throw new InvalidOperationException("DBAccess is not initialized. Use the constructor with DBAccess parameter.");

            SqlConnection conn = _dbAccess.GetConnection();

            _ownsConnection = true;
            cnnGlobal = conn;

            cnnGlobal.Open();

            tranGlobal = cnnGlobal.BeginTransaction();
        }

        public void CloseGlobalConnection()
        {
            try
            {
                if (cnnGlobal == null) return;

                if (tranGlobal != null)
                {
                    try
                    {
                        if (ErrorID != 0)
                            tranGlobal.Rollback();
                        else
                            tranGlobal.Commit();
                    }
                    catch
                    {
                    }
                    finally
                    {
                        tranGlobal.Dispose();
                        tranGlobal = null;
                    }
                }

                if (cnnGlobal.State != ConnectionState.Closed && _ownsConnection)
                {
                    cnnGlobal.Close();
                }
            }
            finally
            {
                if (_ownsConnection && cnnGlobal != null)
                {
                    cnnGlobal.Dispose();
                }
                cnnGlobal = null;
                _ownsConnection = false;
            }
        }

        public SqlCommand SqlCommandForTran_SP(string cmdText)
        {
            EnsureConnectionAndTransaction();
            var cmd = new SqlCommand(cmdText, cnnGlobal!)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 0,
                Transaction = tranGlobal
            };
            return cmd;
        }

        public SqlCommand SqlCommandForTran_Text(string cmdText)
        {
            EnsureConnectionAndTransaction();
            var cmd = new SqlCommand(cmdText, cnnGlobal!)
            {
                CommandType = CommandType.Text,
                CommandTimeout = 0,
                Transaction = tranGlobal
            };
            return cmd;
        }

        private void EnsureConnectionAndTransaction()
        {
            if (cnnGlobal == null || cnnGlobal.State != ConnectionState.Open)
            {
                OpenGlobalConnection();
            }

            if (tranGlobal == null)
            {
                tranGlobal = cnnGlobal!.BeginTransaction();
            }
        }

        public void Dispose()
        {
            CloseGlobalConnection();
        }

        #endregion

        // Methods will be added manually as requested by the user
        

  #region Insert

        public void Insert(HReservation cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ConsumerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ConsumerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RateID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RateID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BookDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BookDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckInDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckInDate;
            cmd.Parameters.Add(param);
                       
            param = new SqlParameter("@CheckOutDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckOutDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NoOfDays", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.NoOfDays;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VAT", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VAT;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StatusID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StatusID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card1", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card2", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card3", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card3;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card4", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card4;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card5", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card5;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@ChildrenNumberID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ChildrenNumberID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AdultNumberID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AdultNumberID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Source", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationOriginID;
            cmd.Parameters.Add(param);



            param = new SqlParameter("@AgencyID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AgencyID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PansonID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PansonID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AccomodationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccomodationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomCount", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomCount;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Discount", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckInMemo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckInMemo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckOutMemo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckOutMemo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HouseKeepingMemo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HouseKeepingMemo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_2", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_3", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_3;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_4", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_4;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_5", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_5;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@AccomodationValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccomodationValue;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@ExtraChargeValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaidValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaidValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TarifDefinitionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TarifDefinitionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentType", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlanDestributionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PlanDistributionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GroupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GroupID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GroupColor", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GroupColor;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EventDetailsID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EventDetailID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BL", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BL;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Parapagim", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Parapagim;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VleraParapagimit", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VleraParapagimit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Avans", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Avans;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RatePrice", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RatePrice;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@SubBookingID", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SubBookingID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NewResID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ID = Convert.ToInt32(cmd.Parameters["@NewResID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        #endregion

        #region UpdateStatus

        public void UpdateStatus(HReservation cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationUpdateStatus", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StatusID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StatusID;
            cmd.Parameters.Add(param);

             param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }
#endregion

        #region Update

        public void Update(HReservation cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ConsumerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ConsumerID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RateID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RateID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BookDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BookDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckInDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckInDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckOutDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckOutDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NoOfDays", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.NoOfDays;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VAT", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VAT;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StatusID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StatusID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TransactionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TransactionID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Card1", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card1;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card2", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card3", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card3;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card4", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card4;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Card5", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Card5;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ChildrenNumberID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ChildrenNumberID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AdultNumberID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AdultNumberID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Source", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationOriginID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@AgencyID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AgencyID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PansonID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PansonID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AccomodationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccomodationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomCount", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomCount;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Discount", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckInMemo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckInMemo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@CheckOutMemo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.CheckOutMemo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HouseKeepingMemo", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HouseKeepingMemo;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_2", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_2;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_3", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_3;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_4", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_4;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GuestID_5", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.GuestID_5;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AccomodationValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccomodationValue;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@ExtraChargeValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaidValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaidValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATValue", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATValue;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TarifDefinitionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TarifDefinitionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentType", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentType;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@PlanDestributionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PlanDistributionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EventDetailsID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EventDetailID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BL", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BL;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Parapagim", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Parapagim;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VleraParapagimit", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VleraParapagimit;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Avans", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Avans;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Anulim", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Anulim;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RatePrice", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RatePrice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        #endregion

        #region Delete

        public void Delete(HReservation cls)
        {
            SqlConnection cnn = new SqlConnection();
            cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        #endregion

        #region SelectAll

        public List<HReservation> SelectAll()
        {
            List<HReservation> clsList = new List<HReservation>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HReservation cls = new HReservation();
                        cls.ID = Convert.ToInt32(dr["ID"]);

                        cls.RoomID = Convert.ToInt32(dr["RoomID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        //cls.RateID = Convert.ToInt32(dr["RateID"]);
                        cls.GuestID = dr["GuestID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["GuestID"].ToString());
                        cls.ConsumerID = Convert.ToInt32(dr["ConsumerID"]);
                        cls.Guest = Convert.ToString(dr["Guest"]);
                        cls.BookDate = Convert.ToDateTime(dr["BookDate"]);
                        cls.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.NoOfDays = Convert.ToInt32(dr["NoOfDays"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VAT = Convert.ToDecimal(dr["VAT"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.ReservationStatus = Convert.ToString(dr["ReservationStatus"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.Card1 = Convert.ToString(dr["Card1"]);
                        cls.Card2 = Convert.ToString(dr["Card2"]);
                        cls.Card3 = Convert.ToString(dr["Card3"]);
                        cls.Card4 = Convert.ToString(dr["Card4"]);
                        cls.Card5 = Convert.ToString(dr["Card5"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.AgencyID = Convert.ToInt32(dr["AgencyID"]);
                        cls.PansonID = Convert.ToInt32(dr["PansonID"]);
                        cls.AccomodationID = Convert.ToInt32(dr["AccomodationID"]);
                        cls.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                        cls.CHeckInBy = Convert.ToInt32(dr["CHeckInBy"]);
                        cls.CheckOutBY = Convert.ToInt32(dr["CheckOutBY"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.CheckInMemo = Convert.ToString(dr["CheckInMemo"]);
                        cls.CheckOutMemo = Convert.ToString(dr["CheckOutMemo"]);
                        cls.HouseKeepingMemo = Convert.ToString(dr["HouseKeepingMemo"]);
                        cls.GuestID_2 = Convert.ToInt32(dr["GuestID_2"]);
                        cls.GuestID_3 = Convert.ToInt32(dr["GuestID_3"]);
                        cls.GuestID_4 = Convert.ToInt32(dr["GuestID_4"]);
                        cls.GuestID_5 = Convert.ToInt32(dr["GuestID_5"]);

                        cls.CheckInName = Convert.ToString(dr["CheckInName"]);
                        cls.CheckOutName = Convert.ToString(dr["CheckOutName"]);
                        cls.ReservationName = Convert.ToString(dr["ReservationName"]);
                        cls.Avans = Convert.ToDecimal(dr["Avans"]); ;
                        cls.Account = Convert.ToString(dr["Account"]);

                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable(string ReservationStatusID,string DateFilteringModel,DateTime FromDate, DateTime ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;
            param = new SqlParameter("@ReservationStatusID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ReservationStatusID;
            cmd.Parameters.Add(param);
            param = new SqlParameter("@DateFilteringModel", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = DateFilteringModel;
            cmd.Parameters.Add(param);
            param = new SqlParameter("@FromDate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate.ToString("yyyy-MM-dd hh:mm:ss");
            cmd.Parameters.Add(param);
            param = new SqlParameter("@ToDate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate.ToString("yyyy-MM-dd hh:mm:ss");
            cmd.Parameters.Add(param);
            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = GlobalAppData.UserID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }
      
        #endregion

        #region SelectByID

        public HReservation SelectByID(HReservation cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new HReservation();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomID = Convert.ToInt32(dr["RoomID"]);
                        cls.ChildrenNumberID = dr["ChildrenNumberID"] == DBNull.Value ? -1: Convert.ToInt32(dr["ChildrenNumberID"]);
                        cls.AdultNumberID = dr["AdultNumberID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["AdultNumberID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.GuestID = dr["GuestID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["GuestID"].ToString());
                        cls.ConsumerID = Convert.ToInt32(dr["ConsumerID"]);
                        cls.Guest = Convert.ToString(dr["GuestName_1"]);
                        try
                        {
                            cls.Guest2 = Convert.ToString(dr["GuestName_2"]);
                        }
                        catch{}
                        cls.Agency = Convert.ToString(dr["AgencyName"]);
                        cls.RateID = Convert.ToInt32(dr["RateID"]);
                        cls.BookDate = Convert.ToDateTime(dr["BookDate"]);
                        cls.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.BookDate = dr["BookDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["BookDate"]);
                        cls.CheckInDate = dr["CheckInDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = dr["CheckOutDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.NoOfDays = Convert.ToInt32(dr["NoOfDays"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VAT = Convert.ToDecimal(dr["VAT"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.StatusID = dr["CheckOutDate"] == DBNull.Value ? 0 :Convert.ToInt32(dr["StatusID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.Card1 = Convert.ToString(dr["Card1"]);
                        cls.Card2 = Convert.ToString(dr["Card2"]);
                        cls.Card3 = Convert.ToString(dr["Card3"]);
                        cls.Card4 = Convert.ToString(dr["Card4"]);
                        cls.Card5 = Convert.ToString(dr["Card5"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.ReservationOriginID = dr["Source"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Source"]);

                        cls.AgencyID = Convert.ToInt32(dr["AgencyID"]);
                        cls.PansonID = Convert.ToInt32(dr["PansonID"]);
                        cls.AccomodationID = Convert.ToInt32(dr["AccomodationID"]);
                        cls.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                        cls.CHeckInBy = Convert.ToInt32(dr["CHeckInBy"]);
                        cls.CheckOutBY = Convert.ToInt32(dr["CheckOutBY"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.CheckInMemo = Convert.ToString(dr["CheckInMemo"]);
                        cls.CheckOutMemo = Convert.ToString(dr["CheckOutMemo"]);
                        cls.HouseKeepingMemo = Convert.ToString(dr["HouseKeepingMemo"]);
                        cls.GuestID_2 = Convert.ToInt32(dr["GuestID_2"]);
                        cls.GuestID_3 = Convert.ToInt32(dr["GuestID_3"]);
                        cls.GuestID_4 = Convert.ToInt32(dr["GuestID_4"]);
                        cls.GuestID_5 = Convert.ToInt32(dr["GuestID_5"]);

                        cls.CheckInName = Convert.ToString(dr["CheckInName"]);
                        cls.CheckOutName = Convert.ToString(dr["CheckOutName"]);
                        cls.ReservationName = Convert.ToString(dr["ReservationName"]);

                        cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                        cls.AccomodationValue = Convert.ToDecimal(dr["AccomodationValue"]);
                        cls.PaymentType = Convert.ToInt32(dr["PaymentType"]);
                        cls.PlanDistributionID = Convert.ToInt32(dr["PlanDestributionID"]);
                        cls.EventDetailID = Convert.ToInt32(dr["EventDetailID"]);
                        cls.EventName = Convert.ToString(dr["EventName"]);
                        cls.EventID = Convert.ToInt32(dr["EventID"]);
                        cls.MasterFolio = Convert.ToInt32(dr["MasterFolio"]);
                        cls.TarifDefinitionID = Convert.ToInt32(dr["TarifDefinitionID"]);
                        cls.BL = Convert.ToBoolean(dr["BL"]);
                        cls.Parapagim = Convert.ToBoolean(dr["Parapagim"]);
                        cls.VleraParapagimit = Convert.ToDecimal(dr["VleraParapagimit"]);
                        cls.Avans = Convert.ToDecimal(dr["Avans"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        try
                        {
                            cls.EventDepartmentID = Convert.ToInt32(dr["EventDepartmentID"]);
                        }
                        catch 
                        {  
                        }
                       
                        cls.SetupID = Convert.ToInt32(dr["SetupID"]);
                        cls.ItemID = Convert.ToString(dr["MenuID"]);

                        break;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return cls;
        }

        #endregion

        public HReservation SelectByBookingID(string SubBookingID)
        {
            HReservation cls = new HReservation();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationBySubBookingID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@SubBooingID", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = SubBookingID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new HReservation();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomID = Convert.ToInt32(dr["RoomID"]);
                        cls.ChildrenNumberID = dr["ChildrenNumberID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ChildrenNumberID"]);
                        cls.AdultNumberID = dr["AdultNumberID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["AdultNumberID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.GuestID = dr["GuestID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["GuestID"].ToString());
                        cls.ConsumerID = Convert.ToInt32(dr["ConsumerID"]);
                        cls.Guest = Convert.ToString(dr["GuestName_1"]);
                        try
                        {
                            cls.Guest2 = Convert.ToString(dr["GuestName_2"]);
                        }
                        catch { }
                        cls.Agency = Convert.ToString(dr["AgencyName"]);
                        cls.RateID = Convert.ToInt32(dr["RateID"]);
                        cls.BookDate = Convert.ToDateTime(dr["BookDate"]);
                        cls.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.BookDate = dr["BookDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["BookDate"]);
                        cls.CheckInDate = dr["CheckInDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = dr["CheckOutDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.NoOfDays = Convert.ToInt32(dr["NoOfDays"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.VAT = Convert.ToDecimal(dr["VAT"]);
                        cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.StatusID = dr["CheckOutDate"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StatusID"]);
                        cls.TransactionID = Convert.ToInt32(dr["TransactionID"]);
                        cls.Card1 = Convert.ToString(dr["Card1"]);
                        cls.Card2 = Convert.ToString(dr["Card2"]);
                        cls.Card3 = Convert.ToString(dr["Card3"]);
                        cls.Card4 = Convert.ToString(dr["Card4"]);
                        cls.Card5 = Convert.ToString(dr["Card5"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.ReservationOriginID = dr["Source"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Source"]);

                        cls.AgencyID = Convert.ToInt32(dr["AgencyID"]);
                        cls.PansonID = Convert.ToInt32(dr["PansonID"]);
                        cls.AccomodationID = Convert.ToInt32(dr["AccomodationID"]);
                        cls.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                        cls.CHeckInBy = Convert.ToInt32(dr["CHeckInBy"]);
                        cls.CheckOutBY = Convert.ToInt32(dr["CheckOutBY"]);
                        cls.Discount = Convert.ToDecimal(dr["Discount"]);
                        cls.CheckInMemo = Convert.ToString(dr["CheckInMemo"]);
                        cls.CheckOutMemo = Convert.ToString(dr["CheckOutMemo"]);
                        cls.HouseKeepingMemo = Convert.ToString(dr["HouseKeepingMemo"]);
                        cls.GuestID_2 = Convert.ToInt32(dr["GuestID_2"]);
                        cls.GuestID_3 = Convert.ToInt32(dr["GuestID_3"]);
                        cls.GuestID_4 = Convert.ToInt32(dr["GuestID_4"]);
                        cls.GuestID_5 = Convert.ToInt32(dr["GuestID_5"]);

                        cls.CheckInName = Convert.ToString(dr["CheckInName"]);
                        cls.CheckOutName = Convert.ToString(dr["CheckOutName"]);
                        cls.ReservationName = Convert.ToString(dr["ReservationName"]);

                        cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                        cls.AccomodationValue = Convert.ToDecimal(dr["AccomodationValue"]);
                        cls.PaymentType = Convert.ToInt32(dr["PaymentType"]);
                        cls.PlanDistributionID = Convert.ToInt32(dr["PlanDestributionID"]);
                        cls.EventDetailID = Convert.ToInt32(dr["EventDetailID"]);
                        cls.EventName = Convert.ToString(dr["EventName"]);
                        cls.EventID = Convert.ToInt32(dr["EventID"]);
                        cls.MasterFolio = Convert.ToInt32(dr["MasterFolio"]);
                        cls.TarifDefinitionID = Convert.ToInt32(dr["TarifDefinitionID"]);
                        cls.BL = Convert.ToBoolean(dr["BL"]);
                        cls.Parapagim = Convert.ToBoolean(dr["Parapagim"]);
                        cls.VleraParapagimit = Convert.ToDecimal(dr["VleraParapagimit"]);
                        cls.Avans = Convert.ToDecimal(dr["Avans"]);
                        cls.Account = Convert.ToString(dr["Account"]);
                        try
                        {
                            cls.EventDepartmentID = Convert.ToInt32(dr["EventDepartmentID"]);
                        }
                        catch
                        {
                        }

                        cls.SetupID = Convert.ToInt32(dr["SetupID"]);
                        cls.ItemID = Convert.ToString(dr["MenuID"]);

                        break;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return cls;
        }

        #region SelectAllForGantt

        public List<HReservation> SelectAllForGantt()
        {
            List<HReservation> clsList = new List<HReservation>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationListForGantt", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HReservation cls = new HReservation();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.RoomID = Convert.ToInt32(dr["RoomID"]);
                        cls.Guest = Convert.ToString(dr["Guest"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;
        }

        #endregion

        #region SelectAllTableForGantt

        public DataTable SelectAllTableForGantt(string FloorID,string RoomTypeID,DateTime FromDate, DateTime ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationListForGantt", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;
            param = new SqlParameter("@FloorID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FloorID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomTypeID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);
          
            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        #endregion

        #region GetReservationsByRoomID

        public  List<HReservation> GetReservationsByRoomID(int RoomID)
        {
            List<HReservation> clsList = new List<HReservation>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationListByRoomID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomID;
            cmd.Parameters.Add(param);

            
            
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HReservation cls = new HReservation();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RoomID = Convert.ToInt32(dr["RoomID"]);
                        cls.RoomName = Convert.ToString(dr["RoomName"]);
                        cls.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                        cls.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                        cls.GuestID = Convert.ToInt32(dr["GuestID"]);
                        cls.Guest = Convert.ToString(dr["Guest"]);
                        
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsList;

        }

        #endregion

        #region SelectAllTableForRoomSearcher

        public DataTable SelectAllTableForRoomSearcher(string FloorID, string RoomTypeID, DateTime FromDate, DateTime ToDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHGetFreeRooms]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;
            param = new SqlParameter("@FloorID", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = FloorID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomType", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = FromDate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = ToDate;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        #endregion
        
        #region SelectDataTableByID

        public DataTable SelectDataTableByID(int ID)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }

        #endregion

        #region UpdateTranID

        public void UpdateTranID(int ID, int TranID)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHReservationUpdateTranID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TranID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TranID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                

                cnn.Close();
            }
            catch (Exception ex)
            {
               
                cnn.Close();
            }
        }
#endregion


        #region GetreservationFromCard
         
        public DataTable GetReservationFromCard(string PIN)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetReservationFromCard", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PIN", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = PIN;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }
        #endregion

        #region GETReservationSalesFromPOS

        public DataTable GETReservationSalesFromPOS(int HReservationID, int HFolioID,bool isGrouped)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGETReservationSalesFromPOS", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@HReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = HReservationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HFolioID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = HFolioID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@GroupItems", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = isGrouped;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }
        #endregion

        public DataTable GetPrice(int TarrifID, int RoomTypeID, string Source,string Season,DateTime date)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetPrice", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@TarrifID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TarrifID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@roomtypeid", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Source", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Source;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Season", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = Season;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = date;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;

        }


        public DataTable GetGuestList()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("SELECT *  FROM [dbo].[vwFINA_GUESTLIST]", cnn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);


            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        public DataTable GetHDailyReport(DateTime Date)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHDailyReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
           

            SqlParameter param;

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                SqlDataAdapter dadap = new SqlDataAdapter(cmd);
                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;

        }

        public void DeleteTransactionDetailsForReservation(int TransactionID)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHDeleteTransactionDetailsForReservation", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = TransactionID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }
        }
      
        public void DeleteTransactionsForReservation(int reservationID)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHDeleteTransactionForReservation", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = reservationID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }
        }
        public int GetTransactionIDForReservation(int reservationID)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHGetTransactionIDForReservation", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = reservationID;
            cmd.Parameters.Add(param);
           int TransID=0;
            try
            {
                cnn.Open();

                TransID = Convert.ToInt32(cmd.ExecuteScalar());

                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }
            return TransID;
        }

        public DataTable GetGroupReservationSchema()
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetGroupReservationSchema", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();
                SqlDataAdapter dadap = new SqlDataAdapter(cmd);
                dadap.Fill(dt);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
           
            return dt;
        }

        public int GetLastGroupID()
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spgetLastGroupID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            
            int TransID = 0;
            try
            {
                cnn.Open();

                TransID = Convert.ToInt32(cmd.ExecuteScalar());

                cnn.Close();
            }
            catch (Exception ex)
            {
                cnn.Close();
            }
            return TransID;
        }
        public DataTable GetActiveEvents()
        {
            DataTable dt = new DataTable();

            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetActiveEvents", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();
                SqlDataAdapter dadap = new SqlDataAdapter(cmd);
                dadap.Fill(dt);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dt;
        }

        public void UpdateEventDetailID(DataTable dt)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateReservationEventID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EventsUpdateDetails", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = dt;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();


                cnn.Close();
            }
            catch (Exception ex)
            {

                cnn.Close();
            }
        }

        public DataTable SelectAllTableForEvents(int EventID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetRoomsForEvent", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;
            param = new SqlParameter("@EventID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = EventID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        public void AzhuroVlerenEAkomodimit(int EventDetailID, decimal Value)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spUpdateEventValue", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EventDetailID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = EventDetailID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = Value;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();


                cnn.Close();
            }
            catch (Exception ex)
            {

                cnn.Close();
            }
        }

        public int GetReservationStatusForRoom(int RoomID, DateTime Date)
        {
            int ReservationID;
            ReservationID = -1;
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetReservationStatusForRoom", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = Date;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ReservationID = Convert.ToInt32(dr["StatusID"]);
                    }
                }
            }
            catch { }

            return ReservationID;
        }

        #region GetReservationForRoom

        public DataTable GetReservationForRoom(string RoomNo)
        {
            DataTable data = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetReservationByRoom", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoomNo", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = RoomNo;
            cmd.Parameters.Add(param);


            try
            {
                cnn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                return data;

            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return data;
        }
        #endregion

        #region SelectResByIDForCheckInPrint

        public DataTable SelectResByIDForCheckInPrint(int resId)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHResByIDForCheckInPrint", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            SqlParameter param;
            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = resId;
            cmd.Parameters.Add(param);
            
            try
            {
                cnn.Open();
                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return dtList;
        }
        #endregion

    }
}

//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Repository for Hotel Extra Charge Details CRUD operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;
using FinabitAPI.Hotel.Reservations;

namespace FinabitAPI.Hotel.ExtraChargeDetails
{
    public class ExtraChargeDetailsRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public ExtraChargeDetailsRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Rate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Rate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ErrorID = ErrorID;
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                cls.ErrorID = ErrorID;
                cls.ErrorDescription = ErrorDescription;
                cnn.Close();
            }
        }

        #endregion

        #region Update

        public void Update(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeDetailsID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ExtraChargeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Rate", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Rate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Description", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Description ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ErrorID = ErrorID;
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                cls.ErrorID = ErrorID;
                cls.ErrorDescription = ErrorDescription;
                cnn.Close();
            }
        }

        #endregion

        #region Delete

        public void Delete(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeDetailsID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ErrorID = ErrorID;
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                cls.ErrorID = ErrorID;
                cls.ErrorDescription = ErrorDescription;
                cnn.Close();
            }
        }

        #endregion

        #region SelectAll

        public List<ExtraChargeDetails> SelectAll()
        {
            List<ExtraChargeDetails> clsList = new List<ExtraChargeDetails>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ExtraChargeDetails cls = new ExtraChargeDetails();
                        cls.ExtraChargeDetailsID = Convert.ToInt32(dr["ExtraChargeDetailsID"]);
                        cls.ExtraChargeID = dr["ExtraChargeID"] != DBNull.Value ? Convert.ToInt32(dr["ExtraChargeID"]) : null;
                        cls.ReservationID = dr["ReservationID"] != DBNull.Value ? Convert.ToInt32(dr["ReservationID"]) : null;
                        cls.Quantity = dr["Quantity"] != DBNull.Value ? Convert.ToDecimal(dr["Quantity"]) : null;
                        cls.Rate = dr["Rate"] != DBNull.Value ? Convert.ToString(dr["Rate"]) : null;
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorDescription = ex.Message;
                ErrorID = -1;
                cnn.Close();
            }
            return clsList;
        }

        #endregion

        #region SelectAllTable

        public DataTable SelectAllTable(int reservationID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@ReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = reservationID;
            cmd.Parameters.Add(param);

            SqlDataAdapter dadap = new SqlDataAdapter(cmd);

            try
            {
                cnn.Open();
                dadap.Fill(dtList);
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorDescription = ex.Message;
                ErrorID = -1;
                cnn.Close();
            }
            return dtList;
        }

        #endregion

        public DataTable SelectExDetailsCheckIN()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsListCheckIN", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
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

        #region SelectByID

        public ExtraChargeDetails SelectByID(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDetailsByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeDetailsID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new ExtraChargeDetails();
                        cls.ExtraChargeDetailsID = Convert.ToInt32(dr["ExtraChargeDetailsID"]);
                        cls.ExtraChargeID = Convert.ToInt32(dr["ExtraChargeID"]);
                        cls.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Rate = Convert.ToString(dr["Rate"]);
                        cls.Date = Convert.ToDateTime(dr["Date"]);
                        cls.Description = Convert.ToString(dr["Description"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);

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

        public void InsertAccomodationInExtraCharge(HReservation cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spHINsertAccomodationInExtraCharge]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;


            param = new SqlParameter("@ReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@TarifDefinitonID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.TarifDefinitionID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Days", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.NoOfDays;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Hadult", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AdultNumberID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HChildren", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ChildrenNumberID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PlanDestributionID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PlanDistributionID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("@Discount", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Discount;
            cmd.Parameters.Add(param);



            param = new SqlParameter("@AccomodationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AccomodationID;
            cmd.Parameters.Add(param);


            param = new SqlParameter("RatePrice", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RatePrice;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                //cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        //public ExtraChargeDetails SelectByFolioID(ExtraChargeDetails cls)
        //{
        //    SqlConnection cnn = _dbAccess.GetConnection();
        //    SqlCommand cmd = new SqlCommand("spHSelectExtraChargeDetailsByFolioID", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlParameter param;

        //    param = new SqlParameter("@FolioId", System.Data.SqlDbType.Int);
        //    param.Direction = ParameterDirection.Input;
        //    param.Value = cls.HFolioID;
        //    cmd.Parameters.Add(param);

        //    try
        //    {
        //        cnn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                cls = new ExtraChargeDetails();

        //                cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
        //                cls.Rate = Convert.ToString(dr["Rate"]);
        //               // cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
        //                cls.DepartmentName= Convert.ToString(dr["DepartmentName"]);

        //                break;
        //            }
        //        }
        //        cnn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string exp = ex.Message;
        //        cnn.Close();
        //    }
        //    return cls;
        //}
        public DataTable SelectByFolioID(int folioID, int ReservationId, bool forInformationInvoice)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHSelectExtraChargeDetailsByFolioID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = new SqlParameter("@FolioId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = folioID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ReservationId;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ForInformationInvoice", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = forInformationInvoice;
            cmd.Parameters.Add(param);

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

        public void UpdateFolioID(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHUpdateFolioId", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FolioId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HFolioID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ExtraChargeDetailsId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                //cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }
        public void UpdateFolioIDRooms(DataTable dt, int newReservationId, int newHFolioID)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHUpdateFolioIdAllRooms", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeDetails", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = dt;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NewReservationID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = newReservationId;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NewHFolioID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = newHFolioID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                //cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                //cls.ErrorID = -1;
                //cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }

        public void UpdateFolioIDNeTransaction(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHUpdateFolioIdNeTransaction", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FolioId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HFolioID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ExtraChargeDetailsId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);
            param = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                //cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }


        public void UpdateFolioIDAndReservationID(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHUpdateFolioIdAndReservationId", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FolioId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HFolioID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ExtraChargeDetailsId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReferenceID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReferenceID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                //cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }

        }
        public void UpdateFolioIDAndReservationIdNeTransaction(ExtraChargeDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHUpdateFolioIdAndReservationIdInTransaction", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FolioId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HFolioID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ExtraChargeDetailsId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeDetailsID;
            cmd.Parameters.Add(param);
            param = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReservationID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                cmd.ExecuteNonQuery();
                //cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.ErrorID = -1;
                cls.ErrorDescription = ex.Message;
                cnn.Close();
            }
        }
    }
}
    

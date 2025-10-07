//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Repository for Hotel Event Details CRUD operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.EventDetails
{
    public class EventDetailsRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public EventDetailsRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert 

        public void Insert(FinabitAPI.Hotel.EventDetails.EventsDetails cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEventsDetailsInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EventID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EventID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EventActivityID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EventActivityID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ActivityDescription", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ActivityDescription ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FromTime", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.FromTime ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToTime", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ToTime ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ShowInInvoice", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ShowInInvoice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Price", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Price;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HasCoupon", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HasCoupon;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SetupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SetupID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@MenuID", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.MenuID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SetupDescription", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SetupDescription ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AudioDescription", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AudioDescription ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmNewID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                cls.ID = Convert.ToInt32(cmd.Parameters["@prmNewID"].Value);
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

        public void Update(FinabitAPI.Hotel.EventDetails.EventsDetails cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEventsDetailsUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EventID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EventID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EventActivityID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EventActivityID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ActivityDescription", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ActivityDescription ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Date", System.Data.SqlDbType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Date;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FromTime", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.FromTime ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToTime", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ToTime ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ShowInInvoice", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ShowInInvoice;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DepartmentID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Quantity", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Quantity;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Price", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Price;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@HasCoupon", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.HasCoupon;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SetupID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SetupID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@MenuID", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.MenuID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SetupDescription", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SetupDescription ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AudioDescription", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AudioDescription ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
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

        public void Delete(FinabitAPI.Hotel.EventDetails.EventsDetails cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEventsDetailsDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@prmErrorID", SqlDbType.Int);
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

        public List<FinabitAPI.Hotel.EventDetails.EventsDetails> SelectAll()
        {
            List<FinabitAPI.Hotel.EventDetails.EventsDetails> clsList = new List<FinabitAPI.Hotel.EventDetails.EventsDetails>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEventsDetailsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FinabitAPI.Hotel.EventDetails.EventsDetails cls = new FinabitAPI.Hotel.EventDetails.EventsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.EventID = Convert.ToInt32(dr["EventID"]);
                        cls.EventActivityID = Convert.ToInt32(dr["EventActivityID"]);
                        cls.ActivityDescription = Convert.ToString(dr["ActivityDescription"]);
                        cls.Date = Convert.ToDateTime(dr["Date"]);
                        cls.FromTime = Convert.ToString(dr["FromTime"]);
                        cls.ToTime = Convert.ToString(dr["ToTime"]);
                        cls.ShowInInvoice = Convert.ToBoolean(dr["ShowInInvoice"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.HasCoupon = Convert.ToBoolean(dr["HasCoupon"]);
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

        public DataTable SelectAllTable()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEventsDetailsList", cnn);
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

        #endregion 

        #region SelectByID

        public FinabitAPI.Hotel.EventDetails.EventsDetails SelectByID(FinabitAPI.Hotel.EventDetails.EventsDetails cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEventsDetailsByID", cnn);
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
                        cls = new FinabitAPI.Hotel.EventDetails.EventsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.EventID = Convert.ToInt32(dr["EventID"]);
                        cls.EventActivityID = Convert.ToInt32(dr["EventActivityID"]);
                        cls.ActivityDescription = Convert.ToString(dr["ActivityDescription"]);
                        cls.Date = Convert.ToDateTime(dr["Date"]);
                        cls.FromTime = Convert.ToString(dr["FromTime"]);
                        cls.ToTime = Convert.ToString(dr["ToTime"]);
                        cls.ShowInInvoice = Convert.ToBoolean(dr["ShowInInvoice"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.HasCoupon = Convert.ToBoolean(dr["HasCoupon"]);
                        
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

        #region SelectByIDTable

        public DataTable SelectByIDTable(int ID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetEventsDetailsByEventID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EventID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
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

        #endregion

        #region SelectAllByEventID

        public List<FinabitAPI.Hotel.EventDetails.EventsDetails> SelectAllByEventID(int ID)
        {
            List<FinabitAPI.Hotel.EventDetails.EventsDetails> clsList = new List<FinabitAPI.Hotel.EventDetails.EventsDetails>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetEventsDetailsByEventID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = new SqlParameter("@EventID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
            cmd.Parameters.Add(param);
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FinabitAPI.Hotel.EventDetails.EventsDetails cls = new FinabitAPI.Hotel.EventDetails.EventsDetails();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.EventID = Convert.ToInt32(dr["EventID"]);
                        cls.EventActivityID = Convert.ToInt32(dr["EventActivityID"]);
                        cls.ActivityDescription = Convert.ToString(dr["ActivityDescription"]);
                        cls.Date = Convert.ToDateTime(dr["Date"]);
                        cls.FromTime = Convert.ToString(dr["FromTime"]);
                        cls.ToTime = Convert.ToString(dr["ToTime"]);
                        cls.ShowInInvoice = Convert.ToBoolean(dr["ShowInInvoice"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        cls.Price = Convert.ToDecimal(dr["Price"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.HasCoupon = Convert.ToBoolean(dr["HasCoupon"]);
                        cls.SetupID = Convert.ToInt32(dr["SetupID"]);
                        cls.MenuID = Convert.ToString(dr["MenuID"]);
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

        #region GetAcomodationValueForEvent

        public DataTable GetAcomodationValueForEvent(int ID)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetAcomodationValueForEvent", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EventDetailsID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = ID;
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

        #endregion

        #region GetEventProfitability

        public DataTable GetEventProfitability(DataTable cls)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetEventProfitability", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Transactions", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = cls;
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

        #endregion
    }
}

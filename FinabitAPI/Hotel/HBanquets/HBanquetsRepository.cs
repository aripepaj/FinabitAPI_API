//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Repository for Hotel Banquets CRUD operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HBanquets
{
    public class HBanquetsRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public HBanquetsRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(HBanquets cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHBanquetsInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@BookedByID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BookedByID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Source", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Source ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Status", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Status ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StartDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StartDate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EndDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EndDate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StartTime", System.Data.SqlDbType.Time);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StartTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EndTime", System.Data.SqlDbType.Time);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EndTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Adult", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Adult ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Child", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Child ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentType", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentType ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BanquetRoom", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BanquetRoom ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Theme", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Theme ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Charges", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Charges ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Paid", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Paid ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Notes", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Notes ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllDayEvent", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllDayEvent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Recurre", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Repeat;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BanquetDetails1", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Detajet ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IsParent", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IsParent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SetUp", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SetUp ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Menu", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Menu ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Note1", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Note1 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Note2", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Note2 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Note3", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Note3 ?? (object)DBNull.Value;
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

        public void Update(HBanquets cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHBanquetsUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BookedByID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BookedByID ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Source", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Source ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Status", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Status ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StartDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StartDate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EndDate", System.Data.SqlDbType.SmallDateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EndDate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StartTime", System.Data.SqlDbType.Time);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StartTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@EndTime", System.Data.SqlDbType.Time);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EndTime;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Adult", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Adult ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Child", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Child ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PaymentType", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PaymentType ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BanquetRoom", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.BanquetRoom ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Theme", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Theme ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Charges", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Charges ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Paid", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Paid ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Notes", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Notes ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllDayEvent", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllDayEvent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Recurre", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Repeat;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@BanquetDetails1", System.Data.SqlDbType.Structured);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Detajet ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@IsParent", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.IsParent;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@SetUp", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.SetUp ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Menu", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Menu ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Note1", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Note1 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Note2", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Note2 ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Note3", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Note3 ?? (object)DBNull.Value;
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

        public void Delete(HBanquets cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHBanquetsDelete", cnn);
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

        public List<HBanquets> SelectAll()
        {
            List<HBanquets> clsList = new List<HBanquets>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHBanquetsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HBanquets cls = new HBanquets();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.BookedByID = dr["BookedByID"] != DBNull.Value ? Convert.ToInt32(dr["BookedByID"]) : null;
                        cls.Source = dr["Source"] != DBNull.Value ? Convert.ToInt32(dr["Source"]) : null;
                        cls.Status = dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : null;
                        cls.StartDate = dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : null;
                        cls.EndDate = dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : null;
                        cls.StartTime = dr["StartTime"] != DBNull.Value ? TimeSpan.Parse((dr["StartTime"]).ToString()!) : TimeSpan.Zero;
                        cls.EndTime = dr["EndTime"] != DBNull.Value ? TimeSpan.Parse((dr["EndTime"]).ToString()!) : TimeSpan.Zero;
                        cls.Adult = dr["Adult"] != DBNull.Value ? Convert.ToInt32(dr["Adult"]) : null;
                        cls.Child = dr["Child"] != DBNull.Value ? Convert.ToInt32(dr["Child"]) : null;
                        cls.PaymentType = dr["PaymentType"] != DBNull.Value ? Convert.ToInt32(dr["PaymentType"]) : null;
                        cls.BanquetRoom = dr["BanquetRoom"] != DBNull.Value ? Convert.ToInt32(dr["BanquetRoom"]) : null;
                        cls.Theme = dr["Theme"] != DBNull.Value ? Convert.ToString(dr["Theme"]) : null;
                        cls.Charges = dr["Charges"] != DBNull.Value ? Convert.ToDecimal(dr["Charges"]) : null;
                        cls.Paid = dr["Paid"] != DBNull.Value ? Convert.ToDecimal(dr["Paid"]) : null;
                        cls.Notes = dr["Notes"] != DBNull.Value ? Convert.ToString(dr["Notes"]) : null;
                        cls.Mysafiri = dr["Guest"] != DBNull.Value ? Convert.ToString(dr["Guest"]) : null;
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

        public DataTable SelectAllTable(string fromDate, string toDate)
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHBanquetsList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = fromDate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = toDate ?? (object)DBNull.Value;
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

        #region SelectByID

        public HBanquets SelectByID(HBanquets cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHBanquetsByID", cnn);
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
                        cls = new HBanquets();
                        cls.ParentID = dr["ParentID"] != DBNull.Value ? Convert.ToInt32(dr["ParentID"]) : 0;
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.BookedByID = dr["BookedByID"] != DBNull.Value ? Convert.ToInt32(dr["BookedByID"]) : null;
                        cls.Status = dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : null;
                        cls.StartDate = dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : null;
                        cls.EndDate = dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : null;
                        cls.StartTime = dr["StartTime"] != DBNull.Value ? TimeSpan.Parse((dr["StartTime"]).ToString()!) : TimeSpan.Zero;
                        cls.EndTime = dr["EndTime"] != DBNull.Value ? TimeSpan.Parse((dr["EndTime"]).ToString()!) : TimeSpan.Zero;
                        cls.Adult = dr["Adult"] != DBNull.Value ? Convert.ToInt32(dr["Adult"]) : null;
                        cls.Child = dr["Child"] != DBNull.Value ? Convert.ToInt32(dr["Child"]) : null;
                        cls.PaymentType = dr["PaymentType"] != DBNull.Value ? Convert.ToInt32(dr["PaymentType"]) : null;
                        cls.BanquetRoom = dr["BanquetRoom"] != DBNull.Value ? Convert.ToInt32(dr["BanquetRoom"]) : null;
                        cls.Theme = dr["Theme"] != DBNull.Value ? Convert.ToString(dr["Theme"]) : null;
                        cls.Charges = dr["Charges"] != DBNull.Value ? Convert.ToDecimal(dr["Charges"]) : null;
                        cls.Paid = dr["Paid"] != DBNull.Value ? Convert.ToDecimal(dr["Paid"]) : null;
                        cls.Notes = dr["Notes"] != DBNull.Value ? Convert.ToString(dr["Notes"]) : null;
                        cls.AllDayEvent = dr["AllDayEvent"] != DBNull.Value ? Convert.ToBoolean(dr["AllDayEvent"]) : false;
                        cls.Repeat = dr["Recurre"] != DBNull.Value ? Convert.ToBoolean(dr["Recurre"]) : false;
                        cls.IsParent = dr["IsParent"] != DBNull.Value ? Convert.ToBoolean(dr["IsParent"]) : false;
                        cls.Note1 = dr["Note1"] != DBNull.Value ? Convert.ToString(dr["Note1"]) : null;
                        cls.Note2 = dr["Note2"] != DBNull.Value ? Convert.ToString(dr["Note2"]) : null;
                        cls.Note3 = dr["Note3"] != DBNull.Value ? Convert.ToString(dr["Note3"]) : null;
                        cls.Menu = dr["Menu"] != DBNull.Value ? Convert.ToString(dr["Menu"]) : null;
                        cls.SetUp = dr["SetUp"] != DBNull.Value ? Convert.ToString(dr["SetUp"]) : null;
                        cls.Source = dr["Source"] != DBNull.Value ? Convert.ToInt32(dr["Source"]) : null;
                        break;
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
            return cls;
        }

        #endregion
        
public List<HBanquets> BanquetsRoomLookup()
{
    List<HBanquets> clsList = new List<HBanquets>();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("spHGetBanquetRooms", cnn);
    cmd.CommandType = CommandType.StoredProcedure;

    try
    {
        cnn.Open();

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                HBanquets cls = new HBanquets();

                cls.ID1 = Convert.ToInt32(dr["ID"]);
                cls.Name = Convert.ToString(dr["Name"]);
                
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

public List<HBanquets> StatusLookup()
{
    List<HBanquets> clsList = new List<HBanquets>();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHGetBanquetStatuses]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;

    try
    {
        cnn.Open();

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                HBanquets cls = new HBanquets();

                cls.ID1 = Convert.ToInt32(dr["ID"]);
                cls.Name = Convert.ToString(dr["Name"]);

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
public List<HBanquets> PaymentLookup()
{
    List<HBanquets> clsList = new List<HBanquets>();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("spHGetPaymentTypes1", cnn);
    cmd.CommandType = CommandType.StoredProcedure;

    try
    {
        cnn.Open();

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                HBanquets cls = new HBanquets();

                cls.ID1 = Convert.ToInt32(dr["ID"]);
                cls.Name = Convert.ToString(dr["Name"]);

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




#region SelectAllForChart

public List<HBanquets> SelectAllForChart()
{
    List<HBanquets> clsList = new List<HBanquets>();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("spHBanquetsListForChart", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    try
    {
        cnn.Open();

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                HBanquets cls = new HBanquets();
                cls.ID = Convert.ToInt32(dr["ID"]);
                cls.BookedByID = Convert.ToInt32(dr["BookedByID"]);
                cls.Source = Convert.ToInt32(dr["Source"]);
                cls.Status = Convert.ToInt32(dr["Status"]);
                cls.StartDate = Convert.ToDateTime(dr["StartDate"]);
                cls.EndDate = Convert.ToDateTime(dr["EndDate"]);
                cls.StartTime = TimeSpan.Parse((dr["StartTime"]).ToString());
                cls.EndTime = TimeSpan.Parse((dr["EndTime"]).ToString());
                cls.Adult = Convert.ToInt32(dr["Adult"]);
                cls.Child = Convert.ToInt32(dr["Child"]);
                cls.PaymentType = Convert.ToInt32(dr["PaymentType"]);
                cls.BanquetRoom = Convert.ToInt32(dr["BanquetRoom"]);
                cls.Theme = Convert.ToString(dr["Theme"]);
                cls.Charges = Convert.ToDecimal(dr["Charges"]);
                cls.Paid = Convert.ToDecimal(dr["Paid"]);
                cls.Notes = Convert.ToString(dr["Notes"]);
                cls.Mysafiri = Convert.ToString(dr["Guest"]);
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

public DataTable GetEvent(int ID)
{
    DataTable dtList = new DataTable();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spGetBanquetEvents]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    SqlParameter param;
    param = new SqlParameter("@parentID", SqlDbType.Int);
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
public DataTable SelectByIDTable(HBanquets cls )
{
    DataTable dtList = new DataTable();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHBanquetsByID]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    SqlParameter param;
 

param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
param.Direction = ParameterDirection.Input;
param.Value = cls.ID;
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

public DataTable SelectDateAndTime(HBanquets cls)
{
    DataTable dtList = new DataTable();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHBDateAndTime]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    SqlParameter param;


    param = new SqlParameter("@ParentID", System.Data.SqlDbType.Int);
    param.Direction = ParameterDirection.Input;
    param.Value = cls.ID;
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
public int GetLatestParentID()
{
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHBGetLatestParentID]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    cnn.Open();
    int ID = Convert.ToInt32(cmd.ExecuteScalar());
    cnn.Close();
    return ID;
}

public int GetLatestID()
{
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHBGetLatestID]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    cnn.Open();
    int ID = Convert.ToInt32(cmd.ExecuteScalar());
    cnn.Close();
    return ID;
}
    }
}

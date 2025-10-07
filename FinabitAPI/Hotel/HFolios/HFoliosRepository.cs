//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	CRUD for HFolios
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HFolios
{
    public class HFoliosRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public HFoliosRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

#region Insert 

public void Insert(HFolios cls) 
{
SqlConnection cnn = _dbAccess.GetConnection();
SqlCommand cmd = new SqlCommand("spHFoliosInsert", cnn);
cmd.CommandType = CommandType.StoredProcedure;

SqlParameter param;


param = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
param.Direction = ParameterDirection.Input;
param.Value = cls.ReservationId;
cmd.Parameters.Add(param);

param = new SqlParameter("@PartnerId", System.Data.SqlDbType.Int);
param.Direction = ParameterDirection.Input;
param.Value = cls.PartnerId;
cmd.Parameters.Add(param);


param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
param.Direction = ParameterDirection.Output;
cmd.Parameters.Add(param);

param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
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
  cls.ErrorID = -1 ;
  cls.ErrorDescription = ex.Message ;
  cnn.Close();
 }

}

#endregion 

#region Update 

public void Update(HFolios cls) 
{
SqlConnection cnn = _dbAccess.GetConnection();
SqlCommand cmd = new SqlCommand("spHFoliosUpdate", cnn);
cmd.CommandType = CommandType.StoredProcedure;

SqlParameter param;

param = new SqlParameter("@ReservationID", System.Data.SqlDbType.Int);
param.Direction = ParameterDirection.Input;
param.Value = cls.ReservationId;
cmd.Parameters.Add(param);

param = new SqlParameter("@PartnerId", System.Data.SqlDbType.Int);
param.Direction = ParameterDirection.Input;
param.Value = cls.PartnerId;
cmd.Parameters.Add(param);

param = new SqlParameter("@FolioID", System.Data.SqlDbType.Int);
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
  cls.ErrorID = -1 ;
  cls.ErrorDescription = ex.Message ;
  cnn.Close();
 }

}

#endregion 

#region Delete 

public void Delete(HFolios cls) 
{
SqlConnection cnn = new SqlConnection();
cnn = _dbAccess.GetConnection();
SqlCommand cmd = new SqlCommand("spHFoliosDelete", cnn);
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
  cls.ErrorID = -1 ;
  cls.ErrorDescription = ex.Message ;
  cnn.Close();
 }

}

#endregion 

#region SelectAll 

public List<HFolios> SelectAll()
{
List<HFolios> clsList = new List<HFolios>();
SqlConnection cnn = _dbAccess.GetConnection();
SqlCommand cmd = new SqlCommand("spHFoliosList", cnn);
cmd.CommandType = CommandType.StoredProcedure;
try
{
 cnn.Open();

 SqlDataReader dr = cmd.ExecuteReader();
if (dr.HasRows)
 {
 while (dr.Read())
 {
  HFolios cls = new HFolios();
  cls.ID = Convert.ToInt32(dr["ID"]);
  cls.Folio = Convert.ToString(dr["Folio"]);
  cls.ReservationId = Convert.ToInt32(dr["ReservationId"]);
  cls.PartnerId = Convert.ToInt32(dr["PartnerId"]);
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
SqlCommand cmd = new SqlCommand("spHFoliosList", cnn);
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

public DataTable SelectByID(HFolios cls )
{
    DataTable dtList = new DataTable();
SqlConnection cnn = _dbAccess.GetConnection();
SqlCommand cmd = new SqlCommand("spHFoliosByID", cnn);
cmd.CommandType = CommandType.StoredProcedure;

SqlParameter param;

param = new SqlParameter("@FolioID", System.Data.SqlDbType.Int);
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
public HFolios SelectByIDClass(HFolios cls)
{
    HFolios result = null;
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("spHFoliosByID", cnn);
    cmd.CommandType = CommandType.StoredProcedure;

    SqlParameter param;

    param = new SqlParameter("@FolioID", SqlDbType.Int);
    param.Direction = ParameterDirection.Input;
    param.Value = cls.ID;
    cmd.Parameters.Add(param);

    try
    {
        cnn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            result = new HFolios();

            result.ID = Convert.ToInt32(reader["FolioID"]);
            result.Folio = reader["Folio"].ToString();
            result.ReservationId = Convert.ToInt32(reader["ReservationId"]);
            result.PartnerId = Convert.ToInt32(reader["PartnerId"]);
            result.PartnerName = reader["PartnerName"].ToString();
           
            result.VATPercent = Convert.ToDecimal(reader["VATPercent"]);
        }
        reader.Close();
        cnn.Close();
    }
    catch (Exception ex)
    {
        string exp = ex.Message; // consider logging properly
        cnn.Close();
    }

    return result;
}

public DataTable SelectByReservationID(HFolios cls)
{
    DataTable dtList = new DataTable();
    SqlConnection conn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHFoliosByReservationID]", conn);
    cmd.CommandType = CommandType.StoredProcedure;
    var param = cmd.Parameters.Add("@ReservationID", SqlDbType.Int);
    param.Value = cls.ReservationId;

    SqlDataAdapter dadap = new SqlDataAdapter(cmd);
    try
    {
        conn.Open();

        dadap.Fill(dtList);
        conn.Close();
    }
    catch (Exception ex)
    {
        string exp = ex.Message;
        conn.Close();
    }
    return dtList;
}


public List<HFolios> SelectAllByReservationId(int ReservationId)
{
    List<HFolios> clsList = new List<HFolios>();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("spHFoliosByReservationID", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    var param = cmd.Parameters.Add("@ReservationId", SqlDbType.Int);
    param.Value = ReservationId;
    try
    {
        cnn.Open();

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                var cls = new HFolios
                {
                    ID=dr.IsDBNull(dr.GetOrdinal("FolioID"))
                                ?0
                                :dr.GetInt32(dr.GetOrdinal("FolioID")),
                    Folio = dr.IsDBNull(dr.GetOrdinal("DisplayMember"))
                                ?string.Empty
                                : dr.GetString(dr.GetOrdinal("DisplayMember")),
                    PartnerId = dr.IsDBNull(dr.GetOrdinal("PartnerId"))
                                ?0
                                :dr.GetInt32(dr.GetOrdinal("PartnerId")),
                    PartnerName = dr.IsDBNull(dr.GetOrdinal("PartnerName"))
                                ?string.Empty
                                : dr.GetString(dr.GetOrdinal("PartnerName")),
                    ChargeValue = dr.IsDBNull(dr.GetOrdinal("ChargeValue"))
                                ?0
                                : dr.GetDecimal(dr.GetOrdinal("ChargeVAlue")),
                    PaidValue = dr.IsDBNull(dr.GetOrdinal("PaidValue"))
                                ?0
                                : dr.GetDecimal(dr.GetOrdinal("PaidValue")),
                    BilancValue = dr.IsDBNull(dr.GetOrdinal("BilancValue"))
                                ?0
                                : dr.GetDecimal(dr.GetOrdinal("BilancValue")),
                    IsMaster = dr.IsDBNull(dr.GetOrdinal("IsMaster"))
                                ? Convert.ToBoolean(null)
                                : dr.GetBoolean(dr.GetOrdinal("IsMaster")),
                   VATPercent = dr.GetDecimal(dr.GetOrdinal("VATPercent"))
                };
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

public DataTable GetInformationInvoice(int ReservationId)
{
    DataTable dtList = new DataTable();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHInformationInvoice]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    SqlParameter param;

    param = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
    param.Direction = ParameterDirection.Input;
    param.Value = ReservationId;
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
public DataTable GetAllFoliosOfReservationStatusCheckIn()
{
    DataTable dtList = new DataTable();
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("[spHAllFoliosWithRStatusCheckIn]", cnn);
    cmd.CommandType = CommandType.StoredProcedure;
    SqlParameter param;


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
public void UpdateVAT(HFolios cls)
{
    SqlConnection cnn = _dbAccess.GetConnection();
    SqlCommand cmd = new SqlCommand("spHFoliosVATUpdate", cnn);
    cmd.CommandType = CommandType.StoredProcedure;

    SqlParameter param;


    param = new SqlParameter("@ReservationID", System.Data.SqlDbType.Int);
    param.Direction = ParameterDirection.Input;
    param.Value = cls.ReservationId;
    cmd.Parameters.Add(param);

    param = new SqlParameter("@FolioID", System.Data.SqlDbType.Int);
    param.Direction = ParameterDirection.Input;
    param.Value = cls.ID;
    cmd.Parameters.Add(param);

    param = new SqlParameter("@VATPercent", System.Data.SqlDbType.Decimal);
    param.Direction = ParameterDirection.Input;
    param.Value = cls.VATPercent;
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
    }
}

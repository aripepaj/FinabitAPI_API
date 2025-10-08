using Microsoft.Data.SqlClient;
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.ExtraCharge
{
    // Use ExtraChargeEntity alias to avoid namespace conflicts
    using ExtraChargeEntity = global::ExtraCharge;

    public class ExtraChargeRepository
    {
        private readonly DBAccess _dbAccess;

        public ExtraChargeRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        #region Insert 

        public void Insert(ExtraChargeEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeName ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Rate", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Rate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account ?? (object)DBNull.Value;
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

        public void Update(ExtraChargeEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ExtraChargeName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeName ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Rate", System.Data.SqlDbType.Decimal);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Rate ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Account", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Account ?? (object)DBNull.Value;
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

        public void Delete(ExtraChargeEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeID;
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

        public List<ExtraChargeEntity> SelectAll()
        {
            List<ExtraChargeEntity> clsList = new List<ExtraChargeEntity>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ExtraChargeEntity cls = new ExtraChargeEntity();
                        cls.ExtraChargeID = Convert.ToInt32(dr["ExtraChargeID"]);
                        cls.ExtraChargeName = Convert.ToString(dr["ExtraChargeName"]) ?? "";
                        cls.Rate = dr["Rate"] == DBNull.Value ? null : Convert.ToDecimal(dr["Rate"]);
                        cls.Account = Convert.ToString(dr["Account"]) ?? "";
                        cls.EventActivityID = Convert.ToInt32(dr["EventActivityID"]);
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
            SqlCommand cmd = new SqlCommand("spExtraChargeList", cnn);
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

        public ExtraChargeEntity? SelectByID(ExtraChargeEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spExtraChargeByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ExtraChargeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ExtraChargeID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new ExtraChargeEntity();
                        cls.ExtraChargeID = Convert.ToInt32(dr["ExtraChargeID"]);
                        cls.ExtraChargeName = Convert.ToString(dr["ExtraChargeName"]) ?? "";
                        cls.Rate = dr["Rate"] == DBNull.Value ? null : Convert.ToDecimal(dr["Rate"]);
                        cls.Account = Convert.ToString(dr["Account"]) ?? "";
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
    }
}

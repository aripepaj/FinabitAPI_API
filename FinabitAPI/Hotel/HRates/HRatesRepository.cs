using Microsoft.Data.SqlClient;
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HRates
{
    // Use HRatesEntity alias to avoid namespace conflicts
    using HRatesEntity = global::HRates;

    public class HRatesRepository
    {
        private readonly DBAccess _dbAccess;

        public HRatesRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        #region Insert 

        public void Insert(HRatesEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRatesInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RateName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RateName ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomTypeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
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

        public void Update(HRatesEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRatesUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RateName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RateName ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoomTypeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.RoomTypeID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Value", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
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

        public void Delete(HRatesEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRatesDelete", cnn);
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

        public List<HRatesEntity> SelectAll()
        {
            List<HRatesEntity> clsList = new List<HRatesEntity>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRatesList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HRatesEntity cls = new HRatesEntity();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RateName = Convert.ToString(dr["RateName"]) ?? "";
                        cls.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                        cls.RoomTypeName = Convert.ToString(dr["RoomTypeName"]) ?? "";
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
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
            SqlCommand cmd = new SqlCommand("spHRatesList", cnn);
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

        public HRatesEntity? SelectByID(HRatesEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHRatesByID", cnn);
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
                        cls = new HRatesEntity();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.RateName = Convert.ToString(dr["RateName"]) ?? "";
                        cls.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                        cls.Value = Convert.ToDecimal(dr["Value"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
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

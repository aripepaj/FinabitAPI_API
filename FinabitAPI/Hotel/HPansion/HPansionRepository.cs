using Microsoft.Data.SqlClient;
using System.Data;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HPansion
{
    // Use HPansionEntity alias to avoid namespace conflicts
    using HPansionEntity = global::HPansion;

    public class HPansionRepository
    {
        private readonly DBAccess _dbAccess;

        public HPansionRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        #region Insert 

        public void Insert(HPansionEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHPansionInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@PansionName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PansionName ?? (object)DBNull.Value;
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

        public void Update(HPansionEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHPansionUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PansionName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PansionName ?? (object)DBNull.Value;
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

        public void Delete(HPansionEntity cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHPansionDelete", cnn);
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

        public List<HPansionEntity> SelectAll()
        {
            List<HPansionEntity> clsList = new List<HPansionEntity>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHPansionList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cnn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        HPansionEntity cls = new HPansionEntity();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.PansionName = Convert.ToString(dr["PansionName"]) ?? "";
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
            SqlCommand cmd = new SqlCommand("spHPansionList", cnn);
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

        public HPansionEntity? SelectByID(HPansionEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHPansionByID", cnn);
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
                        cls = new HPansionEntity();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.PansionName = Convert.ToString(dr["PansionName"]) ?? "";
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

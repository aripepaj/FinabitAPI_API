using Finabit_API.Models;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AutoBit_WebInvoices.Models
{
    public class ItemLocationRepository
    {

        private readonly DBAccess _dbAccess;
        public ItemLocationRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        private SqlConnection GetConnection()
            => _dbAccess != null ? _dbAccess.GetConnection() : GlobalRepository.GetConnection();


        #region Insert 

        public void Insert(ItemLocation cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spItemLocationInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Name;
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

        public void Update(ItemLocation cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spItemLocationUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Name;
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

        public void Delete(ItemLocation cls)
        {
            SqlConnection cnn = new SqlConnection();
            cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spItemLocationDelete", cnn);
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

        public List<ItemLocation> SelectAll()
        {
            var list = new List<ItemLocation>();

            using var cnn = _dbAccess.GetConnection();
            using var cmd = new SqlCommand("spItemLocationList", cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cnn.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new ItemLocation
                {
                    ID = dr.GetInt32(dr.GetOrdinal("ID")),
                    Name = dr["Name"].ToString()
                });
            }

            return list;
        }

        #endregion

        #region SelectAllTable 

        public DataTable SelectAllTable()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spItemLocationList", cnn);
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

        public static ItemLocation SelectByID(ItemLocation cls)
        {
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spItemLocationByID", cnn);
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
                        cls = new ItemLocation();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.Name = Convert.ToString(dr["Name"]);
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
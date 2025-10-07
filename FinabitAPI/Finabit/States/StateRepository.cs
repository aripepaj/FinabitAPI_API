//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Repository for State CRUD operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Utilis;
using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Finabit.States
{
    public class StateRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public StateRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(State cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spStateInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@StateName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StateName ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
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

        public void Update(State cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spStateUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@StateID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@StateName", System.Data.SqlDbType.NVarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.StateName ?? (object)DBNull.Value;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
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

        public void Delete(State cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spStateDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@StateID", System.Data.SqlDbType.Int);
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

        public List<State> SelectAll()
        {
            List<State> clsList = new List<State>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spStateList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        State cls = new State();
                        cls.ID = Convert.ToInt32(dr["StateID"]);
                        cls.StateName = Convert.ToString(dr["StateName"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
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
            SqlCommand cmd = new SqlCommand("spStateList", cnn);
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
                ErrorID = -1;
                ErrorDescription = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        #endregion

        #region SelectByID

        public State SelectByID(State cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spStateByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@StateID", System.Data.SqlDbType.Int);
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
                        cls = new State();
                        cls.ID = Convert.ToInt32(dr["StateID"]);
                        cls.StateName = Convert.ToString(dr["StateName"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        break;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorID = -1;
                ErrorDescription = ex.Message;
                cnn.Close();
            }
            return cls;
        }

        #endregion
    }
}

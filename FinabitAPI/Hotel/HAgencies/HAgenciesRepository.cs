//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Repository for Hotel Agencies CRUD operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Utilis;

namespace FinabitAPI.Hotel.HAgencies
{
    public class HAgenciesRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public HAgenciesRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert 

        public void Insert(FinabitAPI.Hotel.HAgencies.HAgencies cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHAgenciesInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@AgencyName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AgencyName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerID;
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

        public void Update(FinabitAPI.Hotel.HAgencies.HAgencies cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHAgenciesUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AgencyName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AgencyName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PartnerID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.PartnerID;
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

        public void Delete(FinabitAPI.Hotel.HAgencies.HAgencies cls) 
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHAgenciesDelete", cnn);
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

        public List<FinabitAPI.Hotel.HAgencies.HAgencies> SelectAll()
        {
            List<FinabitAPI.Hotel.HAgencies.HAgencies> clsList = new List<FinabitAPI.Hotel.HAgencies.HAgencies>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHAgenciesList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FinabitAPI.Hotel.HAgencies.HAgencies cls = new FinabitAPI.Hotel.HAgencies.HAgencies();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.AgencyName = Convert.ToString(dr["AgencyName"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
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
            SqlCommand cmd = new SqlCommand("spHAgenciesList", cnn);
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

        public FinabitAPI.Hotel.HAgencies.HAgencies SelectByID(FinabitAPI.Hotel.HAgencies.HAgencies cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spHAgenciesByID", cnn);
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
                        cls = new FinabitAPI.Hotel.HAgencies.HAgencies();
                        cls.ID = Convert.ToInt32(dr["ID"]);
                        cls.AgencyName = Convert.ToString(dr["AgencyName"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);

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

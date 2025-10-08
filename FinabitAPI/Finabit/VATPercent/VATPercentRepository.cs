//-- =============================================
//-- Author:		Generated
//-- Create date: 08.10.25 
//-- Description:	Repository for VAT Percent operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Utilis;
using VATPercentEntity = VATPercent;

namespace FinabitAPI.Finabit.VATPercent
{
    public class VATPercentRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public VATPercentRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(VATPercentEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spVATPercentInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@VATName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATPercent", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATPercents;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@InsBy", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.InsBy;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DatexGroup", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DatexGroup;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NovitusGroup", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.NovitusGroup;
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

        public void Update(VATPercentEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spVATPercentUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@VATID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATName", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATName;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@VATPercent", System.Data.SqlDbType.Money);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATPercents;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@LUB", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.LUB;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@DatexGroup", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.DatexGroup;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@NovitusGroup", System.Data.SqlDbType.VarChar);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.NovitusGroup;
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

        public void Delete(VATPercentEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spVATPercentDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@VATID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATID;
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

        public List<VATPercentEntity> SelectAll()
        {
            List<VATPercentEntity> clsList = new List<VATPercentEntity>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spVATPercentList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        VATPercentEntity cls = new VATPercentEntity();
                        cls.VATID = Convert.ToInt32(dr["VATID"]);
                        cls.VATName = Convert.ToString(dr["VATName"]);
                        cls.VATPercents = Convert.ToDecimal(dr["VATPercent"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.DatexGroup = Convert.ToString(dr["DatexGroup"]);
                        cls.NovitusGroup = Convert.ToString(dr["NovitusGroup"]);
                        clsList.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
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
            SqlCommand cmd = new SqlCommand("spVATPercentList", cnn);
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
                ErrorDescription = ex.Message;
                cnn.Close();
            }
            return dtList;
        }

        #endregion

        #region SelectByID

        public VATPercentEntity SelectByID(VATPercentEntity cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spVATPercentByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@VATID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.VATID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new VATPercentEntity();
                        cls.VATID = Convert.ToInt32(dr["VATID"]);
                        cls.VATName = Convert.ToString(dr["VATName"]);
                        cls.VATPercents = Convert.ToDecimal(dr["VATPercent"]);
                        cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.DatexGroup = Convert.ToString(dr["DatexGroup"]);
                        cls.NovitusGroup = Convert.ToString(dr["NovitusGroup"]);
                        break;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                ErrorDescription = ex.Message;
                cnn.Close();
            }
            return cls;
        }

        #endregion
    }
}
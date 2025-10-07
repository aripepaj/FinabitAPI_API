//-- =============================================
//-- Author:		Generated
//-- Create date: 07.10.25 
//-- Description:	Repository for Form Configuration CRUD operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Core.Global;
using FinabitAPI.Utilis;
using FinabitAPI.Core.User;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Core.User.dto;

namespace FinabitAPI.Core.FormConfiguration
{
    public class FormConfigurationRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public FormConfigurationRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        #region Insert

        public void Insert(FinabitAPI.Core.FormConfiguration.FormConfiguration cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spFormConfigInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@RoleID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Role.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FormID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Form.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowShow", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowShow;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowInsert", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowInsert;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowUpdate", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowUpdate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowDelete", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowDelete;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowPrint", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowPrint;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReadOnly", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReadOnly;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Enabled", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Enabled;
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

        public void Update(FinabitAPI.Core.FormConfiguration.FormConfiguration cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spFormConfigUpdate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FormConfigID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@RoleID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Role.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@FormID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Form.ID;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowShow", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowShow;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowInsert", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowInsert;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowUpdate", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowUpdate;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowDelete", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowDelete;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@AllowPrint", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.AllowPrint;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@ReadOnly", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.ReadOnly;
            cmd.Parameters.Add(param);

            param = new SqlParameter("@Enabled", System.Data.SqlDbType.Bit);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.Enabled;
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

        public void Delete(FinabitAPI.Core.FormConfiguration.FormConfiguration cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spFormConfigDelete", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@FormConfigID", System.Data.SqlDbType.Int);
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

        public List<FinabitAPI.Core.FormConfiguration.FormConfiguration> SelectAll()
        {
            List<FinabitAPI.Core.FormConfiguration.FormConfiguration> clsList = new List<FinabitAPI.Core.FormConfiguration.FormConfiguration>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spFormConfigList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FinabitAPI.Core.FormConfiguration.FormConfiguration cls = new FinabitAPI.Core.FormConfiguration.FormConfiguration();
                        cls.ID = Convert.ToInt32(dr["FormConfigID"]);
                        cls.Role.ID = Convert.ToInt32(dr["RoleID"]);
                        cls.Role.RoleName = dr["RoleName"]?.ToString();
                        cls.Form.ID = Convert.ToInt32(dr["FormID"]);
                        cls.Form.Name = dr["Name"]?.ToString();
                        cls.Form.Caption = dr["Caption"]?.ToString();
                        cls.AllowShow = Convert.ToBoolean(dr["AllowShow"]);
                        cls.AllowInsert = Convert.ToBoolean(dr["AllowInsert"]);
                        cls.AllowUpdate = Convert.ToBoolean(dr["AllowUpdate"]);
                        cls.AllowDelete = Convert.ToBoolean(dr["AllowDelete"]);
                        cls.AllowPrint = Convert.ToBoolean(dr["AllowPrint"]);
                        cls.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        cls.Enabled = Convert.ToBoolean(dr["Enabled"]);
                        cls.LUB = Convert.ToInt32(dr["LUB"]);
                        cls.LUD = Convert.ToDateTime(dr["LUD"]);
                        cls.LUN = Convert.ToInt32(dr["LUN"]);
                        cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
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
            SqlCommand cmd = new SqlCommand("spFormConfigList", cnn);
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

        public FormConfiguration SelectByID(FormConfiguration cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spFormConfigByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@FormConfigID", System.Data.SqlDbType.Int);
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
                        cls = new FormConfiguration();
                        cls.ID = Convert.ToInt32(dr["FormConfigID"]);
                        cls.Role.ID = Convert.ToInt32(dr["RoleID"]);
                        cls.Form.ID = Convert.ToInt32(dr["FormID"]);
                        cls.AllowShow = Convert.ToBoolean(dr["AllowShow"]);
                        cls.AllowInsert = Convert.ToBoolean(dr["AllowInsert"]);
                        cls.AllowUpdate = Convert.ToBoolean(dr["AllowUpdate"]);
                        cls.AllowDelete = Convert.ToBoolean(dr["AllowDelete"]);
                        cls.AllowPrint = Convert.ToBoolean(dr["AllowPrint"]);
                        cls.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        cls.Enabled = Convert.ToBoolean(dr["Enabled"]);
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
                string exp = ex.Message;
                cnn.Close();
            }
            return cls;
        }

        #endregion

        #region GetUserRight

        public FormConfiguration GetUserRight(string FormName)
        {
            FormConfiguration cls = new FormConfiguration();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetUserRight", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            try
            {
                param = new SqlParameter("@FormName", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = FormName;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = GlobalAppData.UserID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@RoleID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = GlobalAppData.RoleID;
                cmd.Parameters.Add(param);

                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls.Role.ID = Convert.ToInt32(dr["RoleID"]);
                        cls.Form.Name = dr["Name"].ToString();
                        cls.AllowShow = Convert.ToBoolean(dr["AllowShow"]);
                        cls.AllowInsert = Convert.ToBoolean(dr["AllowInsert"]);
                        cls.AllowUpdate = Convert.ToBoolean(dr["AllowUpdate"]);
                        cls.AllowDelete = Convert.ToBoolean(dr["AllowDelete"]);
                        cls.AllowPrint = Convert.ToBoolean(dr["AllowPrint"]);
                        cls.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        cls.Enabled = Convert.ToBoolean(dr["Enabled"]);
                        cls.AllowChangeStatus = Convert.ToBoolean(dr["AllowChangeStatus"]);
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

        #region GetUserRight

        public FormConfiguration GetUserRightForAdmin(string FormName, int UserID, int RoleID)
        {
            FormConfiguration cls = new FormConfiguration();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetUserRight", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            try
            {
                param = new SqlParameter("@FormName", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = FormName;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = UserID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@RoleID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = RoleID;
                cmd.Parameters.Add(param);

                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls.Role.ID = Convert.ToInt32(dr["RoleID"]);
                        cls.Form.Name = dr["Name"].ToString();
                        cls.AllowShow = Convert.ToBoolean(dr["AllowShow"]);
                        cls.AllowInsert = Convert.ToBoolean(dr["AllowInsert"]);
                        cls.AllowUpdate = Convert.ToBoolean(dr["AllowUpdate"]);
                        cls.AllowDelete = Convert.ToBoolean(dr["AllowDelete"]);
                        cls.AllowPrint = Convert.ToBoolean(dr["AllowPrint"]);
                        cls.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        cls.Enabled = Convert.ToBoolean(dr["Enabled"]);
                        cls.AllowChangeStatus = Convert.ToBoolean(dr["AllowChangeStatus"]);
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

        #region GetUserRight_ALL

        public List<FormConfiguration> GetUserRight_ALL()
        {
            List<FormConfiguration> clsColl = new List<FormConfiguration>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetUserRight_ALL", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            try
            {
                param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = GlobalAppData.UserID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@RoleID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = GlobalAppData.RoleID;
                cmd.Parameters.Add(param);

                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        FormConfiguration cls = new FormConfiguration();
                        cls.Role.ID = Convert.ToInt32(dr["RoleID"]);
                        //cls.UserID = Convert.ToInt32(dr["UserID"]);
                        cls.Form.Name = dr["Name"].ToString();
                        cls.AllowShow = Convert.ToBoolean(dr["AllowShow"]);
                        cls.AllowInsert = Convert.ToBoolean(dr["AllowInsert"]);
                        cls.AllowUpdate = Convert.ToBoolean(dr["AllowUpdate"]);
                        cls.AllowDelete = Convert.ToBoolean(dr["AllowDelete"]);
                        cls.AllowPrint = Convert.ToBoolean(dr["AllowPrint"]);
                        cls.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        cls.Enabled = Convert.ToBoolean(dr["Enabled"]);
                        clsColl.Add(cls);
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                string exp = ex.Message;
                cnn.Close();
            }
            return clsColl;
        }

        #endregion

        #region GetAuthorizationName

        public Users GetAuthorizationName(string FormName, int userID, string Password)
        {
            Users cls = new Users();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("[spGetAuthorizationName]", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            try
            {


                param = new SqlParameter("@FormName", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = FormName;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@UserID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = userID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Password", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                string strPassword = GlobalAppData.EncryptDES(Password);
                param.Value = strPassword;
                cmd.Parameters.Add(param);

                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        cls.UserName = dr["UserName"].ToString();
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

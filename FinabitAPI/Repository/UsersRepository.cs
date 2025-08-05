using Finabit_API.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AutoBit_WebInvoices.Models
{
    public class UsersRepository
    {

        public Users GetLoginUser(string UserName, string Password)
        {
            Users cls = new Users();
            SqlConnection cnn = GlobalRepository.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetLoginUser", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            try
            {
                param = new SqlParameter("@UserName", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = UserName;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Password", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                string strPassword = GlobalAppData.EncryptDES(Password);
                param.Value = strPassword;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@WindowsUser", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = Environment.UserName;
                cmd.Parameters.Add(param);


                cls.HasConnections = true;
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls.ID = Convert.ToInt32(dr["UserID"]);
                        //cls.Employee.ID = Convert.ToInt32(dr["EmpID"]);
                        // cls.Employee.FirstName = Convert.ToString(dr["FirstName"]);
                        cls.UserName = Convert.ToString(dr["Username"]);
                        cls.ExpireDate = Convert.ToDateTime(dr["ExpireDate"]);
                        cls.Status = Convert.ToBoolean(dr["Status"]);
                        cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                        cls.DefaultPartnerID = Convert.ToInt32(dr["PosPartnerID"]);
                        cls.DefaultPartnerName = Convert.ToString(dr["PosPartnerName"]);
                        cls.RoleID = Convert.ToInt32(dr["RoleID"]);
                        //IsDeleteWithAuthorization
                        cls.IsDeleteWithAuthorization = Convert.ToBoolean(dr["IsDeleteWithAuthorization"]);
                        cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
                        cls.IsAuthoriser = Convert.ToBoolean(dr["IsAuthoriser"]);
                        cls.DisableDateInDocuments = Convert.ToBoolean(dr["DisableDateInDocuments"]);
                        break;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                cls.HasConnections = false;
                string exp = ex.Message;
                cnn.Close();
            }
            return cls;
        }
    }
}
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Utilis;


namespace AutoBit_WebInvoices.Models
{
    public class EmployeesRepository
    {

        private readonly DBAccess _dbAccess;

        public EmployeesRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        #region SelectAll

        public List<Employees> SelectAll()
        {
            List<Employees> clsList = new List<Employees>();
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEmployeesList", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Employees cls = new Employees();
                        cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                     
                        cls.FirstName = Convert.ToString(dr["FirstName"]);
                        cls.LastName = dr["LastName"] == DBNull.Value ? "" : Convert.ToString(dr["LastName"]);
                      
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


        public Employees SelectByID(Employees cls)
        {
            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spEmployeesByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;

            param = new SqlParameter("@EmployeeID", System.Data.SqlDbType.Int);
            param.Direction = ParameterDirection.Input;
            param.Value = cls.EmpID;
            cmd.Parameters.Add(param);

            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new Employees();
                        cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                       // cls.PersonalNo = Convert.ToString(dr["PersonalNo"]);
                        cls.FirstName = Convert.ToString(dr["FirstName"]);
                        cls.LastName = Convert.ToString(dr["LastName"]);
                       // cls.Gender = Convert.ToChar(dr["Gender"]);
                       // cls.Title.ID = Convert.ToInt32(dr["TitleID"]);
                      //  cls.Title.TitleName = Convert.ToString(dr["TitleName"]);
                      //
                       
                        cls.CashAccount = Convert.ToString(dr["CashAccount"]);
                  
                    

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

    }
}
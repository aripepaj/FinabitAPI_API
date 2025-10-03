using FinabitAPI.Core.Global.dto;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

public class DepartmentRepository
{
    private readonly DBAccess _dbAccess;

    public DepartmentRepository(DBAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public Department SelectByID(int departmentId)
    {
        Department cls = null;
        using (SqlConnection cnn = _dbAccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand("spDepartmentByID", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

            cnn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cls = new Department
                    {
                        ID = Convert.ToInt32(dr["DepartmentID"]),
                        DepartmentName = Convert.ToString(dr["DepartmentName"]),
                        Account = Convert.ToString(dr["Account"]),
                        PriceMenuID = Convert.ToInt32(dr["PriceMenuID"]),
                        PriceMenuName = Convert.ToString(dr["PriceMenuName"]),
                        LUD = Convert.ToDateTime(dr["LUD"]),
                        LUB = Convert.ToInt32(dr["LUB"]),
                        LUN = Convert.ToInt32(dr["LUN"]),
                        AllowNegative = dr["AllowNegative"] == DBNull.Value ? true : Convert.ToBoolean(dr["AllowNegative"]),
                        CompanyID = dr["CompanyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CompanyID"])
                    };
                    break;
                }
            }
            cnn.Close();
        }
        return cls;
    }
}

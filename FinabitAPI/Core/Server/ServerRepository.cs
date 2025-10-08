//-- =============================================
//-- Author:		Generated
//-- Create date: 08.10.25 
//-- Description:	Repository for Server operations
//-- =============================================
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Utilis;
using ServerEntity = Server;

namespace FinabitAPI.Core.Server
{
    public class ServerRepository
    {
        public int ErrorID = 0;
        public string ErrorDescription = "";

        private readonly DBAccess _dbAccess;

        public ServerRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess ?? throw new ArgumentNullException(nameof(dbAccess));
        }

        public DateTime GetServerTime()
        {
            DateTime ServerDate = new DateTime();

            SqlConnection cnn = _dbAccess.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetServerDate", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            try
            {
                cnn.Open();
                object ob = cmd.ExecuteScalar();
                ServerDate = ob == null ? DateTime.Now : DateTime.Parse(ob.ToString() ?? "");
                cnn.Close();
            }
            catch (Exception ex)
            {
                ServerDate = DateTime.Now;
                ErrorDescription = ex.Message;
                cnn.Close();
            }

            return ServerDate;
        }

        public static List<ServerEntity> ListSevers(string Connection)
        {
            List<ServerEntity> clsList = new List<ServerEntity>();
            ServerEntity cls = new ServerEntity();

            SqlConnection cnn = new SqlConnection(Connection);
            SqlCommand cmd = cnn.CreateCommand();

            string SQLQuery = "CREATE TABLE #servers(sname VARCHAR(255)) ";
            SQLQuery += "INSERT #servers EXEC master..XP_CMDShell 'OSQL -L' ";
            SQLQuery += "DELETE #servers WHERE sname='Servers:' ";
            SQLQuery += "SELECT LTRIM(sname) as ServerName FROM #servers ";
            SQLQuery += "WHERE sname != 'NULL' AND sname NOT LIKE '%(local)%' ";
            SQLQuery += "DROP TABLE #servers";

            cmd.CommandText = SQLQuery;
            //cmd.CommandTimeout = 2;
            try
            {
                cnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cls = new ServerEntity();
                        cls.Name = Convert.ToString(dr["ServerName"]);
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
    }
}
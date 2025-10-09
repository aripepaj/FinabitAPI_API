//-- =============================================
//-- Author:		Gazmend Mehmeti
//-- Create date: 27.04.09 8:37:37 PM
//-- Description:	DALGlobal
//-- =============================================

using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinabitAPI.Core.Global
{
    public class GlobalRepository
    {
        // old path still works as a fallback
        public static string? ConnectionString { get; private set; }

        // DI hooks
        private static IServiceProvider? _rootServices;
        private static IHttpContextAccessor? _http;

        public static void Initialize(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("strCnn");
        }

        public static void UseDbAccess(IServiceProvider services)
        {
            _rootServices = services;
            _http = services.GetService<IHttpContextAccessor>();
        }

        public static SqlConnection GetConnection()
        {
            var http = _http?.HttpContext;
            if (http != null)
            {
                var db = http.RequestServices.GetService<FinabitAPI.Utilis.DBAccess>();
                if (db != null) return db.GetConnection();
            }

            if (_rootServices != null)
            {
                using var scope = _rootServices.CreateScope();
                var db = scope.ServiceProvider.GetService<FinabitAPI.Utilis.DBAccess>();
                if (db != null) return db.GetConnection();
            }

            if (!string.IsNullOrWhiteSpace(ConnectionString))
                return new SqlConnection(ConnectionString);

            throw new InvalidOperationException(
                "GlobalRepository: no way to create SqlConnection. " +
                "Call GlobalRepository.UseDbAccess(app.Services) or Initialize(configuration).");
        }

        #region ListTables

        public static DataTable ListTables(string queryString)
        {
            SqlConnection cnn = GetConnection();
            SqlDataAdapter adapt = new SqlDataAdapter(queryString, cnn);
            DataTable dt = new DataTable();

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }
        #endregion

        #region ListTables

        public static DataTable ListTables(SqlCommand cmd)
        {
            SqlConnection cnn = cmd.Connection;
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Table");

            try
            {
                cnn.Open();
                adapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    return null;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            return dt;
        }
        #endregion

        #region SqlParameter

        public static SqlParameter GetSqlParameterInput(string parName, object parValue, SqlDbType dbType)
        {
            SqlParameter param = new SqlParameter(parName, dbType);
            param.Direction = ParameterDirection.Input;
            param.Value = parValue;
            return param;
        }

        public static SqlParameter GetSqlParameterOutput(string parName, SqlDbType dbType)
        {
            SqlParameter param = new SqlParameter(parName, dbType);
            param.Direction = ParameterDirection.Output;
            return param;
        }

        #endregion

        //#region GlobalConnection

        //public static SqlConnection cnnGlobal = null;
        //public static SqlTransaction tranGlobal = null;

        //public static void OpenGlobalConnection()
        //{
        //    cnnGlobal = new SqlConnection(GlobalAppData.ConnectionString + ";Connection Timeout=15");
        //    int to = cnnGlobal.ConnectionTimeout;            
        //    if (cnnGlobal.State == ConnectionState.Closed)
        //    {
        //        cnnGlobal.Open();
        //        tranGlobal = cnnGlobal.BeginTransaction();
        //    }
        //}

        //public static void CloseGlobalConnection()
        //{
        //    if (cnnGlobal != null && cnnGlobal.State == ConnectionState.Open)
        //    {
        //        if (ErrorID != 0)
        //        {
        //            tranGlobal.Rollback();
        //        }
        //        else
        //        {
        //            tranGlobal.Commit();
        //        }
        //        cnnGlobal.Close();
        //    }
        //}

        //public static SqlCommand SqlCommandForTran_SP(string cmdText)
        //{
        //    SqlCommand cmd = new SqlCommand(cmdText, cnnGlobal);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandTimeout = 0;
        //    cmd.Transaction = tranGlobal;
        //    return cmd;
        //}

        //public static SqlCommand SqlCommandForTran_Text(string cmdText)
        //{
        //    SqlCommand cmd = new SqlCommand(cmdText, cnnGlobal);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandTimeout = 0;
        //    cmd.Transaction = tranGlobal;
        //    return cmd;
        //}

        //#endregion

        public DataTable GetItemTypes()
        {
            DataTable dtList = new DataTable();
            SqlConnection cnn = GetConnection();
            SqlCommand cmd = new SqlCommand("spGetItemTypes", cnn);
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
        public static DataTable ListSystemDataTable()
        {
            DataTable data = new DataTable();
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand("spSystemList", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(data);

                return data;
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                connection.Close();
            }
            return data;
        }

        public static DataTable ListSystemDataTable(DBAccess dbAccess)
        {
            var dt = new DataTable();

            using var connection = dbAccess.GetConnection();
            using var command = new SqlCommand("spSystemList", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                connection.Open();
                using var da = new SqlDataAdapter(command);
                da.Fill(dt);
            }
            catch
            {
                // log if needed; return empty table on error
            }

            return dt;
        }

        //public static int ErrorID { get; set; }
    }
}
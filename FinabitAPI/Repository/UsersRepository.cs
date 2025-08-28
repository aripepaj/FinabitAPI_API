using Finabit_API.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using FinabitAPI.Utilis;

namespace AutoBit_WebInvoices.Models
{
    public class UsersRepository
    {
        private readonly DBAccess _db;

        public UsersRepository(DBAccess db) // <-- inject tenant-aware DBAccess
        {
            _db = db;
        }

        public Users GetLoginUser(string UserName, string Password)
        {
            var cls = new Users();

            using (var cnn = _db.GetConnection()) // <-- per-tenant connection
            using (var cmd = new SqlCommand("spGetLoginUser", cnn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 30
            })
            {
                cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 256) { Value = UserName });

                string strPassword = GlobalAppData.EncryptDES(Password);
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 512) { Value = strPassword });

                cmd.Parameters.Add(new SqlParameter("@WindowsUser", SqlDbType.VarChar, 256) { Value = Environment.UserName });

                try
                {
                    cls.HasConnections = true;
                    cnn.Open();

                    using (var dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (dr.Read())
                        {
                            // helper local functions
                            int Ord(params string[] names) => GetOrdinalSafe(dr, names);
                            int GetInt(params string[] names) => GetIntSafe(dr, names);
                            bool GetBool(params string[] names) => GetBoolSafe(dr, names);
                            DateTime GetDate(params string[] names) => GetDateTimeSafe(dr, names);
                            string? GetStrN(params string[] names) => GetStringOrNullSafe(dr, names);
                            string GetStr(params string[] names) => GetStringSafe(dr, names);

                            cls.ID = GetInt("UserID", "UserId");

                            int userNameOrd = Ord("Username", "UserName");
                            cls.UserName = userNameOrd >= 0 && !dr.IsDBNull(userNameOrd)
                                ? dr.GetString(userNameOrd)
                                : UserName;

                            cls.ExpireDate = GetDate("ExpireDate");
                            cls.Status = GetBool("Status", "IsActive");
                            cls.DepartmentID = GetInt("DepartmentID", "DepartmentId");
                            cls.DefaultPartnerID = GetInt("PosPartnerID", "POSPartnerID", "DefaultPartnerID", "DefaultPartnerId");
                            cls.DefaultPartnerName = GetStrN("PosPartnerName", "POSPartnerName", "DefaultPartnerName");
                            cls.RoleID = GetInt("RoleID", "RoleId");
                            cls.IsDeleteWithAuthorization = GetBool("IsDeleteWithAuthorization");
                            cls.PartnerID = GetInt("PartnerID", "PartnerId");
                            cls.IsAuthoriser = GetBool("IsAuthoriser", "IsAuthorizer");
                            cls.DisableDateInDocuments = GetBool("DisableDateInDocuments");
                        }
                    }
                }
                catch
                {
                    cls.HasConnections = false;
                    throw; // bubble up to auth handler/controller
                }
            }

            return cls;
        }

        // ---------- safe readers ----------
        private static int GetOrdinalSafe(SqlDataReader r, params string[] names)
        {
            foreach (var n in names)
            {
                try { return r.GetOrdinal(n); }
                catch (IndexOutOfRangeException) { }
            }
            return -1;
        }

        private static string? GetStringOrNullSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return null;
            return r.GetString(ord);
        }

        private static string GetStringSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return string.Empty;
            return r.GetString(ord);
        }

        private static int GetIntSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return 0;
            return Convert.ToInt32(r.GetValue(ord));
        }

        private static bool GetBoolSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return false;
            var v = r.GetValue(ord);
            if (v is bool b) return b;
            return Convert.ToInt32(v) != 0;
        }

        private static DateTime GetDateTimeSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return DateTime.MinValue;
            var v = r.GetValue(ord);
            return (v is DateTime dt) ? dt : Convert.ToDateTime(v);
        }
    }
}

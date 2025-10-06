using FinabitAPI.Core.Utilis;
using FinabitAPI.Finabit.Account.dto;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinabitAPI.Finabit.Account
{
    public class AccountDetailsRepository
    {
        private readonly DBAccess _db;

        public AccountDetailsRepository(DBAccess db) // tenant-aware connection (same as UsersRepository)
        {
            _db = db;
        }

        /// <summary>
        /// Calls [dbo].[spAccountDetails_API]
        /// </summary>
        /// <param name="fromDate">inclusive</param>
        /// <param name="toDate">inclusive</param>
        /// <param name="accountPattern">e.g. "10028%" or "%" for all</param>
        public async Task<List<AccountDetail>> GetAccountDetailsAsync(
            DateTime fromDate,
            DateTime toDate,
            string accountPattern,
            CancellationToken ct = default)
        {
            var list = new List<AccountDetail>();

            using var cnn = _db.GetConnection();
            using var cmd = new SqlCommand("spAccountDetails_API", cnn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            // proc signature: @FromDate smalldatetime, @ToDate smalldatetime, @Account varchar(100)
            cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = fromDate });
            cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = toDate });
            cmd.Parameters.Add(new SqlParameter("@Account", SqlDbType.VarChar, 100) { Value = string.IsNullOrWhiteSpace(accountPattern) ? "%" : accountPattern });

            await cnn.OpenAsync(ct).ConfigureAwait(false);
            using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct).ConfigureAwait(false);

            // local safe-read helpers (same pattern as UsersRepository)
            int Ord(params string[] names) => GetOrdinalSafe(dr, names);
            string? GetStrN(params string[] names) => GetStringOrNullSafe(dr, names);
            string GetStr(params string[] names) => GetStringSafe(dr, names);
            DateTime GetDate(params string[] names) => GetDateTimeSafe(dr, names);
            decimal GetDec(params string[] names) => GetDecimalSafe(dr, names);

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                var row = new AccountDetail
                {
                    TransactionType = GetStrN("TransactionType"),
                    Date = GetDate("Date"),
                    Description = GetStrN("Description"),
                    Number = GetStrN("Number"),
                    Name = GetStrN("NAme", "Name"), // proc returns "NAme"
                    Account = GetStrN("Account"),
                    DebitValue = GetDec("DebitValue"),
                    CreditValue = GetDec("CreditValue")
                };

                list.Add(row);
            }

            return list;
        }

        // ---------- shared safe readers (copy aligned to UsersRepository style) ----------
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

        private static DateTime GetDateTimeSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return DateTime.MinValue;
            var v = r.GetValue(ord);
            return v is DateTime dt ? dt : Convert.ToDateTime(v);
        }

        private static decimal GetDecimalSafe(SqlDataReader r, params string[] names)
        {
            int ord = GetOrdinalSafe(r, names);
            if (ord < 0 || r.IsDBNull(ord)) return 0m;
            var v = r.GetValue(ord);
            if (v is decimal d) return d;
            if (v is double db) return Convert.ToDecimal(db);
            if (v is float f) return Convert.ToDecimal(f);
            return Convert.ToDecimal(v);
        }
    }
}

using FinabitAPI.Models;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinabitAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IReadOnlyList<AccountMatchDto>> SearchAccountsAsync(
            string? accountId, string? accountName, CancellationToken ct = default);
    }

    public sealed class AccountRepository : IAccountRepository
    {
        private readonly DBAccess _db;

        private const string ProcSearch = "dbo.spGeAccountMatches_API";
        private const int DefaultTimeout = 60;

        public AccountRepository(DBAccess db) => _db = db;

        public async Task<IReadOnlyList<AccountMatchDto>> SearchAccountsAsync(
            string? accountId, string? accountName, CancellationToken ct = default)
        {
            var list = new List<AccountMatchDto>();

            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(ProcSearch, conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.NVarChar, 200)
            { Value = (object?)accountId ?? string.Empty });

            cmd.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar, 200)
            { Value = (object?)accountName ?? string.Empty });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!dr.HasRows) return list;

            int oDetailsType = dr.GetOrdinal("DetailsType");
            int oItemID = dr.GetOrdinal("ItemID");
            int oDesc = dr.GetOrdinal("Description");
            int oItemName = dr.GetOrdinal("ItemName");

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(new AccountMatchDto
                {
                    DetailsType = dr.IsDBNull(oDetailsType) ? 0 : dr.GetInt32(oDetailsType),
                    ItemID = dr.IsDBNull(oItemID) ? string.Empty : dr.GetString(oItemID),
                    Description = dr.IsDBNull(oDesc) ? string.Empty : dr.GetString(oDesc),
                    ItemName = dr.IsDBNull(oItemName) ? string.Empty : dr.GetString(oItemName)
                });
            }

            return list;
        }
    }
}

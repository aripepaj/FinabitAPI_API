using FinabitAPI.Models;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinabitAPI.Repository
{
    public interface ICustomizationRepository
    {
        // Customization Lists
        Task<CustomizationList?> GetListByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<CustomizationList>> GetListsByUserAsync(string user, string? type = null, CancellationToken ct = default);
        Task<CustomizationList?> GetListByUserNameTypeAsync(string user, string name, string type, CancellationToken ct = default);
        Task<int> CreateListAsync(CustomizationListDto dto, CancellationToken ct = default);
        Task<bool> UpdateListAsync(int id, CustomizationListUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteListAsync(int id, CancellationToken ct = default);

        // Customization Favorites
        Task<CustomizationFavorite?> GetFavoriteByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<CustomizationFavorite>> GetFavoritesByUserAsync(string user, string? itemType = null, CancellationToken ct = default);
        Task<CustomizationFavorite?> GetFavoriteByUserItemAsync(string user, string itemId, string itemType, CancellationToken ct = default);
        Task<int> CreateFavoriteAsync(CustomizationFavoriteDto dto, CancellationToken ct = default);
        Task<bool> DeleteFavoriteAsync(int id, CancellationToken ct = default);
        Task<bool> DeleteFavoriteByUserItemAsync(string user, string itemId, string itemType, CancellationToken ct = default);

        // Customization Profile Preferences
        Task<CustomizationProfilePreference?> GetPreferenceByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<CustomizationProfilePreference>> GetPreferencesByUserAsync(string user, CancellationToken ct = default);
        Task<CustomizationProfilePreference?> GetPreferenceByUserKeyAsync(string user, string prefKey, CancellationToken ct = default);
        Task<int> UpsertPreferenceAsync(CustomizationProfilePreferenceDto dto, CancellationToken ct = default);
        Task<bool> DeletePreferenceAsync(int id, CancellationToken ct = default);
    }

    public sealed class CustomizationRepository : ICustomizationRepository
    {
        private readonly DBAccess _db;
        private const int DefaultTimeout = 60;

        public CustomizationRepository(DBAccess db) => _db = db;

        #region Customization Lists

        public async Task<CustomizationList?> GetListByIdAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], name, type, config, created_at, updated_at FROM tblAPI_customization_lists WHERE id = @Id",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!await dr.ReadAsync(ct).ConfigureAwait(false))
                return null;

            return MapCustomizationList(dr);
        }

        public async Task<IReadOnlyList<CustomizationList>> GetListsByUserAsync(string user, string? type = null, CancellationToken ct = default)
        {
            var list = new List<CustomizationList>();
            await using var conn = _db.GetConnection();

            var sql = "SELECT id, [user], name, type, config, created_at, updated_at FROM tblAPI_customization_lists WHERE [user] = @User";
            if (!string.IsNullOrEmpty(type))
                sql += " AND type = @Type";

            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            if (!string.IsNullOrEmpty(type))
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = type });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(MapCustomizationList(dr));
            }

            return list;
        }

        public async Task<CustomizationList?> GetListByUserNameTypeAsync(string user, string name, string type, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], name, type, config, created_at, updated_at FROM tblAPI_customization_lists WHERE [user] = @User AND name = @Name AND type = @Type",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = name });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = type });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!await dr.ReadAsync(ct).ConfigureAwait(false))
                return null;

            return MapCustomizationList(dr);
        }

        public async Task<int> CreateListAsync(CustomizationListDto dto, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                @"INSERT INTO tblAPI_customization_lists ([user], name, type, config, created_at, updated_at)
                  VALUES (@User, @Name, @Type, @Config, GETDATE(), GETDATE());
                  SELECT CAST(SCOPE_IDENTITY() as int);",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = dto.User });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = dto.Name });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = dto.Type });
            cmd.Parameters.Add(new SqlParameter("@Config", SqlDbType.NVarChar, -1) { Value = dto.Config });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var result = await cmd.ExecuteScalarAsync(ct).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task<bool> UpdateListAsync(int id, CustomizationListUpdateDto dto, CancellationToken ct = default)
        {
            var setClauses = new List<string>();
            if (!string.IsNullOrEmpty(dto.Name))
                setClauses.Add("name = @Name");
            if (!string.IsNullOrEmpty(dto.Config))
                setClauses.Add("config = @Config");

            if (setClauses.Count == 0)
                return false;

            var sql = $"UPDATE tblAPI_customization_lists SET {string.Join(", ", setClauses)} WHERE id = @Id";

            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
            if (!string.IsNullOrEmpty(dto.Name))
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = dto.Name });
            if (!string.IsNullOrEmpty(dto.Config))
                cmd.Parameters.Add(new SqlParameter("@Config", SqlDbType.NVarChar, -1) { Value = dto.Config });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var rowsAffected = await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteListAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "DELETE FROM tblAPI_customization_lists WHERE id = @Id",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var rowsAffected = await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            return rowsAffected > 0;
        }

        private static CustomizationList MapCustomizationList(SqlDataReader dr)
        {
            return new CustomizationList
            {
                Id = dr.GetInt32(dr.GetOrdinal("id")),
                User = dr.GetString(dr.GetOrdinal("user")),
                Name = dr.GetString(dr.GetOrdinal("name")),
                Type = dr.GetString(dr.GetOrdinal("type")),
                Config = dr.GetString(dr.GetOrdinal("config")),
                CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at")),
                UpdatedAt = dr.GetDateTime(dr.GetOrdinal("updated_at"))
            };
        }

        #endregion

        #region Customization Favorites

        public async Task<CustomizationFavorite?> GetFavoriteByIdAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], item_id, item_type, metadata, created_at FROM tblAPI_customization_favorites WHERE id = @Id",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!await dr.ReadAsync(ct).ConfigureAwait(false))
                return null;

            return MapCustomizationFavorite(dr);
        }

        public async Task<IReadOnlyList<CustomizationFavorite>> GetFavoritesByUserAsync(string user, string? itemType = null, CancellationToken ct = default)
        {
            var list = new List<CustomizationFavorite>();
            await using var conn = _db.GetConnection();

            var sql = "SELECT id, [user], item_id, item_type, metadata, created_at FROM tblAPI_customization_favorites WHERE [user] = @User";
            if (!string.IsNullOrEmpty(itemType))
                sql += " AND item_type = @ItemType";

            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            if (!string.IsNullOrEmpty(itemType))
                cmd.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 100) { Value = itemType });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(MapCustomizationFavorite(dr));
            }

            return list;
        }

        public async Task<CustomizationFavorite?> GetFavoriteByUserItemAsync(string user, string itemId, string itemType, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], item_id, item_type, metadata, created_at FROM tblAPI_customization_favorites WHERE [user] = @User AND item_id = @ItemId AND item_type = @ItemType",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar, 255) { Value = itemId });
            cmd.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 100) { Value = itemType });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!await dr.ReadAsync(ct).ConfigureAwait(false))
                return null;

            return MapCustomizationFavorite(dr);
        }

        public async Task<int> CreateFavoriteAsync(CustomizationFavoriteDto dto, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                @"INSERT INTO tblAPI_customization_favorites ([user], item_id, item_type, metadata, created_at)
                  VALUES (@User, @ItemId, @ItemType, @Metadata, GETDATE());
                  SELECT CAST(SCOPE_IDENTITY() as int);",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = dto.User });
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar, 255) { Value = dto.ItemId });
            cmd.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 100) { Value = dto.ItemType });
            cmd.Parameters.Add(new SqlParameter("@Metadata", SqlDbType.NVarChar, -1) { Value = (object?)dto.Metadata ?? DBNull.Value });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var result = await cmd.ExecuteScalarAsync(ct).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task<bool> DeleteFavoriteAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "DELETE FROM tblAPI_customization_favorites WHERE id = @Id",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var rowsAffected = await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteFavoriteByUserItemAsync(string user, string itemId, string itemType, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "DELETE FROM tblAPI_customization_favorites WHERE [user] = @User AND item_id = @ItemId AND item_type = @ItemType",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar, 255) { Value = itemId });
            cmd.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 100) { Value = itemType });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var rowsAffected = await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            return rowsAffected > 0;
        }

        private static CustomizationFavorite MapCustomizationFavorite(SqlDataReader dr)
        {
            int metadataOrdinal = dr.GetOrdinal("metadata");
            return new CustomizationFavorite
            {
                Id = dr.GetInt32(dr.GetOrdinal("id")),
                User = dr.GetString(dr.GetOrdinal("user")),
                ItemId = dr.GetString(dr.GetOrdinal("item_id")),
                ItemType = dr.GetString(dr.GetOrdinal("item_type")),
                Metadata = dr.IsDBNull(metadataOrdinal) ? null : dr.GetString(metadataOrdinal),
                CreatedAt = dr.GetDateTime(dr.GetOrdinal("created_at"))
            };
        }

        #endregion

        #region Customization Profile Preferences

        public async Task<CustomizationProfilePreference?> GetPreferenceByIdAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], pref_key, pref_value, updated_at FROM tblAPI_customization_profile_prefs WHERE id = @Id",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!await dr.ReadAsync(ct).ConfigureAwait(false))
                return null;

            return MapCustomizationProfilePreference(dr);
        }

        public async Task<IReadOnlyList<CustomizationProfilePreference>> GetPreferencesByUserAsync(string user, CancellationToken ct = default)
        {
            var list = new List<CustomizationProfilePreference>();
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], pref_key, pref_value, updated_at FROM tblAPI_customization_profile_prefs WHERE [user] = @User",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(MapCustomizationProfilePreference(dr));
            }

            return list;
        }

        public async Task<CustomizationProfilePreference?> GetPreferenceByUserKeyAsync(string user, string prefKey, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], pref_key, pref_value, updated_at FROM tblAPI_customization_profile_prefs WHERE [user] = @User AND pref_key = @PrefKey",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@PrefKey", SqlDbType.NVarChar, 255) { Value = prefKey });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            if (!await dr.ReadAsync(ct).ConfigureAwait(false))
                return null;

            return MapCustomizationProfilePreference(dr);
        }

        public async Task<int> UpsertPreferenceAsync(CustomizationProfilePreferenceDto dto, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                @"MERGE tblAPI_customization_profile_prefs AS target
                  USING (SELECT @User AS [user], @PrefKey AS pref_key) AS source
                  ON target.[user] = source.[user] AND target.pref_key = source.pref_key
                  WHEN MATCHED THEN
                      UPDATE SET pref_value = @PrefValue, updated_at = GETDATE()
                  WHEN NOT MATCHED THEN
                      INSERT ([user], pref_key, pref_value, updated_at)
                      VALUES (@User, @PrefKey, @PrefValue, GETDATE())
                  OUTPUT INSERTED.id;",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = dto.User });
            cmd.Parameters.Add(new SqlParameter("@PrefKey", SqlDbType.NVarChar, 255) { Value = dto.PrefKey });
            cmd.Parameters.Add(new SqlParameter("@PrefValue", SqlDbType.NVarChar, -1) { Value = dto.PrefValue });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var result = await cmd.ExecuteScalarAsync(ct).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task<bool> DeletePreferenceAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "DELETE FROM tblAPI_customization_profile_prefs WHERE id = @Id",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var rowsAffected = await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            return rowsAffected > 0;
        }

        private static CustomizationProfilePreference MapCustomizationProfilePreference(SqlDataReader dr)
        {
            return new CustomizationProfilePreference
            {
                Id = dr.GetInt32(dr.GetOrdinal("id")),
                User = dr.GetString(dr.GetOrdinal("user")),
                PrefKey = dr.GetString(dr.GetOrdinal("pref_key")),
                PrefValue = dr.GetString(dr.GetOrdinal("pref_value")),
                UpdatedAt = dr.GetDateTime(dr.GetOrdinal("updated_at"))
            };
        }

        #endregion
    }
}

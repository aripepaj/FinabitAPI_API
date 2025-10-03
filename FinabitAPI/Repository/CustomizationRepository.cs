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
    Task<IReadOnlyList<CustomizationList>> GetListsByUserAsync(string user, string? mode = null, string? storageKey = null, string? device = null, CancellationToken ct = default);
    Task<CustomizationList?> GetListByCompositeAsync(string user, string name, string mode, string? storageKey = null, string? device = null, CancellationToken ct = default);
        Task<int> CreateListAsync(CustomizationListDto dto, CancellationToken ct = default);
        Task<bool> UpdateListAsync(int id, CustomizationListUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteListAsync(int id, CancellationToken ct = default);

        // Customization Favorites
    Task<CustomizationFavorite?> GetFavoriteByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<CustomizationFavorite>> GetFavoritesByUserAsync(string user, CancellationToken ct = default);
    Task<CustomizationFavorite?> GetFavoriteByUserItemAsync(string user, string itemId, CancellationToken ct = default);
    Task<int> CreateFavoriteAsync(CustomizationFavoriteDto dto, CancellationToken ct = default);
        Task<bool> DeleteFavoriteAsync(int id, CancellationToken ct = default);
    Task<bool> DeleteFavoriteByUserItemAsync(string user, string itemId, CancellationToken ct = default);

        // Customization Profile Preferences
    Task<CustomizationProfilePreference?> GetPreferenceByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<CustomizationProfilePreference>> GetPreferencesByUserAsync(string user, CancellationToken ct = default);
    Task<CustomizationProfilePreference?> GetPreferenceCompositeAsync(string user, string? storageKey, string? mode, string? device, CancellationToken ct = default);
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
                "SELECT id, [user], storage_key, name, mode, device, data, created_at, updated_at FROM tblAPI_customization_lists WHERE id = @Id",
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

        public async Task<IReadOnlyList<CustomizationList>> GetListsByUserAsync(string user, string? mode = null, string? storageKey = null, string? device = null, CancellationToken ct = default)
        {
            var list = new List<CustomizationList>();
            await using var conn = _db.GetConnection();
            var sql = "SELECT id, [user], storage_key, name, mode, device, data, created_at, updated_at FROM tblAPI_customization_lists WHERE [user] = @User";
            if (!string.IsNullOrEmpty(mode)) sql += " AND mode = @Mode";
            if (!string.IsNullOrEmpty(storageKey)) sql += " AND storage_key = @StorageKey";
            if (!string.IsNullOrEmpty(device)) sql += " AND device = @Device";

            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            if (!string.IsNullOrEmpty(mode)) cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.NVarChar, 50) { Value = mode });
            if (!string.IsNullOrEmpty(storageKey)) cmd.Parameters.Add(new SqlParameter("@StorageKey", SqlDbType.NVarChar, 255) { Value = storageKey });
            if (!string.IsNullOrEmpty(device)) cmd.Parameters.Add(new SqlParameter("@Device", SqlDbType.NVarChar, 20) { Value = device });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(MapCustomizationList(dr));
            }

            return list;
        }

        public async Task<CustomizationList?> GetListByCompositeAsync(string user, string name, string mode, string? storageKey = null, string? device = null, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            var sql = "SELECT id, [user], storage_key, name, mode, device, data, created_at, updated_at FROM tblAPI_customization_lists WHERE [user] = @User AND name = @Name AND mode = @Mode";
            if (!string.IsNullOrEmpty(storageKey)) sql += " AND storage_key = @StorageKey";
            if (!string.IsNullOrEmpty(device)) sql += " AND device = @Device";
            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };
            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = name });
            cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.NVarChar, 50) { Value = mode });
            if (!string.IsNullOrEmpty(storageKey)) cmd.Parameters.Add(new SqlParameter("@StorageKey", SqlDbType.NVarChar, 255) { Value = storageKey });
            if (!string.IsNullOrEmpty(device)) cmd.Parameters.Add(new SqlParameter("@Device", SqlDbType.NVarChar, 20) { Value = device });

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
                                @"INSERT INTO tblAPI_customization_lists ([user], storage_key, name, mode, device, data, created_at, updated_at)
                                    VALUES (@User, @StorageKey, @Name, @Mode, @Device, @Data, GETDATE(), GETDATE());
                  SELECT CAST(SCOPE_IDENTITY() as int);",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            // Backward compatibility mapping
            var mode = !string.IsNullOrEmpty(dto.Mode) ? dto.Mode : (dto.Type ?? string.Empty);
            var data = !string.IsNullOrEmpty(dto.Data) ? dto.Data : (dto.Config ?? string.Empty);

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = dto.User });
            cmd.Parameters.Add(new SqlParameter("@StorageKey", SqlDbType.NVarChar, 255) { Value = (object?)dto.StorageKey ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = dto.Name });
            cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.NVarChar, 50) { Value = mode });
            cmd.Parameters.Add(new SqlParameter("@Device", SqlDbType.NVarChar, 20) { Value = (object?)dto.Device ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Data", SqlDbType.NVarChar, -1) { Value = data });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var result = await cmd.ExecuteScalarAsync(ct).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public async Task<bool> UpdateListAsync(int id, CustomizationListUpdateDto dto, CancellationToken ct = default)
        {
            var setClauses = new List<string>();
            if (!string.IsNullOrEmpty(dto.Name)) setClauses.Add("name = @Name");
            // Data / legacy Config
            var dataProvided = !string.IsNullOrEmpty(dto.Data) || !string.IsNullOrEmpty(dto.Config);
            if (dataProvided) setClauses.Add("data = @Data");
            if (!string.IsNullOrEmpty(dto.StorageKey)) setClauses.Add("storage_key = @StorageKey");
            if (!string.IsNullOrEmpty(dto.Device)) setClauses.Add("device = @Device");
            var modeProvided = !string.IsNullOrEmpty(dto.Mode) || !string.IsNullOrEmpty(dto.Type);
            if (modeProvided) setClauses.Add("mode = @Mode");

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
            if (!string.IsNullOrEmpty(dto.Name)) cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = dto.Name });
            if (dataProvided)
            {
                var newData = !string.IsNullOrEmpty(dto.Data) ? dto.Data : (dto.Config ?? string.Empty);
                cmd.Parameters.Add(new SqlParameter("@Data", SqlDbType.NVarChar, -1) { Value = newData });
            }
            if (!string.IsNullOrEmpty(dto.StorageKey)) cmd.Parameters.Add(new SqlParameter("@StorageKey", SqlDbType.NVarChar, 255) { Value = dto.StorageKey });
            if (!string.IsNullOrEmpty(dto.Device)) cmd.Parameters.Add(new SqlParameter("@Device", SqlDbType.NVarChar, 20) { Value = dto.Device });
            if (modeProvided)
            {
                var newMode = !string.IsNullOrEmpty(dto.Mode) ? dto.Mode : (dto.Type ?? string.Empty);
                cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.NVarChar, 50) { Value = newMode });
            }

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
                StorageKey = dr.IsDBNull(dr.GetOrdinal("storage_key")) ? null : dr.GetString(dr.GetOrdinal("storage_key")),
                Name = dr.GetString(dr.GetOrdinal("name")),
                Mode = dr.GetString(dr.GetOrdinal("mode")),
                Device = dr.IsDBNull(dr.GetOrdinal("device")) ? null : dr.GetString(dr.GetOrdinal("device")),
                Data = dr.GetString(dr.GetOrdinal("data")),
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
                "SELECT id, [user], item_id, label, path, icon, metadata, added_at, item_type FROM tblAPI_customization_favorites WHERE id = @Id",
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

        public async Task<IReadOnlyList<CustomizationFavorite>> GetFavoritesByUserAsync(string user, CancellationToken ct = default)
        {
            var list = new List<CustomizationFavorite>();
            await using var conn = _db.GetConnection();
            var sql = "SELECT id, [user], item_id, label, path, icon, metadata, added_at, item_type FROM tblAPI_customization_favorites WHERE [user] = @User";

            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, ct).ConfigureAwait(false);

            while (await dr.ReadAsync(ct).ConfigureAwait(false))
            {
                list.Add(MapCustomizationFavorite(dr));
            }

            return list;
        }

        public async Task<CustomizationFavorite?> GetFavoriteByUserItemAsync(string user, string itemId, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], item_id, label, path, icon, metadata, added_at, item_type FROM tblAPI_customization_favorites WHERE [user] = @User AND item_id = @ItemId",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar, 255) { Value = itemId });

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
                                @"INSERT INTO tblAPI_customization_favorites ([user], item_id, label, path, icon, metadata, added_at, item_type)
                                    VALUES (@User, @ItemId, @Label, @Path, @Icon, @Metadata, GETDATE(), @ItemType);
                  SELECT CAST(SCOPE_IDENTITY() as int);",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = dto.User });
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar, 255) { Value = dto.ItemId });
            cmd.Parameters.Add(new SqlParameter("@Label", SqlDbType.NVarChar, 255) { Value = dto.Label });
            cmd.Parameters.Add(new SqlParameter("@Path", SqlDbType.NVarChar, 500) { Value = dto.Path });
            cmd.Parameters.Add(new SqlParameter("@Icon", SqlDbType.NVarChar, -1) { Value = (object?)dto.Icon ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Metadata", SqlDbType.NVarChar, -1) { Value = (object?)dto.Metadata ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@ItemType", SqlDbType.NVarChar, 100) { Value = (object?)dto.ItemType ?? DBNull.Value });

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

        public async Task<bool> DeleteFavoriteByUserItemAsync(string user, string itemId, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "DELETE FROM tblAPI_customization_favorites WHERE [user] = @User AND item_id = @ItemId",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar, 255) { Value = itemId });

            await conn.OpenAsync(ct).ConfigureAwait(false);
            var rowsAffected = await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            return rowsAffected > 0;
        }

        private static CustomizationFavorite MapCustomizationFavorite(SqlDataReader dr)
        {
            int metadataOrdinal = dr.GetOrdinal("metadata");
            int labelOrdinal = dr.GetOrdinal("label");
            int pathOrdinal = dr.GetOrdinal("path");
            int iconOrdinal = dr.GetOrdinal("icon");
            int itemTypeOrdinal = dr.GetOrdinal("item_type");
            return new CustomizationFavorite
            {
                Id = dr.GetInt32(dr.GetOrdinal("id")),
                User = dr.GetString(dr.GetOrdinal("user")),
                ItemId = dr.GetString(dr.GetOrdinal("item_id")),
                Label = dr.IsDBNull(labelOrdinal) ? string.Empty : dr.GetString(labelOrdinal),
                Path = dr.IsDBNull(pathOrdinal) ? string.Empty : dr.GetString(pathOrdinal),
                Icon = dr.IsDBNull(iconOrdinal) ? null : dr.GetString(iconOrdinal),
                Metadata = dr.IsDBNull(metadataOrdinal) ? null : dr.GetString(metadataOrdinal),
                ItemType = dr.IsDBNull(itemTypeOrdinal) ? null : dr.GetString(itemTypeOrdinal),
                AddedAt = dr.GetDateTime(dr.GetOrdinal("added_at"))
            };
        }

        #endregion

        #region Customization Profile Preferences

        public async Task<CustomizationProfilePreference?> GetPreferenceByIdAsync(int id, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            await using var cmd = new SqlCommand(
                "SELECT id, [user], storage_key, mode, device, last_view_name, pref_key, pref_value, updated_at FROM tblAPI_customization_profile_prefs WHERE id = @Id",
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
                "SELECT id, [user], storage_key, mode, device, last_view_name, pref_key, pref_value, updated_at FROM tblAPI_customization_profile_prefs WHERE [user] = @User",
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

        public async Task<CustomizationProfilePreference?> GetPreferenceCompositeAsync(string user, string? storageKey, string? mode, string? device, CancellationToken ct = default)
        {
            await using var conn = _db.GetConnection();
            var sql = "SELECT id, [user], storage_key, mode, device, last_view_name, pref_key, pref_value, updated_at FROM tblAPI_customization_profile_prefs WHERE [user] = @User";
            if (!string.IsNullOrEmpty(storageKey)) sql += " AND storage_key = @StorageKey"; else sql += " AND storage_key IS NULL";
            if (!string.IsNullOrEmpty(mode)) sql += " AND mode = @Mode"; else sql += " AND mode IS NULL";
            if (!string.IsNullOrEmpty(device)) sql += " AND device = @Device"; else sql += " AND device IS NULL";
            await using var cmd = new SqlCommand(sql, conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };
            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = user });
            if (!string.IsNullOrEmpty(storageKey)) cmd.Parameters.Add(new SqlParameter("@StorageKey", SqlDbType.NVarChar, 255) { Value = storageKey });
            if (!string.IsNullOrEmpty(mode)) cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.NVarChar, 50) { Value = mode });
            if (!string.IsNullOrEmpty(device)) cmd.Parameters.Add(new SqlParameter("@Device", SqlDbType.NVarChar, 20) { Value = device });

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
                                    USING (SELECT @User AS [user], @StorageKey AS storage_key, @Mode AS mode, @Device AS device) AS source
                                    ON target.[user] = source.[user]
                                         AND ISNULL(target.storage_key,'') = ISNULL(source.storage_key,'')
                                         AND ISNULL(target.mode,'') = ISNULL(source.mode,'')
                                         AND ISNULL(target.device,'') = ISNULL(source.device,'')
                                    WHEN MATCHED THEN
                                            UPDATE SET last_view_name = @LastViewName, updated_at = GETDATE(), pref_key = @PrefKey, pref_value = @PrefValue
                                    WHEN NOT MATCHED THEN
                                            INSERT ([user], storage_key, mode, device, last_view_name, pref_key, pref_value, updated_at)
                                            VALUES (@User, @StorageKey, @Mode, @Device, @LastViewName, @PrefKey, @PrefValue, GETDATE())
                                    OUTPUT INSERTED.id;",
                conn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = DefaultTimeout
            };

            cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.NVarChar, 255) { Value = dto.User });
            cmd.Parameters.Add(new SqlParameter("@StorageKey", SqlDbType.NVarChar, 255) { Value = (object?)dto.StorageKey ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Mode", SqlDbType.NVarChar, 50) { Value = (object?)dto.Mode ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Device", SqlDbType.NVarChar, 20) { Value = (object?)dto.Device ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@LastViewName", SqlDbType.NVarChar, 255) { Value = (object?)dto.LastViewName ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PrefKey", SqlDbType.NVarChar, 255) { Value = (object?)dto.PrefKey ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@PrefValue", SqlDbType.NVarChar, -1) { Value = (object?)dto.PrefValue ?? DBNull.Value });

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
                StorageKey = dr.IsDBNull(dr.GetOrdinal("storage_key")) ? null : dr.GetString(dr.GetOrdinal("storage_key")),
                Mode = dr.IsDBNull(dr.GetOrdinal("mode")) ? null : dr.GetString(dr.GetOrdinal("mode")),
                Device = dr.IsDBNull(dr.GetOrdinal("device")) ? null : dr.GetString(dr.GetOrdinal("device")),
                LastViewName = dr.IsDBNull(dr.GetOrdinal("last_view_name")) ? null : dr.GetString(dr.GetOrdinal("last_view_name")),
                PrefKey = dr.IsDBNull(dr.GetOrdinal("pref_key")) ? null : dr.GetString(dr.GetOrdinal("pref_key")),
                PrefValue = dr.IsDBNull(dr.GetOrdinal("pref_value")) ? null : dr.GetString(dr.GetOrdinal("pref_value")),
                UpdatedAt = dr.GetDateTime(dr.GetOrdinal("updated_at"))
            };
        }

        #endregion
    }
}

using FinabitAPI.Core.Utilis;
using FinabitAPI.Finabit.Items.dto;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FinabitAPI.Finabit.Items
{
    public class ItemsMasterImportRepository
    {
        private const string TVP_Name = "dbo.ImportItems";
        private const string PROC_Name = "[dbo].[_ImportItems_API]";   

        private readonly DBAccess _db;
        public ItemsMasterImportRepository(DBAccess db) { _db = db; }

        public async Task<(int inserted, string error, List<Dictionary<string, object>> items)>
            ImportItemsAsync(List<ImportItemRow> items, int newTransactionId)
        {
            if (items == null || items.Count == 0)
                return (0, "Items payload required", new List<Dictionary<string, object>>());

            try
            {
                using var conn = _db.GetConnection();
                await conn.OpenAsync();

                var tvp = await BuildItemsTableFromTvpSchemaAsync(conn, TVP_Name, items);

                using var cmd = new SqlCommand(PROC_Name, conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ImportItems", SqlDbType.Structured) { TypeName = TVP_Name, Value = tvp });
                cmd.Parameters.Add(new SqlParameter("@NewID", SqlDbType.Int) { Value = newTransactionId });

                var rows = new List<Dictionary<string, object>>();

                using var rdr = await cmd.ExecuteReaderAsync();

                var fieldCount = rdr.FieldCount;
                while (await rdr.ReadAsync())
                {
                    var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < fieldCount; i++)
                    {
                        var name = rdr.GetName(i);
                        var val = await rdr.IsDBNullAsync(i) ? null : rdr.GetValue(i);
                        dict[name] = val;
                    }
                    rows.Add(dict);
                }

                return (rows.Count, null, rows);
            }
            catch (Exception ex)
            {
                return (0, ex.Message, new List<Dictionary<string, object>>());
            }
        }

        private static async Task<DataTable> BuildItemsTableFromTvpSchemaAsync(
            SqlConnection conn,
            string tvpFullName,
            List<ImportItemRow> rows)
        {
            // Split schema-qualified name (e.g., dbo.ImportItems)
            var parts = tvpFullName.Split('.', 2, StringSplitOptions.RemoveEmptyEntries);
            var schema = parts.Length == 2 ? parts[0] : "dbo";
            var typeName = parts.Length == 2 ? parts[1] : parts[0];

            // Read TVP column definitions in order
            var schemaRows = new List<(int Ordinal, string Name, string SqlType, int MaxLen, bool IsNullable)>();
            using (var cmd = new SqlCommand(@"
                SELECT c.column_id,
                       c.name      AS ColumnName,
                       t.name      AS SqlType,
                       c.max_length,
                       c.is_nullable
                FROM sys.table_types tt
                JOIN sys.columns     c ON c.object_id    = tt.type_table_object_id
                JOIN sys.types       t ON t.user_type_id = c.user_type_id
                WHERE tt.schema_id = SCHEMA_ID(@schema)
                  AND tt.name      = @typeName
                ORDER BY c.column_id;", conn))
            {
                cmd.Parameters.AddWithValue("@schema", schema);
                cmd.Parameters.AddWithValue("@typeName", typeName);

                using var rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    schemaRows.Add((
                        rdr.GetInt32(0),
                        rdr.GetString(1),
                        rdr.GetString(2),
                        rdr.GetInt16(3),
                        rdr.GetBoolean(4)
                    ));
                }
            }

            if (schemaRows.Count == 0)
                throw new InvalidOperationException($"TVP type {tvpFullName} not found or has zero columns.");

            // Create DataTable columns with compatible .NET types
            var dt = new DataTable();
            foreach (var col in schemaRows)
            {
                dt.Columns.Add(col.Name, MapSqlTypeToDotNet(col.SqlType, col.IsNullable));
            }

            // Map DTO -> TVP by column name
            foreach (var src in rows)
            {
                var dr = dt.NewRow();

                // Common known mappings (fill what your TVP actually has)
                SetIfExists(dr, "ItemID", src.ItemID);
                SetIfExists(dr, "ItemName", src.ItemName);

                // You renamed your DTO to CategoryName/UnitName; map both ways to be safe
                SetIfExists(dr, "CategoryName", FirstNonNull(src.CategoryName, src.CategoryName));
                SetIfExists(dr, "Category", FirstNonNull(src.CategoryName, src.CategoryName)); // in case TVP is "Category"

                SetIfExists(dr, "UnitName", FirstNonNull(src.UnitName, src.UnitName));
                SetIfExists(dr, "Unit1", FirstNonNull(src.UnitName, src.UnitName));         // in case TVP is "Unit1"

                // Transactional fields (your proc uses these in the final INSERT…SELECT):
                SetIfExists(dr, "Quantity", src.Quantity ?? 0m);
                SetIfExists(dr, "Price", src.Price ?? 0m);
                SetIfExists(dr, "DepartmentID", src.DepartmentID);
                SetIfExists(dr, "InternalDepartmentID", src.InternalDepartmentID);
                SetIfExists(dr, "TransactionDate", src.TransactionDate ?? DateTime.UtcNow);
                SetIfExists(dr, "TransactionTypeID", src.TransactionTypeID);

                // Optional/extra fields you may have in TVP (all safe to ignore if not present):
                SetIfExists(dr, "Unit2", src.Unit2);
                SetIfExists(dr, "Coef_Quantity", src.Coef_Quantity);
                SetIfExists(dr, "ItemType", src.ItemType);
                SetIfExists(dr, "IncomeAccount", src.IncomeAccount);
                SetIfExists(dr, "ExpenseAccount", src.ExpenseAccount);
                SetIfExists(dr, "AssetAccount", src.AssetAccount);
                SetIfExists(dr, "MinimumQuantity", src.MinimumQuantity);
                SetIfExists(dr, "SalesPrice", src.SalesPrice);
                SetIfExists(dr, "TaxValue", src.TaxValue);
                SetIfExists(dr, "Origin", src.Origin);
                SetIfExists(dr, "SubGroupID", src.SubGroupID);
                SetIfExists(dr, "Barcode1", src.Barcode1);
                SetIfExists(dr, "Barcode3", src.Barcode3);
                SetIfExists(dr, "SalesPrice2", src.SalesPrice2);
                SetIfExists(dr, "SalesPrice3", src.SalesPrice3);
                SetIfExists(dr, "Coefficient", src.Coefficient);
                SetIfExists(dr, "Author", src.Author);
                SetIfExists(dr, "Publisher", src.Publisher);
                SetIfExists(dr, "CustomField1", src.CustomField1);
                SetIfExists(dr, "CustomField2", src.CustomField2);
                SetIfExists(dr, "CustomField3", src.CustomField3);
                SetIfExists(dr, "CustomField4", src.CustomField4);
                SetIfExists(dr, "CustomField5", src.CustomField5);
                SetIfExists(dr, "CustomField6", src.CustomField6);
                SetIfExists(dr, "CustomFields7", src.CustomFields7);
                SetIfExists(dr, "CustomFields8", src.CustomFields8);
                SetIfExists(dr, "PLU", src.PLU);

                // Any TVP columns we didn’t explicitly set remain DBNull
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static object FirstNonNull(object a, object b) => a ?? b ?? DBNull.Value;

        private static void SetIfExists(DataRow dr, string colName, object value)
        {
            if (!dr.Table.Columns.Contains(colName))
                return;

            dr[colName] = value switch
            {
                null => DBNull.Value,
                DateTime dt when dr.Table.Columns[colName].DataType == typeof(DateTime) => dt,
                decimal m when dr.Table.Columns[colName].DataType == typeof(decimal) => m,
                int i when dr.Table.Columns[colName].DataType == typeof(int) => i,
                bool b when dr.Table.Columns[colName].DataType == typeof(bool) => b,
                _ => value
            };
        }

        private static Type MapSqlTypeToDotNet(string sqlType, bool nullable)
        {
            // minimal map; extend if your TVP uses more types
            Type t = sqlType.ToLower() switch
            {
                "int" => typeof(int),
                "bigint" => typeof(long),
                "smallint" => typeof(short),
                "bit" => typeof(bool),
                "decimal" => typeof(decimal),
                "numeric" => typeof(decimal),
                "money" => typeof(decimal),
                "smallmoney" => typeof(decimal),
                "float" => typeof(double),
                "real" => typeof(float),
                "date" => typeof(DateTime),
                "datetime" => typeof(DateTime),
                "datetime2" => typeof(DateTime),
                "smalldatetime" => typeof(DateTime),
                "time" => typeof(TimeSpan),
                "uniqueidentifier" => typeof(Guid),
                // strings
                "varchar" or "nvarchar" or "char" or "nchar" or "text" or "ntext" => typeof(string),
                _ => typeof(string)
            };
            // DataColumn nullability is handled via DBNull.Value, so we return the underlying type
            return t;
        }
    }
}

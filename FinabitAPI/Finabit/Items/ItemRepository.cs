using FinabitAPI.Core.Global;
using FinabitAPI.Core.Global.dto;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Finabit.Items.dto;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinabitAPI.Finabit.Items
{
    public class ItemRepository
    {

        private readonly DBAccess _dbAccess;

        public ItemRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        private SqlConnection GetConnection() => _dbAccess.GetConnection();

        //Gjirafa
        public static List<ItemsGjirafa> SelectAllGjirafa()
        {
            var list = new List<ItemsGjirafa>();

            using (SqlConnection cnn = GlobalRepository.GetConnection())
            using (SqlCommand cmd = new SqlCommand("spGetItemsAPI_Gjirafa", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows) return list;

                        // Cache ordinals once for speed/safety
                        int oProductCode = dr.GetOrdinal("ProductCode");
                        int oGTIN = dr.GetOrdinal("GTIN");
                        int oTitle = dr.GetOrdinal("Title");
                        int oDescription = dr.GetOrdinal("Description");
                        int oBrand = dr.GetOrdinal("Brand");
                        int oImageUrls = dr.GetOrdinal("ImageUrls");
                        int oCategories = dr.GetOrdinal("Categories");
                        int oPrice = dr.GetOrdinal("Price");
                        int oOldPrice = dr.GetOrdinal("OldPrice");
                        int oStoreStockQuantity = dr.GetOrdinal("StoreStockQuantity");
                        int oSpecifications = dr.GetOrdinal("Specifications");

                        while (dr.Read())
                        {
                            var item = new ItemsGjirafa
                            {
                                ProductCode = dr.IsDBNull(oProductCode) ? null : dr.GetString(oProductCode),
                                GTIN = dr.IsDBNull(oGTIN) ? null : dr.GetString(oGTIN),
                                Title = dr.IsDBNull(oTitle) ? null : dr.GetString(oTitle),
                                Description = dr.IsDBNull(oDescription) ? null : dr.GetString(oDescription),
                                Brand = dr.IsDBNull(oBrand) ? null : dr.GetString(oBrand),
                                ImageUrls = SplitToList(dr, oImageUrls),
                                Categories = SplitToList(dr, oCategories),
                                Price = dr.IsDBNull(oPrice) ? 0m : dr.GetDecimal(oPrice),
                                OldPrice = dr.IsDBNull(oOldPrice) ? 0m : dr.GetDecimal(oOldPrice),
                                StoreStockQuantity = dr.IsDBNull(oStoreStockQuantity) ? 0 : dr.GetInt32(oStoreStockQuantity),
                                Specifications = ParseSpecifications(dr, oSpecifications)
                            };

                            list.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // log ex as needed
                    // optionally rethrow or handle
                }
            }

            return list;
        }

        /// <summary>
        /// Splits a comma-separated NVARCHAR column into a List<string>.
        /// If null/empty, returns an empty list.
        /// Trims items and ignores empties.
        /// </summary>
        private static List<string> SplitToList(SqlDataReader dr, int ordinal)
        {
            if (ordinal < 0 || dr.IsDBNull(ordinal)) return new List<string>();

            var raw = dr.GetString(ordinal)?.Trim();
            if (string.IsNullOrEmpty(raw)) return new List<string>();

            // If no comma, return single-item list
            if (!raw.Contains(",")) return new List<string> { raw };

            return raw
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();
        }

        /// <summary>
        /// Builds a List<Specification> from a comma-separated string.
        /// Accepts tokens like "Name:Value" or "Name=Value".
        /// If no separator in a token, puts it as Name with empty Value.
        /// If the entire column is a single value (e.g., SubGroup),
        /// returns one Specification with Name="Specifications" and the value.
        /// </summary>
        private static List<Specification> ParseSpecifications(SqlDataReader dr, int ordinal)
        {
            var specs = new List<Specification>();
            if (ordinal < 0 || dr.IsDBNull(ordinal)) return specs;

            var raw = dr.GetString(ordinal)?.Trim();
            if (string.IsNullOrEmpty(raw)) return specs;

            // If it looks like a single label (no commas and no pair separator),
            // treat it as one spec bucket.
            if (!raw.Contains(",") && !(raw.Contains(":") || raw.Contains("=")))
            {
                specs.Add(new Specification { Name = "Specifications", Value = raw });
                return specs;
            }

            // Split by comma for multiple tokens
            var tokens = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in tokens)
            {
                var token = t.Trim();
                if (token.Length == 0) continue;

                // Try colon first, then equals
                int idx = token.IndexOf(':');
                if (idx < 0) idx = token.IndexOf('=');

                if (idx > 0 && idx < token.Length - 1)
                {
                    var name = token.Substring(0, idx).Trim();
                    var val = token.Substring(idx + 1).Trim();
                    if (name.Length > 0 || val.Length > 0)
                    {
                        specs.Add(new Specification { Name = name, Value = val });
                    }
                }
                else
                {
                    // No key/value separator—store as name only
                    specs.Add(new Specification { Name = token, Value = string.Empty });
                }
            }

            return specs;
        }

        public List<DistinctItemNameDto> GetDistinctItemNames(string itemId = "", string itemName = "")
        {
            var results = new List<DistinctItemNameDto>();

            using (SqlConnection cnn = GlobalRepository.GetConnection())
            using (SqlCommand cmd = new SqlCommand("dbo.spGeDistinctItyemNames_API", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.NVarChar, 200) { Value = (object?)itemId ?? string.Empty });
                cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 200) { Value = (object?)itemName ?? string.Empty });

                try
                {
                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (!dr.HasRows) return results;

                        int oDetailsType = dr.GetOrdinal("DetailsType");
                        int oItemID = dr.GetOrdinal("ItemID");
                        int oDescription = dr.GetOrdinal("Description");
                        int oItemName = dr.GetOrdinal("ItemName");

                        while (dr.Read())
                        {
                            results.Add(new DistinctItemNameDto
                            {
                                DetailsType = dr.IsDBNull(oDetailsType) ? 0 : dr.GetInt32(oDetailsType),
                                ItemID = dr.IsDBNull(oItemID) ? null : dr.GetString(oItemID),
                                Description = dr.IsDBNull(oDescription) ? null : dr.GetString(oDescription),
                                ItemName = dr.IsDBNull(oItemName) ? null : dr.GetString(oItemName)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO: log ex
                }
            }

            return results;
        }
        }
    }

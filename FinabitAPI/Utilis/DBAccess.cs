using AutoBit_WebInvoices.Models;
using Finabit_API.Models;
using FinabitAPI.Models;
using FinabitAPI.Multitenancy;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace FinabitAPI.Utilis
{
    public class DBAccess
    {
        private readonly ITenantAccessor _tenant;

        public DBAccess(ITenantAccessor tenant) => _tenant = tenant;

        private string GetConnectionString()
        {
            var t = _tenant.Current!;
            if (!string.IsNullOrWhiteSpace(t.ConnectionString)) return t.ConnectionString!;

            var sb = new SqlConnectionStringBuilder
            {
                DataSource = t.Server,
                InitialCatalog = t.Database,
                UserID = "Fina",
                Password = "Fina-10",
                TrustServerCertificate = true,
                PersistSecurityInfo = true,
                ConnectTimeout = 60,                
                Pooling = true,
                MinPoolSize = 1
            };
            return sb.ConnectionString;
        }

        public SqlConnection GetConnection() => new SqlConnection(GetConnectionString());

        private const int DefaultCommandTimeoutSeconds = 30;

        private async Task<SqlConnection> OpenWithRetryAsync(CancellationToken ct)
        {
            var cn = new SqlConnection(GetConnectionString());
            int[] delaysMs = { 100, 200, 400 };
            for (int i = 0; ; i++)
            {
                try { await cn.OpenAsync(ct); return cn; }
                catch when (i < delaysMs.Length) { await Task.Delay(delaysMs[i], ct); }
            }
        }

        public async Task TestOpenAsync(CancellationToken ct = default)
        {
            var sb = new SqlConnectionStringBuilder(GetConnectionString()) { ConnectTimeout = 5 };
            await using var cn = new SqlConnection(sb.ConnectionString);
            await cn.OpenAsync(ct);
            await using var cmd = new SqlCommand("SELECT 1;", cn);
            _ = await cmd.ExecuteScalarAsync(ct);
        }

        #region ItemStateList
        public List<XMLItems> M_GetItemsStateList(int DepartmentID)
        {
            List<XMLItems> clsList = new List<XMLItems>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_m_GetItemsState", cnn);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter param;
                param = new SqlParameter("@p_DepartamentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = DepartmentID;
                cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            XMLItems cls = new XMLItems();
                            cls.ID = Convert.ToInt32(dr["ID"]);
                            cls.ItemID = Convert.ToString(dr["ItemID"]);
                            cls.ItemName = Convert.ToString(dr["ItemName"]);
                            cls.Quantity = Convert.ToDecimal(dr["Quantity"]);
                            cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                            cls.Price = Convert.ToDecimal(dr["Price"]);
                            cls.Barcode = Convert.ToString(dr["Barcode"]);
                            cls.Gratis = Convert.ToBoolean(dr["Gratis"]);
                            cls.Coefficient = Convert.ToDecimal(dr["Coefficient"]);
                            cls.VATValue = Convert.ToDecimal(dr["VatValue"]);
                            cls.ItemGroupID = Convert.ToInt32(dr["ItemGroupID"]);
                            cls.Unit1Default = Convert.ToBoolean(dr["Unit1Default"]);
                            cls.UnitName = Convert.ToString(dr["UnitName"]);
                            cls.Coef_Quantity = Convert.ToDecimal(dr["Coef_Quantity"]);
                            cls.MinimalPrice = Convert.ToDecimal(dr["MinimalPrice"]);
                            cls.Discount = Convert.ToDecimal(dr["Discount"]);
                            cls.IncludeInTargetPlan = Convert.ToBoolean(dr["IncludeInTargetPlan"]);
                            cls.NotaKreditore = Convert.ToDecimal(dr["NotaKreditore"]);
                            cls.CustomField6 = Convert.ToString(dr["CustomField6"]);
                            cls.ItemGroupName = Convert.ToString(dr["ItemGroupName"]);
                            cls.CustomField1 = Convert.ToString(dr["CustomField1"]);
                            cls.CustomField2 = Convert.ToString(dr["CustomField2"]);
                            cls.CustomField3 = Convert.ToString(dr["CustomField3"]);
                            cls.CustomField4 = Convert.ToString(dr["CustomField4"]);
                            cls.CustomField5 = Convert.ToString(dr["CustomField5"]);
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
            }
            return clsList;
        }

        #endregion
        public List<Items> GetItems_API(DataTable dt,string Email)
        {
            List<Items> clsList = new List<Items>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItems_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;
                param = new SqlParameter("@Items", SqlDbType.Structured);
                param.Direction = ParameterDirection.Input;
                param.Value = dt;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Email", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = Email;
                cmd.Parameters.Add(param);



                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Items cls = new Items();
                            cls.Shifra = Convert.ToString(dr["Shifra"]);
                            cls.Emertimi = Convert.ToString(dr["Emertimi"]);
                            cls.Shifra_prodhuesit = Convert.ToString(dr["Shifra_e_prodhuesit"]);
                            cls.Shifra_e_katalogut = Convert.ToString(dr["Shifra_e_katalogut"]);
                            cls.Lokacioni = Convert.ToString(dr["Lokacioni"]);
                            cls.Sasia = Convert.ToInt32(dr["Sasia"]);
                            cls.Cmimi = Convert.ToDecimal(dr["Cmimi"]);
                            cls.Prodhuesi = Convert.ToString(dr["Prodhuesi"]);

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
            }
            return clsList;


        }
        public List<Items> GetItemsByItemID_API(string ItemID,string Email)
        {
            List<Items> clsList = new List<Items>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsForItemID_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;
                param = new SqlParameter("@ItemID", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = ItemID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Email", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = Email;
                cmd.Parameters.Add(param);



                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Items cls = new Items();
                            cls.Shifra = Convert.ToString(dr["Shifra"]);
                            cls.Emertimi = Convert.ToString(dr["Emertimi"]);
                            cls.Shifra_prodhuesit = Convert.ToString(dr["Shifra_e_prodhuesit"]);
                            cls.Shifra_e_katalogut = Convert.ToString(dr["Shifra_e_katalogut"]);
                            cls.Lokacioni = Convert.ToString(dr["Lokacioni"]);
                            cls.Sasia = Convert.ToInt32(dr["Sasia"]);
                            cls.Cmimi = Convert.ToDecimal(dr["Cmimi"]);
                            cls.Prodhuesi = Convert.ToString(dr["Prodhuesi"]);
                            cls.Cmimi2 = Convert.ToDecimal(dr["Cmimi2"]);
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
            }
            return clsList;


        }
        public List<ItemsLookup> GetItemsByDate_API(string Date)
        {
            List<ItemsLookup> clsList = new List<ItemsLookup>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsByDate_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;
                param = new SqlParameter("@Date", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = Date;
                cmd.Parameters.Add(param);


                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ItemsLookup cls = new ItemsLookup();
                            cls.ID = Convert.ToInt32(dr["Id"]);
                            cls.ItemID = Convert.ToString(dr["ItemID"]);
                            cls.ItemName = Convert.ToString(dr["ItemName"]);
                            cls.UnitName = Convert.ToString(dr["UnitName"]);
                            cls.UnitID = Convert.ToInt32(dr["UnitID"]);
                            cls.ItemGroupID = Convert.ToInt32(dr["ItemGroupID"]);
                            cls.ItemGroup = Convert.ToString(dr["ItemGroup"]);
                            cls.Taxable = Convert.ToBoolean(dr["Taxable"]);
                            cls.Active = Convert.ToBoolean(dr["Active"]);
                            cls.Dogana = Convert.ToBoolean(dr["Dogana"]);
                            cls.Akciza = Convert.ToBoolean(dr["Akciza"]);
                            cls.Color = Convert.ToString(dr["Color"]);
                            cls.PDAItemName = Convert.ToString(dr["PDAItemName"]);
                            cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                            cls.AkcizaValue = Convert.ToDecimal(dr["AkcizaValue"]);
                            cls.MaximumQuantity = Convert.ToString(dr["MaximumQuantity"]);
                            cls.Coefficient = Convert.ToDecimal(dr["Coefficient"]);
                            cls.Barcode1 = Convert.ToString(dr["barcode1"]);
                            cls.Barcode2 = Convert.ToString(dr["barcode2"]);
                            cls.SalesPrice2 = Convert.ToDecimal(dr["SalesPrice2"]);
                            cls.SalesPrice3 = Convert.ToDecimal(dr["SalesPrice3"]);
                            cls.Origin = Convert.ToString(dr["Origin"]);
                            cls.Category = Convert.ToString(dr["Category"]);
                            cls.PLU = Convert.ToString(dr["PLU"]);
                            cls.ItemTemplate = Convert.ToString(dr["ItemTemplate"]);
                            cls.Weight = Convert.ToDecimal(dr["Weight"]);
                            cls.Author = Convert.ToString(dr["Author"]);
                            cls.Publisher = Convert.ToString(dr["Publisher"]);
                            cls.CustomField1 = Convert.ToString(dr["CustomField1"]);
                            cls.CustomField2 = Convert.ToString(dr["CustomField2"]);
                            cls.CustomField3 = Convert.ToString(dr["CustomField3"]);
                            cls.CustomField4 = Convert.ToString(dr["CustomField4"]);
                            cls.CustomField5 = Convert.ToString(dr["CustomField5"]);
                            cls.CustomField6 = Convert.ToString(dr["CustomField6"]);
                            cls.Barcode3 = Convert.ToString(dr["Barcode3"]);
                            cls.NettoBruttoWeight = Convert.ToDecimal(dr["NettoBruttoWeight"]);
                            cls.BrutoWeight = Convert.ToDecimal(dr["BrutoWeight"]);
                            cls.MaxDiscount = Convert.ToDecimal(dr["MaxDiscount"]);
                            cls.ShifraProdhuesi = Convert.ToInt32(dr["ShifraProdhuesit"]);
                            cls.Prodhuesi = Convert.ToString(dr["Prodhuesi"]);
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
            }
            return clsList;

        }

        public List<ItemsLookup> GetAllItems()
        {
            List<ItemsLookup> clsList = new List<ItemsLookup>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsAll_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ItemsLookup cls = new ItemsLookup();
                            cls.ID = Convert.ToInt32(dr["Id"]);
                            cls.ItemID = Convert.ToString(dr["ItemID"]);
                            cls.ItemName = Convert.ToString(dr["ItemName"]);
                            cls.UnitName = Convert.ToString(dr["UnitName"]);
                            cls.UnitID = Convert.ToInt32(dr["UnitID"]);
                            cls.ItemGroupID = Convert.ToInt32(dr["ItemGroupID"]);
                            cls.ItemGroup = Convert.ToString(dr["ItemGroup"]);
                            cls.Taxable = Convert.ToBoolean(dr["Taxable"]);
                            cls.Active = Convert.ToBoolean(dr["Active"]);
                            cls.Dogana = Convert.ToBoolean(dr["Dogana"]);
                            cls.Akciza = Convert.ToBoolean(dr["Akciza"]);
                            cls.Color = Convert.ToString(dr["Color"]);
                            cls.PDAItemName = Convert.ToString(dr["PDAItemName"]);
                            cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                            cls.AkcizaValue = Convert.ToDecimal(dr["AkcizaValue"]);
                            cls.MaximumQuantity = Convert.ToString(dr["MaximumQuantity"]);
                            cls.Coefficient = Convert.ToDecimal(dr["Coefficient"]);
                            cls.Barcode1 = Convert.ToString(dr["barcode1"]);
                            cls.Barcode2 = Convert.ToString(dr["barcode2"]);
                            cls.SalesPrice2 = Convert.ToDecimal(dr["SalesPrice2"]);
                            cls.SalesPrice3 = Convert.ToDecimal(dr["SalesPrice3"]);
                            cls.Origin = Convert.ToString(dr["Origin"]);
                            cls.Category = Convert.ToString(dr["Category"]);
                            cls.PLU = Convert.ToString(dr["PLU"]);
                            cls.ItemTemplate = Convert.ToString(dr["ItemTemplate"]);
                            cls.Weight = Convert.ToDecimal(dr["Weight"]);
                            cls.Author = Convert.ToString(dr["Author"]);
                            cls.Publisher = Convert.ToString(dr["Publisher"]);
                            cls.CustomField1 = Convert.ToString(dr["CustomField1"]);
                            cls.CustomField2 = Convert.ToString(dr["CustomField2"]);
                            cls.CustomField3 = Convert.ToString(dr["CustomField3"]);
                            cls.CustomField4 = Convert.ToString(dr["CustomField4"]);
                            cls.CustomField5 = Convert.ToString(dr["CustomField5"]);
                            cls.CustomField6 = Convert.ToString(dr["CustomField6"]);
                            cls.Barcode3 = Convert.ToString(dr["Barcode3"]);
                            cls.NettoBruttoWeight = Convert.ToDecimal(dr["NettoBruttoWeight"]);
                            cls.BrutoWeight = Convert.ToDecimal(dr["BrutoWeight"]);
                            cls.MaxDiscount = Convert.ToDecimal(dr["MaxDiscount"]);
                            cls.ShifraProdhuesi = Convert.ToInt32(dr["ShifraProdhuesit"]);
                            cls.Prodhuesi = Convert.ToString(dr["Prodhuesi"]);
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
            }
            return clsList;

        }

        public List<Items> GetItemsProdhuesi_API(string Prodhuesi)
        {
            List<Items> clsList = new List<Items>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsProdhuesi_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;
                param = new SqlParameter("@Prodhuesi", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = Prodhuesi;
                cmd.Parameters.Add(param);

                //param = new SqlParameter("@PageNumber", System.Data.SqlDbType.Int);
                //param.Direction = ParameterDirection.Input;
                //param.Value = PageNumber;
                //cmd.Parameters.Add(param);


                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Items cls = new Items();

                            cls.Shifra = Convert.ToString(dr["Shifra"]);
                            cls.Emertimi = Convert.ToString(dr["Emertimi"]);
                            cls.Shifra_prodhuesit = Convert.ToString(dr["Shifra_e_prodhuesit"]);
                            // cls.Shifra_e_katalogut = Convert.ToString(dr["Shifra_e_katalogut"]);
                            cls.Lokacioni = Convert.ToString(dr["Lokacioni"]);
                            cls.Sasia = Convert.ToInt32(dr["Sasia"]);
                            cls.Cmimi = dr["Cmimi"] == null ? 0 : Convert.ToDecimal(dr["Cmimi"]);
                            cls.Prodhuesi = Convert.ToString(dr["Prodhuesi"]);

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
            }
            return clsList;

        }
        public List<Prodhuesi> GetProdhuesi()
        {
            List<Prodhuesi> clsList = new List<Prodhuesi>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spProdhuesiList_api", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                //param = new SqlParameter("@PageNumber", System.Data.SqlDbType.Int);
                //param.Direction = ParameterDirection.Input;
                //param.Value = PageNumber;
                //cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Prodhuesi cls = new Prodhuesi();

                            cls.Shifra = Convert.ToString(dr["Shifra"]);
                            cls.Emertimi = Convert.ToString(dr["Emri"]);


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
            }
            return clsList;
        }
        public List<Items> GetItems_API_2(DataTable dt,string Email)
        {
            List<Items> clsList = new List<Items>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItems_API_2", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;
                param = new SqlParameter("@Items", SqlDbType.Structured);
                param.Direction = ParameterDirection.Input;
                param.Value = dt;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Email", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = Email;
                cmd.Parameters.Add(param);


                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Items cls = new Items();
                            cls.Shifra = Convert.ToString(dr["Shifra"]);
                            cls.Emertimi = Convert.ToString(dr["Emertimi"]);
                            cls.Shifra_prodhuesit = Convert.ToString(dr["Shifra_e_prodhuesit"]);
                            cls.Shifra_e_katalogut = Convert.ToString(dr["Shifra_e_katalogut"]);
                            cls.Lokacioni = Convert.ToString(dr["Lokacioni"]);
                            cls.Sasia = Convert.ToInt32(dr["Sasia"]);
                            cls.Cmimi = Convert.ToDecimal(dr["Cmimi"]);
                            cls.Prodhuesi = Convert.ToString(dr["Prodhuesi"]);

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
            }
            return clsList;


        }
        #region ListSystemDataTable

        public DataTable ListSystemDataTable()
        {
            DataTable data = new DataTable();
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spSystemList", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(data);

                    return data;
                }
                catch (Exception exception)
                {
                    string message = exception.Message;
                    cnn.Close();
                }
            }
            return data;
        }

        #endregion

        #region GetOptions

        public void GetOptions()
        {

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetOptions", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            OptionsData.ID = Convert.ToInt32(dr["ID"]);
                            OptionsData.POSPartnerID = Convert.ToInt32(dr["POSPartnerID"]);
                            OptionsData.AllTables = Convert.ToBoolean(dr["AllTables"]);
                            OptionsData.NrCopies = Convert.ToInt32(dr["NrCopies"]);
                            OptionsData.TerminCashAccount = Convert.ToString(dr["TerminCashAccount"]);
                            OptionsData.EmployeeCashAccount = Convert.ToString(dr["EmployeeCashAccount"]);
                            OptionsData.GetFirstDescription = Convert.ToBoolean(dr["GetFirstDescription"]);
                            OptionsData.EmployeeAdvanceAccount = Convert.ToString(dr["EmployeeAdvanceAccount"]);
                            OptionsData.PayablesVATAccount = Convert.ToString(dr["PayablesVATAccount"]);
                            OptionsData.SalesVATAccount = Convert.ToString(dr["SalesVATAccount"]);
                            //OptionsData.ItemPendingAccount = Convert.ToString(dr["ItemPendingAccount"]);
                            OptionsData.DUDPartner = Convert.ToInt32(dr["DUDPartner"]);
                            OptionsData.AkcizaPartner = Convert.ToString(dr["AkcizaPartner"]);
                            OptionsData.VATPartner = Convert.ToString(dr["VATPartner"]);
                            OptionsData.CashAccount = Convert.ToString(dr["CashAccount"]);
                            //OptionsData.FINAPartnersAccount = Convert.ToString(dr["FINAPartnersAccount"]);
                            //OptionsData.FeeAccount = Convert.ToString(dr["FeeAccount"]);
                            //OptionsData.AdvanceAccount = Convert.ToString(dr["AdvanceAccount"]);
                            OptionsData.POSVAT = Convert.ToInt32(dr["POSVAT"]);
                            OptionsData.PrintTranIDInFiscal = Convert.ToBoolean(dr["PrintTranIDInFiscal"]);
                            OptionsData.WorksWithRFID = Convert.ToBoolean(dr["WorksWithRFID"]);
                            OptionsData.UseOldItems = Convert.ToBoolean(dr["UseOldItems"]);
                            OptionsData.LogOffAfterPOS = Convert.ToBoolean(dr["LogOffAfterPOS"]);
                            //OptionsData.Printer1 = Convert.ToString(dr["Printer1"]);
                            //OptionsData.Printer2 = Convert.ToString(dr["Printer2"]);
                            OptionsData.ProposeVAT = Convert.ToBoolean(dr["ProposeVAT"]);
                            OptionsData.PayableAccount = Convert.ToString(dr["PayableAccount"]);
                            OptionsData.ReceivableAccount = Convert.ToString(dr["ReceivableAccount"]);
                            OptionsData.UseDoublePartners = Convert.ToBoolean(dr["UseDoublePartners"]);
                            OptionsData.UseRegularInvoice = Convert.ToBoolean(dr["UseRegularInvoice"]);
                            OptionsData.ShowTimeAtPOSInvoice = Convert.ToBoolean(dr["ShowTimeAtPOSInvoice"]);
                            OptionsData.AutomaticallyCalculatePrice = Convert.ToBoolean(dr["AutomaticallyCalculatePrice"]);
                            OptionsData.NumberOfCopy1 = Convert.ToInt32(dr["NumberOfCopy1"]);
                            OptionsData.ShowWages = Convert.ToBoolean(dr["ShowWages"]);
                            OptionsData.ShowFixedAssets = Convert.ToBoolean(dr["ShowFixedAssets"]);
                            OptionsData.ShowHotel = Convert.ToBoolean(dr["ShowHotel"]);
                            OptionsData.PrintFiscalInvoice = dr["PrintFiscalInvoice"] == DBNull.Value ? false : Convert.ToBoolean(dr["PrintFiscalInvoice"]);
                            OptionsData.PrintFiscalAlways = dr["PrintFiscalAlways"] == DBNull.Value ? false : Convert.ToBoolean(dr["PrintFiscalAlways"]);
                            OptionsData.TranNoType = dr["TranNoType"] == DBNull.Value ? 1 : Convert.ToInt32(dr["TranNoType"]);
                            OptionsData.GroupIncomeAccount = dr["GroupIncomeAccount"] == DBNull.Value ? "" : Convert.ToString(dr["GroupIncomeAccount"]);
                            OptionsData.GroupExpenseAccount = dr["GroupExpenseAccount"] == DBNull.Value ? "" : Convert.ToString(dr["GroupExpenseAccount"]);
                            OptionsData.GroupAssetAccount = dr["GroupAssetAccount"] == DBNull.Value ? "" : Convert.ToString(dr["GroupAssetAccount"]);
                            OptionsData.PrintFiscalAfterEachOrder = dr["PrintFiscalAfterEachOrder"] == DBNull.Value ? false : Convert.ToBoolean(dr["PrintFiscalAfterEachOrder"]);
                            OptionsData.UseSubOrders = Convert.ToBoolean(dr["UseSubOrders"]);
                            OptionsData.UseDoubleNumberInSales = Convert.ToBoolean(dr["UseDoubleNumberInSales"]);
                            OptionsData.ShowPaletsInMobile = Convert.ToBoolean(dr["DontShowPaletsInMobile"]);
                            OptionsData.GjeneroNotenKreditore = Convert.ToBoolean(dr["GjeneroNotenKreditore"]);

                            break;
                        }
                    }
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    string exp = ex.Message;
                    cnn.Close();
                }
            }
        }

        #endregion

        #region GetTransactionNo

        public string GetTransactionNo(int TransactionType, DateTime Date, int DepartmentID)
        {
            string TransactionNo = "";

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetTransactionNo", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                SqlParameter param;
                param = new SqlParameter("@TypeID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = TransactionType;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Date", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = Date;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = DepartmentID;
                cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();
                    object ob = cmd.ExecuteScalar();
                    TransactionNo = ob == null ? "" : ob.ToString();
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    TransactionNo = "";
                    cnn.Close();
                }
            }
            return TransactionNo;
        }

        #endregion

        #region DepartmentById
        public Department SelectDepartmentByID(Department cls)
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spDepartmentByID", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.ID;
                cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cls = new Department();
                            cls.ID = Convert.ToInt32(dr["DepartmentID"]);
                            cls.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                            cls.Account = Convert.ToString(dr["Account"]);
                            cls.PriceMenuID = Convert.ToInt32(dr["PriceMenuID"]);
                            cls.PriceMenuName = Convert.ToString(dr["PriceMenuName"]);
                            cls.LUD = Convert.ToDateTime(dr["LUD"]);
                            cls.LUB = Convert.ToInt32(dr["LUB"]);
                            cls.LUN = Convert.ToInt32(dr["LUN"]);
                            cls.AllowNegative = dr["AllowNegative"] == DBNull.Value ? true : Convert.ToBoolean(dr["AllowNegative"]);
                            cls.CompanyID = dr["CompanyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CompanyID"]);
                            break;
                        }
                    }
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    string exp = ex.Message;
                    cnn.Close();
                }
            }
            return cls;
        }

        #endregion
        
        #region SelectVatPercentByID

        public VATPercent SelectVatPercentByID(VATPercent cls)
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spVATPercentByID", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = new SqlParameter("@VATID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.VATID;
                cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cls = new VATPercent();
                            cls.VATID = Convert.ToInt32(dr["VATID"]);
                            cls.VATName = Convert.ToString(dr["VATName"]);
                            cls.VATPercents = Convert.ToDecimal(dr["VATPercent"]);
                            cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                            cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                            cls.LUD = Convert.ToDateTime(dr["LUD"]);
                            cls.LUN = Convert.ToInt32(dr["LUN"]);
                            cls.LUB = Convert.ToInt32(dr["LUB"]);
                            break;
                        }
                    }
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    string exp = ex.Message;
                    cnn.Close();
                }
            }
            return cls;
        }

        #endregion

        #region GetItemsForPOS

        public List<ItemsLookup> GetItemsForPOS(int PriceMenuID, int DepartmentID)
        {
            List<ItemsLookup> clsList = new List<ItemsLookup>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsForPOS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PriceMenuID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = PriceMenuID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = DepartmentID;
                cmd.Parameters.Add(param);

                ItemsLookup cls;

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cls = new ItemsLookup();
                            cls.ID = Convert.ToInt32(dr["ID"]);
                            cls.ItemID = Convert.ToString(dr["ItemID"]);
                            cls.ItemName = Convert.ToString(dr["ItemName"]);
                            cls.SalesPrice = Convert.ToDecimal(dr["SalesPrice"]);
                            cls.ViewPOS = Convert.ToBoolean(dr["ViewPOS"]);
                            cls.Color = Convert.ToString(dr["Color"]);
                            cls.LocationID = dr["LocationID"] == DBNull.Value ? 1 : Convert.ToInt32(dr["LocationID"]);
                            cls.Taxable = dr["Taxable"] == DBNull.Value ? false : Convert.ToBoolean(dr["Taxable"]);
                            cls.VATValue = dr["VATValue"] == DBNull.Value ? 1 : Convert.ToDecimal(dr["VATValue"]);
                            cls.DoganaValue = dr["DoganaValue"] == DBNull.Value ? 1 : Convert.ToDecimal(dr["DoganaValue"]);
                            cls.AkcizaValue = dr["AkcizaValue"] == DBNull.Value ? 1 : Convert.ToDecimal(dr["AkcizaValue"]);
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
            }

            return clsList;
        }

        #endregion

        #region Insert

        public void Insert(Transactions cls)
        {

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spTransactionsInsert", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlCommand cmd = SqlCommandForTran_SP("spTransactionsInsert");
                try
                {
                    SqlParameter param;
                    param = new SqlParameter("@TransactionTypeID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.TransactionTypeID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.TransactionDate;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@InvoiceDate", SqlDbType.SmallDateTime);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.InvoiceDate;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@DueDate", SqlDbType.SmallDateTime);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.DueDate;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@TransactionNo", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.TransactionNo;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@DUDNo", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.DUDNo;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@InvoiceNo", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.InvoiceNo;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@VAT", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.VAT;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@InPL", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.InPL;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PartnerID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PartnerID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PartnersAddress", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PartnersAddress;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PartnersContactPerson", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PartnersContactPerson;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PartnersPhoneNo", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PartnersPhoneNo;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.DepartmentID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@DriverID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.DriverID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PlateNo", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PlateNo;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@InternalDepartmentID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.InternalDepartmentID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@EmpID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.EmpID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@CashAccount", SqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.CashAccount;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Import", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Import;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Value", SqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Value;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@VATValue", SqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.VATValue;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@AllValue", SqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.AllValue;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PaidValue", SqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PaidValue;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@VATPercent", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.VATPercent;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@VATPercentID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.VATPercentID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Memo", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Memo;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Reference", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Reference;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Links", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Links;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Active", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Active;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@JournalStatus", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.JournalStatus;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@InsBy", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.InsBy;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@LUB", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.InsBy;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@prmErrorID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@prmNewID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Transport", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Transport;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Dogana", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Dogana;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Akciza", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Akciza;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@RABAT", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.RABAT;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@TableID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.TableID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@POSStatus", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.POSStatus;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@TerminID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.TerminID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Commission1", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Commission1;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Commission2", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Commission2;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Commission3", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Commission3;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@SuficitAccount", SqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.SuficitAccount;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@DeficitAccount", SqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.DeficitAccount;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@VehicleID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.VehicleID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@ItemID", SqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.ItemID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Quantity", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Quantity;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@CurrencyID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.CurrencyID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@CurrencyRate", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.CurrencyRate;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@OverValue", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.OverValue;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Charges", SqlDbType.Money);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Charges;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PartnerItemID", SqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PartnerItemID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@ContractID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.ContractID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@POSPaid", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.POSPaid;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@HReservationID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.HReservationID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PaymentTypeID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@VisitID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.VisitID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@ReferenceID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.ReferenceID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@CompanyID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.CompanyID;
                    cmd.Parameters.Add(param);

                    if (cls.PDAInsDate == DateTime.MinValue)
                        cls.PDAInsDate = DateTime.Now;

                    param = new SqlParameter("@PDAInsDate", SqlDbType.DateTime);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PDAInsDate;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@IsNoCustom", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.IsNoCustom;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@IncludeTransport", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.IncludeTransport;
                    cmd.Parameters.Add(param);


                    param = new SqlParameter("@HostName", SqlDbType.VarChar);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Memo; // perkohesisht luan rolin e HostName
                    cmd.Parameters.Add(param);


                    param = new SqlParameter("@Longitude", SqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Longitude;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@Latitude", SqlDbType.Decimal);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.Latitude;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@ServiceType", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.ServiceType;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@AssetID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.AssetID;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@BL", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.BL;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@IsPriceFromPartner", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.IsPriceFromPartner;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@PayValueWithForce", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.PayValueWithForce;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@CardID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = cls.CardID;
                    cmd.Parameters.Add(param);

                    //cnn.Open();

                    cmd.ExecuteNonQuery();
                    cls.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);
                    //this.ErrorID = cls.ErrorID;
                    cls.ID = Convert.ToInt32(cmd.Parameters["@prmNewID"].Value);

                    //cnn.Close();
                }
                catch (Exception ex)
                {
                    cls.ErrorID = -1;
                    //this.ErrorID = cls.ErrorID;
                    cls.ErrorDescription = ex.Message;
                    //cnn.Close();
                }
            }

           // this.ErrorID = cls.ErrorID;
        }

        #endregion

        #region SelectTransactionByID

        public Transactions SelectTransactionByID(Transactions cls)
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spTransactionsByID", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                SqlParameter param;

                param = new SqlParameter("@ID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = cls.ID;
                cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cls = new Transactions();
                            cls.ID = Convert.ToInt32(dr["ID"]);
                            cls.TransactionTypeID = Convert.ToInt32(dr["TransactionTypeID"]);
                            cls.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            cls.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                            cls.DueDate = Convert.ToDateTime(dr["DueDate"]);
                            cls.TransactionNo = Convert.ToString(dr["TransactionNo"]);
                            cls.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                            cls.DUDNo = Convert.ToString(dr["DUDNo"]);
                            cls.VAT = Convert.ToBoolean(dr["VAT"]);
                            cls.InPL = dr["InPL"] == DBNull.Value ? false : Convert.ToBoolean(dr["InPL"]);
                            cls.PartnerID = Convert.ToInt32(dr["PartnerID"]);
                            cls.PartnersAddress = Convert.ToString(dr["PartnersAddress"]);
                            cls.PartnersContactPerson = Convert.ToString(dr["PartnersContactPerson"]);
                            cls.PartnersPhoneNo = Convert.ToString(dr["PartnersPhoneNo"]);
                            cls.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                            cls.DriverID = dr["DriverID"] == DBNull.Value ? -1 : int.Parse(dr["DriverID"].ToString());
                            cls.PlateNo = Convert.ToString(dr["PlateNoDriver"]);
                            try
                            {
                                cls.InternalDepartmentID = Convert.ToInt32(dr["InternalDepartmentID"]);
                            }
                            catch
                            {
                                cls.InternalDepartmentID = 0;
                            }
                            cls.EmpID = Convert.ToInt32(dr["EmpID"]);
                            cls.CashAccount = dr["CashAccount"] == DBNull.Value ? "" : Convert.ToString(dr["CashAccount"]);
                            cls.Import = Convert.ToBoolean(dr["Import"]);
                            cls.Value = Convert.ToDecimal(dr["Value"]);
                            cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                            cls.AllValue = Convert.ToDecimal(dr["AllValue"]);
                            cls.PaidValue = Convert.ToDecimal(dr["PaidValue"]);
                            cls.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
                            cls.VATPercentID = Convert.ToInt32(dr["VATPercentID"]);
                            cls.Memo = Convert.ToString(dr["Memo"]);
                            cls.Reference = Convert.ToString(dr["Reference"]);
                            cls.Links = Convert.ToString(dr["Links"]);
                            cls.Active = Convert.ToBoolean(dr["Active"]);
                            cls.JournalStatus = dr["JournalStatus"] == DBNull.Value ? false : Convert.ToBoolean(dr["JournalStatus"]); ;
                            cls.InsBy = Convert.ToInt32(dr["InsBy"]);
                            cls.InsDate = Convert.ToDateTime(dr["InsDate"]);
                            cls.LUB = Convert.ToInt32(dr["LUB"]);
                            cls.LUN = Convert.ToInt32(dr["LUN"]);
                            cls.LUD = Convert.ToDateTime(dr["LUD"]);
                            cls.Transport = Convert.ToDecimal(dr["Transport"]);
                            cls.Dogana = Convert.ToDecimal(dr["Dogana"]);
                            cls.Akciza = Convert.ToDecimal(dr["Akciza"]);
                            cls.RABAT = Convert.ToDecimal(dr["RABAT"]);
                            cls.ReferenceID = Convert.ToInt32(dr["ReferenceID"]);
                            cls.Commission1 = dr["Commission1"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission1"]);
                            cls.Commission2 = dr["Commission2"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission2"]);
                            cls.Commission3 = dr["Commission3"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Commission3"]);
                            cls.SuficitAccount = dr["SuficitAccount"] == DBNull.Value ? "" : Convert.ToString(dr["SuficitAccount"]);
                            cls.DeficitAccount = dr["DeficitAccount"] == DBNull.Value ? "" : Convert.ToString(dr["DeficitAccount"]);
                            cls.VehicleID = dr["VehicleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VehicleID"]);
                            cls.ItemID = dr["ItemID"] == DBNull.Value ? "" : Convert.ToString(dr["ItemID"]);
                            cls.Quantity = dr["Quantity"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Quantity"]);
                            cls.PartnerItemID = dr["PartnerItemID"] == DBNull.Value ? "" : dr["PartnerItemID"].ToString();
                            cls.CurrencyID = dr["CurrencyID"] == DBNull.Value ? 0 : int.Parse(dr["CurrencyID"].ToString());
                            cls.CurrencyRate = dr["CurrencyRate"] == DBNull.Value ? 0 : decimal.Parse(dr["CurrencyRate"].ToString());
                            cls.OverValue = dr["OverValue"] == DBNull.Value ? 0 : decimal.Parse(dr["OverValue"].ToString());
                            cls.Charges = dr["Charges"] == DBNull.Value ? 0 : decimal.Parse(dr["Charges"].ToString());
                            cls.ContractID = Convert.ToInt32(dr["ContractID"]);
                            cls.POSPaid = dr["POSPaid"] == DBNull.Value ? false : Convert.ToBoolean(dr["POSPaid"]);
                            cls.HReservationID = dr["HReservationID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HReservationID"]);
                            cls.PaymentTypeID = dr["PaymentTypeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PaymentTypeID"]);
                            cls.PaymentTypeName = dr["PaymentTypeName"] == DBNull.Value ? "" : Convert.ToString(dr["PaymentTypeName"]);
                            cls.CompanyID = Convert.ToInt16(dr["CompanyID"]);
                            cls.Longitude = Convert.ToDecimal(dr["Longitude"]);
                            cls.Latitude = Convert.ToDecimal(dr["Latitude"]);
                            cls.CardID = Convert.ToInt32(dr["CardID"]);
                            cls.CardBarcode = Convert.ToString(dr["CardBarcode"]);

                            break;
                        }
                    }
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    string exp = ex.Message;
                    cnn.Close();
                }
            }
            return cls;
        }

        #endregion

        #region CashJournalIDByCashAccount
        public int CashJournalIDByCashAccount(string CashAccount, int TypeID, DateTime Date, int CompanyID)
        {
            int CashJournalID = 0;

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spCashJournalIDByCashAccount", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                SqlParameter param;
                param = new SqlParameter("@Account", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = CashAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Date", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = Date;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TypeID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = TypeID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CompanyID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = CompanyID;
                cmd.Parameters.Add(param);

                try
                {
                    cnn.Open();
                    object ob = cmd.ExecuteScalar();
                    CashJournalID = ob == DBNull.Value ? 0 : int.Parse(ob.ToString());
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    CashJournalID = 0;
                    cnn.Close();
                }
            }

            return CashJournalID;
        }
        #endregion

        public DataTable GetCustomerAnalyticsAll(DateTime FromDate, DateTime ToDate)
        {

            DataTable dt = new DataTable();
            dt.TableName = "GetCustomerAnalyticsAll";

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetCustomersAnalyticsAll", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.Add("@FromDate", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = FromDate;

                param = cmd.Parameters.Add("@ToDate", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = ToDate;

                try
                {
                    cnn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception)
                {

                    string s = "";
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return dt;

        }

        public DataTable GetCustomerAnalyticsByPartnerID(DateTime FromDate, DateTime ToDate, int PartnerID)
        {

            DataTable dt = new DataTable();
            dt.TableName = "GetCustomerAnalyticsByPartnerID";
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetCustomerAnalyticsByPartnerID", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = cmd.Parameters.Add("@FromDate", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = FromDate;

                param = cmd.Parameters.Add("@ToDate", SqlDbType.SmallDateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = ToDate;

                param = cmd.Parameters.Add("@PartnerID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = PartnerID;

                try
                {
                    cnn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception)
                {

                    string s = "";
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return dt;

        }

        public DataTable GetCustomerAnalyticsByPartnerID_MAR(int PartnerID, int ProfileID, int TransactionType,int PageId,int RowsForPage,int Language)
        {

            DataTable dt = new DataTable();
            dt.TableName = "GetCustomerAnalyticsByPartnerID_MAR";
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetCustomerAnalyticsByPartnerID_MAR", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;
                param = cmd.Parameters.Add("@PartnerID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = PartnerID;

                param = cmd.Parameters.Add("@ProfileID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = ProfileID;

                param = cmd.Parameters.Add("@TransactionType", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = TransactionType;

                param = cmd.Parameters.Add("@PageID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = PageId;

                param = cmd.Parameters.Add("@RowsForPage", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = RowsForPage;

                param = cmd.Parameters.Add("@Language", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = Language;

                try
                {
                    cnn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception)
                {

                    string s = "";
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return dt;

        }
        #region SelectAll

        public  List<Orders> OrdersList(string FromDate, string ToDate)
        {
            List<Orders> clsList = new List<Orders>();
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spOrdersList_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                SqlParameter param;

                param = new SqlParameter("@FromDate", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = FromDate;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ToDate", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = ToDate;
                cmd.Parameters.Add(param);



                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Orders cls = new Orders();
                            cls.ID = Convert.ToInt32(dr["ID"]);
                            cls.Data = Convert.ToDateTime(dr["Data"]);

                            cls.Numri = Convert.ToString(dr["Numri"]);

                            cls.ID_Konsumatorit = Convert.ToInt32(dr["ID_Konsumatorit"]);

                            cls.Konsumatori = Convert.ToString(dr["Konsumatori"]);
                            cls.Komercialisti = Convert.ToString(dr["Komercialisti"]);
                            cls.Statusi_Faturimit = Convert.ToString(dr["Statusi_Faturimit"]);

                            cls.Shifra = Convert.ToString(dr["Shifra"]);

                            cls.Emertimi = Convert.ToString(dr["Emertimi"]);
                            cls.Njesia_Artik = Convert.ToString(dr["Njesia_Artik"]);

                            cls.Sasia = Convert.ToDecimal(dr["Sasia"]);
                            cls.Cmimi = Convert.ToDecimal(dr["Cmimi"]);


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
            }
            return clsList;
        }


        public DataTable CustomersList(int Type,int DepartmentID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spPartnerByType", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = cmd.Parameters.Add("@Type", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = Type;

                param = cmd.Parameters.Add("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = DepartmentID;

                try
                {
                    cnn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception)
                {

                    string s = "";
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return dt;
        }
        public DataTable DepartmentsList()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spDepartmentList", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();

                SqlParameter param;
                param = cmd.Parameters.Add("@IsCombo", SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = 0;

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception)
                {

                    string s = "";
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
            return dt;
        }

        #endregion


        public List<ItemsWeb> GetItemsWeb(int DepartmentID)
        {
            List<ItemsWeb> clsList = new List<ItemsWeb>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsForPOS_web", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();

                SqlParameter param;


                param = new SqlParameter("@DepartmentID", SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = DepartmentID;
                cmd.Parameters.Add(param);


                try
                {

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ItemsWeb cls = new ItemsWeb();
                            cls.ItemID = Convert.ToString(dr["ItemID"]);
                            cls.ItemName = Convert.ToString(dr["ItemName"]);
                            cls.SalesPrice = Convert.ToDecimal(dr["SalesPrice"]);
                            cls.LocationID = Convert.ToInt32(dr["LocationID"]);
                            cls.Taxable = Convert.ToBoolean(dr["Taxable"]);
                            cls.VATValue = Convert.ToDecimal(dr["VATValue"]);
                            cls.ItemGroup = Convert.ToString(dr["ItemGroup"]);
                            cls.Photo = dr["Photo"] == DBNull.Value ? "" : Convert.ToBase64String((byte[])dr["Photo"]);

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
            }
            return clsList;

        }



        public List<ItemsAttributes> GetItemsAttributesWeb(string ItemID)
        {
            List<ItemsAttributes> clsList = new List<ItemsAttributes>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("spGetItemsAttributes_web", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();

                SqlParameter param;

                param = new SqlParameter("@ItemID", SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = ItemID;
                cmd.Parameters.Add(param);


                try
                {

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ItemsAttributes cls = new ItemsAttributes();
                            cls.ID = Convert.ToInt32(dr["ID"]);
                            cls.ItemID = Convert.ToString(dr["ItemID"]);
                            cls.AttributeName = Convert.ToString(dr["AttributeName"]);

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
            }
            return clsList;
        }

        public List<ItemState> GetItemsStateForOneItem(string ItemID)
        {
            List<ItemState> clsList = new List<ItemState>();

            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetItemsStateForOneItem_API", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cnn.Open();


                SqlParameter param;

                param = new SqlParameter("@ItemID", SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = ItemID;
                cmd.Parameters.Add(param);

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ItemState cls = new ItemState
                                {
                                    ID = Convert.ToInt32(dr["ID"]),
                                    ItemID = Convert.ToString(dr["ItemID"]),
                                    ItemName = Convert.ToString(dr["ItemName"]),
                                    Quantity = Convert.ToDecimal(dr["Quantity"]),
                                    Price = Convert.ToDecimal(dr["Price"]),
                                    Barcode = Convert.ToString(dr["Barcode"]),
                                    Coefficient = Convert.ToDecimal(dr["Coefficient"]),
                                    VATValue = Convert.ToDecimal(dr["VATValue"]),
                                    Unit1Default = Convert.ToBoolean(dr["Unit1Default"]),
                                    UnitName = Convert.ToString(dr["UnitName"]),
                                    Coef_Quantity = Convert.ToDecimal(dr["Coef_Quantity"]),
                                    Discount = Convert.ToDecimal(dr["Discount"]),
                                    ItemGroupName = Convert.ToString(dr["ItemGroupName"]),
                                    UnitName1 = Convert.ToString(dr["UnitName1"]),
                                    UnitName2 = Convert.ToString(dr["UnitName2"]),
                                    Color = Convert.ToString(dr["Color"]),
                                    Number = Convert.ToString(dr["Number"])
                                };

                                clsList.Add(cls);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string exp = ex.Message;
                }
            }
            return clsList;
        }

        public async Task<List<Orders>> GetTransactions(
     string fromDate, string toDate, int type,
     string itemID = null, string itemName = null, string partnerName = null,
     CancellationToken ct = default)
        {
            var list = new List<Orders>();

            await using var cnn = await OpenWithRetryAsync(ct);
            await using var cmd = new SqlCommand("spTransactionsList_API", cnn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = DefaultCommandTimeoutSeconds
            };

            cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar) { Value = fromDate });
            cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar) { Value = toDate });
            cmd.Parameters.Add(new SqlParameter("@TranTypeID", SqlDbType.Int) { Value = type });
            cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(itemID) ? "%" : itemID });
            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(itemName) ? "%" : itemName });
            cmd.Parameters.Add(new SqlParameter("@PartnerName", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(partnerName) ? "%" : partnerName });

            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct);
            while (await dr.ReadAsync(ct))
            {
                list.Add(new Orders
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Data = Convert.ToDateTime(dr["Data"]),
                    Numri = Convert.ToString(dr["Numri"]),
                    ID_Konsumatorit = Convert.ToInt32(dr["ID_Konsumatorit"]),
                    Konsumatori = Convert.ToString(dr["Konsumatori"]),
                    Komercialisti = Convert.ToString(dr["Komercialisti"]),
                    Statusi_Faturimit = Convert.ToString(dr["Statusi_Faturimit"]),
                    Shifra = Convert.ToString(dr["Shifra"]),
                    Emertimi = Convert.ToString(dr["Emertimi"]),
                    Njesia_Artik = Convert.ToString(dr["Njesia_Artik"]),
                    Sasia = Convert.ToDecimal(dr["Sasia"]),
                    Cmimi = Convert.ToDecimal(dr["Cmimi"])
                });
            }

            return list;
        }

        public async Task<List<TransactionAggregate>> GetTransactionsAggregate(
     string fromDate, string toDate, int type,
     string itemID = null, string itemName = null, string partnerName = null,
     string departmentName = null,
     CancellationToken ct = default)
        {
            var list = new List<TransactionAggregate>();

            await using var cnn = await OpenWithRetryAsync(ct);
            await using var cmd = new SqlCommand("spTransactionsListAggregate_API", cnn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = DefaultCommandTimeoutSeconds
            };

            cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.VarChar) { Value = fromDate });
            cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.VarChar) { Value = toDate });
            cmd.Parameters.Add(new SqlParameter("@TranTypeID", SqlDbType.Int) { Value = type });
            cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.NVarChar, 200) { Value = string.IsNullOrEmpty(itemID) ? "%" : itemID });
            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, 200) { Value = itemName ?? string.Empty });
            cmd.Parameters.Add(new SqlParameter("@PartnerName", SqlDbType.NVarChar, 200) { Value = partnerName ?? string.Empty });
            cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 200) { Value = departmentName ?? string.Empty });

            await using var dr = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess, ct);
            while (await dr.ReadAsync(ct))
            {
                list.Add(new TransactionAggregate
                {
                    Data = Convert.ToDateTime(dr["Data"]),
                    Value = Convert.ToDecimal(dr["Value"]),
                    Rows = Convert.ToInt32(dr["rows"])
                });
            }

            return list;
        }
    }
}
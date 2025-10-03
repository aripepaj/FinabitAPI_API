using FinabitAPI.Core.Global.dto;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FinabitAPI.Core.Global
{
    public class OptionsRepository
    {
        private readonly DBAccess _dbAccess;

        public OptionsRepository(DBAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        #region Update

        public void Update()
        {
            using var cnn = _dbAccess.GetConnection();
            using var cmd = new SqlCommand("spOptionsUpdate", cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add parameters (same as your original code)
            cmd.Parameters.AddWithValue("@ID", OptionsData.ID);
            cmd.Parameters.AddWithValue("@POSPartnerID", OptionsData.POSPartnerID);
            cmd.Parameters.AddWithValue("@AllTables", OptionsData.AllTables);
            cmd.Parameters.AddWithValue("@NrCopies", OptionsData.NrCopies);
            cmd.Parameters.AddWithValue("@TerminCashAccount", OptionsData.TerminCashAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EmployeeCashAccount", OptionsData.EmployeeCashAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GetFirstDescription", OptionsData.GetFirstDescription);
            cmd.Parameters.AddWithValue("@EmployeeAdvanceAccount", OptionsData.EmployeeAdvanceAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@PayablesVATAccount", OptionsData.PayablesVATAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SalesVATAccount", OptionsData.SalesVATAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CashAccount", OptionsData.CashAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ItemPendingAccount", OptionsData.ItemPendingAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DUDPartner", OptionsData.DUDPartner);
            cmd.Parameters.AddWithValue("@AkcizaPartner", OptionsData.AkcizaPartner ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@VATPartner", OptionsData.VATPartner ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FINAPartnersAccount", OptionsData.FINAPartnersAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FeeAccount", OptionsData.FeeAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@AdvanceAccount", OptionsData.AdvanceAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@POSVAT", OptionsData.POSVAT);
            cmd.Parameters.AddWithValue("@WorksWithRFID", OptionsData.WorksWithRFID);
            cmd.Parameters.AddWithValue("@UseOldItems", OptionsData.UseOldItems);
            cmd.Parameters.AddWithValue("@LogOffAfterPOS", OptionsData.LogOffAfterPOS);
            cmd.Parameters.AddWithValue("@Printer1", "");
            cmd.Parameters.AddWithValue("@Printer2", "");
            cmd.Parameters.AddWithValue("@ProposeVAT", OptionsData.ProposeVAT);
            cmd.Parameters.AddWithValue("@PayableAccount", OptionsData.PayableAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ReceivableAccoun", OptionsData.ReceivableAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@UseDoublePartners", OptionsData.UseDoublePartners);
            cmd.Parameters.AddWithValue("@UseRegularInvoice", OptionsData.UseRegularInvoice);
            cmd.Parameters.AddWithValue("@ShowTimeAtPOSInvoice", OptionsData.ShowTimeAtPOSInvoice);
            cmd.Parameters.AddWithValue("@AutomaticallyCalculatePrice", OptionsData.AutomaticallyCalculatePrice);
            cmd.Parameters.AddWithValue("@PrintFiscalInvoice", OptionsData.PrintFiscalInvoice);
            cmd.Parameters.AddWithValue("@PrintFiscalAlways", OptionsData.PrintFiscalAlways);
            cmd.Parameters.AddWithValue("@TranNoType", OptionsData.TranNoType);
            cmd.Parameters.AddWithValue("@GroupIncomeAccount", OptionsData.GroupIncomeAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GroupExpenseAccount", OptionsData.GroupExpenseAccount ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GroupAssetAccount", OptionsData.GroupAssetAccount ?? (object)DBNull.Value);

            var outParam = cmd.Parameters.Add("@prmErrorID", SqlDbType.Int);
            outParam.Direction = ParameterDirection.Output;

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                OptionsData.ErrorID = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                OptionsData.ErrorID = -1;
                OptionsData.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region GetOptions

        public void GetOptions()
        {
            using var cnn = _dbAccess.GetConnection();
            using var cmd = new SqlCommand("spGetOptions", cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                cnn.Open();
                using var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    OptionsData.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    OptionsData.POSPartnerID = dr.GetInt32(dr.GetOrdinal("POSPartnerID"));
                    OptionsData.AllTables = dr.GetBoolean(dr.GetOrdinal("AllTables"));
                    OptionsData.NrCopies = dr.GetInt32(dr.GetOrdinal("NrCopies"));
                    OptionsData.TerminCashAccount = dr["TerminCashAccount"]?.ToString();
                    OptionsData.EmployeeCashAccount = dr["EmployeeCashAccount"]?.ToString();
                    OptionsData.GetFirstDescription = dr.GetBoolean(dr.GetOrdinal("GetFirstDescription"));
                    OptionsData.EmployeeAdvanceAccount = dr["EmployeeAdvanceAccount"]?.ToString();
                    OptionsData.PayablesVATAccount = dr["PayablesVATAccount"]?.ToString();
                    OptionsData.SalesVATAccount = dr["SalesVATAccount"]?.ToString();
                    OptionsData.DUDPartner = dr.GetInt32(dr.GetOrdinal("DUDPartner"));
                    OptionsData.AkcizaPartner = dr["AkcizaPartner"]?.ToString();
                    OptionsData.VATPartner = dr["VATPartner"]?.ToString();
                    OptionsData.CashAccount = dr["CashAccount"]?.ToString();
                    OptionsData.POSVAT = dr.GetInt32(dr.GetOrdinal("POSVAT"));
                    OptionsData.PrintTranIDInFiscal = Convert.ToBoolean(dr["PrintTranIDInFiscal"]);
                    OptionsData.WorksWithRFID = dr.GetBoolean(dr.GetOrdinal("WorksWithRFID"));
                    OptionsData.UseOldItems = dr.GetBoolean(dr.GetOrdinal("UseOldItems"));
                    OptionsData.LogOffAfterPOS = dr.GetBoolean(dr.GetOrdinal("LogOffAfterPOS"));
                    OptionsData.ProposeVAT = dr.GetBoolean(dr.GetOrdinal("ProposeVAT"));
                    OptionsData.PayableAccount = dr["PayableAccount"]?.ToString();
                    OptionsData.ReceivableAccount = dr["ReceivableAccount"]?.ToString();
                    OptionsData.UseDoublePartners = Convert.ToBoolean(dr["UseDoublePartners"]);
                    OptionsData.UseRegularInvoice = Convert.ToBoolean(dr["UseRegularInvoice"]);
                    OptionsData.ShowTimeAtPOSInvoice = Convert.ToBoolean(dr["ShowTimeAtPOSInvoice"]);
                    OptionsData.AutomaticallyCalculatePrice = Convert.ToBoolean(dr["AutomaticallyCalculatePrice"]);
                    OptionsData.NumberOfCopy1 = dr.GetInt32(dr.GetOrdinal("NumberOfCopy1"));
                    OptionsData.ShowWages = Convert.ToBoolean(dr["ShowWages"]);
                    OptionsData.ShowFixedAssets = Convert.ToBoolean(dr["ShowFixedAssets"]);
                    OptionsData.ShowHotel = Convert.ToBoolean(dr["ShowHotel"]);
                    OptionsData.PrintFiscalInvoice = dr["PrintFiscalInvoice"] == DBNull.Value ? false : Convert.ToBoolean(dr["PrintFiscalInvoice"]);
                    OptionsData.PrintFiscalAlways = dr["PrintFiscalAlways"] == DBNull.Value ? false : Convert.ToBoolean(dr["PrintFiscalAlways"]);
                    OptionsData.TranNoType = dr["TranNoType"] == DBNull.Value ? 1 : Convert.ToInt32(dr["TranNoType"]);
                    OptionsData.GroupIncomeAccount = dr["GroupIncomeAccount"]?.ToString() ?? "";
                    OptionsData.GroupExpenseAccount = dr["GroupExpenseAccount"]?.ToString() ?? "";
                    OptionsData.GroupAssetAccount = dr["GroupAssetAccount"]?.ToString() ?? "";
                    OptionsData.PrintFiscalAfterEachOrder = dr["PrintFiscalAfterEachOrder"] == DBNull.Value ? false : Convert.ToBoolean(dr["PrintFiscalAfterEachOrder"]);
                    OptionsData.UseSubOrders = dr.GetBoolean(dr.GetOrdinal("UseSubOrders"));
                    OptionsData.UseDoubleNumberInSales = dr.GetBoolean(dr.GetOrdinal("UseDoubleNumberInSales"));
                    OptionsData.ShowPaletsInMobile = dr.GetBoolean(dr.GetOrdinal("DontShowPaletsInMobile"));
                    OptionsData.GjeneroNotenKreditore = dr.GetBoolean(dr.GetOrdinal("GjeneroNotenKreditore"));
                }
            }
            catch (Exception ex)
            {
                OptionsData.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region GetOptionsList (Mobile)

        public List<string> GetOptionsList()
        {
            var lstO = new List<string>();

            using var cnn = _dbAccess.GetConnection();
            using var cmd = new SqlCommand("sp_m_GetOptions", cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                cnn.Open();
                using var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lstO.Add(dr["POSPartnerID"].ToString());
                    lstO.Add(dr["POSVAT"].ToString());
                    lstO.Add(dr["EmployeeCashAccount"].ToString());
                    lstO.Add(dr["PartnerName"].ToString());
                    lstO.Add(dr["PrintFiscalInvoice"].ToString());
                    lstO.Add(dr["PrintFiscalAlways"].ToString());
                    lstO.Add(dr["HDepartmentID"].ToString());
                    lstO.Add(dr["UseSubOrders"].ToString());
                    lstO.Add(dr["ShowRoutes"].ToString());
                    lstO.Add(dr["UseDoubleNumberInSales"].ToString());
                    lstO.Add(dr["UseDoublePartners"].ToString());
                    lstO.Add(dr["DontShowPaletsInMobile"].ToString());
                    lstO.Add(dr["UseMinimumsForCategoryRegion"].ToString());
                    lstO.Add(dr["GjeneroNotenKreditore"].ToString());
                    lstO.Add(dr["GetNumbersOnlyFromServer"].ToString());
                    lstO.Add(dr["NAVIPAddress"].ToString());
                    lstO.Add(dr["ShowOnlyRealStock"].ToString());
                    lstO.Add(dr["UseBatchAssets"].ToString());
                    lstO.Add(dr["ForceVersionUpdate"].ToString());
                    lstO.Add(dr["DontAllowSalesOffline"].ToString());
                    lstO.Add(dr["UseProductionSerialNumbers"].ToString());
                    lstO.Add(dr["UseCreditLimitinOrder"].ToString());
                    lstO.Add(dr["NAVIPAddress2"].ToString());
                    lstO.Add(dr["SupportSubjectType"].ToString());
                    lstO.Add(dr["ShowTickets"].ToString());
                    lstO.Add(dr["MakeBL_IfNoFiscal"].ToString());
                    lstO.Add(dr["ExpenseAssetsWithDepartment"].ToString());
                    lstO.Add(dr["UseHotel_Mobile"].ToString());
                    lstO.Add(dr["RestaurantMasterGroup"].ToString());
                }
            }
            catch (Exception ex)
            {
                OptionsData.ErrorDescription = ex.Message;
            }

            return lstO;
        }

        #endregion

        #region GetOptionsShowSpecificMenus

        public void GetOptionsShowSpecificMenus()
        {
            using var cnn = _dbAccess.GetConnection();
            using var cmd = new SqlCommand("spOptionsShowSpecificMenus", cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                cnn.Open();
                using var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    OptionsData.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    OptionsData.ShowWages = dr.GetBoolean(dr.GetOrdinal("ShowWages"));
                    OptionsData.ShowFixedAssets = dr.GetBoolean(dr.GetOrdinal("ShowFixedAssets"));
                    OptionsData.ShowHotel = dr.GetBoolean(dr.GetOrdinal("ShowHotel"));
                }
            }
            catch (Exception ex)
            {
                OptionsData.ErrorDescription = ex.Message;
            }
        }

        #endregion

        #region GetPrintersForPOS

        public List<PrintersForPOS> GetPrintersForPOS()
        {
            var lp = new List<PrintersForPOS>();

            using var cnn = _dbAccess.GetConnection();
            using var cmd = new SqlCommand("spGetPrintersForPOS", cnn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                cnn.Open();
                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lp.Add(new PrintersForPOS
                    {
                        PrinterAlias = dr["PrinterAlias"]?.ToString(),
                        PrinterPath = dr["PrinterPath"]?.ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                OptionsData.ErrorDescription = ex.Message;
            }

            return lp;
        }

        #endregion
    }
}

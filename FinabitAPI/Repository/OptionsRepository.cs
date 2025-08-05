using Finabit_API.Models;
using System.Data;
using Microsoft.Data.SqlClient;


namespace AutoBit_WebInvoices.Models
{
    public class OptionsRepository
    {
        #region Update

            public void Update()
            {
                SqlConnection cnn = GlobalRepository.GetConnection();
                SqlCommand cmd = new SqlCommand("spOptionsUpdate", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param;

                param = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.ID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@POSPartnerID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.POSPartnerID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AllTables", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.AllTables;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@NrCopies", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.NrCopies;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TerminCashAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.TerminCashAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@EmployeeCashAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.EmployeeCashAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@GetFirstDescription", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.GetFirstDescription;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@EmployeeAdvanceAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.EmployeeAdvanceAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PayablesVATAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.PayablesVATAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@SalesVATAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.SalesVATAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CashAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.CashAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ItemPendingAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.ItemPendingAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DUDPartner", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.DUDPartner;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AkcizaPartner", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.AkcizaPartner;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@VATPartner", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.VATPartner;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@FINAPartnersAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.FINAPartnersAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@FeeAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.FeeAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AdvanceAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.AdvanceAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@POSVAT", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.POSVAT;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@WorksWithRFID", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.WorksWithRFID;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@UseOldItems", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.UseOldItems;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@LogOffAfterPOS", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.LogOffAfterPOS;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Printer1", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = "";
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Printer2", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = "";
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ProposeVAT", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.ProposeVAT;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PayableAccount", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.PayableAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ReceivableAccoun", System.Data.SqlDbType.VarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.ReceivableAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@UseDoublePartners", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.UseDoublePartners;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@UseRegularInvoice", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.UseRegularInvoice;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@ShowTimeAtPOSInvoice", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.ShowTimeAtPOSInvoice;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AutomaticallyCalculatePrice", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.AutomaticallyCalculatePrice;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PrintFiscalInvoice", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.PrintFiscalInvoice;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@PrintFiscalAlways", System.Data.SqlDbType.Bit);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.PrintFiscalAlways;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@TranNoType", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.TranNoType;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prmErrorID", System.Data.SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@GroupIncomeAccount", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.GroupIncomeAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@GroupExpenseAccount", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.GroupExpenseAccount;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@GroupAssetAccount", System.Data.SqlDbType.NVarChar);
                param.Direction = ParameterDirection.Input;
                param.Value = OptionsData.GroupAssetAccount;
                cmd.Parameters.Add(param);


                try
                {
                    cnn.Open();

                    cmd.ExecuteNonQuery();
                    OptionsData.ErrorID = Convert.ToInt32(cmd.Parameters["@prmErrorID"].Value);

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    OptionsData.ErrorID = -1;
                    OptionsData.ErrorDescription = ex.Message;
                    cnn.Close();
                }
            }

            #endregion

            #region GetOptions

            public static void GetOptions()
            {
                SqlConnection cnn = GlobalRepository.GetConnection();
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
                           // OptionsData.UseProductionSerialNumbers = Convert.ToBoolean(dr["UseProductionSerialNumbers"]);
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

            #endregion

            #region GetOptionsMobile

            public static List<string> GetOptionsList()
            {
                SqlConnection cnn = GlobalRepository.GetConnection();
                SqlCommand cmd = new SqlCommand("sp_m_GetOptions", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<string> lstO = new List<string>();
                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstO.Add(dr["POSPartnerID"].ToString());   //0
                            lstO.Add(dr["POSVAT"].ToString());   //1
                            lstO.Add(dr["EmployeeCashAccount"].ToString());  //2
                            lstO.Add(dr["PartnerName"].ToString());  //3
                            lstO.Add(dr["PrintFiscalInvoice"].ToString()); //4
                            lstO.Add(dr["PrintFiscalAlways"].ToString());  //5
                            lstO.Add(dr["HDepartmentID"].ToString()); //6
                            lstO.Add(dr["UseSubOrders"].ToString()); //7

                            lstO.Add(dr["ShowRoutes"].ToString()); //8
                            lstO.Add(dr["UseDoubleNumberInSales"].ToString());  //9
                            lstO.Add(dr["UseDoublePartners"].ToString());   //10
                            lstO.Add(dr["DontShowPaletsInMobile"].ToString()); //11
                            lstO.Add(dr["UseMinimumsForCategoryRegion"].ToString()); //12
                            lstO.Add(dr["GjeneroNotenKreditore"].ToString()); //13
                            lstO.Add(dr["GetNumbersOnlyFromServer"].ToString());//14
                            lstO.Add(dr["NAVIPAddress"].ToString());//15
                            lstO.Add(dr["ShowOnlyRealStock"].ToString());//16
                            lstO.Add(dr["UseBatchAssets"].ToString());//17
                            lstO.Add(dr["ForceVersionUpdate"].ToString());//18
                            lstO.Add(dr["DontAllowSalesOffline"].ToString());//19
                            lstO.Add(dr["UseProductionSerialNumbers"].ToString()); // 20
                            lstO.Add(dr["UseCreditLimitinOrder"].ToString()); // 21
                            lstO.Add(dr["NAVIPAddress2"].ToString()); // 22
                            lstO.Add(dr["SupportSubjectType"].ToString());//23
                            lstO.Add(dr["ShowTickets"].ToString());//24
                            lstO.Add(dr["MakeBL_IfNoFiscal"].ToString());//25
                            lstO.Add(dr["ExpenseAssetsWithDepartment"].ToString());//26
                            lstO.Add(dr["UseHotel_Mobile"].ToString()); // 27
                            lstO.Add(dr["RestaurantMasterGroup"].ToString()); //28
                                                                              //lstO.Add(dr["ID"].ToString());                        
                                                                              //lstO.Add(dr["AllTables"].ToString());
                                                                              //lstO.Add(dr["NrCopies"].ToString());
                                                                              //lstO.Add(dr["TerminCashAccount"].ToString());                        
                                                                              //lstO.Add(dr["GetFirstDescription"].ToString());
                                                                              //lstO.Add(dr["EmployeeAdvanceAccount"].ToString());
                                                                              //lstO.Add(dr["PayablesVATAccount"].ToString());
                                                                              //lstO.Add(dr["SalesVATAccount"].ToString());
                                                                              //lstO.Add(dr["DUDPartner"].ToString());
                                                                              //lstO.Add(dr["AkcizaPartner"].ToString());
                                                                              //lstO.Add(dr["VATPartner"].ToString());
                                                                              //lstO.Add(dr["CashAccount"].ToString());                        
                                                                              //lstO.Add(dr["WorksWithRFID"].ToString());
                                                                              //lstO.Add(dr["UseOldItems"].ToString());
                                                                              //lstO.Add(dr["LogOffAfterPOS"].ToString());
                                                                              //lstO.Add(dr["ProposeVAT"].ToString());
                                                                              //lstO.Add(dr["PayableAccount"].ToString());
                                                                              //lstO.Add(dr["ReceivableAccount"].ToString());
                                                                              //lstO.Add(dr["UseDoublePartners"].ToString());
                                                                              //lstO.Add(dr["UseRegularInvoice"].ToString());
                                                                              //lstO.Add(dr["ShowTimeAtPOSInvoice"].ToString());
                                                                              //lstO.Add(dr["AutomaticallyCalculatePrice"].ToString());
                                                                              //lstO.Add(dr["NumberOfCopy1"].ToString());
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

                return lstO;
            }

            #endregion

            #region GetOptionsShowSpecificMenus

            public static void GetOptionsShowSpecificMenus()
            {
                SqlConnection cnn = GlobalRepository.GetConnection();
                SqlCommand cmd = new SqlCommand("spOptionsShowSpecificMenus", cnn);
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
                            OptionsData.ShowWages = Convert.ToBoolean(dr["ShowWages"]);
                            OptionsData.ShowFixedAssets = Convert.ToBoolean(dr["ShowFixedAssets"]);
                            OptionsData.ShowHotel = Convert.ToBoolean(dr["ShowHotel"]);

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

            #endregion

            #region GetPrintersForPOS

            public static List<PrintersForPOS> GetPrintersForPOS()
            {
                SqlConnection cnn = GlobalRepository.GetConnection();
                SqlCommand cmd = new SqlCommand("spGetPrintersForPOS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                List<PrintersForPOS> lp = new List<PrintersForPOS>();

                try
                {
                    cnn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            PrintersForPOS p = new PrintersForPOS();
                            p.PrinterAlias = Convert.ToString(dr["PrinterAlias"]);
                            p.PrinterPath = Convert.ToString(dr["PrinterPath"]);
                            lp.Add(p);

                        }
                    }
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    string exp = ex.Message;
                    cnn.Close();
                }

                return lp;
            }

            #endregion

        }
    }

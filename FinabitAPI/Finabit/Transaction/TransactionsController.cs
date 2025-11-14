using System.Data;
using System.Text.RegularExpressions;
using FinabitAPI.Core.Global;
using FinabitAPI.Core.Global.dto;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Employee;
using FinabitAPI.Finabit.Employees.dto;
using FinabitAPI.Finabit.Items;
using FinabitAPI.Finabit.Items.dto;
using FinabitAPI.Finabit.Partner;
using FinabitAPI.Finabit.Partner.dto;
using FinabitAPI.Finabit.Transaction;
using FinabitAPI.Finabit.Transaction.dto;
using FinabitAPI.Service;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinabitAPI.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;
        private readonly EmployeesRepository _dalEmployee;
        private readonly DepartmentRepository _dalDepartment;
        private TransactionsService transactionsService => new TransactionsService(_dbAccess);

        public TransactionsController(
            IConfiguration configuration,
            DBAccess dbAccess,
            EmployeesRepository dalEmployee,
            DepartmentRepository dalDepartment
        )
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
            _dalEmployee = dalEmployee;
            _dalDepartment = dalDepartment;
        }

        //[Route("api/Items/LoadItems")]
        //[HttpGet]
        //public List<XMLItems> LoadItems(int DepartmentID)
        //{
        //    List<XMLItems> list = new List<XMLItems>();
        //    list = DBAccess.M_GetItemsStateList(DepartmentID);
        //    return list;
        //}

        [HttpPost("TransactionInsert")]
        public ActionResult<XMLTransactions> XMLTransaction_Insert([FromBody] XMLTransactions tt)
        {
            //return tt;

            Transactions t = GetTransaction(tt);
            //Users_GetLoginUserByPIN(t.Memo);
            //if (TranExistID > 0)
            //{
            //    //goto fundi;
            //    tt.ID = TranExistID;
            //    tt.Exists = true;
            //    return tt;

            //}
            //else
            //{

            Transactions TranCash = new Transactions();
            //Users user = new Users();

            Department Dep = new Department();
            Dep.ID = tt.DepartmentID;
            Dep = _dbAccess.SelectDepartmentByID(Dep);

            if (
                (
                    tt.PaymentValue != 0
                    && (
                        tt.TransactionType != 15
                        && tt.TransactionType != 10
                        && tt.TransactionType != 17
                    )
                )
            )
            {
                TranCash.ID = GetBankJournalID(tt.CashAccount, Dep.CompanyID);
                TranCash = TransactionsService.SelectByID(TranCash);

                // user.ID = tt.InsBy;
                //user = BLLUsers.SelectByID(user);
            }

            if (TransactionsService.CheckTransactionIfExists(t))
            {
                tt.IsSynchronized = true;
                tt.Exists = true;
                return tt;
            }
            else
            {
                //Users_GetLoginUserByPIN(t.ErrorDescription);
            }

            TransactionsService bllt = new TransactionsService(_dbAccess);
            try
            {
                //Users_GetLoginUserByPIN(t.Memo);
                t.ErrorID = bllt.Insert(t, false);
                //DataTable dt1 = BLLTransactions.GetDetailsFromDoc(t.ID);
                //if (dt1.Rows.Count == 0)
                //{
                //    t.ErrorID = -1;
                //    t.ErrorDescription = "Nuk ka Detaje te ruajtura! \nProvoni përsëri!";
                //    bllt.ErrorID = -1;
                //}

                if (bllt.ErrorID == 0)
                {
                    tt.IsSynchronized = true;
                    tt.TransactionNo = t.TransactionNo;
                    tt.ID = t.ID;
                    //DataTable dt = BLLPartner.M_GetPartnerCoords(tt.PartnerID);
                    //decimal Lat = 0;
                    //decimal Lon = 0;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    Lat = Convert.ToDecimal(dt.Rows[0]["Lat"]);
                    //    Lon = Convert.ToDecimal(dt.Rows[0]["Lon"]);
                    //}
                    //if (Lat == 0 && Lon == 0)
                    //{
                    //    BLLPartner.M_UpdatePartnerCoords(tt.PartnerID, tt.Latitude, tt.Longitude);
                    //}
                }
                //bllt.CloseGlobalConnection();
                //========================================================
                // PAYMENT
                if (
                    (
                        tt.PaymentValue != 0
                        && (
                            tt.TransactionType != 15
                            && tt.TransactionType != 10
                            && tt.TransactionType != 17
                        )
                    )
                    && bllt.ErrorID == 0
                )
                {
                    if (TranCash.ID != 0)
                    {
                        // ------------ Detail --------------
                        TransactionsDetails Detail = new TransactionsDetails();
                        Detail.TransactionID = TranCash.ID;
                        Detail.PaymentID = t.ID;

                        Detail.ID = 0;
                        Detail.DetailsType = 4;
                        Detail.ItemID = tt.PartnerID.ToString();
                        Detail.PartnerItremID = "";
                        //if (tt.BL && !OptionsData.UseDoubleNumberInSales && (tt.TransactionType == 2 || tt.TransactionType == 15))
                        //{
                        //    DataTable dt = BLLPartner.GenerateItemID_ForPartner(tt.PartnerID);

                        //    int PartnerItemID = CheckForInt(dt.Rows[0][0].ToString());

                        //    if (PartnerItemID > 0)
                        //    {
                        //        Detail.ItemID = OptionsData.POSPartnerID.ToString();
                        //        Detail.PartnerItremID = PartnerItemID + "";
                        //    }
                        //}

                        Detail.ItemName = ""; // user.UserName;
                        Detail.Quantity = 1;
                        decimal Input = tt.PaymentValue;
                        decimal OutPut = 0;
                        Detail.Price = Input + OutPut;
                        Detail.Value = Detail.Price;
                        Detail.Mode = 1;
                        Detail.IsPrintFiscalInvoice = tt.IsPrintFiscalInvoice;
                        Detail.CompanyID = Dep.CompanyID;

                        //-----------------------------------

                        TranCash.TranDetailsColl = new List<TransactionsDetails>();
                        TranCash.TranDetailsColl.Add(Detail);
                        TranCash.GenerateJournal = true;

                        try
                        {
                            t.ErrorID = bllt.Update(TranCash);
                        }
                        catch
                        {
                            bllt.ErrorID = -1;
                            t.ErrorID = -1;
                        }
                    }
                }
            }
            catch
            {
                bllt.ErrorID = -1;
                t.ErrorID = -1;
            }
            finally
            {
                bllt.CloseGlobalConnection();
            }
            //}
            //fundi:

            //if (tt.Vlera_NotaKreditore > 0 && tt.Llogaria_NotaKreditore != "")
            //{
            //    Users_GetLoginUserByPIN("Gjenero Noten Kreditore: " + OptionsData.GjeneroNotenKreditore);
            //    if (OptionsData.GjeneroNotenKreditore)
            //    {
            //        XMLTransaction_InsertNotaKreditore(tt, true);
            //    }
            //    else
            //    {
            //        XMLTransaction_InsertBlerjenNgaShitja(tt, true);
            //    }
            //}
            //try
            //{
            //    // PayTransaction(GetCompensationJournalID(), false,tt.ID,tt.ID,tt.Vlera_NotaKreditore);
            //}
            //catch { }
            tt.ErrorID = t.ErrorID;
            tt.ErrorDescription = t.ErrorDescription;
            if (tt.ErrorDescription != "")
            {
                try
                {
                    // WriteLog(tt.ErrorDescription);
                }
                catch { }
            }
            //tt.ErrorDescription = "Test shkruarja ne log";
            //try
            //{
            //    WriteLog(tt.ErrorDescription);
            //}fv
            //catch (Exception ex) { Users_GetLoginUserByPIN(ex.Message); }
            return tt;
        }

        private bool printTermik = false;

        [HttpPost("TransactionInsertWithPrint")]
        public ActionResult<Transactions> TransactionInsertWithPrint(
            [FromBody] TransactionRequest transaction
        )
        {
            Transactions t = transaction.t;
            TransactionsDetails detailsList = transaction.detailsList;
            List<ItemsWeb> lista = new List<ItemsWeb>();

            lista = _dbAccess.GetItemsWeb(1);

            foreach (var item in transaction.t.TranDetailsColl)
            {
                var matchedItem = lista.FirstOrDefault(x => x.ItemID == item.ItemID);

                // If a match is found, set the LocationId for the current item
                if (matchedItem != null)
                {
                    item.LocationID = matchedItem.LocationID;
                }
            }

            OptionsRepository optionsRepository = new OptionsRepository(_dbAccess);
            var printersForPOS = optionsRepository.GetPrintersForPOS();
            var pattern = @"^Printeri \d+$";
            var filteredPrinters = printersForPOS
                .Where(printer => Regex.IsMatch(printer.PrinterAlias, pattern))
                .ToList();

            printTermik = filteredPrinters.Count > 0;
            var fiscalPrinter = printersForPOS
                .Where(printer => printer.PrinterAlias.ToUpper().Contains("FISKAL"))
                .FirstOrDefault();

            bool PrintALL = transaction.PrintALL;
            bool isOrder = true;
            bool isSelectedPayment = transaction.isSelectedPayment;
            string POSUserName = transaction.POSUserName;
            bool PrintFiscal = transaction.PrintFiscal;
            string Temp = fiscalPrinter != null ? fiscalPrinter.PrinterPath : transaction.Temp;
            int CashJournalPOSID = transaction.CashJournalPOSID;
            WriteLog("Refernce: " + t.Reference);
            if (transaction.t.TransactionTypeID == 2 && !String.IsNullOrEmpty(t.Reference))
            {
                WriteLog("Termin " + t.Reference);
                Employees emp = new Employees();

                int emp1 = int.TryParse(_configuration["AppSettings:EmpNderrimi1"], out var tmpEmp1)
                    ? tmpEmp1
                    : 0;
                int emp2 = int.TryParse(_configuration["AppSettings:EmpNderrimi2"], out var tmpEmp2)
                    ? tmpEmp2
                    : 0;

                // Nderrimi i paradites
                if (t.Reference.Equals("1") && emp1 > 0)
                {
                    emp.EmpID = emp1; // 32;
                }
                // Nderrimi i pasdites
                else if (t.Reference.Equals("2") && emp2 > 0)
                {
                    emp.EmpID = emp2; // 21;
                }
                else
                {
                    emp.EmpID = emp1 > 0 ? emp1 : emp2;
                }

                emp = _dalEmployee.SelectByID(emp);

                string[] s = TerminsRepository.OpenedTerminID(emp.EmpID, t.DepartmentID);
                GlobalAppData.TermnID = s[0];
                GlobalAppData.CashJournalPOSID = CommonApp.CheckForInt(s[1]);

                if (GlobalAppData.TermnID.Equals(""))
                {
                    Termins ttermin = GetTermins(emp.EmpID, t.DepartmentID);
                    TerminsRepository blltermin = new TerminsRepository();

                    blltermin.Insert(ttermin);
                }
                s = TerminsRepository.OpenedTerminID(emp.EmpID, t.DepartmentID);
                GlobalAppData.TermnID = s[0];
                GlobalAppData.CashJournalPOSID = CommonApp.CheckForInt(s[1]);
            }

            //return t;
            //  Users_GetLoginUserByPIN("Useri:" + POSUserName);
            //List<string> strPr = new List<string>();
            //strPr.Add(transaction.printers);
            //strPr.Add(transaction.printers);
            //strPr.Add(transaction.printers);
            //strPr.Add(transaction.printers);
            //strPr.Add(transaction.printers);
            //strPr.Add(transaction.printers);
            string PosUSR = "";
            bool chkChecked = false;
            try
            {
                PosUSR = transaction.POSUserName.Split('?')[0];
            }
            catch
            {
                PosUSR = transaction.POSUserName;
            }
            try
            {
                chkChecked = Convert.ToBoolean(transaction.POSUserName.Split('?')[1]);
            }
            catch
            {
                chkChecked = false;
            }
            string[] pr = filteredPrinters
                .Select(printer => printer.PrinterPath) // Get the 'Value' field from each printer object
                .ToArray();
            //pr = strPr.ToArray();

            bool CloseGlobal = true;
            Transactions TranCash = new Transactions();
            if (transaction.t.POSPaid)
            {
                TranCash.ID = transaction.CashJournalPOSID;
                if (TranCash.ID != 0)
                {
                    TranCash = TransactionsService.SelectByID(TranCash);
                }
            }
            string TableName = "table"; // BLLTables.SelectByID(new BELayer.Tables() { ID = t.TableID }).TableName;
            DataTable SDTable = GlobalRepository.ListSystemDataTable(_dbAccess);

            ItemLocationRepository itemLocationRepository = new ItemLocationRepository(_dbAccess);
            List<ItemLocation> ILList = itemLocationRepository.SelectAll();
            // Determine the effective date: use provided TransactionDate if set, otherwise today
            var effectiveDate =
                (t.TransactionDate == DateTime.MinValue)
                    ? DateTime.Now.Date
                    : t.TransactionDate.Date;
            bool GenerateTranNo = Convert.ToBoolean(SDTable.Rows[0]["GenerateTransactionNo"]);
            if (GenerateTranNo || String.IsNullOrEmpty(transaction.t.TransactionNo))
            {
                transaction.t.TransactionNo = transactionsService.GetTransactionNo(
                    transaction.t.TransactionTypeID,
                    effectiveDate,
                    t.DepartmentID
                );
            }

            transaction.t.InvoiceNo = transaction.t.TransactionNo;

            // dgj 18122011 merret nga serveri direkt data e transaskasionit

            // Use the effective date for all transaction date fields
            t.TransactionDate = effectiveDate;
            t.InvoiceDate = effectiveDate;
            t.DueDate = effectiveDate;
            t.TerminID = CommonApp.CheckForInt(GlobalAppData.TermnID);
            // Fix for CS0029: Convert ActionResult<List<string>> to List<string> by accessing the Value property
            List<string> o = GetOptionsList().Value;
            int PartID = t.PartnerID;
            int OptionsPartner = CommonApp.CheckForInt(o[0]);
            // int.TryParse(o[0], out PartID);

            Partner P;
            // Gjetja e Partnerit me Email
            if (!String.IsNullOrEmpty(t.PartnerEmail))
            {
                int part = PartnerRepository.SelectByEmail(t.PartnerEmail);
                if (part != 0)
                {
                    PartID = part;
                }
            }
            // Gjetja e Partnerit me Emer Mbiemer
            if (PartID == 0 && t.PartnerName != "")
            {
                int part = PartnerRepository.SelectByName(t.PartnerName);
                if (part != 0)
                {
                    PartID = part;
                }
            }

            Partner newPartner = new Partner();
            P = PartnerRepository.SelectByID(
                new Partner { ID = t.PartnerID == 0 ? PartID : t.PartnerID }
            );

            if (P.ID == 0 && !String.IsNullOrEmpty(t.PartnerName))
            {
                newPartner.Email = t.PartnerEmail;
                newPartner.PartnerName = t.PartnerName;
                newPartner.Tel1 = t.PartnersPhoneNo;
                PartnersType type = new PartnersType();
                type.ID = 2;
                newPartner.PartnerType = type;
                PartnerRepository dalPartner = new PartnerRepository(_dbAccess);
                dalPartner.Insert(newPartner);
            }

            t.PartnerName = P.PartnerName;
            t.PartnerID = P.ID > 0 ? P.ID : newPartner.ID;
            if (t.PartnerID == PartID)
            {
                t.PartnerName = "";
            }

            GetOptions();

            // Kontrollimi i detajeve

            t.TranDetailsColl = GetTranDetails(transaction.t);

            var Totals = GetRowsTotalValue(t.TranDetailsColl);
            t.RABAT = Totals[2];
            t.Value = Totals[0];
            t.VATValue = Totals[1];
            t.AllValue = t.VATValue + (t.Value);

            TransactionsService bllt = new TransactionsService(_dbAccess);
            try
            {
                t.ErrorID = bllt.Insert(t, false);
                if (bllt.ErrorID == 0)
                {
                    if (!t.POSPaid || TranCash.ID == 0)
                    {
                        try
                        {
                            //ThreadPool.QueueUserWorkItem(delegate
                            //{
                            if (!OptionsData.UseSubOrders)
                            {
                                //if (PrintALL)
                                //{
                                //    //Users_GetLoginUserByPIN("Printimi Total");
                                //    PrintInvoiceTotal(t, isSelectedPayment, 1, PosUSR, pr, TableName, SDTable, ILList, chkChecked);
                                //}
                                //Users_GetLoginUserByPIN("Printimi");
                                //PrintInvoice(t, isSelectedPayment, 1, PosUSR, pr, TableName, SDTable, ILList, chkChecked, PrintALL);
                                //WriteLog("U kry printimi");
                            }

                            if (OptionsData.PrintFiscalAfterEachOrder && PrintFiscal)
                            {
                                //Users_GetLoginUserByPIN("Hyn per fiskal");
                                bllt.CloseGlobalConnection();
                                CloseGlobal = false;

                                //PrintFiscalInvoice(t.ID, Temp, PosUSR, isOrder, t.CardBarcode);
                                ////Users_GetLoginUserByPIN("Kryhet printimi ne fiskal");
                            }
                            //});
                        }
                        catch (Exception ex)
                        {
                            //Users_GetLoginUserByPIN("Ka error ");
                            bllt.ErrorID = -1;
                            t.ErrorID = -1;
                            try
                            {
                                WriteLog(ex.Message);
                            }
                            catch { }
                        }
                        finally
                        {
                            bllt.CloseGlobalConnection();

                            //Users_GetLoginUserByPIN("E mbyll Koneksionin");
                        }
                        //try
                        //{
                        //    PrintInvoice(t, isSelectedPayment, 1, PosUSR, pr, TableName, SDTable, ILList,chkChecked);
                        //}
                        //catch
                        //{

                        //}
                    }

                    #region LoyaltyCard
                    if (t.CardID != 0 && t.TransactionTypeID == 2)
                    {
                        decimal LoyaltyRedeemValue = 0;

                        WriteLog("Hyn marrjen e detajeve per Loyalty Card");
                        //dtItemsForLoyaltyCard = new DataTable();
                        //dtItems = ToDataTable(t.TranDetailsColl);
                        //// Logjika per insertim/shfrytezim te kuponave
                        //try
                        //{
                        //    DataColumn cl = new DataColumn("ItemID", typeof(string));
                        //    dtItemsForLoyaltyCard.Columns.Add(cl);

                        //    cl = new DataColumn("Quantity", typeof(decimal));
                        //    dtItemsForLoyaltyCard.Columns.Add(cl);

                        //    cl = new DataColumn("Price", typeof(decimal));
                        //    dtItemsForLoyaltyCard.Columns.Add(cl);

                        //    cl = new DataColumn("Points", typeof(decimal));
                        //    dtItemsForLoyaltyCard.Columns.Add(cl);
                        //}
                        //catch { }
                        //try
                        //{
                        //    foreach (DataRow dr in dtItems.Rows)
                        //    {
                        //        DataRow drNew = dtItemsForLoyaltyCard.NewRow();
                        //        drNew["ItemID"] = dr["ItemID"];
                        //        drNew["Quantity"] = dr["Quantity"];
                        //        drNew["Price"] = dr["VATPrice"];

                        //        drNew["Points"] = dr["Points"];

                        //        dtItemsForLoyaltyCard.Rows.Add(drNew);

                        //        if (CheckForDecimal(dr["Points"].ToString()) > 0)
                        //        {
                        //            LoyaltyRedeemValue += CheckForDecimal(dr["Points"].ToString());
                        //        }
                        //    }
                        //}
                        //catch (Exception ex)
                        //{

                        //    string str = ex.Message;
                        //   WriteLog(str);
                        //}
                        //Users_GetLoginUserByPIN("Nr i rreshtave " + dtItemsForLoyaltyCard.Rows.Count);
                        //if (dtItemsForLoyaltyCard.Rows.Count > 0 && t.POSPaid)
                        //{
                        //    Users_GetLoginUserByPIN("Hyn ne insertim te analitikes se pikeve");
                        //    BLLLoyaltyCardsAnalytics.InsertAnalytics(t.DepartmentID, t.TransactionDate, dtItemsForLoyaltyCard, t.CardBarcode, t.ID, POSUserName);
                        //    //JaneInsertuarPiket = true;
                        //}
                        //if (t.RedeemPoints && LoyaltyRedeemValue > 0 && t.POSPaid)
                        //{
                        //    Users_GetLoginUserByPIN("Hyn ne insertim te redeem te pikeve");

                        //    BLLLoyaltyCardsAnalytics.LoyaltyCardsAnalyticsInsert_RedeemValue(t.DepartmentID, t.TransactionDate, dtItemsForLoyaltyCard, t.CardBarcode, LoyaltyRedeemValue, t.ID);
                        //}
                    }
                    #endregion
                }
                //========================================================
                // PAYMENT
                if (t.POSPaid && bllt.ErrorID == 0)
                {
                    if (t.TransactionTypeID == 2)
                    {
                        //21 Pasdite
                        //32 Paradite
                        try
                        {
                            PayTransaction(t.AllValue, t.ID);
                        }
                        catch (Exception ex)
                        {
                            WriteLog("PayTransactions:" + ex.Message);
                        }
                    }

                    if (TranCash.ID != 0)
                    {
                        detailsList.TransactionID = TranCash.ID;
                        detailsList.PaymentID = t.ID;

                        TranCash.TranDetailsColl = new List<TransactionsDetails>();
                        TranCash.TranDetailsColl.Add(detailsList);
                        //Users_GetLoginUserByPIN("Pagesa : ");
                        t.ErrorID = bllt.Update(TranCash);
                        try
                        {
                            if (bllt.ErrorID == 0)
                            {
                                //ThreadPool.QueueUserWorkItem(delegate
                                //{
                                //if (!OptionsData.UseSubOrders)
                                //{
                                //    PrintInvoice(t, isSelectedPayment, 1, PosUSR, pr, TableName, SDTable, ILList, chkChecked, PrintALL);
                                //}
                                //});
                            }
                        }
                        catch
                        {
                            bllt.ErrorID = -1;
                            t.ErrorID = -1;
                        }

                        if (t.ErrorID == 0)
                        {
                            try
                            {
                                //if (PrintFiscal)   //dgj 18122011 perkohsisht per prince me js mundesu mis htyp krejt
                                //{
                                //    bllt.CloseGlobalConnection();
                                //    CloseGlobal = false;

                                //    PrintFiscalInvoice(t.ID, Temp, PosUSR, isOrder, t.CardBarcode);
                                //}
                            }
                            catch { }
                            finally
                            {
                                bllt.CloseGlobalConnection();
                            }
                        }
                    }
                }
                //========================================================
            }
            catch (Exception eee)
            {
                t.ErrorID = -1;
                bllt.ErrorID = -1;
                t.ErrorDescription = eee.Message;
                //Users_GetLoginUserByPIN("KA error 2 "+eee.Message);
            }
            finally
            {
                if (CloseGlobal)
                    bllt.CloseGlobalConnection();

                bllt = null;
            }
            try
            {
                if (OptionsData.UseSubOrders)
                {
                    //PrintInvoice(t, isSelectedPayment, 1, PosUSR, pr, TableName, SDTable, ILList, chkChecked, PrintALL);
                }
            }
            catch
            {
                bllt.CloseGlobalConnection();
            }
            if (t.ErrorDescription != "")
            {
                try
                {
                    WriteLog(t.ErrorDescription);
                    try
                    {
                        if (bllt != null)
                            bllt.CloseGlobalConnection();
                    }
                    catch (Exception ex)
                    { /*Users_GetLoginUserByPIN(ex.Message);*/
                    }
                }
                catch { }
            }
            //Users_GetLoginUserByPIN("Rreshtat e detajeve: "+t.Details.Count);
            return t;
        }

        [HttpGet("UpdateTransactionDriver")]
        public ActionResult<bool> UpdateTransactionDriver(
            [FromQuery] int TransactionID,
            [FromQuery] int EmpID
        )
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.UpdateDriverInTransaction(TransactionID, EmpID);
        }

        [HttpGet("GetOptionsList")]
        public ActionResult<List<string>> GetOptionsList()
        {
            OptionsRepository optionsRepository = new OptionsRepository(_dbAccess);
            return optionsRepository.GetOptionsList();
        }

        [HttpGet("OrdersList")]
        public ActionResult<List<Orders>> OrdersList(
            [FromQuery] string FromDate,
            [FromQuery] string ToDate
        )
        {
            return GetOrders(FromDate, ToDate);
        }

        [HttpPost("clone")]
        public async Task<ActionResult<Transactions>> CloneWithProc(
            CloneTransactionRequest body,
            CancellationToken ct
        )
        {
            if (body == null || body.TransactionId <= 0)
                return BadRequest("transactionId and newDate are required.");

            var src = TransactionsService.SelectByID(new Transactions { ID = body.TransactionId });
            if (src == null || src.ID == 0)
                return NotFound($"Source transaction {body.TransactionId} not found.");

            var newTransactionNo = _dbAccess
                .GetTransactionNo(src.TransactionTypeID, body.NewDate, src.DepartmentID)
                .ToString();

            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                return StatusCode(401, "UserId not available.");

            int newId;
            try
            {
                newId = await _dbAccess.CloneTransactionExactAsync(
                    body.TransactionId,
                    body.NewDate,
                    newTransactionNo,
                    body.Memo,
                    userId,
                    ct
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Clone failed: {ex.Message}");
            }

            var cloned = TransactionsService.SelectByID(new Transactions { ID = newId });
            if (cloned == null || cloned.ID == 0)
                return StatusCode(
                    500,
                    $"Cloned transaction {newId} inserted, but could not be loaded."
                );

            return Ok(cloned);
        }

        [HttpGet("TransactionsList")]
        public async Task<ActionResult<List<Orders>>> TransactionsList(
            [FromQuery] string FromDate,
            [FromQuery] string ToDate,
            [FromQuery] int TransactionTypeID,
            [FromQuery] string ItemID = null,
            [FromQuery] string ItemName = null,
            [FromQuery] string PartnerName = null,
            [FromQuery] string LocationName = null,
            CancellationToken ct = default
        )
        {
            var rows = await GetTransactionsAsync(
                FromDate,
                ToDate,
                TransactionTypeID,
                ItemID,
                ItemName,
                PartnerName,
                LocationName,
                ct
            );
            return Ok(rows);
        }

        private void PayTransaction(decimal PaymentValue, int id)
        {
            Transactions TranCash = new Transactions();
            TranCash.ID = GlobalAppData.CashJournalPOSID;

            if (TranCash.ID == 0)
            {
                return;
            }

            TranCash = TransactionsService.SelectByID(TranCash);

            Transactions ThisTran = new Transactions();
            ThisTran.ID = id;
            ThisTran = TransactionsService.SelectByID(ThisTran);

            TranCash.TranDetailsColl = GetDetailsForPayment(ThisTran, TranCash.ID, PaymentValue);
            TranCash.InsBy = 1;
            TransactionsService bllt = new TransactionsService(_dbAccess);
            bllt.Update(TranCash);
            if (bllt.ErrorID == 0)
            {
                bllt.CloseGlobalConnection();
                //this.DialogResult = DialogResult.OK;
            }
            else
            {
                bllt.CloseGlobalConnection();
            }
        }

        private List<TransactionsDetails> GetDetailsForPayment(
            Transactions ThisTran,
            int TranID,
            decimal PaymentValue
        )
        {
            List<TransactionsDetails> tranDetails = new List<TransactionsDetails>();
            TransactionsDetails clsDet;

            clsDet = new TransactionsDetails();
            clsDet.ID = 0;
            if (2 == 2)
            {
                clsDet.DetailsType = 4;
            }
            else
            {
                clsDet.DetailsType = 4;
            }
            clsDet.TransactionID = TranID;
            clsDet.ItemID = "Pagese Porosi Web"; // CostumerID.ToString();
            //Users user = new Users();
            //user.ID = UserID;
            //user = BLLUsers.SelectByID(user);
            clsDet.ItemName = "Pagese Porosi Web";
            clsDet.PaymentID = ThisTran.ID;
            clsDet.Quantity = 1;
            decimal Input = 0;
            if (2 == 14)
                Input = -PaymentValue;
            else
                Input = PaymentValue;

            // CommonApp.CheckForDecimal(uteTotal.Text);
            decimal OutPut = 0;
            clsDet.Price = Input + OutPut;
            clsDet.Value = clsDet.Price;
            clsDet.Mode = 1;
            if (ThisTran.PaymentTypeID == 1)
            {
                clsDet.IsPrintFiscalInvoice = false;
            }
            tranDetails.Add(clsDet);
            // clsDet.CompanyID = CompanyID;

            return tranDetails;
        }

        private Transactions GetCashTransaction(int EmpID, int departmentID)
        {
            Transactions cls = new Transactions();
            cls.ID = 0;
            //-----------------------------------------------------------------------------
            Employees e = new Employees();
            e.EmpID = EmpID;
            e = _dalEmployee.SelectByID(e);
            //-----------------------------------------------------------------------------
            Department dep = new Department();
            dep.ID = departmentID;
            // Update the following line to use an instance of DALDepartment instead of calling the method statically.
            dep = _dalDepartment.SelectByID(dep.ID);
            //-----------------------------------------------------------------------------

            cls.EmpID = EmpID;
            cls.DepartmentID = departmentID;
            cls.CompanyID = dep.CompanyID; // Kompania e cila meren nga depo lokale
            //-----------------------------------------------------------------------------

            cls.InvoiceNo =
                DateTime.Now.ToString("ddMMyyy_HH")
                + "_"
                + e.FirstName
                + " "
                + e.LastName
                + " - "
                + dep.DepartmentName;
            cls.TransactionNo =
                DateTime.Now.ToString("ddMMyyy_HH")
                + "_"
                + e.FirstName
                + " "
                + e.LastName
                + " - "
                + dep.DepartmentName;
            cls.TransactionTypeID = 25;
            cls.InvoiceDate = DateTime.Now;
            cls.TransactionDate = DateTime.Now;
            cls.DueDate = DateTime.Now;
            cls.Value = 0;
            cls.AllValue = 0;
            cls.PaidValue = 0;
            cls.Active = true;
            cls.Reference = "";
            cls.Links = "";
            cls.Memo = "";
            // cls.VersionNo = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            cls.PaidValue = 0;

            string CashAccount = e.CashAccount;
            if (CashAccount == "")
            {
                CashAccount = OptionsData.EmployeeCashAccount;
            }
            cls.CashAccount = CashAccount;
            cls.InsBy = 1;

            cls.VATPercentID = 0;

            return cls;
        }

        private Termins GetTermins(int EmpID, int DepartmentID)
        {
            Termins t = new Termins();
            //t.ID = int.Parse(GlobalAppData.TermnID);
            if (1 == 1)
            {
                t.Status = 0;
            }
            else
            {
                t.Status = 1;
            }
            t.CashJournalPOSID = NewCashTransaction(EmpID, DepartmentID);
            t.EmployeeID = EmpID;
            t.DepartmentID = DepartmentID;

            //  t.DevicesColl = DeviceColl;

            return t;
        }

        private int NewCashTransaction(int empid, int departmentid)
        {
            int CashJournalPOSID = 0;
            Transactions t = GetCashTransaction(empid, departmentid);
            TransactionsService bllt = new TransactionsService(_dbAccess);
            bllt.Insert(t, false);
            if (bllt.ErrorID == 0)
            {
                bllt.CloseGlobalConnection();
                CashJournalPOSID = t.ID;
            }
            else
            {
                bllt.CloseGlobalConnection();
            }
            return CashJournalPOSID;
        }

        [HttpPost("OrdersInsert")]
        public ActionResult<int> OrdersInsert([FromBody] List<Orders_Tecmotion> cls)
        {
            int countInserted = 0;

            var requestBody = JsonConvert.SerializeObject(cls);
            CommonApp.WriteLog(requestBody);

            DataTable ord = CreateTableForInsert(cls);
            countInserted = TransactionsService.OrdersBatchInsert(ord);
            return countInserted;
        }

        private DataTable CreateTableForInsert(List<Orders_Tecmotion> cls)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Orders_API";
            dt.Columns.Add("Lokacioni", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Email_Konsumatori", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Data", typeof(DateTime));
            dt.AcceptChanges();
            dt.Columns.Add("Shifra", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Shifra_prodhuesit", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Sasia", typeof(decimal));
            dt.AcceptChanges();
            dt.Columns.Add("Cmimi", typeof(decimal));
            dt.AcceptChanges();

            foreach (var order in cls)
            {
                foreach (var details in order.Detajet)
                {
                    DataRow row = dt.NewRow();
                    row[0] = order.Lokacioni;
                    row[1] = order.Email_konsumatori;
                    row[2] = order.Data;
                    row[3] = details.Shifra;
                    row[4] = details.Shifra_prodhuesit;
                    row[5] = details.Sasia;
                    row[6] = details.Cmimi;
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        [HttpGet("CustomersList")]
        public DataTable CustomersList()
        {
            return GetCustomers();
        }

        private DataTable GetCustomers()
        {
            return _dbAccess.CustomersList(2, 0);
        }

        [HttpGet("DepartmentsList")]
        public IActionResult DepartmentsList()
        {
            var dt = _dbAccess.DepartmentsList();
            var rows = ToTableRows(dt); // see method above
            return Ok(rows); // This is a "datatable-like" structure, but JSON-safe!
        }

        [HttpGet("DepartmentsListByName")]
        public IActionResult DepartmentsListByName([FromQuery] string name = null)
        {
            var dt = _dbAccess.DepartmentsListByName(name);
            var rows = ToTableRows(dt);
            return Ok(rows);
        }

        private Employees ResolveShiftEmployee(string? reference)
        {
            int emp1 = int.TryParse(_configuration["AppSettings:EmpNderrimi1"], out var e1)
                ? e1
                : 0;
            int emp2 = int.TryParse(_configuration["AppSettings:EmpNderrimi2"], out var e2)
                ? e2
                : 0;

            // support numeric EmpID directly (besides shift "1"/"2")
            if (
                !string.IsNullOrWhiteSpace(reference)
                && int.TryParse(reference.Trim(), out var asEmpId)
                && asEmpId > 0
                && reference.Trim() != "1"
                && reference.Trim() != "2"
            )
            {
                return _dalEmployee.SelectByID(new Employees { EmpID = asEmpId });
            }

            int pick = 0;
            if (!string.IsNullOrWhiteSpace(reference) && reference.Trim() == "1" && emp1 > 0)
                pick = emp1;
            else if (!string.IsNullOrWhiteSpace(reference) && reference.Trim() == "2" && emp2 > 0)
                pick = emp2;
            else
                pick = emp1 > 0 ? emp1 : emp2;

            if (pick <= 0)
                throw new InvalidOperationException(
                    "No shift employee configured (EmpNderrimi1/2)."
                );

            return _dalEmployee.SelectByID(new Employees { EmpID = pick }); // <-- use pick, not int.Parse(reference)
        }

        private (string terminId, int cashJournalId) EnsureTerminCashHeader(
            int departmentID,
            string? reference,
            int empIdOverride,
            DateTime date,
            string cashAccount,
            int journalTypeId
        )
        {
            var wantToday = date.Date == DateTime.Now.Date;

            // === Path 1: TODAY -> keep your existing Termin logic ===
            if (wantToday)
            {
                var s = TerminsRepository.OpenedTerminID(empIdOverride, departmentID);
                string terminId = (s != null && s.Length > 0) ? s[0] : "";
                int cashJournalId = (s != null && s.Length > 1) ? CommonApp.CheckForInt(s[1]) : 0;

                bool needNew = true;

                if (!string.IsNullOrEmpty(terminId) && cashJournalId > 0)
                {
                    var hdr = TransactionsService.SelectByID(
                        new Transactions { ID = cashJournalId }
                    );
                    if (
                        hdr != null
                        && hdr.ID > 0
                        && hdr.TransactionTypeID == journalTypeId
                        && hdr.DepartmentID == departmentID
                        && (hdr.CashAccount ?? "").Trim() == (cashAccount ?? "").Trim()
                        && hdr.TransactionDate.Date == date.Date
                    )
                    {
                        needNew = false;
                    }
                }

                if (needNew)
                {
                    var ttermin = GetTermins(empIdOverride, departmentID);
                    var blltermin = new TerminsRepository();
                    blltermin.Insert(ttermin);

                    var s2 = TerminsRepository.OpenedTerminID(empIdOverride, departmentID);
                    terminId = (s2 != null && s2.Length > 0) ? s2[0] : "";
                    cashJournalId =
                        (s2 != null && s2.Length > 1) ? CommonApp.CheckForInt(s2[1]) : 0;
                }

                GlobalAppData.TermnID = terminId;
                GlobalAppData.CashJournalPOSID = cashJournalId;
                return (terminId, cashJournalId);
            }

            // === Path 2: NOT TODAY -> bypass Termin; find or create header for (date, account, dept, type) ===
            int companyID = _dalDepartment.SelectByID(departmentID).CompanyID;
            int headerId = _dbAccess.CashJournalIDByCashAccount(
                cashAccount,
                journalTypeId,
                date,
                companyID
            ); // make sure this overload takes 'date'
            if (headerId == 0)
            {
                headerId = NewCashTransaction(empIdOverride, departmentID);
            }

            // we don't have a Termin for back-dated headers; return empty terminId
            return ("", headerId);
        }

        [HttpPost("FilesImport")]
        [RequestSizeLimit(1024L * 1024L * 200L)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ContractsDocsImport(
            [FromForm] ImportContractsDocsForm form
        )
        {
            if (form.Files == null || form.Files.Count == 0)
                return BadRequest("No files uploaded.");

            if (form.DocTypes != null && form.DocTypes.Count != form.Files.Count)
                return BadRequest("DocTypes count must match Files count.");
            if (form.Descriptions != null && form.Descriptions.Count != form.Files.Count)
                return BadRequest("Descriptions count must match Files count.");

            // Build the TVP DataTable to match dbo.ContractsDocs
            var dt = new DataTable();
            dt.Columns.Add("DossierID", typeof(int));
            dt.Columns.Add("Images", typeof(byte[]));
            dt.Columns.Add("DocType", typeof(int));
            dt.Columns.Add("ExtensionPath", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Type", typeof(int));

            for (int i = 0; i < form.Files.Count; i++)
            {
                var f = form.Files[i];
                if (f?.Length <= 0)
                    continue;

                using var ms = new MemoryStream();
                await f.CopyToAsync(ms);
                var bytes = ms.ToArray();

                var ext = Path.GetExtension(f.FileName)?.ToLowerInvariant() ?? "";
                var desc =
                    (
                        form.Descriptions != null
                        && i < form.Descriptions.Count
                        && !string.IsNullOrWhiteSpace(form.Descriptions[i])
                    )
                        ? form.Descriptions[i]
                        : f.FileName;

                var docType =
                    (form.DocTypes != null && i < form.DocTypes.Count)
                        ? form.DocTypes[i]
                        : (form.DocType ?? 0);

                var row = dt.NewRow();
                row["DossierID"] = form.DossierId;
                row["Images"] = bytes;
                row["DocType"] = docType;
                row["ExtensionPath"] = ext;
                row["Description"] = desc;
                row["Type"] = 1;
                dt.Rows.Add(row);
            }

            // Call repository (no userId passed from the form; repo defaults to -1)
            _dbAccess.ContractsDocsInsert(
                dt,
                form.ScanDocMode,
                form.DocNo,
                form.DocDate,
                form.SubjectName
            );

            return Ok(
                new
                {
                    inserted = dt.Rows.Count,
                    dossierId = form.DossierId,
                    mode = form.ScanDocMode,
                }
            );
        }

        [HttpGet("DistinctLast300")]
        public ActionResult<List<TransactionDetailDistinctDto>> GetDistinctLast300()
        {
            try
            {
                var rows = TransactionsDetailsRepository.GetLast300DistinctDetails();
                return Ok(rows);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, message = ex.Message });
            }
        }

        private List<Dictionary<string, object>> ToTableRows(DataTable dt)
        {
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                    dict[col.ColumnName] = dr[col];
                rows.Add(dict);
            }
            return rows;
        }

        private List<Orders> GetOrders(string FromDate, string ToDate)
        {
            return _dbAccess.OrdersList(FromDate, ToDate);
        }

        private Task<List<Orders>> GetTransactionsAsync(
            string fromDate,
            string toDate,
            int tranTypeID,
            string itemID = null,
            string itemName = null,
            string partnerName = null,
            string locationName = null,
            CancellationToken ct = default
        )
        {
            return _dbAccess.GetTransactions(
                fromDate,
                toDate,
                tranTypeID,
                itemID,
                itemName,
                partnerName,
                locationName,
                ct
            );
        }

        private Transactions GetTransaction(XMLTransactions t)
        {
            _dbAccess.GetOptions();

            Transactions cls = new Transactions();

            Department dep = new Department();
            dep.ID = t.DepartmentID;
            dep = _dbAccess.SelectDepartmentByID(dep);

            //Users u = new Users();

            //u.ID = t.InsBy;
            //u = BLLUsers.SelectByID(u);
            DataTable SDTable = _dbAccess.ListSystemDataTable();

            bool GenerateTranNo = Convert.ToBoolean(SDTable.Rows[0]["GenerateTransactionNo"]);
            //if (!GenerateTranNo)
            //{
            //    //Users_GetLoginUserByPIN("Hin ketu");
            //    //Users_GetLoginUserByPIN(t.TransactionNo);
            //    //Users_GetLoginUserByPIN(t.PartnerID.ToString());
            //    //Users_GetLoginUserByPIN(t.TransactionDate.ToString("yyy-MM-dd"));
            //    try
            //    {
            //        TranExistID = BLLTransactions.GetTransactionIDIfExist(t.TransactionNo, t.PartnerID, t.TransactionDate.ToString("yyy-MM-dd"));
            //    }
            //    catch { }
            //}
            //else
            //{
            //    TranExistID = 0;
            //}
            // Users_GetLoginUserByPIN("Kalon");
            //Employees e = new Employees();
            //e.ID = u.Employee.ID;
            //e = BLLEmployees.SelectByID(e);

            cls.TransactionTypeID = t.TransactionType;
            cls.DepartmentID = t.DepartmentID;
            cls.Links = "";

            if (t.TransactionType == 34)
            {
                cls.ItemID = t.AssetID.ToString();
                cls.Memo = t.TransactionNo;
                t.TransactionNo = "";
            }
            else
            {
                cls.Memo = t.Memo;
            }

            if (cls.TransactionTypeID == 25 || cls.TransactionTypeID == 24)
            {
                cls.TransactionDate = DateTime.Now.Date;
            }
            else
            {
                cls.TransactionDate = t.TransactionDate.Date;
            }

            cls.IsPriceFromPartner = t.IsPriceFromPartner;
            cls.InvoiceDate = t.TransactionDate.Date;
            cls.DueDate = t.TransactionDate.AddDays(t.DueDays).Date;
            cls.Reference = "";

            cls.VAT = true;
            cls.InPL = false;
            cls.CashAccount = "";

            //if (cls.TransactionTypeID == 25)
            //{
            //    if (GenerateTranNo || t.TransactionNo == "")
            //    {
            //        cls.TransactionNo = DateTime.Now.ToString("ddMMyyy_HH") + "_" + e.FirstName + " " + e.LastName + " - " + dep.DepartmentName;
            //    }
            //    else
            //    {
            //        cls.TransactionNo = t.TransactionNo;
            //    }
            //    //cls.CashAccount = e.CashAccount;
            //}
            if (t.IsSelectedPayment)
            {
                cls.CashAccount = t.CashAccount; // perkohesisht e lun rolin e CashAccount per banke
                if (GenerateTranNo || t.TransactionNo == "")
                {
                    cls.TransactionNo =
                        DateTime.Now.ToString("ddMMyyy_HH") + " - " + dep.DepartmentName;
                }
                else
                {
                    cls.TransactionNo = t.TransactionNo;
                }
            }
            //}
            //else
            //{
            if (GenerateTranNo || t.TransactionNo == "" || t.TransactionNo == null)
            {
                cls.TransactionNo = _dbAccess
                    .GetTransactionNo(t.TransactionType, t.TransactionDate, t.DepartmentID)
                    .ToString();
            }
            else
            {
                cls.TransactionNo = t.TransactionNo;
            }
            //}

            cls.PDAInsDate = t.TransactionDate;
            if (t.InvoiceNo == "" || t.InvoiceNo == null)
            {
                cls.InvoiceNo = _dbAccess
                    .GetTransactionNo(t.TransactionType, t.TransactionDate, t.DepartmentID)
                    .ToString();
            }
            else
            {
                cls.InvoiceNo = t.InvoiceNo;
            }
            if (t.TransactionType == 42)
            {
                t.InvoiceNo = "";
            }
            cls.PartnerID = t.PartnerID;
            cls.VisitID = t.VisitID;

            cls.PaidValue = 0;
            cls.Active = true;

            cls.PartnerItemID = "";
            cls.ContractID = 0;

            VATPercent vpc = new VATPercent();
            vpc.VATID = t.VATPrecentID;
            cls.VATPercent = _dbAccess.SelectVatPercentByID(vpc).VATPercents;
            cls.VATPercentID = vpc.VATID;
            //Users_GetLoginUserByPIN("VatPercentID :" + cls.VATPercentID);
            //Add Details
            cls.TranDetailsColl = GetTranDetails(t);

            //Users_GetLoginUserByPIN("_2");

            decimal[] Totals = GetRowsTotalValue(cls.TranDetailsColl);
            cls.RABAT = Totals[2];
            cls.Value = Totals[0];
            cls.VATValue = Totals[1];
            cls.AllValue = cls.VATValue + (cls.Value);

            cls.ReferenceID = 0;
            cls.InsBy = t.InsBy;

            if (t.TransactionType == 15 || t.TransactionType == 10 || t.TransactionType == 17)
            {
                try
                {
                    cls.InternalDepartmentID = Convert.ToInt32(Math.Truncate(t.PaymentValue)); // Convert.ToInt32(t.PaymentValue.ToString());
                }
                catch { }
            }

            if (t.TransactionType == 2)
            {
                cls.InternalDepartmentID = t.DepartmentID;
            }

            //if (cls.TransactionTypeID == 25)
            //{
            //    List<string> op = BLLOptions.GetOptionsList();
            //    string CashAccount = e.CashAccount;
            //    if (CashAccount == "")
            //    {
            //        CashAccount = op[2];
            //    }
            //    cls.CashAccount = CashAccount;
            //}
            cls.EmpID = 0; // e.ID;
            cls.CompanyID = dep.CompanyID;
            cls.JournalStatus = false;
            cls.Longitude = t.Longitude;
            cls.Latitude = t.Latitude;
            cls.BL = t.BL;

            cls.ServiceType = t.ServiceTypeID;

            //Users_GetLoginUserByPIN("_3");

            return cls;
        }

        private Task<List<TransactionAggregate>> GetTransactionsAggregateAsync(
            string fromDate,
            string toDate,
            int tranTypeID,
            string itemID = null,
            string itemName = null,
            string partnerName = null,
            string departmentName = null,
            bool isMonthly = false, // <-- NEW
            CancellationToken ct = default
        )
        {
            return _dbAccess.GetTransactionsAggregate(
                fromDate,
                toDate,
                tranTypeID,
                itemID,
                itemName,
                partnerName,
                departmentName,
                isMonthly, // <-- pass it down
                ct
            );
        }

        [HttpGet("TransactionsAggregate")]
        public async Task<IActionResult> GetAggregate(
            [FromQuery] string fromDate,
            [FromQuery] string toDate,
            [FromQuery] int tranTypeID,
            [FromQuery] string itemID = null,
            [FromQuery] string itemName = null,
            [FromQuery] string partnerName = null,
            [FromQuery] string departmentName = null,
            [FromQuery] bool isMonthly = false, // <-- NEW
            CancellationToken ct = default
        )
        {
            if (string.IsNullOrWhiteSpace(fromDate) || string.IsNullOrWhiteSpace(toDate))
                return BadRequest("fromDate and toDate are required.");

            var data = await GetTransactionsAggregateAsync(
                fromDate,
                toDate,
                tranTypeID,
                itemID,
                itemName,
                partnerName,
                departmentName,
                isMonthly, // <-- pass it
                ct
            );

            return Ok(data);
        }

        [HttpGet("TransactionsWithDetails")]
        public async Task<IActionResult> GetTransactionWithDetails(
            [FromQuery] int transactionId,
            CancellationToken ct = default
        )
        {
            if (transactionId <= 0)
                return BadRequest("Invalid transaction ID.");

            var dto = await _dbAccess.GetTransactionWithDetails(transactionId, ct);
            if (dto == null)
                return NotFound($"Transaction {transactionId} not found.");

            return Ok(dto);
        }

        [HttpGet("IncomeStatement")]
        public async Task<IActionResult> GetIncomeStatement(
            [FromQuery] string fromDate,
            [FromQuery] string toDate,
            [FromQuery] bool isMonthly = false, // NEW
            [FromQuery] string filter = "", // NEW
            CancellationToken ct = default
        )
        {
            if (string.IsNullOrWhiteSpace(fromDate) || string.IsNullOrWhiteSpace(toDate))
                return BadRequest("fromDate and toDate are required.");

            var data = await _dbAccess.GetIncomeStatementAsync(
                fromDate,
                toDate,
                isMonthly,
                filter,
                ct
            );
            return Ok(data);
        }

        private List<ItemsLookup> ItemList;

        [HttpGet("options")]
        public void GetOptions()
        {
            OptionsRepository optionsRepository = new OptionsRepository(_dbAccess);
            optionsRepository.GetOptions();
        }

        private List<TransactionsDetails> GetTranDetails(XMLTransactions t)
        {
            ItemsLookup? Item = null;

            List<TransactionsDetails> tranDetails = new List<TransactionsDetails>();
            TransactionsDetails clsDet;

            //Mbush artikujt;
            if (ItemList == null)
            {
                try
                {
                    ItemList = _dbAccess.GetItemsForPOS(0, t.DepartmentID); // Me ndrru logjiken
                }
                catch { }
            }

            foreach (XMLTransactionDetails row in t.Details)
            {
                decimal VatValue = 18;
                bool resolvedByAccount = false;

                try
                {
                    //Users_GetLoginUserByPIN("Item Name para :" + row.ItemName);
                    Item = (
                        from i in ItemList
                        where i.ItemID == row.ItemID
                        select i
                    ).FirstOrDefault<ItemsLookup>();
                    if (Item != null)
                    {
                        VatValue = Item.VATValue;
                    }
                }
                catch { }

                if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    try
                    {
                        Item = (
                            from i in ItemList
                            where i.ItemName.ToLower().Equals(row.ItemName.ToLower())
                            select i
                        ).FirstOrDefault<ItemsLookup>();
                    }
                    catch { }
                }

                if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    // fallback: try resolve as account when item not found
                    try
                    {
                        var acc = _dbAccess
                            .AccountGetByCodeAsync(row.ItemID ?? row.ItemName)
                            .GetAwaiter()
                            .GetResult();
                        if (acc != null)
                        {
                            Item = new ItemsLookup
                            {
                                ItemID = acc.AccountCode,
                                ItemName = acc.AccountDescription,
                                VATValue = VatValue,
                            };
                            resolvedByAccount = true;
                        }
                    }
                    catch { }

                    if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                    {
                        continue;
                    }
                }

                //Users_GetLoginUserByPIN("Item Name pas :" + row.ItemName);
                //Users_GetLoginUserByPIN("VatPErcentID: " + t.VATPrecentID);
                if (t.TransactionType == 42)
                {
                    //Users_GetLoginUserByPIN("SubOrderID: " + row.SubOrderID);
                    VatValue = row.PriceMenuID;
                    // Users_GetLoginUserByPIN("VatValue: " + t.VATPrecentID);
                }
                clsDet = new TransactionsDetails();
                clsDet.ID = 0;

                if (t.TransactionType == 36)
                {
                    clsDet.LocationID = Convert.ToInt32(row.Value);
                    clsDet.Contracts = row.Rabat.ToString();
                    ;
                }
                else
                {
                    clsDet.Contracts = "";
                }

                clsDet.TransactionID = t.ID;
                // DetailsType = 2 only when resolved by account; keep 1 for items
                clsDet.DetailsType = resolvedByAccount ? 2 : 1;
                // Use account code as ItemID only in fallback; preserve original row.ItemID otherwise
                clsDet.ItemID = resolvedByAccount
                    ? (Item?.ItemID ?? string.Empty)
                    : (row.ItemID ?? string.Empty);

                clsDet.ItemName = row.ItemName;

                if (t.TransactionType != 36)
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = 0; //(row.Price * 100) / (100 + vpc.VATPercents);
                }
                else
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = row.Price;
                }

                //VATPercent vpc = new VATPercent();
                //vpc.VATID = t.VATPrecentID;
                //vpc = BLLVATPercent.SelectByID(vpc);

                clsDet.CostPrice = (row.Price * 100) / (100 + VatValue); //vpc.VATPercents);
                clsDet.Value =
                    (
                        (
                            (row.Price * (1.0M - row.Rabat / 100.0M) * (1.0M - row.Rabat2 / 100.0M))
                            * 100
                        ) / (100 + VatValue)
                    ) * clsDet.Quantity; //row.Price * row.Quantity;1.
                clsDet.ProjectID = 0;
                clsDet.Mode = 1;
                clsDet.Discount = row.Rabat;
                clsDet.Discount2 = row.Rabat2;
                clsDet.Discount3 = row.Rabat3;
                clsDet.PriceWithDiscount =
                    (
                        (
                            row.Price
                            * (1.0M - row.Rabat / 100.0M)
                            * (1.0M - row.Rabat2 / 100.0M)
                            * (1.0M - row.Rabat3 / 100.0M)
                        ) * 100
                    ) / (100 + VatValue);
                if (t.TransactionType == 42)
                {
                    clsDet.Price = clsDet.PriceWithDiscount;
                }
                clsDet.VATPrice =
                    row.Price
                    * (1.0M - row.Rabat / 100.0M)
                    * (1.0M - row.Rabat2 / 100.0M)
                    * (1.0M - row.Rabat3 / 100.0M);
                if (t.TransactionType != 42)
                {
                    clsDet.SalesPrice = (row.Price * 100) / (100 + VatValue);
                }
                else
                {
                    clsDet.SalesPrice = 0;
                }
                //clsDet.Numbers = null;
                clsDet.VATValue = VatValue;
                clsDet.SubOrderID = row.SubOrderID;
                clsDet.Memo = row.Memo;
                if (t.TransactionType != 42)
                {
                    clsDet.PriceMenuID = row.PriceMenuID;
                }
                clsDet.Barcode = row.Barcode;
                // Users_GetLoginUserByPIN("Item Name:" + clsDet.ItemName);

                tranDetails.Add(clsDet);
            }
            return tranDetails;
        }

        //private decimal[] GetRowsTotalValue(List<TransactionsDetails> list)
        //{
        //    decimal[] decValue = new decimal[6];
        //    decimal VATPrice = 0;
        //    decimal PriceWithRabat = 0;
        //    decimal Transport = 0;
        //    decimal Quantity = 0;

        //    foreach (var row in list)
        //    {
        //        int DetailsType = CommonApp.CheckForInt(row.DetailsType.Value.ToString());
        //        if ("12".Contains(DetailsType.ToString()))
        //        {

        //            VATPrice = CommonApp.CheckForDecimal(row.VATPrice.ToString());
        //            PriceWithRabat = CommonApp.CheckForDecimal(row.PriceWithDiscount.ToString());
        //            Quantity = CommonApp.CheckForDecimal(row.Quantity.ToString());
        //            Transport = CommonApp.CheckForDecimal(row.Transport.ToString()); // /Quantity;

        //            decimal Koeficienti = CommonApp.CheckForDecimal(row.Coefficient.Value.ToString());

        //            decValue[4] += CommonApp.CheckForDecimal(row.AkcizaValue.ToString()) * CommonApp.CheckForDecimal(row.Quantity.ToString()) * Koeficienti;
        //            decValue[0] += CommonApp.CheckForDecimal(row.Value.ToString()); //Value
        //            decValue[1] += (VATPrice - PriceWithRabat - Transport) * Quantity;
        //            decValue[2] += (CommonApp.CheckForDecimal(row.Price.ToString()) - CommonApp.CheckForDecimal(row.PriceWithDiscount.ToString())) * CommonApp.CheckForDecimal(row.Cells["Quantity"].Value.ToString());
        //            decValue[3] += CommonApp.CheckForDecimal(row.Cells["IM7Price"].Value.ToString()) * CommonApp.CheckForDecimal(row.Cells["Quantity"].Value.ToString());

        //            if (TaxAtSourceWithVAT == false)
        //                decValue[5] += CommonApp.CheckForDecimal(row.Cells["TaxAtSource"].Value.ToString()) * CommonApp.CheckForDecimal(row.Cells["Value"].Value.ToString()) / 100;
        //            else
        //                decValue[5] += CommonApp.CheckForDecimal(row.Cells["TaxAtSource"].Value.ToString()) * CommonApp.CheckForDecimal(row.Cells["VatValue"].Value.ToString()) / 100;

        //        }
        //    }

        //    return decValue;
        //}
        private List<TransactionsDetails> GetTranDetails(Transactions t)
        {
            ItemsLookup? Item = null;

            List<TransactionsDetails> tranDetails = new List<TransactionsDetails>();
            TransactionsDetails clsDet;

            //Mbush artikujt;
            if (ItemList == null)
            {
                try
                {
                    ItemList = _dbAccess.GetItemsForPOS(0, t.DepartmentID); // Me ndrru logjiken
                }
                catch { }
            }

            foreach (TransactionsDetails row in t.TranDetailsColl)
            {
                decimal VatValue = 18;
                bool resolvedByAccount = false;

                try
                {
                    //Users_GetLoginUserByPIN("Item Name para :" + row.ItemName);
                    Item = (
                        from i in ItemList
                        where i.ItemID == row.ItemID
                        select i
                    ).FirstOrDefault<ItemsLookup>();
                    if (Item != null)
                    {
                        VatValue = Item.VATValue;
                    }
                }
                catch { }

                if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    try
                    {
                        Item = (
                            from i in ItemList
                            where i.ItemName.ToLower().Equals(row.ItemName.ToLower())
                            select i
                        ).FirstOrDefault<ItemsLookup>();
                    }
                    catch { }
                }

                if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    // fallback: try resolve as account when item not found
                    try
                    {
                        var acc = _dbAccess
                            .AccountGetByCodeAsync(row.ItemID ?? row.ItemName)
                            .GetAwaiter()
                            .GetResult();
                        if (acc != null)
                        {
                            Item = new ItemsLookup
                            {
                                ItemID = acc.AccountCode,
                                ItemName = acc.AccountDescription,
                                VATValue = VatValue,
                            };
                            resolvedByAccount = true;
                        }
                    }
                    catch { }

                    if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                    {
                        continue;
                    }
                }

                //Users_GetLoginUserByPIN("Item Name pas :" + row.ItemName);
                //Users_GetLoginUserByPIN("VatPErcentID: " + t.VATPrecentID);
                if (t.TransactionTypeID == 42)
                {
                    //Users_GetLoginUserByPIN("SubOrderID: " + row.SubOrderID);
                    VatValue = row.PriceMenuID;
                    // Users_GetLoginUserByPIN("VatValue: " + t.VATPrecentID);
                }
                clsDet = new TransactionsDetails();
                clsDet.ID = 0;

                if (t.TransactionTypeID == 36)
                {
                    clsDet.LocationID = Convert.ToInt32(row.Value);
                    clsDet.Contracts = row.Discount.ToString();
                    ;
                }
                else
                {
                    clsDet.Contracts = "";
                }

                clsDet.TransactionID = t.ID;
                // DetailsType = 2 only when resolved by account; keep 1 for items
                clsDet.DetailsType = resolvedByAccount ? 2 : 1;
                // Use account code (from Item) when resolved by account; else keep existing Item.ItemID fallback to row.ItemID
                clsDet.ItemID = resolvedByAccount
                    ? (Item?.ItemID ?? string.Empty)
                    : (Item?.ItemID ?? row.ItemID ?? string.Empty);

                clsDet.ItemName = row.ItemName;

                if (t.TransactionTypeID != 36)
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = 0; //(row.Price * 100) / (100 + vpc.VATPercents);
                }
                else
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = row.Price;
                }

                //VATPercent vpc = new VATPercent();
                //vpc.VATID = t.VATPrecentID;
                //vpc = BLLVATPercent.SelectByID(vpc);

                //clsDet.CostPrice = (row.Price * 100) / (100 + VatValue);//vpc.VATPercents);
                clsDet.Value =
                    (
                        (
                            (
                                row.Price
                                * (1.0M - row.Discount / 100.0M)
                                * (1.0M - row.Discount2 / 100.0M)
                            ) * 100
                        ) / (100 + VatValue)
                    ) * clsDet.Quantity; //row.Price * row.Quantity;1.
                clsDet.ProjectID = 0;
                clsDet.Mode = 1;
                clsDet.Discount = row.Discount;
                clsDet.Discount2 = row.Discount2;
                clsDet.Discount3 = row.Discount3;
                clsDet.PriceWithDiscount =
                    (
                        (
                            row.Price
                            * (1.0M - row.Discount / 100.0M)
                            * (1.0M - row.Discount / 100.0M)
                            * (1.0M - row.Discount / 100.0M)
                        ) * 100
                    ) / (100 + VatValue);
                if (t.TransactionTypeID == 42)
                {
                    clsDet.Price = clsDet.PriceWithDiscount;
                }
                clsDet.VATPrice =
                    row.Price
                    * (1.0M - row.Discount / 100.0M)
                    * (1.0M - row.Discount2 / 100.0M)
                    * (1.0M - row.Discount3 / 100.0M);
                if (t.TransactionTypeID != 42)
                {
                    clsDet.SalesPrice = (row.Price * 100) / (100 + VatValue);
                }
                else
                {
                    clsDet.SalesPrice = 0;
                }
                //clsDet.Numbers = null;
                clsDet.VATValue = VatValue;
                clsDet.SubOrderID = row.SubOrderID;
                clsDet.Memo = row.Memo;
                if (t.TransactionTypeID != 42)
                {
                    clsDet.PriceMenuID = row.PriceMenuID;
                }
                clsDet.Barcode = row.Barcode;
                // Users_GetLoginUserByPIN("Item Name:" + clsDet.ItemName);

                tranDetails.Add(clsDet);
            }
            return tranDetails;
        }

        private decimal[] GetRowsTotalValue(List<TransactionsDetails> Details)
        {
            decimal[] decValue = new decimal[4];
            foreach (TransactionsDetails row in Details)
            {
                decValue[0] += row.PriceWithDiscount * row.Quantity; //Value
                decValue[1] += (row.VATPrice - row.PriceWithDiscount) * row.Quantity; //VATValue
                decValue[2] += (row.SalesPrice - row.PriceWithDiscount) * row.Quantity;
            }
            return decValue;
        }

        private int GetBankJournalID(string cashaccount, int DepartmentID)
        {
            //Transactions Tran = new Transactions();
            //Tran.ID = TransactionID;
            //Tran = _dbAccess.SelectTransactionByID(Tran);

            Department Dep = new Department();
            Dep.ID = DepartmentID;
            Dep = _dbAccess.SelectDepartmentByID(Dep);

            int BankJournalID = _dbAccess.CashJournalIDByCashAccount(
                cashaccount,
                24,
                DateTime.Now,
                Dep.CompanyID
            );

            if (BankJournalID == 0)
            {
                Transactions t = GetBankTransactionForJournal(cashaccount, Dep.CompanyID);
                TransactionsService bllt = new TransactionsService(_dbAccess);
                try
                {
                    bllt.Insert(t, false);
                    bllt.CloseGlobalConnection();
                    if (bllt.ErrorID == 0)
                    {
                        BankJournalID = t.ID;
                        //MessageBox.Show(this, "Është krijuar arkë e re për ditën e sotme!", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //BankJournalID = t.ID;
                    //MessageBox.Show(this, "Është krijuar një ekstrakt banke i ri për ditën e zgjedhur!", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    bllt.ErrorID = -1;
                    bllt.CloseGlobalConnection();
                }
            }

            return BankJournalID;
        }

        private Transactions GetBankTransactionForJournal(string BankAccount, int CompanyID)
        {
            Transactions cls = new Transactions();
            cls.ID = 0;

            cls.InvoiceNo = DateTime.Now.ToString("ddMMyyyy");
            cls.TransactionNo = DateTime.Now.ToString("ddMMyyyy");
            cls.TransactionTypeID = 24;
            cls.InvoiceDate = DateTime.Now;
            cls.TransactionDate = DateTime.Now;
            cls.DueDate = DateTime.Now;
            cls.Value = 0;
            cls.AllValue = 0;
            cls.PaidValue = 0;
            cls.Active = true;
            cls.Reference = "";
            cls.Links = "";
            cls.Memo = "";

            cls.PaidValue = 0;

            cls.CashAccount = BankAccount;

            cls.VATPercentID = 0;
            cls.InsBy = GlobalAppData.UserID;
            cls.CompanyID = CompanyID;
            return cls;
        }

        [HttpGet("GetItemStateForOneItem")]
        public ActionResult<List<ItemState>> GetItemStateForOneItem([FromQuery] string ItemID)
        {
            if (string.IsNullOrEmpty(ItemID))
            {
                return BadRequest("ItemID is required.");
            }

            List<ItemState> itemStates = _dbAccess.GetItemsStateForOneItem(ItemID);

            if (itemStates == null || itemStates.Count == 0)
            {
                return NotFound();
            }

            return Ok(itemStates);
        }

        private void WriteLog(string log)
        {
            string filePath = @"c:\temp\Logs.txt";
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                //StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine(DateTime.Now.ToString() + "**** " + log + "****");
                sw.Close();
            }
        }

        string NewFLinkVersion = "0";
        #region RaportetExtreamPica
        //public void PrintFiscalInvoice(int TransactionID, string Pathi, string UserNam, bool isOrder, string Barcode)
        //{
        //    //Users_GetLoginUserByPIN("Hyn ne printim te kuponit fiskal");
        //    NewFLinkVersion = System.Configuration.ConfigurationManager.AppSettings["NewFLinkVersion"];
        //    DataTable TranDet;
        //    Transactions t = BLLTransactions.SelectByID(new Transactions() { ID = TransactionID });
        //    string TableName = "table";// BLLTables.SelectByID(new BELayer.Tables() { ID = t.TableID }).TableName;

        //    if (OptionsData.PrintFiscalAfterEachOrder && 1 == 2)
        //    {
        //        TranDet = BLLTransactionsDetails.GetDetailsForFiscal_Eachorder_Mobile(TransactionID, isOrder);
        //    }
        //    else
        //    {
        //        TranDet = BLLTransactionsDetails.GetDetailsForFiscal(TransactionID);
        //    }

        //    PrintFiscalInvoice(TranDet, Pathi, UserNam, Barcode, TableName);
        //}
        //bool ErrorPrintFiscal = false;
        //public void PrintFiscalInvoice(DataTable TranDet, string Pathi, string UserName, string Barcode, string TableName)
        //{
        //    // Users_GetLoginUserByPIN("Thirret printimi i fiskalit");
        //    //Users_GetLoginUserByPIN(Pathi);
        //    decimal totalPoints = 0;
        //    int PaymentType = 0;
        //    decimal TotalValueDatex = 0;
        //    try
        //    {
        //        if (Barcode != null && Barcode != "")
        //        {
        //            //List<ASHelpClass> list = LoyaltyCardByBarcode(Barcode);
        //            //ASHelpClass ash = list[0];
        //            //totalPoints = ash.Discount;
        //        }
        //    }
        //    catch { }
        //    NewFLinkVersion = System.Configuration.ConfigurationManager.AppSettings["NewFLinkVersion"];
        //    string PrintTableInFiscal = System.Configuration.ConfigurationManager.AppSettings["PrintTableInFiscal"];
        //    string DateForItem;
        //    int TransactionID;
        //    DateForItem = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

        //    #region DATECS
        //    if (!Pathi.Contains("http://"))
        //    {
        //        try
        //        {
        //            string lines = "";
        //            if (OptionsData.PrintTranIDInFiscal)
        //            {
        //                if (NewFLinkVersion == "0")
        //                {
        //                    //lines += "Q,1,______,_,__;2;" + " NR." + TranDet.Rows[0]["TransactionNo"].ToString() + "(" + TranDet.Rows[0]["TransactionID"].ToString() + ")" + "\r\n";
        //                    UserName += "(" + TranDet.Rows[0]["TransactionID"].ToString() + ")";
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        lines += "Q,1,______,_,__;1;" + " Fat. NR." + TranDet.Rows[0]["TransactionNo"].ToString() + "\r\n";
        //                    }
        //                    catch { }
        //                }

        //            }

        //            if (NewFLinkVersion == "0")
        //            {
        //                lines = "L,1,______,_,__;SetOperator;1;" + UserName + ";0000;0000" + "\r\n";
        //            }

        //            int i = 1;

        //            foreach (DataRow det in TranDet.Rows)
        //            {
        //                try
        //                {
        //                    PaymentType = CheckForInt(det["PaymentType"].ToString());
        //                }
        //                catch (Exception)
        //                {

        //                }
        //                TotalValueDatex += decimal.Round(CheckForDecimal(det["VATPrice"].ToString())
        //                                            * CheckForDecimal(det["Quantity"].ToString())
        //                    , 2, MidpointRounding.AwayFromZero);

        //                lines += "S,1,______,_,__;" + det["ItemName"];// Emertim

        //                lines += ";" + CheckForDecimal(det["VATPriceWithoutRabat"].ToString()).ToString("N2");// Cmimi

        //                lines += ";" + CheckForDecimal(det["Quantity"].ToString()).ToString("N3");// Sasia

        //                lines += ";1;1;";

        //                lines += det["DatexGroup"].ToString();

        //                DateForItem = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

        //                lines += ";0;" + i + DateForItem + det["ItemID"];

        //                try
        //                {
        //                    if (CheckForDecimal(det["RABAT"].ToString()) > 0)
        //                        lines += ";" + CheckForDecimal(det["RABAT"].ToString()).ToString("N2");

        //                }
        //                catch { }

        //                lines += "\r\n";

        //                i++;
        //            }
        //            if (totalPoints > 0)
        //            {
        //                lines += "Q,1,______,_,__;1;" + " PIKE TE GRUMBULLUARA : " + totalPoints.ToString("N2") + "\r\n";
        //            }
        //            else
        //            {
        //                if (PrintTableInFiscal == "1")
        //                {
        //                    lines += "Q,1,______,_,__;1;" + " JU FALEMINDERIT!  Tavolina : " + TableName + "\r\n";
        //                }
        //                else
        //                {
        //                    lines += "Q,1,______,_,__;1;" + " JU FALEMINDERIT! " + "\r\n";
        //                }
        //            }
        //            lines += "T,1,______,_,__;";

        //            //lines = "First line.\r\nSecond line.\r\nThird line.";

        //            if (!System.IO.Directory.Exists(Pathi))
        //            {
        //                // Create the subfolder
        //                System.IO.Directory.CreateDirectory(Pathi);
        //            }
        //            try
        //            {
        //                //versioni i ri i FLink-ut, operatori behet ne fajll te veqante
        //                if (NewFLinkVersion == "1")
        //                {
        //                    string lines_operator = "V,1,______,_,__;f1;0000;" + UserName + "\r\n";
        //                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(Pathi + "\\_operatori.inp");//"C:\\Temp\\kuponishitjes.inp"
        //                    file1.WriteLine(lines_operator);

        //                    file1.Close();
        //                }

        //            }
        //            catch
        //            {
        //                ErrorPrintFiscal = true;
        //            }

        //            try
        //            {
        //                TransactionID = Convert.ToInt32(TranDet.Rows[0]["TransactionID"].ToString());
        //                // Write the string to a file.
        //                System.IO.StreamWriter file = new System.IO.StreamWriter(Pathi + "\\ksh_" + TransactionID + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".inp");//"C:\\Temp\\kuponishitjes.inp"
        //                file.WriteLine(lines);

        //                file.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                ErrorPrintFiscal = true;
        //                //Users_GetLoginUserByPIN(ex.Message);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorPrintFiscal = true;
        //            //Users_GetLoginUserByPIN(ex.Message);
        //        }
        //    } //fundi DATECS
        //    #endregion
        //    #region GLOBAL_EU
        //    else if (Pathi.Contains("http://"))
        //    {
        //        try
        //        {
        //            PrintFiscalInvoice_GLOBAL(TranDet, Pathi, UserName, Barcode, TableName);
        //        }
        //        catch (Exception ex)
        //        {

        //            ErrorPrintFiscal = true;
        //        }

        //    }
        //    #endregion

        //    if (ErrorPrintFiscal == false)
        //    {
        //        string UpdateIsPrintFiscalInvoice = "1";
        //        try
        //        {
        //            UpdateIsPrintFiscalInvoice = System.Configuration.ConfigurationManager.AppSettings["UpdateIsPrintFiscalInvoice"];
        //        }
        //        catch { UpdateIsPrintFiscalInvoice = "1"; }
        //        try
        //        {
        //            if (UpdateIsPrintFiscalInvoice == "1")
        //            {
        //                TransactionID = Convert.ToInt32(TranDet.Rows[0]["TransactionID"].ToString());
        //                BLLTransactions.UpdateIsPrintFiscalInvoiceAsTrueByTranID(TransactionID);
        //            }
        //        }
        //        catch { }
        //    }
        //}

        #endregion

        public static decimal CheckForDecimal(string senderText)
        {
            decimal decS = 0;

            if (decimal.TryParse(senderText, out decS))
            {
                decimal decValue = Convert.ToDecimal(senderText.Trim());
                decS = decValue;
            }

            return decS;
        }

        public static int CheckForInt(string senderText)
        {
            int intS = 0;
            if (int.TryParse(senderText, out intS))
            {
                int intValue = Convert.ToInt32(senderText.Trim());
                intS = intValue;
            }
            return intS;
        }

        [HttpPost("UpdateIsPrintFiscalInvoice")]
        public void UpdateIsPrintFiscalInvoice(int TransactionID)
        {
            TransactionsService.UpdateIsPrintFiscalInvoiceAsTrueByTranID(TransactionID);
        }

        [HttpGet("TransactionByID")]
        public IActionResult GetTransactionByID([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid transaction ID.");

            var cls = new Transactions { ID = id };
            var transaction = _dbAccess.SelectTransactionByID(cls);

            if (transaction == null)
                return NotFound($"Transaction with ID {id} not found.");

            return Ok(transaction);
        }

        #region RaportetExtreamPica
        //public void PrintFiscalInvoice_GLOBAL(DataTable TranDet, string Pathi, string UserName, string Barcode, string TableName)
        //{
        //    // Users_GetLoginUserByPIN("Thirret printimi i fiskalit");
        //    WriteLog("useri:" + UserName);
        //    decimal totalPoints = 0;
        //    int PaymentType = 0;
        //    decimal TotalValueDatex = 0;
        //    try
        //    {
        //        if (Barcode != null && Barcode != "")
        //        {
        //            //List<ASHelpClass> list = LoyaltyCardByBarcode(Barcode);
        //            //ASHelpClass ash = list[0];
        //            //totalPoints = ash.Discount;
        //        }
        //    }
        //    catch { }
        //    NewFLinkVersion = System.Configuration.ConfigurationManager.AppSettings["NewFLinkVersion"];
        //    string PrintTableInFiscal = System.Configuration.ConfigurationManager.AppSettings["PrintTableInFiscal"];
        //    string DateForItem;
        //    int TransactionID;
        //    DateForItem = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

        //    string address = Pathi; //"http://192.168.199.37:4444/";
        //    string comPort = "";
        //    int baud = 0;
        //    var fp = new FP { ServerAddress = address };
        //    try
        //    {
        //        if (fp.ServerFindDevice(out comPort, out baud))
        //        {
        //            //txtCOMPort.Text = comPort;
        //            //txtBaud.Text = baud.ToString();
        //            Thread.Sleep(150);

        //            //string url = FINA.Properties.Settings.Default.FiscalPrinterPath + "Settings(com=" + comPort + ",baud=" + baud + ",tcp=,ip=,port=,password=)";
        //            //SetPrinterSettings(url).Wait();
        //            fp.ServerSetDeviceSerialPortSettings(comPort, baud);
        //            Thread.Sleep(150);
        //        }
        //    }
        //    catch (SException sx)
        //    {

        //        //MessageBox.Show(sx.Message.ToString());
        //        ErrorPrintFiscal = true;
        //        return;
        //    }

        //    //hapja fatures
        //    try
        //    {
        //        fp.OpenReceipt(1, "0", OptionPrintType.Step_by_step_printing);

        //        Thread.Sleep(150);
        //    }
        //    catch (SException sx)
        //    {
        //        //MessageBox.Show(sx.Message.ToString());
        //        ErrorPrintFiscal = true;
        //        return;
        //    }

        //    decimal TotalValueBanks = 0;
        //    decimal TotalValueCash = 0;

        //    //TotalValueCash = CheckForDecimal(strPayments[0]);

        //    //for (int j = 1; j < strPayments.Length; j++)
        //    //{
        //    //    TotalValueBanks += CommonApp.CheckForDecimal(strPayments[j]);
        //    //}
        //    //PayValue = (TotalValueCash + TotalValueBanks).ToString("N2");

        //    {
        //        string lines = "";  // = "1;";
        //        decimal TotalAmount = 0;
        //        decimal TotalValue = 0;
        //        decimal Quantity = 0;
        //        decimal Price;
        //        string ItemName = "";

        //        int i = 1;

        //        lines = " 01,0000,1" + "\r\n";

        //        fp.DisplayTextLine1(UserName);//emri i operatorit
        //        //fp.ProgOperator(1,UserName,"0");
        //        fp.PrintText(UserName);
        //        Thread.Sleep(120);

        //        foreach (DataRow det in TranDet.Rows)
        //        {
        //            Quantity = Math.Round(CheckForDecimal(det["Quantity"].ToString()), 3, MidpointRounding.AwayFromZero);
        //            Price = Math.Round(CheckForDecimal(det["VATPrice"].ToString()), 2, MidpointRounding.AwayFromZero);
        //            ItemName = (det["OriginalItemName"] + "                      ").Substring(0, 19);

        //            DateForItem = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

        //            //lines += "#1" + (det["OriginalItemName"] + "                      ").Substring(0, 24) + (char)9;// Emertim + <TAB>

        //            //lines += det["FP500_MK"].ToString() + string.Format("{0:F}", Price) + '*' + string.Format("{0:G}", Quantity);// Sasia

        //            //lines += "\r\n";
        //            OptionVATClass options = new OptionVATClass();
        //            if (det["VAT"].ToString() == "0")
        //                options = OptionVATClass.VAT_Class_A; //partneri pa tvsh
        //            else if (det["DatexGroup"].ToString() == "1") //tvsh =0
        //                options = OptionVATClass.VAT_Class_C;
        //            else if (det["DatexGroup"].ToString() == "4") //tvsh =8
        //                options = OptionVATClass.VAT_Class_D;
        //            else //tvsh =18
        //                options = OptionVATClass.VAT_Class_E;

        //            fp.SellPLUwithSpecifiedVAT(ItemName, options, Price, Quantity, 0, 0, 1);//18 tvsh
        //            Thread.Sleep(150);

        //            TotalValue = Convert.ToDecimal((Quantity * Price).ToString("N2"));
        //            TotalAmount = TotalAmount + TotalValue;

        //            //lines += ";0;" + det["ItemID"] + "\r\n";
        //            i++;
        //        }

        //        if (false) // anulohet njehere (FINA.Properties.Settings.Default.PrintPaymentFiscal)
        //        {
        //            if (TotalValueBanks > TotalAmount)
        //            {
        //                TotalValueBanks = TotalAmount;
        //                TotalValueCash = 0;
        //            }

        //            if (TotalValueBanks > 0)
        //            {
        //                fp.Payment(OptionPaymentType.Card, OptionChange.Without_Change, TotalValueBanks, OptionChangeType.Same_As_The_payment);
        //                //lines += "&5" + (char)9 + "D" + string.Format("{0:F}", TotalValueBanks) + "\r\n";
        //                Thread.Sleep(150);
        //            }

        //            if (TotalValueCash >= 0) //cdohere e perdorim per te mbyllur faturen
        //                try
        //                {
        //                    fp.CashPayCloseReceipt();
        //                    Thread.Sleep(150);
        //                }
        //                catch (SException sx)
        //                {
        //                    //MessageBox.Show(sx.ToString());
        //                    ErrorPrintFiscal = true;
        //                    return;
        //                }

        //            //lines += "&5" + (char)9 + "P" + string.Format("{0:F}", TotalValueCash) + "\r\n";

        //        }
        //        else
        //        {
        //            try
        //            {
        //                fp.CashPayCloseReceipt();
        //            }
        //            catch (SException sx)
        //            {
        //                // MessageBox.Show(sx.ToString());
        //                ErrorPrintFiscal = true;
        //            }

        //        }

        //        if (false) //anulohet njehere (FINA.Properties.Settings.Default.PrintDuplicateFiscal)
        //        {
        //            try
        //            {
        //                fp.PrintLastReceiptDuplicate();
        //                Thread.Sleep(150);
        //            }
        //            catch (SException sx)
        //            {
        //                // MessageBox.Show(sx.Message, "Printeri Fiskal");
        //                return;
        //            }
        //        }

        //        //lines += "%8" + "\r\n";

        //        return;

        //    }

        //}
        #endregion
    }
}


#region RaportetExtreamPica
//public class ReportFactory
//{
//    protected static Queue reportQueue = new Queue();

//    protected static ReportClass CreateReport(Type reportClass)
//    {
//        object report = Activator.CreateInstance(reportClass);
//        reportQueue.Enqueue(report);
//        return (ReportClass)report;
//    }
//    protected static ReportDocument CreateReportDoc(Type reportClass)
//    {
//        object report = Activator.CreateInstance(reportClass);
//        reportQueue.Enqueue(report);
//        return (ReportDocument)report;
//    }
//    public static ReportClass GetReport(Type reportClass)
//    {

//        //75 is my print job limit.
//        if (reportQueue.Count > 30) ((ReportClass)reportQueue.Dequeue()).Dispose();
//        return CreateReport(reportClass);
//    }

//    public static ReportDocument GetReportDoc(Type reportDoc)
//    {
//        if (reportQueue.Count > 30)
//        {

//            ((ReportDocument)reportQueue.Dequeue()).Dispose();

//        }
//        return CreateReportDoc(reportDoc);
//    }

//    public static string TranslateText(string input, string langFrom, string langTo)
//    {
//        input = input.Replace("&", "");
//        //Defines a new WebClient
//        WebClient Client = new WebClient();
//        //Sets the client encoding to UTF8
//        Client.Headers.Add("Charset", "text/html; charset=UTF-8");
//        //Creates the string. And yes I prefer this over string.format ! ;)
//        string downloadUrl = "http://www.google.com/translate_t?hl=da&ie=UTF7&text=" + input + "&langpair=" + langFrom + "|" + langTo;
//        //Downloads the string from the URL above
//        string data = Client.DownloadString(downloadUrl);
//        //Searches for the beginning of the resultbox and cuts everything away before that
//        data = data.Substring(data.IndexOf("<span id=result_box") + 19);
//        //Finds the ending of the resultbox by searching for two spans right after each other
//        data = data.Remove(data.IndexOf("</span></span>") + 7);
//        //Defines a new regex used for counting all spans inside the resultbox
//        Regex spans = new Regex("<span");
//        //Finds the count and puts it inside the variable spanOccurences
//        int spanOccurences = spans.Matches(data).Count;
//        //Defines an empty string for use in the for loop
//        string translatedText = "";
//        //Extract each tiny bit of text from each span in the resultbox
//        for (int i = 0; i < spanOccurences; i++)
//        {
//            //Defines currentBlock and sets it to everything which comes after the first "<span"
//            string currentBlock = data.Substring(data.IndexOf("<span") + 5);
//            //Finds the ending of the current span and removes everything after that
//            currentBlock = currentBlock.Remove(currentBlock.IndexOf("</span>"));
//            //Goes back to the beginning and cleans everything from inside the first span
//            currentBlock = currentBlock.Substring(currentBlock.IndexOf(">") + 1);
//            //Removes the current processed span from the beginning of the data for next extraction
//            data = data.Substring(data.IndexOf("</span>") + 7);
//            //Adds the extracted text to the translatedText variable
//            translatedText += ReplaceCharacters(currentBlock);
//        }
//        //Returns the translated text
//        return translatedText;
//    }

//    private static string ReplaceCharacters(string text)
//    {

//        text = text.Replace("&#304;", "İ");
//        text = text.Replace("&#305;", "ı");
//        text = text.Replace("&#350;", "Ş");
//        text = text.Replace("&#351;", "ş");
//        text = text.Replace("ÃƒÂ «", "ë");
//        text = text.Replace("ÃƒÂ« ", "ë");
//        text = text.Replace("ÃƒÂ &quot;", "ë");
//        text = text.Replace("&#287;", "ğ");
//        text = text.Replace("&#286;", "Ğ");
//        text = text.Replace("ÃƒÂ&quot;", "ë");
//        text = text.Replace("Aktif Konular Aktif Konular «r", "");

//        return text;
//    }

//}
#endregion

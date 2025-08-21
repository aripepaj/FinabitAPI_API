

using AutoBit_WebInvoices.Models;
using Finabit_API.Models;
using FinabitAPI.Service;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;

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

        public TransactionsController(IConfiguration configuration, DBAccess dbAccess, EmployeesRepository dalEmployee, DepartmentRepository dalDepartment)
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

        #region ToDelete
        //string _SchDate = "";
        //string _SchTime = "";
        //[HttpPost]
        //[AcceptVerbs("POST")]
        //[Route("api/Invoice/PostInvoiceWithTermin")]
        //public IHttpActionResult PostInvoiceWithTermin(string SchDate,  string SchTime, string PersonalNo, string Email, string PhoneNo)
        //{

        //        _SchDate = SchDate;
        //        _SchTime = SchTime;
        //        DocumentPrice dp = new DocumentPrice();// (DocumentPrice)cboDocPrice.SelectedItem;
        //        int docPriceID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DocPriceID"]);
        //        decimal VatPrc = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["VATPrc"]);
        //        dp.ID = docPriceID;
        //        //BLLDocPrice bllDocPrice=new BLLDocPrice();
        //        dp = _dbAccess.GetDocPriceByID(dp);
        //        docPrice = dp.DocPrice;
        //        docVAT = docPrice * VatPrc / 100;
        //        docInvoiceValue = docPrice + docVAT;

        //        Invoice i = new Invoice();

        //        bool ExistsOwner = GetOwnerByPersonalNo(PersonalNo, Email, PhoneNo) == 1;
        //        if (ExistsOwner)
        //        {
        //       // return Content(HttpStatusCode.BadRequest, "ExistsOwner");
        //            i = Invoice;
        //            _dbAccess.NewInvoice(i);


        //        }
        //    else
        //    {
        //        return Content(HttpStatusCode.NonAuthoritativeInformation, "Numri personal i dhene nuk gjindet!");
        //    }

        //    if (i.ID>0)
        //    {
        //        return Content(HttpStatusCode.OK, "Te dhenat jane ruajtur me sukses!");
        //    }
        //    else
        //    {
        //        return Content(HttpStatusCode.InternalServerError, "Te dhenat nuk jane ruajtur!");
        //    }
        //}
        //decimal docPrice = 0;
        //decimal docVAT = 0;
        //decimal docInvoiceValue = 0;
        //private Invoice _mInvoice = new Invoice();
        //public Invoice Invoice
        //{
        //    get
        //    {

        //        _mInvoice.Owner.ID = _mOwnerID;

        //        GlobalAppData.CompanyID = 21;
        //        _mInvoice.Price = docPrice;
        //        _mInvoice.VAT = docVAT;
        //        _mInvoice.InvoiceValue = docInvoiceValue;

        //        _mInvoice.Owner = new Owner { FirstName = FirstName };
        //        _mInvoice.Owner.LastName = LastName;
        //        _mInvoice.Owner.PersonalNo = _PersonalNo;
        //        _mInvoice.Owner.ID = _mOwnerID;
        //        _mInvoice.Owner.Phone = _PhoneNo;
        //        if (DOB.ToString().EndsWith("."))
        //        {
        //            _mInvoice.Owner.DOB = DateTime.Now;
        //        }
        //        else
        //        {
        //            _mInvoice.Owner.DOB = DOB.ToString() == "" ? (DateTime?)null : DOB;
        //        }
        //        _mInvoice.InvoiceDate = DateTime.Now;
        //        _mInvoice.Owner.Address = Address;
        //        _mInvoice.Owner.BusinessNo = "";
        //        _mInvoice.InvoiceType = 2;
        //        _mInvoice.DocumentPrice = new DocumentPrice { ID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DocPriceID"]) };
        //        _mInvoice.Quantity = 1;
        //        _mInvoice.SHCHDate = DateTime.Parse(_SchDate);
        //        _mInvoice.SHCHTime = DateTime.Parse(_SchTime);
        //        _mInvoice.ChannelID = 1;// CommonApp.CheckComboForInt(cboChannel);
        //        _mInvoice.EmployeeId = 1;// Convert.ToInt32(cmbEmployees.SelectedValue);
        //        _mInvoice.InvoiceTotalValue = docInvoiceValue;
        //        _mInvoice.InvoiceNo = _dbAccess.GetInvoiceNo(); //txtInvoiceNo.Text;
        //        _mInvoice.Description = "Online";//txtDescription.Text;
        //        _mInvoice.LUB = GlobalAppData.UserID;
        //        _mInvoice.EmployeeId = 0;// Convert.ToInt32(cmbEmployees.SelectedValue);
        //        _mInvoice.VIN = 0;// CommonApp.CheckForInt(txtVIN.Text);
        //        _mInvoice.AllowPrintDoc = true;// chkPrintDoc.Checked;
        //        _mInvoice.OwnerMiddleName = Middlename;
        //        _mInvoice.OwnerPlace = PlaceName;
        //        _mInvoice.OwnerPersonalNo = _PersonalNo;
        //        _mInvoice.OwnerName = FirstName;
        //        _mInvoice.OwnerSurname = LastName;
        //        _mInvoice.OwnerMiddleName = Middlename;
        //        _mInvoice.OwnerBirthDate = DOB.ToString() == "" ? (DateTime?)null : DOB;
        //        _mInvoice.OwnerAddress = Address;
        //        _mInvoice.FromWeb = true;
        //        return _mInvoice;
        //    }

        //}
        //bool ExistOnARC;
        //int _mOwnerID = 0;
        //string FirstName = "";
        //string LastName = "";
        //string Middlename = "";
        //DateTime? DOB = null;
        //string Address = "";
        //string PlaceName = "";
        //string _PersonalNo = "";
        //string _PhoneNo = "";
        //public int GetOwnerByPersonalNo(string PersonalNo, string Email, string PhoneNo)
        //{
        //    int intCount = 0;
        //    Owner own = new Owner();
        //    ExistOnARC = false;
        //    own.PersonalNo = PersonalNo;
        //    _PersonalNo = PersonalNo;
        //    //txtFirstName.Text = "";
        //    //txtLastName.Text = "";
        //    //txtMiddleName.Text = "";
        //    //txtAddress.Text = "";
        //    //txtDOB.Value = null;
        //    //txtEmail.Text = "";

        //    intCount = _dbAccess.LoadByPersNo(ref own);
        //    DataTable dt = null;
        //    try
        //    {
        //        QRA.Service1SoapClient qra = new QRA.Service1SoapClient();
        //        dt = qra.GetQRAData(PersonalNo).Tables[0];
        //    }
        //    catch { }
        //    if (intCount == 1 && PersonalNo.Trim().Length == 10)
        //    {
        //        if (dt.Rows.Count == 1 && dt.Rows[0]["Emri"] != "")
        //        {
        //            Owner o = new Owner();
        //            o.ID = own.ID;
        //            _mOwnerID = o.ID;

        //            o.OwnerType.ID = 2;
        //            o.FirstName = dt.Rows[0]["Emri"].ToString();
        //            o.MiddleName = dt.Rows[0]["EmriPrindit"].ToString();
        //            o.LastName = dt.Rows[0]["Mbiemri"].ToString();
        //            string[] bd = dt.Rows[0]["DateLindja"].ToString().Split('/');

        //            string dbd =int.Parse( bd[1]).ToString("D2") + "." + int.Parse(bd[0]).ToString("D2") + "." + bd[2].Split(' ')[0];
        //            o.DOB = DateTime.ParseExact(dbd,"dd.MM.yyyy",null); 
        //            if (o.DOB == null)
        //            {
        //                o.DOB = DateTime.Now;
        //            }
        //            DOB = o.DOB;
        //            o.PersonalNo = PersonalNo;
        //            o.Address = dt.Rows[0]["Adresa"].ToString(); ;
        //            o.PlaceName = dt.Rows[0]["Komuna"].ToString(); ;
        //            o.LUB = GlobalAppData.UserID;
        //            o.Email = Email;
        //            o.FromQRA = true;
        //            o.Phone = PhoneNo;
        //            _PhoneNo = PhoneNo;
        //            ExistOnARC = true;
        //            _dbAccess.Update(o);
        //            intCount = _dbAccess.LoadByPersNo(ref own);
        //            //txtPersonalNo.Text = o.PersonalNo;
        //            FirstName = o.FirstName;
        //            LastName = o.LastName;
        //            Address = o.Address;
        //            ////DOB = (object)o.DOB.Value;
        //            Email = o.Email;
        //            Middlename = o.MiddleName;
        //            PlaceName = o.PlaceName;

        //            //txtMiddleName.Text = o.MiddleName;
        //            //OwnerMiddleName = o.MiddleName;
        //            //OwnerPlace = o.PlaceName;
        //            //txtPhone.Text = o.Phone;


        //        }


        //    }
        //    else if (dt.Rows.Count == 1 && dt.Rows[0]["Emri"] != "")
        //    {
        //        Owner o = new Owner();
        //        o.ID = 0;
        //        o.OwnerType.ID = 2;
        //        o.FirstName = dt.Rows[0]["Emri"].ToString();
        //        o.MiddleName = dt.Rows[0]["EmriPrindit"].ToString();
        //        o.LastName = dt.Rows[0]["Mbiemri"].ToString();
        //        string[] bd = dt.Rows[0]["DateLindja"].ToString().Split('/');

        //        string dbd = bd[1] + "." + bd[0] + "." + bd[2].Split(' ')[0];
        //        o.DOB = DateTime.ParseExact(dbd, "dd.MM.yyyy", null);
        //        if (o.DOB == null)
        //        {
        //            o.DOB = DateTime.Now;
        //        }
        //        DOB = o.DOB;
        //        o.Email = Email;
        //        o.PersonalNo = PersonalNo;
        //        o.Address = dt.Rows[0]["Adresa"].ToString(); ;
        //        o.PlaceName = dt.Rows[0]["Komuna"].ToString(); ;
        //        o.LUB = GlobalAppData.UserID;
        //        o.FromQRA = true;
        //        _PhoneNo = PhoneNo;
        //        o.Phone = PhoneNo;
        //        _dbAccess.New(o);
        //        _mOwnerID = o.ID;
        //        FirstName = o.FirstName;
        //        LastName = o.LastName;
        //        Address = o.Address;
        //        ////DOB = (object)o.DOB.Value;
        //        Email = o.Email;
        //        Middlename = o.MiddleName;
        //        PlaceName = o.PlaceName;
        //        ExistOnARC = true;
        //        intCount = _dbAccess.LoadByPersNo(ref own);
        //    }
        //    if (intCount == 1 && PersonalNo.Trim().Length != 10)
        //    {
        //        intCount = _dbAccess.LoadByPersNo(ref own);
        //        _mOwnerID = own.ID;
        //        //txtPersonalNo.Text = own.PersonalNo;
        //        //txtFirstName.Text = own.FirstName;
        //        //txtLastName.Text = own.LastName;
        //        //txtAddress.Text = own.Address;
        //        //txtDOB.Value = (object)own.DOB.Value;
        //        //txtEmail.Text = own.Email;
        //        //txtMiddleName.Text = own.MiddleName;
        //        //OwnerMiddleName = own.MiddleName;
        //        //OwnerPlace = own.PlaceName;
        //        //txtPhone.Text = own.Phone;
        //    }

        //    return intCount;
        //}

        #endregion



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

            if ((tt.PaymentValue != 0 && (tt.TransactionType != 15 && tt.TransactionType != 10 && tt.TransactionType != 17)))
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

            TransactionsService bllt = new TransactionsService(true);
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
                if ((tt.PaymentValue != 0 && (tt.TransactionType != 15 && tt.TransactionType != 10 && tt.TransactionType != 17)) && bllt.ErrorID == 0)
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


                        Detail.ItemName = "";// user.UserName;
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
            //}
            //catch (Exception ex) { Users_GetLoginUserByPIN(ex.Message); }
            return tt;



        }

        private bool printTermik = false;



        [HttpPost("TransactionInsertWithPrint")]
        public ActionResult<Transactions> TransactionInsertWithPrint([FromBody] TransactionRequest transaction)
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



            var printersForPOS = OptionsRepository.GetPrintersForPOS();
            var pattern = @"^Printeri \d+$";
            var filteredPrinters = printersForPOS
                .Where(printer => Regex.IsMatch(printer.PrinterAlias, pattern))
                .ToList();



            printTermik = filteredPrinters.Count > 0;
            var fiscalPrinter = printersForPOS.Where(printer => printer.PrinterAlias.ToUpper().Contains("FISKAL")).FirstOrDefault();


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


                int emp1 = int.TryParse(_configuration["AppSettings:EmpNderrimi1"], out var tmpEmp1) ? tmpEmp1 : 0;
                int emp2 = int.TryParse(_configuration["AppSettings:EmpNderrimi2"], out var tmpEmp2) ? tmpEmp2 : 0;

                // Nderrimi i paradites
                if (t.Reference.Equals("1") && emp1 > 0)
                {
                    emp.EmpID = emp1;// 32;
                }
                // Nderrimi i pasdites
                else if (t.Reference.Equals("2") && emp2 > 0)
                {
                    emp.EmpID = emp2;// 21;
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
            string TableName = "table";// BLLTables.SelectByID(new BELayer.Tables() { ID = t.TableID }).TableName;
            DataTable SDTable = GlobalRepository.ListSystemDataTable();

            List<ItemLocation> ILList = ItemLocationRepository.SelectAll();
            bool GenerateTranNo = Convert.ToBoolean(SDTable.Rows[0]["GenerateTransactionNo"]);
            if (GenerateTranNo || String.IsNullOrEmpty(transaction.t.TransactionNo))
            {
                transaction.t.TransactionNo = TransactionsService.GetTransactionNo(transaction.t.TransactionTypeID, t.TransactionDate, t.DepartmentID);
            }

            transaction.t.InvoiceNo = transaction.t.TransactionNo;

            // dgj 18122011 merret nga serveri direkt data e transaskasionit

            t.TransactionDate = DateTime.Now.Date;
            t.InvoiceDate = DateTime.Now.Date;
            t.DueDate = DateTime.Now.Date;
            t.TerminID = CommonApp.CheckForInt(GlobalAppData.TermnID);
            // Fix for CS0029: Convert ActionResult<List<string>> to List<string> by accessing the Value property
            List<string> o = GetOptionsList().Value;
            int PartID = t.PartnerID;
            int OptionsPartner =CommonApp.CheckForInt(o[0]);
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
            if (PartID == 0 && t.PartnerName!="")
            {
                int part = PartnerRepository.SelectByName(t.PartnerName);
                if (part != 0)
                {
                    PartID = part;
                }
            }

            Partner newPartner = new Partner();
            P = PartnerRepository.SelectByID(new Partner { ID = t.PartnerID == 0 ? PartID : t.PartnerID });


            if (P.ID == 0 && !String.IsNullOrEmpty(t.PartnerName))
            {

                newPartner.Email = t.PartnerEmail;
                newPartner.PartnerName = t.PartnerName;
                newPartner.Tel1 = t.PartnersPhoneNo;
                PartnersType type = new PartnersType();
                type.ID = 2;
                newPartner.PartnerType = type;
                PartnerRepository dalPartner = new PartnerRepository();
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

            TransactionsService bllt = new TransactionsService(true);
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
                            bllt.ErrorID = -1; t.ErrorID = -1;
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
                        catch { bllt.ErrorID = -1; t.ErrorID = -1; }

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
            catch { bllt.CloseGlobalConnection(); }
            finally
            {
                try
                {
                    bllt.CloseGlobalConnection();
                }
                catch (Exception ex) { /*Users_GetLoginUserByPIN(ex.Message);*/ }
                //Users_GetLoginUserByPIN("E mbyll koneksionin 3");
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
                    catch (Exception ex) { /*Users_GetLoginUserByPIN(ex.Message);*/ }
                }
                catch { }
            }
            //Users_GetLoginUserByPIN("Rreshtat e detajeve: "+t.Details.Count);
            return t;
        }


        [HttpGet("UpdateTransactionDriver")]
        public ActionResult<bool> UpdateTransactionDriver([FromQuery] int TransactionID, [FromQuery] int EmpID)
        {
            TransactionsRepository dal = new TransactionsRepository();
            return dal.UpdateDriverInTransaction(TransactionID, EmpID);
        }

        [HttpGet("GetOptionsList")]
        public ActionResult<List<string>> GetOptionsList()
        {
            return OptionsRepository.GetOptionsList();
        }


        [HttpGet("OrdersList")]
        public ActionResult<List<Orders>> OrdersList([FromQuery] string FromDate, [FromQuery] string ToDate)
        {
            return GetOrders(FromDate, ToDate);
        }


        [HttpGet("TransactionsList")]
        public async Task<ActionResult<List<Orders>>> TransactionsList(
            [FromQuery] string FromDate,
            [FromQuery] string ToDate,
            [FromQuery] int TransactionTypeID,
            [FromQuery] string ItemID = null,
            [FromQuery] string ItemName = null,
            [FromQuery] string PartnerName = null,
            CancellationToken ct = default)      
        {
            var rows = await GetTransactionsAsync(FromDate, ToDate, TransactionTypeID, ItemID, ItemName, PartnerName, ct);
            return Ok(rows);
        }

        private void PayTransaction(decimal PaymentValue,int id)
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
            TransactionsService bllt = new TransactionsService(true);
            bllt.Update(TranCash);
            if (bllt.ErrorID==0)
            {
                bllt.CloseGlobalConnection();
                //this.DialogResult = DialogResult.OK;
               

            }
            else
            {
                bllt.CloseGlobalConnection();
            }
        }
        private List<TransactionsDetails> GetDetailsForPayment(Transactions ThisTran, int TranID, decimal PaymentValue)
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
            clsDet.ItemID = "Pagese Porosi Web";// CostumerID.ToString();
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

        private Transactions GetCashTransaction(int EmpID,int departmentID)
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
          
            cls.CompanyID = dep.CompanyID; // Kompania e cila meren nga depo lokale
                                           //-----------------------------------------------------------------------------

            cls.InvoiceNo = DateTime.Now.ToString("ddMMyyy_HH") + "_" + e.FirstName + " " + e.LastName + " - " + dep.DepartmentName;
            cls.TransactionNo = DateTime.Now.ToString("ddMMyyy_HH") + "_" + e.FirstName + " " + e.LastName + " - " + dep.DepartmentName;
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

        private Termins GetTermins(int EmpID,int DepartmentID)
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
            t.CashJournalPOSID = NewCashTransaction(EmpID,DepartmentID);
            t.EmployeeID = EmpID;
            t.DepartmentID = DepartmentID;

          //  t.DevicesColl = DeviceColl;
          
            return t;
        }
        private int NewCashTransaction(int empid,int departmentid)
        {
            int CashJournalPOSID = 0;
            Transactions t = GetCashTransaction(empid,departmentid);
            TransactionsService bllt = new TransactionsService(true);
            bllt.Insert(t,false);
            if (bllt.ErrorID ==0)
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

        //private void PrintInvoiceTotal(Transactions t, bool isSelectedPayment, int Mode, string POSUserName, string[] pr, string TableName, DataTable SDTable, List<ItemLocation> ILList, bool chkChecked)
        //{
        //    DataTable dtTransaction = new DataTable();
        //    dtTransaction.TableName = "Transaction";
        //    //TransactionNo
        //    DataColumn col = new DataColumn("TransactionNo", typeof(string));
        //    col.DefaultValue = t.TransactionNo;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("Value", typeof(decimal));
        //    col.DefaultValue = t.Value;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("VATValue", typeof(decimal));
        //    col.DefaultValue = t.VATValue;
        //    dtTransaction.Columns.Add(col);

        //    //TableName
        //    col = new DataColumn("TableName", typeof(string));
        //    col.DefaultValue = TableName;
        //    dtTransaction.Columns.Add(col);

        //    //isSelectedPayment
        //    col = new DataColumn("IsPayment", typeof(bool));
        //    col.DefaultValue = isSelectedPayment;
        //    dtTransaction.Columns.Add(col);

        //    //LUN
        //    col = new DataColumn("LUN", typeof(int));
        //    col.DefaultValue = t.LUN;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("VATPercent", typeof(decimal));
        //    col.DefaultValue = t.VATPercent;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("PartnerName", typeof(string));
        //    col.DefaultValue = t.PartnerName;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("TableID", typeof(string));
        //    col.DefaultValue = t.TableID;
        //    dtTransaction.Columns.Add(col);

        //    DataRow trow = dtTransaction.NewRow();
        //    dtTransaction.Rows.Add(trow);

        //    DataTable tdDetails = new DataTable();
        //    tdDetails.TableName = "Details";
        //    col = new DataColumn("ItemName", typeof(string));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Quantity", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("VATPrice", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Price", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("VATPriceValue", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Value", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("VATValue", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("SalesValue", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("LocationID", typeof(int));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("ItemID", typeof(string));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("DetailsMode", typeof(string));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("LocationID2", typeof(int));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Memo", typeof(string));
        //    tdDetails.Columns.Add(col);
        //    DataRow row;

        //    foreach (TransactionsDetails d in t.TranDetailsColl)
        //    {

        //        row = tdDetails.NewRow();
        //        row["ItemName"] = d.ItemName;
        //        row["Quantity"] = d.Quantity;
        //        row["VATPrice"] = d.VATPrice;
        //        row["Price"] = d.Price;
        //        row["Value"] = d.Value;
        //        row["VATPriceValue"] = d.VATPrice * d.Quantity;

        //        row["LocationID"] = d.LocationID;
        //        row["ItemID"] = d.ItemID;
        //        row["DetailsMode"] = d.Mode;
        //        row["LocationID2"] = d.LocationID2;
        //        row["Memo"] = d.Memo;
        //        tdDetails.Rows.Add(row);

        //    }
        //    //DataView dv = new DataView(tdDetails);
        //    //dv.Sort = "Memo";
        //    //DataTable dt1 = dv.ToTable();
        //    //string Memo="";
        //    //string itemName="";
        //    //DataTable details = tdDetails.Clone();


        //    //foreach (DataRow rw in dt1.Rows)
        //    //{
        //    //    Memo = rw["Memo"].ToString();
        //    //    itemName=Memo+"[";
        //    //    if (rw["Memo"] != "" && rw["Memo"] == Memo)
        //    //    {
        //    //        rw["ItemName"] += rw["Quantity"] + " " + rw["ItemName"] + ",";
        //    //    }
        //    //    else
        //    //    {

        //    //    }
        //    //}
        //    DataTable details = tdDetails.Clone();
        //    try
        //    {
        //        var distinctRows = tdDetails.DefaultView.ToTable(true, "Memo").Rows.OfType<DataRow>().Select(k => k[0] + "").ToArray();
        //        foreach (string name in distinctRows)
        //        {
        //            var rows = tdDetails.Select("Memo = '" + name + "' AND Memo <>''");
        //            string value = "";
        //            int i = 0;
        //            foreach (DataRow rw in rows)
        //            {
        //                if (rw["Memo"].ToString() != "")
        //                {
        //                    value += string.Format("{0:0.##}", Convert.ToDecimal(rw["Quantity"])) + "x " + rw["ItemName"] + ",";
        //                }
        //                else
        //                {
        //                    value = rw["ItemName"].ToString();
        //                }
        //                i++;
        //                if (i == rows.Length)
        //                {
        //                    if (rw["Memo"].ToString() != "")
        //                    {
        //                        rw["ItemName"] = name.Split('-')[0] + "[" + value.Trim(',') + "]";
        //                        rw["Quantity"] = 1;
        //                    }
        //                    else
        //                    {
        //                        rw["ItemName"] = rw["ItemName"];
        //                    }
        //                    details.ImportRow(rw);
        //                }
        //            }

        //            //value =name+"["+ value.Trim(',')+"]";
        //            //Users_GetLoginUserByPIN("Deri qetu mire");

        //            //details.Rows.Add(name, value);
        //            value = "";


        //        }
        //        foreach (DataRow rw in tdDetails.Rows)
        //        {
        //            if (rw["Memo"].ToString() == "")
        //            {
        //                details.ImportRow(rw);
        //            }
        //        }
        //        //Users_GetLoginUserByPIN("Numri i rreshtave te detajeve per printim: " + details.Rows.Count.ToString());
        //    }
        //    catch (Exception ex) { /*Users_GetLoginUserByPIN(ex.Message);*/ }


        //    PrintInvoice(t.ID, dtTransaction, details, Mode, POSUserName, pr, SDTable, ILList, chkChecked, true);
        //}

        bool UpdateSubOrderReceived = true;


        #region PrintInvoice

        //private void PrintInvoice(Transactions t, bool isSelectedPayment, int Mode, string POSUserName, string[] pr, string TableName, DataTable SDTable, List<ItemLocation> ILList, bool chkChecked, bool PrintALl)
        //{
        //    if (!printTermik)
        //        return;

        //    DataTable dtTransaction = new DataTable();
        //    dtTransaction.TableName = "Transaction";
        //    //TransactionNo
        //    DataColumn col = new DataColumn("TransactionNo", typeof(string));
        //    col.DefaultValue = t.TransactionNo;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("Value", typeof(decimal));
        //    col.DefaultValue = t.Value;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("VATValue", typeof(decimal));
        //    col.DefaultValue = t.VATValue;
        //    dtTransaction.Columns.Add(col);

        //    //TableName
        //    col = new DataColumn("TableName", typeof(string));
        //    col.DefaultValue = TableName;
        //    dtTransaction.Columns.Add(col);

        //    //isSelectedPayment
        //    col = new DataColumn("IsPayment", typeof(bool));
        //    col.DefaultValue = isSelectedPayment;
        //    dtTransaction.Columns.Add(col);

        //    //LUN
        //    col = new DataColumn("LUN", typeof(int));
        //    col.DefaultValue = t.LUN;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("VATPercent", typeof(decimal));
        //    col.DefaultValue = t.VATPercent;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("PartnerName", typeof(string));
        //    col.DefaultValue = t.PartnerName;
        //    dtTransaction.Columns.Add(col);

        //    col = new DataColumn("TableID", typeof(string));
        //    col.DefaultValue = t.TableID;
        //    dtTransaction.Columns.Add(col);

        //    DataRow trow = dtTransaction.NewRow();
        //    dtTransaction.Rows.Add(trow);

        //    DataTable tdDetails = new DataTable();
        //    tdDetails.TableName = "Details";
        //    col = new DataColumn("ItemName", typeof(string));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Quantity", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("VATPrice", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Price", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("VATPriceValue", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Value", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("VATValue", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("SalesValue", typeof(decimal));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("LocationID", typeof(int));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("ItemID", typeof(string));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("DetailsMode", typeof(string));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("LocationID2", typeof(int));
        //    tdDetails.Columns.Add(col);

        //    col = new DataColumn("Memo", typeof(string));
        //    tdDetails.Columns.Add(col);
        //    DataRow row;

        //    foreach (TransactionsDetails d in t.TranDetailsColl)
        //    {
        //        if (isSelectedPayment || d.Mode == 1 || OptionsData.UseSubOrders)
        //        {
        //            String memo = "";
        //            if (!String.IsNullOrEmpty(d.Memo))
        //            {
        //                memo = " [" + d.Memo + "]";
        //            }

        //            row = tdDetails.NewRow();
        //            row["ItemName"] = d.ItemName + memo;
        //            row["Quantity"] = d.Quantity;
        //            row["VATPrice"] = d.VATPrice;
        //            row["Price"] = d.Price;
        //            row["Value"] = d.Value;
        //            row["VATPriceValue"] = d.VATPrice * d.Quantity;

        //            row["LocationID"] = d.LocationID;
        //            row["ItemID"] = d.ItemID;
        //            row["DetailsMode"] = d.Mode;
        //            row["LocationID2"] = d.LocationID2;
        //            row["Memo"] = d.Memo;
        //            tdDetails.Rows.Add(row);
        //        }
        //    }
        //    //DataView dv = new DataView(tdDetails);
        //    //dv.Sort = "Memo";
        //    //DataTable dt1 = dv.ToTable();
        //    //string Memo="";
        //    //string itemName="";
        //    //DataTable details = tdDetails.Clone();


        //    //foreach (DataRow rw in dt1.Rows)
        //    //{
        //    //    Memo = rw["Memo"].ToString();
        //    //    itemName=Memo+"[";
        //    //    if (rw["Memo"] != "" && rw["Memo"] == Memo)
        //    //    {
        //    //        rw["ItemName"] += rw["Quantity"] + " " + rw["ItemName"] + ",";
        //    //    }
        //    //    else
        //    //    {

        //    //    }
        //    //}
        //    //  DataTable details = tdDetails.Clone();
        //    //try
        //    //{
        //    //    var distinctRows = tdDetails.DefaultView.ToTable(true, "Memo").Rows.OfType<DataRow>().Select(k => k[0] + "").ToArray();
        //    //    foreach (string name in distinctRows)
        //    //    {
        //    //        var rows = tdDetails.Select("Memo = '" + name + "' AND Memo <>''");
        //    //        string value = "";
        //    //        int i = 0;
        //    //        foreach (DataRow rw in rows)
        //    //        {
        //    //            if (rw["Memo"].ToString() != "")
        //    //            {
        //    //                value += string.Format("{0:0.##}", Convert.ToDecimal(rw["Quantity"])) + "x " + rw["ItemName"] + ",";
        //    //            }
        //    //            else
        //    //            {
        //    //                value = rw["ItemName"].ToString();
        //    //            }
        //    //            i++;
        //    //            if (i == rows.Length)
        //    //            {
        //    //                if (rw["Memo"].ToString() != "")
        //    //                {
        //    //                    rw["ItemName"] = name.Split('-')[0] + "[" + value.Trim(',') + "]";
        //    //                    rw["Quantity"] = 1;
        //    //                }
        //    //                else
        //    //                {
        //    //                    rw["ItemName"] = rw["ItemName"];
        //    //                }
        //    //                details.ImportRow(rw);
        //    //            }
        //    //        }

        //    //        //value =name+"["+ value.Trim(',')+"]";
        //    //        //Users_GetLoginUserByPIN("Deri qetu mire");

        //    //        //details.Rows.Add(name, value);
        //    //        value = "";


        //    //    }
        //    //    foreach (DataRow rw in tdDetails.Rows)
        //    //    {
        //    //        if (rw["Memo"].ToString() == "")
        //    //        {
        //    //            details.ImportRow(rw);
        //    //        }
        //    //    }
        //    //    //Users_GetLoginUserByPIN("Numri i rreshtave te detajeve per printim: " + details.Rows.Count.ToString());
        //    //}
        //    //catch (Exception ex) { /*Users_GetLoginUserByPIN(ex.Message);*/ }


        //    PrintInvoice(t.ID, dtTransaction, tdDetails, Mode, POSUserName, pr, SDTable, ILList, chkChecked, PrintALl);
        //}

        #endregion

        #region RaportetExtremPica
        //public void PrintInvoice(int TranID, DataTable t, DataTable td, int Mode, string UserName, string[] printers, DataTable SDTable, List<ItemLocation> ILList, bool chkChecked, bool PrintAll)
        //{
        //    if (!printTermik)
        //    {
        //        return;
        //    }

        //    WriteLog("PrintInvoice");
        //    //Users_GetLoginUserByPIN("u thirr PrintInvoice ");

        //    try
        //    {
        //        if (!OptionsData.UseSubOrders || 1 == 1)
        //        {
        //            WriteLog("usesuborders");

        //            string PrinterDefault = "0";

        //            try
        //            {
        //                PrinterDefault = _configuration["AppSettings:PrinterDefault"] ?? "0";

        //            }
        //            catch { PrinterDefault = "0"; }
        //            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "ReportDoc");
        //            ReportDocument rptc = new ReportDocument();
        //            if (OptionsData.POSVAT != 0 && bool.Parse(t.Rows[0]["IsPayment"].ToString()))
        //            {
        //                WriteLog("PrinterDefault" + PrinterDefault);

        //                if (PrinterDefault == "0")
        //                {
        //                    string invoiceReportPath = Path.Combine(reportPath, "rptInvoiceWithVAT.rpt");
        //                    rptc.Load(invoiceReportPath);
        //                    // rptc = new rptInvoiceWithVAT();
        //                }
        //                else
        //                {
        //                    string invoiceReportPath = Path.Combine(reportPath, "rptInvoiceWithVAT2.rpt");
        //                    rptc.Load(invoiceReportPath);
        //                    // rptc = new rptInvoiceWithVAT2();
        //                }
        //                ReportFactory.GetReportDoc(rptc.GetType());
        //                try
        //                {
        //                    TextObject txtPartner = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtPartner"]);
        //                    txtPartner.Text = t.Rows[0]["PartnerName"].ToString();
        //                    TextObject lblPartner = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["lblPartner"]);
        //                    lblPartner.Width = t.Rows[0]["PartnerName"].ToString() == "" ? 0 : txtPartner.Width;

        //                }
        //                catch (Exception ex)
        //                {
        //                    WriteLog("1355" + ex.Message);

        //                }


        //            }
        //            else
        //            {
        //                if (PrinterDefault == "0")
        //                {
        //                    rptc.Load(Path.Combine(reportPath, "rptInvoice.rpt"));
        //                    //  rptc = new rptInvoice();
        //                }
        //                else
        //                {
        //                    rptc.Load(Path.Combine(reportPath, "rptInvoice1.rpt"));

        //                    // rptc = new rptInvoice1();
        //                }
        //                ReportFactory.GetReportDoc(rptc.GetType());
        //            }
        //            DataTable dt1 = SDTable;
        //            DataTable dt2 = t;
        //            DataTable dt3 = td;

        //            decimal Value = 0;
        //            decimal VATValue = 0;

        //            foreach (DataRow row1 in dt3.Rows)
        //            {
        //                Value += Convert.ToDecimal(row1["Price"].ToString()) * Convert.ToDecimal(row1["Quantity"].ToString());
        //                VATValue += (Convert.ToDecimal(row1["VATPrice"].ToString()) - Convert.ToDecimal(row1["Price"].ToString())) * Convert.ToDecimal(row1["Quantity"].ToString());
        //            }

        //            dt2.Rows[0]["Value"] = Value;
        //            dt2.Rows[0]["VATValue"] = VATValue;

        //            dt3.AcceptChanges();

        //            if (dt3.Rows.Count != 0 && !OptionsData.UseSubOrders)
        //            {
        //                rptc.Database.Tables["spSystemList;1"].SetDataSource(dt1);
        //                rptc.Database.Tables["spTransactionsByID;1"].SetDataSource(dt2);
        //                rptc.Database.Tables["spTransactionsDetailsByID;1"].SetDataSource(dt3);



        //                TextObject txtUserName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtUserName"]);
        //                txtUserName.Text = UserName;
        //                TextObject txtTableName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtTableName"]);

        //                txtTableName.Text = t.Rows[0]["TableName"].ToString();
        //                FieldObject sfTime = ((FieldObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["sfTime"]);

        //                if (!OptionsData.ShowTimeAtPOSInvoice)
        //                {
        //                    sfTime.Width = 0;
        //                }

        //                TextObject Fatura = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //                TextObject OrderNo = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtOrderNo"]);
        //                TextObject txtTranNo = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtTranNo"]);
        //                TextObject Copy = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Copy"]);





        //                DataRow[] dtr = dt2.Select();
        //                if (!Convert.ToBoolean(t.Rows[0]["IsPayment"].ToString()))
        //                {

        //                    if (Mode == 1)
        //                    {
        //                        OrderNo.Text = "/1";
        //                    }
        //                    else
        //                    {
        //                        OrderNo.Text = "/" + (int.Parse(dtr[0]["LUN"].ToString()) + 1).ToString();
        //                    }
        //                    txtTranNo.Text = "Nr. Porosisë:";
        //                    Fatura.Text = "POROSI";
        //                    Copy.Width = 0;

        //                }
        //                else
        //                {
        //                    Fatura.Text = "FATURË";
        //                    txtTranNo.Text = "Nr. Faturës:";
        //                    OrderNo.Text = "";
        //                    Copy.Width = 0;
        //                }



        //                // rastet kur perfshihet edhe printeri 1 mi shtyp krejt rreshtat
        //                string PrintAllOrdersInPlaceOfInvoice = "0";
        //                try
        //                {
        //                    PrintAllOrdersInPlaceOfInvoice = System.Configuration.ConfigurationManager.AppSettings["PrintAllOrdersInPlaceOfInvoice"];
        //                }
        //                catch { PrintAllOrdersInPlaceOfInvoice = "0"; }






        //                try
        //                {
        //                    if (printers[0] != string.Empty)
        //                    {
        //                        if (Fatura.Text != "POROSI" && PrintAllOrdersInPlaceOfInvoice == "0")
        //                        {
        //                            WriteLog("Printeri 0 ne printinvoice");

        //                            rptc.PrintOptions.PrinterName = printers[0];
        //                            rptc.PrintToPrinter(1, true, 0, 0);
        //                            // GetServerTime();
        //                        }

        //                    }
        //                }
        //                catch (Exception ex)
        //                {

        //                    WriteLog("1477" + ex.Message);
        //                }

        //                // Per porosi neper lokacione
        //                if (Mode == 1)
        //                {
        //                    OrderNo.Text = "/1";
        //                }
        //                else
        //                {
        //                    OrderNo.Text = "/" + (int.Parse(dtr[0]["LUN"].ToString()) + 1).ToString();
        //                }

        //                txtTranNo.Text = "Nr. Porosisë:";
        //                Fatura.Text = "POROSI";
        //                Copy.Width = 0;

        //                string[] args = { txtUserName.Text, txtTableName.Text, Fatura.Text, OrderNo.Text, txtTranNo.Text };

        //                try
        //                {

        //                    AllocateOrder(GetReportClassForLocation(args, dt1), dt2, dt3, printers, ILList, TranID, PrintAll);

        //                }
        //                catch (Exception ex)
        //                {
        //                    WriteLog(ex.Message);
        //                }
        //                //AllocateOrder(rptc, dt2, dt3, printers);
        //                rptc.Close();
        //                rptc.Dispose();
        //            }
        //        }
        //        else
        //        {
        //            #region SubOrders
        //            ReportClass rptc = new ReportClass();

        //            //decimal Value;
        //            DataTable Tab;
        //            string LocationName = "";
        //            Tab = new DataTable();
        //            Tab = t.Clone();
        //            DataTable dtShank;
        //            DataTable dtKuzhina;
        //            DataRow[] rows1 = null;
        //            DataRow[] rows2 = null;

        //            rows1 = td.Select("LocationID<>2 AND DetailsMode=1");
        //            rows2 = td.Select("LocationID=2");

        //            dtShank = td.Clone();
        //            dtKuzhina = td.Clone();
        //            if (rows1 != null && rows1.Length > 0)
        //            {
        //                foreach (DataRow item in rows1)
        //                {
        //                    dtShank.ImportRow(item);
        //                }
        //            }
        //            if (rows2 != null && rows2.Length > 0)
        //            {
        //                foreach (DataRow item in rows2)
        //                {
        //                    dtKuzhina.ImportRow(item);
        //                }
        //            }
        //            //Users_GetLoginUserByPIN(dtShank.Rows.Count.ToString());
        //            //Users_GetLoginUserByPIN(dtKuzhina.Rows.Count.ToString());
        //            if (dtShank.Rows.Count > 0)
        //            {
        //                WriteLog("Shank");

        //                ItemLocation il = new ItemLocation();
        //                il.ID = 1;
        //                il = DALItemLocation.SelectByID(il);
        //                //Fatura.Text = FatName + "\n( " + il.Name + " )";
        //                DataTable dt1 = SDTable;//BLLSystemData.ListSystemDataTable();
        //                DataTable dt2 = BLLTransactions.SelectDataTableByID(TranID);

        //                DataTable dt3 = dtShank;
        //                rptc = new rptInvoiceSubOrderShank();
        //                ReportFactory.GetReportDoc(rptc.GetType());
        //                try
        //                {
        //                    TextObject Fatura = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //                    string FatName = Fatura.Text;
        //                    TextObject Fatura1 = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //                    Fatura1.Text = FatName + "\n( " + il.Name + " )";
        //                    TextObject txtUserName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtUserName"]);
        //                    txtUserName.Text = GlobalAppData.POSUserName;
        //                    TextObject txtTableName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtTableName"]);
        //                    //  BELayer.Tables tb = new BELayer.Tables();
        //                    //  tb.ID = Convert.ToInt32(dt2.Rows[0]["TableID"].ToString());
        //                    txtTableName.Text = "table";// BLLTables.SelectByID(tb).TableName;
        //                }
        //                catch
        //                {

        //                }


        //                rptc.Database.Tables["spSystemList;1"].SetDataSource(dt1);
        //                rptc.Database.Tables["spTransactionsByID;1"].SetDataSource(dt2);

        //                rptc.Database.Tables["spTransactionsDetailsByID;1"].SetDataSource(dtShank);

        //                if (printers[2] != "" && dt3.Rows.Count > 0)
        //                {
        //                    String print = printers[2];
        //                    rptc.PrintOptions.PrinterName = print; //printers[2].ToString();
        //                                                           //CommonApp.SetDefaultPrinter(FINA.Properties.Settings.Default.Printer2);
        //                    rptc.PrintToPrinter(1, true, 0, 0);
        //                }
        //            }
        //            if (dtKuzhina.Rows.Count > 0)
        //            {
        //                WriteLog("Kuzhina");

        //                int LastSuborderID = BLLTransactionsDetails.GetLastSubOrder(TranID);

        //                ItemLocation il = new ItemLocation();
        //                il.ID = 2;
        //                il = DALItemLocation.SelectByID(il);

        //                DataTable dt1 = SDTable;//BLLSystemData.ListSystemDataTable();


        //                DataTable dt2 = BLLTransactions.SelectDataTableByID(TranID);

        //                PrintAll = PrintAll && !(TranID > 0 && LastSuborderID > 1);//!chkPrintoNenporosine.Visible;
        //                DataTable dt3 = null;
        //                try
        //                {

        //                    dt3 = BLLTransactionsDetails.GetDetailsReportSubOrder(TranID, PrintAll);

        //                }
        //                catch (Exception ex)
        //                {
        //                    //Users_GetLoginUserByPIN(ex.Message);
        //                }


        //                if (!PrintAll || (dt3.Rows.Count > 0 && Convert.ToInt32(dt3.Rows[0]["SubOrderID"].ToString()) > 1) && Convert.ToBoolean(dt3.Rows[0]["Printed"]) == true)
        //                {

        //                    rptc = new rptInvoiceSubOrder2();
        //                }
        //                else if (PrintAll || (dt3.Rows.Count > 0 && Convert.ToInt32(dt3.Rows[0]["SubOrderID"].ToString()) > 1))
        //                {

        //                    rptc = new rptInvoiceSubOrder();
        //                }
        //                TextObject Fatura = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //                string FatName = Fatura.Text;
        //                TextObject Fatura1 = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //                if (PrintAll)
        //                {
        //                    Fatura1.Text = FatName + "\n( " + il.Name + " )";
        //                }
        //                else
        //                {

        //                    if (dt3.Rows.Count > 0 && (TranID > 0 && LastSuborderID > 1) && Convert.ToBoolean(dt3.Rows[0]["Printed"]) == true)
        //                    {
        //                        Fatura1.Text = "Nënporosia " + LastSuborderID + " " + "\n( " + il.Name + " )";
        //                    }
        //                }

        //                TextObject txtUserName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtUserName"]);
        //                txtUserName.Text = GlobalAppData.POSUserName;
        //                TextObject txtTableName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtTableName"]);
        //                //   BELayer.Tables tb = new BELayer.Tables();
        //                // tb.ID = Convert.ToInt32(dt2.Rows[0]["TableID"].ToString());
        //                txtTableName.Text = "table";// BLLTables.SelectByID(tb).TableName;
        //                rptc.Database.Tables["spSystemList;1"].SetDataSource(dt1);
        //                rptc.Database.Tables["spTransactionsByID;1"].SetDataSource(dt2);

        //                rptc.Database.Tables["spTransactionsDetailsByID;1"].SetDataSource(dt3);

        //                if (printers[3] != "")
        //                {

        //                    if (chkChecked || !(TranID > 0 && LastSuborderID > 1))
        //                    {
        //                        try
        //                        {
        //                            rptc.PrintOptions.PrinterName = printers[3];
        //                            DataTable dt4 = BLLTransactionsDetails.GetDetailsReportSubOrder(TranID, PrintAll);
        //                            //CommonApp.SetDefaultPrinter(FINA.Properties.Settings.Default.Printer3);
        //                            if (dt4.Rows.Count > 0)
        //                            {
        //                                WriteLog(printers[3]);

        //                                rptc.PrintToPrinter(1, true, 0, 0);
        //                                if (printers[2] != "")
        //                                {
        //                                    WriteLog(printers[2]);

        //                                    rptc.PrintOptions.PrinterName = printers[2];
        //                                    //CommonApp.SetDefaultPrinter(FINA.Properties.Settings.Default.Printer2);
        //                                    rptc.PrintToPrinter(1, true, 0, 0);
        //                                }
        //                            }

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            WriteLog("1683" + ex.Message);

        //                        }
        //                    }
        //                    else if ((TranID > 0 && LastSuborderID > 1) && !chkChecked)
        //                    {
        //                        rptc = new rptInvoiceSubOrder2();
        //                        DataTable dtt1 = DALGlobal.ListSystemDataTable();
        //                        DataTable dtt2 = BLLTransactions.SelectDataTableByID(TranID);
        //                        DataTable dtt3 = BLLTransactionsDetails.GetUnprintedDetails(TranID);


        //                        rptc.Database.Tables["spSystemList;1"].SetDataSource(dtt1);
        //                        rptc.Database.Tables["spTransactionsByID;1"].SetDataSource(dtt2);

        //                        rptc.Database.Tables["spTransactionsDetailsByID;1"].SetDataSource(dtt3);
        //                        if (dtt3.Rows.Count > 0)
        //                        {
        //                            TextObject Fatura11 = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);

        //                            Fatura11.Text = FatName + "\n( " + il.Name + " )";
        //                            WriteLog("1704" + printers[3]);

        //                            rptc.PrintOptions.PrinterName = printers[3];

        //                            //CommonApp.SetDefaultPrinter(FINA.Properties.Settings.Default.Printer3);

        //                            rptc.PrintToPrinter(1, true, 0, 0);

        //                            try
        //                            {
        //                                if (printers[2] != "")
        //                                {

        //                                    rptc.PrintOptions.PrinterName = printers[2];
        //                                    //CommonApp.SetDefaultPrinter(FINA.Properties.Settings.Default.Printer2);
        //                                    if (dt3.Rows.Count > 0)
        //                                    {
        //                                        rptc.PrintToPrinter(1, true, 0, 0);
        //                                    }
        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                WriteLog("1726" + ex.Message);

        //                            }
        //                            BLLTransactionsDetails.MakeTransactionPrinted(TranID);
        //                        }
        //                    }
        //                }

        //            }
        //            UpdateSubOrderReceived = !(TranID > 0 && BLLTransactionsDetails.GetLastSubOrder(TranID) > 1);
        //            //if ((TranID > 0 && BLLTransactionsDetails.GetLastSubOrder(TranID) > 1))
        //            //{

        //            //}
        //            //Users_GetLoginUserByPIN(UpdateSubOrderReceived.ToString() + "," + chkChecked.ToString());
        //            if (OptionsData.UseSubOrders && (UpdateSubOrderReceived || chkChecked))
        //            {
        //                BLLTransactionsDetails.MakeSubOrderReceieved(TranID);
        //            }

        //        }

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("PRINT INVOICE:" + ex.Message);
        //    }


        //}
        //private ReportDocument GetReportClassForLocation(string[] args, DataTable dt1)
        //{
        //    string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportDoc");
        //    string PrinterDefault = "0";
        //    try
        //    {
        //        PrinterDefault = System.Configuration.ConfigurationManager.AppSettings["PrinterDefault"];
        //    }
        //    catch { PrinterDefault = "0"; }

        //    ReportDocument rptc = new ReportDocument();
        //    if (PrinterDefault == "0")
        //    {
        //        rptc.Load(Path.Combine(reportPath, "rptInvoice.rpt"));
        //        // rptc = new rptInvoice();
        //    }
        //    else
        //    {
        //        rptc.Load(Path.Combine(reportPath, "rptInvoice1.rpt"));

        //        //rptc = new rptInvoice1();
        //    }

        //    ReportFactory.GetReportDoc(rptc.GetType());


        //    rptc.Database.Tables["spSystemList;1"].SetDataSource(dt1);

        //    TextObject txtUserName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtUserName"]);
        //    txtUserName.Text = args[0];
        //    TextObject txtTableName = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtTableName"]);
        //    txtTableName.Text = args[1];


        //    FieldObject Quantity = ((FieldObject)rptc.ReportDefinition.Sections["Section3"].ReportObjects["Quantity1"]);

        //    Quantity.FieldFormat.NumericFormat.DecimalPlaces = 2;
        //    Quantity.FieldFormat.NumericFormat.RoundingFormat = CrystalDecisions.Shared.RoundingFormat.RoundToTenth;


        //    TextObject Copy = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Copy"]);
        //    TextObject Fatura = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //    TextObject OrderNo = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtOrderNo"]);
        //    TextObject txtTranNo = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["txtTranNo"]);
        //    FieldObject sfTime = ((FieldObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["sfTime"]);

        //    Copy.Width = 0;
        //    Fatura.Text = args[2];
        //    OrderNo.Text = args[3];
        //    txtTranNo.Text = args[4];
        //    if (!OptionsData.ShowTimeAtPOSInvoice)
        //    {
        //        sfTime.Width = 0;
        //    }



        //    return rptc;
        //}
        //private void AllocateOrder(ReportDocument rptc, DataTable tran, DataTable table, string[] printers, List<ItemLocation> IList, int TranID, bool PrintAll)
        //{

        //    WriteLog("AllocateOrder");

        //    try
        //    {
        //        // Users_GetLoginUserByPIN("u thirr allocateorder ");

        //        TextObject Fatura = ((TextObject)rptc.ReportDefinition.Sections["Section1"].ReportObjects["Fatura"]);
        //        string FatName = Fatura.Text;
        //        decimal Value;
        //        DataTable Tab;
        //        string LocationName = "";

        //        string PrintAllOrdersInPlaceOfInvoice = "0";


        //        //GM
        //        for (int i = 0; i < printers.Length; i++)
        //        {

        //            Tab = new DataTable();
        //            Tab = table.Clone();

        //            DataRow[] rows = null;

        //            if (i == 0)
        //            {

        //                try
        //                {
        //                    PrintAllOrdersInPlaceOfInvoice = System.Configuration.ConfigurationManager.AppSettings["PrintAllOrdersInPlaceOfInvoice"];
        //                }
        //                catch { PrintAllOrdersInPlaceOfInvoice = "0"; }


        //                // Users_GetLoginUserByPIN("PrintAllOrdersInPlaceOfInvoice:" + PrintAllOrdersInPlaceOfInvoice);

        //                if (PrintAllOrdersInPlaceOfInvoice == "1")
        //                {

        //                    rows = table.Select("LocationID > 0 OR LocationID2 > 0");


        //                }
        //                else
        //                {

        //                    rows = table.Select("LocationID = -1");

        //                }
        //                //////
        //            }
        //            else
        //            {
        //                rows = table.Select("LocationID = " + i + " OR LocationID2 =" + i);

        //            }

        //            //
        //            //Users_GetLoginUserByPIN("rows.length" + i + ":" + rows.Length);
        //            if (rows.Length > 0)
        //            {

        //                Value = 0;
        //                foreach (DataRow row1 in rows)
        //                {
        //                    Tab.ImportRow(row1);
        //                    Value += Convert.ToDecimal(row1["VATPrice"].ToString()) * Convert.ToDecimal(row1["Quantity"].ToString());
        //                }
        //                Tab.AcceptChanges();

        //                tran.Rows[0]["Value"] = (100 * Value) / (100 + OptionsData.POSVAT);
        //                tran.Rows[0]["VATValue"] = OptionsData.POSVAT * Value / (100 + OptionsData.POSVAT);

        //                /// per rastin e shtypjes te te gjitha porosive ne nje vend mirret teksti "te gjitha"
        //                if (i != 0)
        //                {
        //                    ItemLocation il = (from ii in IList
        //                                       where ii.ID.ToString() == i.ToString()
        //                                       select ii).FirstOrDefault();

        //                    LocationName = il.Name;
        //                }
        //                else
        //                {
        //                    LocationName = "Te Gjitha";
        //                }
        //                //////

        //                Fatura.Text = FatName + "\n( " + LocationName + " )";

        //                rptc.Database.Tables["spTransactionsByID;1"].SetDataSource(tran);
        //                rptc.Database.Tables["spTransactionsDetailsByID;1"].SetDataSource(Tab);

        //                try
        //                {
        //                    if (PrintAll)
        //                    {
        //                        if (printers[0] != string.Empty)
        //                        {
        //                            // Users_GetLoginUserByPIN("Printeri: " + printers[i]);
        //                            rptc.PrintOptions.PrinterName = printers[0];
        //                            rptc.PrintToPrinter(1, true, 0, 0);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (printers[i] != string.Empty)
        //                        {
        //                            WriteLog(printers[i]);
        //                            //Users_GetLoginUserByPIN("Printeri" + i +": " + printers[i]);
        //                            rptc.PrintOptions.PrinterName = printers[i];
        //                            rptc.PrintToPrinter(1, true, 0, 0);
        //                        }
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    WriteLog("1928" + e.Message);
        //                    //Users u = Users_GetLoginUserByPIN(e.Message);
        //                }
        //            }

        //        }

        //        rptc.Dispose();
        //    }
        //    catch (Exception ex) { /*Users_GetLoginUserByPIN("error: " + ex.Message);*/ }


        //}

        #endregion

        private List<Orders> GetOrders(string FromDate, string ToDate)
        {
            return _dbAccess.OrdersList(FromDate, ToDate);
        }


        private Task<List<Orders>> GetTransactionsAsync(string fromDate, string toDate, int tranTypeID, string itemID = null, string itemName = null, string partnerName = null, CancellationToken ct = default)
        {
            return _dbAccess.GetTransactions(fromDate, toDate, tranTypeID, itemID, itemName, partnerName, ct);
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
                    cls.TransactionNo = DateTime.Now.ToString("ddMMyyy_HH") + " - " + dep.DepartmentName;
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

                cls.TransactionNo = _dbAccess.GetTransactionNo(t.TransactionType, t.TransactionDate, t.DepartmentID).ToString();

            }
            else
            {
                cls.TransactionNo = t.TransactionNo;
            }
            //}

            cls.PDAInsDate = t.TransactionDate;
            if (t.InvoiceNo == "" || t.InvoiceNo == null)
            {

                cls.InvoiceNo = _dbAccess.GetTransactionNo(t.TransactionType, t.TransactionDate, t.DepartmentID).ToString();

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
                    cls.InternalDepartmentID = Convert.ToInt32(Math.Truncate(t.PaymentValue));// Convert.ToInt32(t.PaymentValue.ToString());
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
            cls.EmpID = 0;// e.ID;
            cls.CompanyID = dep.CompanyID;
            cls.JournalStatus = false;
            cls.Longitude = t.Longitude;
            cls.Latitude = t.Latitude;
            cls.BL = t.BL;



            cls.ServiceType = t.ServiceTypeID;

            //Users_GetLoginUserByPIN("_3");

            return cls;
        }

        private List<ItemsLookup> ItemList;

        [HttpGet("options")]
        public void GetOptions()
        {
            OptionsRepository.GetOptions();
        }
        private List<TransactionsDetails> GetTranDetails(XMLTransactions t)
        {
            ItemsLookup Item = new ItemsLookup();

            List<TransactionsDetails> tranDetails = new List<TransactionsDetails>();
            TransactionsDetails clsDet;

            //Mbush artikujt;
            if (ItemList == null)
            {
                try
                {
                    ItemList = _dbAccess.GetItemsForPOS(0, t.DepartmentID);  // Me ndrru logjiken 
                }
                catch { }
            }

            foreach (XMLTransactionDetails row in t.Details)
            {
                decimal VatValue = 18;

                try
                {
                    //Users_GetLoginUserByPIN("Item Name para :" + row.ItemName);
                    Item = (from i in ItemList
                            where i.ItemID == row.ItemID
                            select i).FirstOrDefault<ItemsLookup>();
                    VatValue = Item.VATValue;
                }
                catch
                {

                }

                if(Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    try
                    {
                        Item = (from i in ItemList
                             where i.ItemName.ToLower().Equals(row.ItemName.ToLower())
                               select i
                               ).FirstOrDefault<ItemsLookup>();
                    }
                    catch { }
                }

                if(Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    continue;
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
                    clsDet.Contracts = row.Rabat.ToString(); ;
                }
                else
                {
                    clsDet.Contracts = "";
                }

                clsDet.TransactionID = t.ID;
                if (t.TransactionType != 42)
                {
                    clsDet.DetailsType = 1;// menaxhohet ndryshe per pagesa!!!
                }
                else
                {
                    clsDet.DetailsType = 2;
                }
                clsDet.ItemID = row.ItemID;

                clsDet.ItemName = row.ItemName;

                if (t.TransactionType != 36)
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = 0;//(row.Price * 100) / (100 + vpc.VATPercents);
                }
                else
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = row.Price;
                }


                //VATPercent vpc = new VATPercent();
                //vpc.VATID = t.VATPrecentID;
                //vpc = BLLVATPercent.SelectByID(vpc);

                clsDet.CostPrice = (row.Price * 100) / (100 + VatValue);//vpc.VATPercents);
                clsDet.Value = (((row.Price * (1.0M - row.Rabat / 100.0M) * (1.0M - row.Rabat2 / 100.0M)) * 100) / (100 + VatValue)) * clsDet.Quantity;  //row.Price * row.Quantity;1.
                clsDet.ProjectID = 0;
                clsDet.Mode = 1;
                clsDet.Discount = row.Rabat;
                clsDet.Discount2 = row.Rabat2;
                clsDet.Discount3 = row.Rabat3;
                clsDet.PriceWithDiscount = ((row.Price * (1.0M - row.Rabat / 100.0M) * (1.0M - row.Rabat2 / 100.0M) * (1.0M - row.Rabat3 / 100.0M)) * 100) / (100 + VatValue);
                if (t.TransactionType == 42)
                {
                    clsDet.Price = clsDet.PriceWithDiscount;
                }
                clsDet.VATPrice = row.Price * (1.0M - row.Rabat / 100.0M) * (1.0M - row.Rabat2 / 100.0M) * (1.0M - row.Rabat3 / 100.0M);
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
            ItemsLookup Item = new ItemsLookup();

            List<TransactionsDetails> tranDetails = new List<TransactionsDetails>();
            TransactionsDetails clsDet;

            //Mbush artikujt;
            if (ItemList == null)
            {
                try
                {
                    ItemList = _dbAccess.GetItemsForPOS(0, t.DepartmentID);  // Me ndrru logjiken 
                }
                catch { }
            }

            foreach (TransactionsDetails row in t.TranDetailsColl)
            {
                decimal VatValue = 18;

                try
                {
                    //Users_GetLoginUserByPIN("Item Name para :" + row.ItemName);
                    Item = (from i in ItemList
                            where i.ItemID == row.ItemID
                            select i).FirstOrDefault<ItemsLookup>();
                    VatValue = Item.VATValue;
                }
                catch
                {

                }

                if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    try
                    {
                        Item = (from i in ItemList
                                where i.ItemName.ToLower().Equals(row.ItemName.ToLower())
                                select i
                               ).FirstOrDefault<ItemsLookup>();
                    }
                    catch { }
                }

                if (Item == null || String.IsNullOrEmpty(Item.ItemID))
                {
                    continue;
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
                    clsDet.Contracts = row.Discount.ToString(); ;
                }
                else
                {
                    clsDet.Contracts = "";
                }

                clsDet.TransactionID = t.ID;
                if (t.TransactionTypeID != 42)
                {
                    clsDet.DetailsType = 1;// menaxhohet ndryshe per pagesa!!!
                }
                else
                {
                    clsDet.DetailsType = 2;
                }
                clsDet.ItemID = Item.ItemID;

                clsDet.ItemName = row.ItemName;

                if (t.TransactionTypeID != 36)
                {
                    clsDet.Quantity = row.Quantity;
                    clsDet.Price = 0;//(row.Price * 100) / (100 + vpc.VATPercents);
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
                clsDet.Value = (((row.Price * (1.0M - row.Discount / 100.0M) * (1.0M - row.Discount2 / 100.0M)) * 100) / (100 + VatValue)) * clsDet.Quantity;  //row.Price * row.Quantity;1.
                clsDet.ProjectID = 0;
                clsDet.Mode = 1;
                clsDet.Discount = row.Discount;
                clsDet.Discount2 = row.Discount2;
                clsDet.Discount3 = row.Discount3;
                clsDet.PriceWithDiscount = ((row.Price * (1.0M - row.Discount / 100.0M) * (1.0M - row.Discount / 100.0M) * (1.0M - row.Discount / 100.0M)) * 100) / (100 + VatValue);
                if (t.TransactionTypeID == 42)
                {
                    clsDet.Price = clsDet.PriceWithDiscount;
                }
                clsDet.VATPrice = row.Price * (1.0M - row.Discount / 100.0M) * (1.0M - row.Discount2 / 100.0M) * (1.0M - row.Discount3 / 100.0M);
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
                decValue[0] += row.PriceWithDiscount * row.Quantity;//Value
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

            int BankJournalID = _dbAccess.CashJournalIDByCashAccount(cashaccount, 24, DateTime.Now, Dep.CompanyID);

            if (BankJournalID == 0)
            {

                Transactions t = GetBankTransactionForJournal(cashaccount, Dep.CompanyID);
                TransactionsService bllt = new TransactionsService(true);
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

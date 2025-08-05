using AutoBit_WebInvoices.Models;
using Finabit_API.Models;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace FinabitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DBAccess _dbAccess;

        public ItemsController(IConfiguration configuration, DBAccess dbAccess)
        {
            _configuration = configuration;
            _dbAccess = dbAccess;
        }

        [HttpGet("LoadItems")]
        public ActionResult<List<XMLItems>> LoadItems([FromQuery] int DepartmentID)
        {
            return _dbAccess.M_GetItemsStateList(DepartmentID);
        }


        //AutoJehona
        [HttpPost("GetItems")]
        public ActionResult<List<Items2>> GetItems([FromBody] List<Items> c)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable
            DataTable dt = new DataTable();
            dt.TableName = "Items_API";
            dt.Columns.Add("Shifra", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Shifra_prodhuesit", typeof(string));
            dt.AcceptChanges();


            foreach (var item in c)
            {
               
                    DataRow row = dt.NewRow();
                    row[0] = item.Shifra;
                    row[1] = item.Shifra_prodhuesit;
                
                    dt.Rows.Add(row);
               
            }

            list = _dbAccess.GetItems_API(dt,"");
            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLocations = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if(item.Shifra == item2.Shifra)
                    {
                        if(item2.Sasia >= 0)
                        {
                            Locations l = new Locations();
                            l.Lokacioni = item2.Lokacioni;
                            l.Sasia = item2.Sasia;
                            listaLocations.Add(l);
                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }
                       
                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_e_katalogut = item.Shifra_e_katalogut, Shifra_prodhuesit = item.Shifra_prodhuesit,Prodhuesi = item.Prodhuesi,Lokacionet = listaLocations });


            }




            return rezultati;
        }



        [HttpPost("GetItems2")]
        public ActionResult<List<Items2>> GetItems2([FromBody] List<string> c)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable
            DataTable dt = new DataTable();
            dt.TableName = "Items_API";
            dt.Columns.Add("Shifra", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Shifra_prodhuesit", typeof(string));
            dt.AcceptChanges();


            foreach (var item in c)
            {

                DataRow row = dt.NewRow();
                row[0] = item;
                row[1] = "";

                dt.Rows.Add(row);

            }

            list = _dbAccess.GetItems_API_2(dt,"");
            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLokacionet = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if (item.Shifra == item2.Shifra)
                    {
                        if (item2.Sasia >= 0)
                        {
                            Locations location = new Locations();
                            location.Lokacioni = item2.Lokacioni;
                            location.Sasia = item2.Sasia;
                            listaLokacionet.Add(location);
                           
                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }
                      
                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_e_katalogut = item.Shifra_e_katalogut, Shifra_prodhuesit = item.Shifra_prodhuesit,Prodhuesi = item.Prodhuesi,Lokacionet=listaLokacionet });

               
            }
            return rezultati;
        }

        [HttpPost("GetItems_Email")]
        public ActionResult<List<Items2>> GetItems_Email([FromBody] ItemsEmail c)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable
            DataTable dt = new DataTable();
            dt.TableName = "Items_API";
            dt.Columns.Add("Shifra", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Shifra_prodhuesit", typeof(string));
            dt.AcceptChanges();


            foreach (var item in c.Items)
            {

                DataRow row = dt.NewRow();
                row[0] = item.Shifra;
                row[1] = item.Shifra_prodhuesit;

                dt.Rows.Add(row);

            }

            list = _dbAccess.GetItems_API(dt, c.Email);
            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLokacionet = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if (item.Shifra == item2.Shifra)
                    {
                        if (item2.Sasia >= 0)
                        {
                            Locations location = new Locations();
                            location.Lokacioni = item2.Lokacioni;
                            location.Sasia = item2.Sasia;
                            listaLokacionet.Add(location);
                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }
                       
                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_e_katalogut = item.Shifra_e_katalogut, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi,Lokacionet = listaLokacionet });


            }




            return rezultati;
        }



        [HttpPost("GetItems2_Email")]
        public ActionResult<List<Items2>> GetItems2_Email([FromBody] Items2Email c)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable
            DataTable dt = new DataTable();
            dt.TableName = "Items_API";
            dt.Columns.Add("Shifra", typeof(string));
            dt.AcceptChanges();
            dt.Columns.Add("Shifra_prodhuesit", typeof(string));
            dt.AcceptChanges();


            foreach (var item in c.Shifra)
            {

                DataRow row = dt.NewRow();
                row[0] = item;
                row[1] = "";

                dt.Rows.Add(row);

            }

            list = _dbAccess.GetItems_API_2(dt,c.Email);
            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLokacionet = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if (item.Shifra == item2.Shifra)
                    {
                        if(item2.Sasia >= 0)
                        {
                            Locations location = new Locations();
                            location.Lokacioni = item2.Lokacioni;
                            location.Sasia = item2.Sasia;
                            listaLokacionet.Add(location);

                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }
                       
                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_e_katalogut = item.Shifra_e_katalogut, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi,Lokacionet=listaLokacionet });


            }
            return rezultati;
        }


        [HttpPost("GetItemsByProducer")]
        public ActionResult<List<Items2>> GetItemsByProducer([FromBody] ItemsProducer producer)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable
          

            list = _dbAccess.GetItemsProdhuesi_API(producer.Producer);
            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLocations = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if (item.Shifra == item2.Shifra)
                    {
                        if (item2.Sasia >= 0)
                        {
                            Locations l = new Locations();
                            l.Lokacioni = item2.Lokacioni;
                            l.Sasia = item2.Sasia;
                            listaLocations.Add(l);
                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }

                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi, Lokacionet = listaLocations});


            }
            int counter = 1;
            foreach (var item in rezultati)
            {
                item.Nr = counter;
                counter++;
            }


            return rezultati;
            //return Paginate<Items2>(rezultati,1,30).Items;
        }

        [HttpGet("GetItemsByItemID")]
        public ActionResult<List<Items2>> GetItemsByItemID([FromQuery] string ItemID, [FromQuery] string Email)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable
         

            list = _dbAccess.GetItemsByItemID_API(ItemID,Email);

            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLocations = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if (item.Shifra == item2.Shifra)
                    {
                        if (item2.Sasia >= 0)
                        {
                            Locations l = new Locations();
                            l.Lokacioni = item2.Lokacioni;
                            l.Sasia = item2.Sasia;
                            listaLocations.Add(l);
                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }

                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi, Lokacionet = listaLocations,Cmimi2=item.Cmimi2 });


            }
            int counter = 1;
            foreach (var item in rezultati)
            {
                item.Nr = counter;
                counter++;
            }


            return rezultati;
            
        }

        [HttpGet("GetItemsByDate")]
        public ActionResult<List<ItemsLookup>> GetItemsByDate([FromQuery] string date)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<ItemsLookup> list = new List<ItemsLookup>();
            //DataTable


            list = _dbAccess.GetItemsByDate_API(date);
            //List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            //List<Items2> rezultati = new List<Items2>();
            //foreach (var item in distinctList)
            //{
            //    List<string> locations = new List<string>();
            //    List<Locations> listaLocations = new List<Locations>();
            //    int sumQuantity = 0;
            //    foreach (var item2 in list)
            //    {
            //        if (item.Shifra == item2.Shifra)
            //        {
            //            if (item2.Sasia >= 0)
            //            {
            //                Locations l = new Locations();
            //                l.Lokacioni = item2.Lokacioni;
            //                l.Sasia = item2.Sasia;
            //                listaLocations.Add(l);
            //                sumQuantity += item2.Sasia;
            //                //locations.Add(item2.Lokacioni);
            //            }

            //        }
            //    }
            //    rezultati.Add(
            //        new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_e_katalogut = item.Shifra_e_katalogut, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi, Lokacionet = listaLocations });


            //}




            return list;
        }

        [HttpGet("GetAllItems")]
        public ActionResult<PaginationResult<ItemsLookup>> GetAllItems([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<ItemsLookup> list = new List<ItemsLookup>();
            //DataTable


            list = _dbAccess.GetAllItems();
            //List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            //List<Items2> rezultati = new List<Items2>();
            //foreach (var item in distinctList)
            //{
            //    List<string> locations = new List<string>();
            //    List<Locations> listaLocations = new List<Locations>();
            //    int sumQuantity = 0;
            //    foreach (var item2 in list)
            //    {
            //        if (item.Shifra == item2.Shifra)
            //        {
            //            if (item2.Sasia >= 0)
            //            {
            //                Locations l = new Locations();
            //                l.Lokacioni = item2.Lokacioni;
            //                l.Sasia = item2.Sasia;
            //                listaLocations.Add(l);
            //                sumQuantity += item2.Sasia;
            //                //locations.Add(item2.Lokacioni);
            //            }

            //        }
            //    }
            //    rezultati.Add(
            //        new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_e_katalogut = item.Shifra_e_katalogut, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi, Lokacionet = listaLocations });


            //}




            return Paginate(list,pageNumber,pageSize);
        }

        [HttpPost("GetItemsByProducer2")]
        public ActionResult<PaginationResult<Items2>> GetItemsByProducer2([FromBody] ItemsProducer2 producer)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Items> list = new List<Items>();
            //DataTable


            list = _dbAccess.GetItemsProdhuesi_API(producer.Producer);
            List<Items> distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();


            foreach (var item in distinctList)
            {
                if(item.Cmimi == 0)
                {
                    var newItem = list.Where(x => x.Shifra.Equals(item.Shifra)&& x.Cmimi>0).FirstOrDefault();
                    if(newItem != null )
                    {
                        item.Cmimi = newItem.Cmimi;
                    }



                }
               
            }






            //var groupsWithPriceGreaterThanZero = list
            //                                    .Where(item => item.Cmimi > 0)
            //                                    .GroupBy(item => item.Shifra)
            //                                    .Select(group => group.First());

            //// If there are groups with price > 0, use them, otherwise, select the first item from the list
            //if (groupsWithPriceGreaterThanZero.Any())
            //{
            //    distinctList = groupsWithPriceGreaterThanZero.ToList();
            //}
            //else
            //{
            //    distinctList = list.GroupBy(elem => elem.Shifra).Select(group => group.First()).ToList();
            //}



            List<Items2> rezultati = new List<Items2>();
            foreach (var item in distinctList)
            {
                List<string> locations = new List<string>();
                List<Locations> listaLocations = new List<Locations>();
                int sumQuantity = 0;
                foreach (var item2 in list)
                {
                    if (item.Shifra == item2.Shifra)
                    {
                        if (item2.Sasia >= 0)
                        {
                            Locations l = new Locations();
                            l.Lokacioni = item2.Lokacioni;
                            l.Sasia = item2.Sasia;
                            listaLocations.Add(l);
                            sumQuantity += item2.Sasia;
                            //locations.Add(item2.Lokacioni);
                        }

                    }
                }
                rezultati.Add(
                    new AutoBit_WebInvoices.Models.Items2 { Shifra = item.Shifra, Emertimi = item.Emertimi, Cmimi = item.Cmimi, Lokacioni = locations, Sasia = sumQuantity, Shifra_prodhuesit = item.Shifra_prodhuesit, Prodhuesi = item.Prodhuesi, Lokacionet = listaLocations });


            }
            int counter = 1;
            string defaultDep = _configuration["AppSettings:DefaultDepartment"];
            foreach (var item in rezultati)
            {
                item.Nr = counter;
                counter++;

                try
                {
                  
                    var depojaQendrore = item.Lokacionet.FirstOrDefault(loc => loc.Lokacioni == defaultDep);
                    if (depojaQendrore != null)
                    {
                        item.Lokacionet.Remove(depojaQendrore);
                        item.Lokacionet.Insert(0, depojaQendrore);
                    }
                }
                catch
                {

                }
               


            }


            
            return Paginate<Items2>(rezultati,producer.PageNumber,30);
        }

        [HttpGet("Prodhuesit")]
        public ActionResult<List<Prodhuesi>> GetProdhuesit()
        {
            return _dbAccess.GetProdhuesi();
        }

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


        [HttpGet("Get")]
        public ActionResult<List<ItemsWeb>> Get([FromQuery] int DepartmentID)
        {
            return _dbAccess.GetItemsWeb(DepartmentID);
        }

        [HttpGet("Attributes")]
        public ActionResult<List<ItemsAttributes>> GetAttributes([FromQuery] string ItemID)
        {
            return _dbAccess.GetItemsAttributesWeb(ItemID);
        }

        public PaginationResult<T> Paginate<T>(List<T> items, int pageNumber, int pageSize)
        {
            var totalCount = items.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var results = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<T>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = results
            };
        }
    }
}

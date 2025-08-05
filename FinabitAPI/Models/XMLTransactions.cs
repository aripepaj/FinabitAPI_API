using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finabit_API.Models
{
    public class XMLTransactions
    {
        public XMLTransactions()
        {
            ID = 0;
            //TransactionNo = "";
            //InvoiceNo = "";
            TransactionType = 0;
            TransactionDate = DateTime.Now;
            PartnerID = 0;
            VisitID = 0;
            DepartmentID = 0;
            InsDate = DateTime.Now;
            InsBy = 0;
            EmployeeID = 0;
            IsSynchronized = false;
            VATPrecentID = 0;
            DueDays = 0;
            AllValue = 0;
            Longitude = 0;
            Latitude = 0;
            Memo = "";
            Llogaria_NotaKreditore = "";
            Vlera_NotaKreditore = 0;
            Exists = false;
            IsPrintFiscalInvoice = false;
            ErrorID = 0;
            ErrorDescription = "";
            NrIFatBlerje = "";
            CashAccount = "";
        }

        public int ID { get; set; }
        public string TransactionNo { get; set; }
        public string InvoiceNo { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int PartnerID { get; set; }
        public int VisitID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime InsDate { get; set; }
        public int InsBy { get; set; }
        public int EmployeeID { get; set; }
        public bool IsSynchronized { get; set; }
        public int VATPrecentID { get; set; }
        public int DueDays { get; set; }
        public decimal PaymentValue { get; set; }
        public decimal AllValue { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public List<XMLTransactionDetails> Details = new List<XMLTransactionDetails>();

        public int ServiceTypeID { get; set; }
        public int AssetID { get; set; }
        public bool BL { get; set; }
        public string Memo { get; set; }
        public string Llogaria_NotaKreditore { get; set; }
        public decimal Vlera_NotaKreditore { get; set; }


        public bool Exists { get; set; }
        public bool IsPrintFiscalInvoice { get; set; }


        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public bool IsPriceFromPartner { get; set; }
        public string NrIFatBlerje { get; set; }
        public string CashAccount { get; set; }
        public bool IsSelectedPayment { get; set; }
    }
}

using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class TransactionRequest
    {
        public Transactions t { get; set; }
        public TransactionsDetails detailsList { get; set; }
        public int CashJournalPOSID { get; set; } = 0;
        public bool isSelectedPayment { get; set; }
        public string Temp { get; set; } = "C://Temp";
        public string POSUserName { get; set; }

        public bool PrintFiscal { get; set; } = false;
        // public bool isOrder { get; set; }
        public bool PrintALL { get; set; } = false;


    }

}
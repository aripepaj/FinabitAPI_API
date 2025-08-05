using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class Bank : BaseClass
    {
        public Bank()
        {
            BankName = "";
            BankAccount = "";
        }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
    }
}
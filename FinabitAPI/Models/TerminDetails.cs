using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class TerminDetails
    {
        public int TerminID { get; set; }
        public int UserID { get; set; }
        public decimal Value { get; set; }
        public decimal PaidValue { get; set; }
    }
}
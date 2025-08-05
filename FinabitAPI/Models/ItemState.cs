using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class ItemState
    {
        public int ID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public decimal Coefficient { get; set; }
        public decimal VATValue { get; set; }
        public bool Unit1Default { get; set; }
        public string UnitName { get; set; }
        public decimal Coef_Quantity { get; set; }
        public decimal Discount { get; set; }
        public string ItemGroupName { get; set; }
        public string UnitName1 { get; set; }
        public string UnitName2 { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
    }
}
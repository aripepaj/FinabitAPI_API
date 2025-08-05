using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finabit_API.Models
{
    public class XMLItems
    {
       
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int DepartmentID { get; set; }
        public decimal Rabat { get; set; }
        public string Barcode { get; set; }
        public bool Gratis { get; set; }
        public decimal Coefficient { get; set; }
        public decimal VATValue { get; set; }
        public int ItemGroupID { get; set; }
        public string UnitName { get; set; }
        public bool Unit1Default { get; set; }
        public decimal Coef_Quantity { get; set; }
        public decimal MinimalPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IncludeInTargetPlan { get; set; }// perdoret per me blloku editimin e cmimit te porosite
        public int ID { get; set; }
        public int PriceMenuID { get; set; }
        public decimal NotaKreditore { get; set; }
        public string CustomField6 { get; set; }
        public string ItemGroupName { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
    }
}

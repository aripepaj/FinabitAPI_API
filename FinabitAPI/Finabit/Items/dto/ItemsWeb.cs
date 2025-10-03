using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Items.dto
{
    public class ItemsWeb
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal SalesPrice { get; set; }
        public int LocationID { get; set; }
        public bool Taxable { get; set; }
        public decimal VATValue { get; set; }
        public string ItemGroup { get; set; }
        public string Photo { get; set; }

    }
}
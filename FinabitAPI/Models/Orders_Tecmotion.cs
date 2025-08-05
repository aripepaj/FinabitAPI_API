using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class Orders_Tecmotion
    {
        public int Id { get; set; }
        public string Lokacioni { get; set; }
        public string Email_konsumatori { get; set; }
        public DateTime Data { get; set; }
        public List<OrdersDetails_Tecmotion> Detajet { get; set; }
    }
    public class OrdersDetails_Tecmotion
    {
        public string Shifra { get; set; }
        public string Shifra_prodhuesit { get; set; }
        public decimal Sasia { get; set; }
        public decimal Cmimi { get; set; }
    }
}
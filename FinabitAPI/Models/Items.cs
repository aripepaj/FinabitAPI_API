using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class Items
    {
        public int Nr { get; set; }
        public String Shifra { get; set; }
        public String Emertimi { get; set; }
        //public String Shifra_e_prodhuesit { get; set; }
        public String Shifra_e_katalogut { get; set; }
        public String Lokacioni { get; set; }
        public int Sasia { get; set; }
        public decimal Cmimi { get; set; }
        public string Shifra_prodhuesit { get; set; }
        public string Prodhuesi { get; set; }
        public decimal Cmimi2 { get; set; }
        

    }
    public class Items2
    {
        public int Nr { get; set; }
        public String Shifra { get; set; }
        public String Emertimi { get; set; }
        //public String Shifra_e_prodhuesit { get; set; }
        public String Shifra_e_katalogut { get; set; }
        public List<String> Lokacioni { get; set; }
        public int Sasia { get; set; }
        public decimal Cmimi { get; set; }
        public string Shifra_prodhuesit { get; set; }
        public string Prodhuesi { get; set; }
        public List<Locations> Lokacionet { get; set; }
        public decimal Cmimi2 { get; set; }


    }
    public class ItemsEmail
    {
        public List<Items> Items { get; set; }
        public string Email { get; set; }
    }
    public class Items2Email
    {
        public List<string> Shifra { get; set; }
        public string Email { get; set; }
    }
    public class ItemsProducer
    {
        public string Producer { get; set; }
        //public int PageNumber { get; set; }
    }
    public class ItemsProducer2
    {
        public string Producer { get; set; }
        public int PageNumber { get; set; }
    }
}
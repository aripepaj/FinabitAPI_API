using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Transaction.dto
{
    public class Orders
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Numri { get; set; }
        public int ID_Konsumatorit { get; set; }
        public string Konsumatori { get; set; }
        public string Location { get; set; }
        public string Statusi_Faturimit { get; set; }
        public string Shifra { get; set; }
        public string Emertimi { get; set; }
        public string Njesia_Artik { get; set; }
        public string Description { get; set; }
        public decimal Sasia { get; set; }
        public decimal Cmimi { get; set; }       
        public decimal SalesPrice { get; set; }   
        public decimal CostPrice { get; set; }
    }
}
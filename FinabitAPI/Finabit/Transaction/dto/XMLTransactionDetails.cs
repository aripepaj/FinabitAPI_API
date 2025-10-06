using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinabitAPI.Finabit.Transaction.dto
{

    public class XMLTransactionDetails
    {
        public XMLTransactionDetails()
        {
            ItemID = "";
            ItemName = "";
            Quantity = 0;
            Price = 0;
            Value = 0;
            TransactionID = 0;
            Rabat = 0;
            SubOrderID = 0;
            Rabat2 = 0;
            Rabat3 = 0;
        }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }
        public int TransactionID { get; set; }
        public decimal Rabat { get; set; }
        public int SubOrderID { get; set; }
        public decimal Rabat2 { get; set; }
        public decimal Rabat3 { get; set; }
        public string Memo { get; set; }
        public int PriceMenuID { get; set; }
        public string Barcode { get; set; }

    }
}

using System.Collections.Generic;

namespace Finabit_API.Models
{
    public class ItemBatchRow
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int DepartmentID { get; set; }
        public int InternalDepartmentID { get; set; }
        public string TransactionDate { get; set; } // yyyy-MM-dd
        public int TransactionTypeID { get; set; } = 0;
    }

    public class ItemBatchImportRequest
    {
        public int TransactionID { get; set; }
        public List<ItemBatchRow> Items { get; set; }
    }

    public class ItemBatchImportResponse
    {
        public int Inserted { get; set; }
        public int Failed { get; set; }
        public string Error { get; set; }
    }

    public class ItemMatchResponse
    {
        public bool Exists { get; set; }
        public List<ItemsLookup> Matches { get; set; }
    }
}

using System.Collections.Generic;
using FinabitAPI.Finabit.Partner.dto; // for PartnerModel

namespace FinabitAPI.Core.Global.dto
{
    // Represents a single partner row in a bulk upload operation
    public class PartnerBatchItem
    {
        public string PartnerName { get; set; }
        public string Type { get; set; } // Fu, Ko, Kont ...
        public string Account { get; set; }
        public string PartnerGroup { get; set; }
        public string PartnerCategory { get; set; }
        public string StateID { get; set; } // textual state name per procedure
        public string City { get; set; }
        public string Region { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string FiscalNo { get; set; }
        public string BussinesNo { get; set; }
        public string VatNo { get; set; }
        public decimal DiscountPercent { get; set; }
        public string ItemID { get; set; }
        public string Adress { get; set; }
        public string ContactPerson { get; set; }
    }

    public class PartnerBatchResponse
    {
        public int Inserted { get; set; }
        public int Failed { get; set; }
        public string Error { get; set; }
    }

    public class PartnerMatchResponse
    {
        public bool Exists { get; set; }
        public List<PartnerModel> Matches { get; set; }
    }
}

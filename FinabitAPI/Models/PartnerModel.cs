using System;

namespace AutoBit_WebInvoices.Models
{
    public class PartnerModel
    {
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string FiscalNo { get; set; }
        public string BusinessNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PlaceName { get; set; }
        public string StateName { get; set; }
        public string Group { get; set; }
        public string Category { get; set; }
        public int DueDays { get; set; }
        public decimal DueValueMaximum { get; set; }
        public decimal DueValue { get; set; }
    }
}
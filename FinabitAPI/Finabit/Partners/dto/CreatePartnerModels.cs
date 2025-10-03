namespace FinabitAPI.Finabit.Partner.dto
{
    public class CreatePartnerRequest
    {
        public string PartnerName { get; set; }
        public int PartnerTypeID { get; set; } = 2; // default customer
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string BusinessNo { get; set; }
        public string VATNO { get; set; }
        public string BankAccount { get; set; }
        public decimal DiscountPercent { get; set; }
        public string PIN { get; set; }
        public string ItemID { get; set; }
        public int PriceMenuID { get; set; }
        public int DueDays { get; set; }
        public decimal DueValueMaximum { get; set; }
        public string ContractNo { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int StateID { get; set; }
        public int PlaceID { get; set; }
        public string AccountCode { get; set; } = string.Empty; // optional
    }

    public class CreatePartnerResponse
    {
        public int PartnerID { get; set; }
        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
    }
}

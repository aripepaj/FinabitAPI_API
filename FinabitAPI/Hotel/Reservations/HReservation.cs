using System;
using System.ComponentModel.DataAnnotations;
using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.Reservations
{
    public class HReservation : BaseClass
    {
        public HReservation()
        {
            ID = 0;
            RoomID = 0;
            GuestID = 0;
            ConsumerID = 0;
            RateID = 0;
            BookDate = DateTime.Now;
            CheckInDate = DateTime.Now;
            CheckOutDate = DateTime.Now;
            NoOfDays = 0;
            Value = 0;
            VAT = 0;
            AllValue = 0;
            Description = "";
            StatusID = 0;
            TransactionID = 0;
            Card1 = "";
            Card2 = "";
            Card3 = "";
            Card4 = "";
            Card5 = "";
            InsBy = 0;
            LUB = 0;
            ChildrenNumberID = 0;
            AdultNumberID = 0;
            ReservationOriginID = 0;
            AgencyID = 0;
            PansonID = 0;
            AccomodationID = 0;
            RoomCount = 0;
            Discount = 0;
            CheckInMemo = "";
            CheckOutMemo = "";
            HouseKeepingMemo = "";
            GuestID_2 = 0;
            GuestID_3 = 0;
            GuestID_4 = 0;
            GuestID_5 = 0;
            AccomodationValue = 0;
            ExtraChargeValue = 0;
            PaidValue = 0;
            VATValue = 0;
            TarifDefinitionID = 0;
            PaymentType = 0;
            PlanDistributionID = 0;
            GroupID = 0;
            GroupColor = "";
            EventDetailID = 0;
            BL = false;
            Parapagim = false;
            VleraParapagimit = 0;
            Avans = 0;
            Account = "";
            RatePrice = 0;
            SubBookingID = "";
            Anulim = false;
            
            // Additional properties from your SelectByID method
            RoomName = "";
            Guest = "";
            Guest2 = "";
            Agency = "";
            ReservationStatus = "";
            CHeckInBy = 0;
            CheckOutBY = 0;
            CheckInName = "";
            CheckOutName = "";
            ReservationName = "";
            EventName = "";
            EventID = 0;
            MasterFolio = 0;
            EventDepartmentID = 0;
            SetupID = 0;
            ItemID = "";
        }

        // Primary Key
        public int ID { get; set; }

        // Room Information
        public int RoomID { get; set; }
        public string RoomName { get; set; }

        // Guest Information
        public int GuestID { get; set; }
        public string Guest { get; set; }
        public string Guest2 { get; set; }
        public int GuestID_2 { get; set; }
        public int GuestID_3 { get; set; }
        public int GuestID_4 { get; set; }
        public int GuestID_5 { get; set; }

        // Consumer and Rate Information
        public int ConsumerID { get; set; }
        public int RateID { get; set; }
        public decimal RatePrice { get; set; }

        // Dates
        public DateTime BookDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfDays { get; set; }

        // Financial Information
        public decimal Value { get; set; }
        public decimal VAT { get; set; }
        public decimal AllValue { get; set; }
        public decimal VATValue { get; set; }
        public decimal AccomodationValue { get; set; }
        public decimal ExtraChargeValue { get; set; }
        public decimal PaidValue { get; set; }
        public decimal Discount { get; set; }
        public decimal VleraParapagimit { get; set; }
        public decimal Avans { get; set; }

        // Status and Transaction Information
        public string Description { get; set; }
        public int StatusID { get; set; }
        public string ReservationStatus { get; set; }
        public int TransactionID { get; set; }

        // Card Information
        public string Card1 { get; set; }
        public string Card2 { get; set; }
        public string Card3 { get; set; }
        public string Card4 { get; set; }
        public string Card5 { get; set; }

        // User Information
        public int InsBy { get; set; }
        public int LUB { get; set; }
        public int CHeckInBy { get; set; }
        public int CheckOutBY { get; set; }
        public string CheckInName { get; set; }
        public string CheckOutName { get; set; }
        public string ReservationName { get; set; }

        // Guest Numbers
        public int ChildrenNumberID { get; set; }
        public int AdultNumberID { get; set; }

        // Source and Agency Information
        public int ReservationOriginID { get; set; }
        public int AgencyID { get; set; }
        public string Agency { get; set; }
        public int PansonID { get; set; }

        // Accommodation Information
        public int AccomodationID { get; set; }
        public int RoomCount { get; set; }

        // Memos
        public string CheckInMemo { get; set; }
        public string CheckOutMemo { get; set; }
        public string HouseKeepingMemo { get; set; }

        // Additional Properties
        public int TarifDefinitionID { get; set; }
        public int PaymentType { get; set; }
        public int PlanDistributionID { get; set; }
        public int GroupID { get; set; }
        public string GroupColor { get; set; }
        public int EventDetailID { get; set; }
        public string EventName { get; set; }
        public int EventID { get; set; }
        public int EventDepartmentID { get; set; }
        public bool BL { get; set; }
        public bool Parapagim { get; set; }
        public string Account { get; set; }
        public bool Anulim { get; set; }
        public string SubBookingID { get; set; }
        public int MasterFolio { get; set; }
        public int SetupID { get; set; }
        public string ItemID { get; set; }
    }
}
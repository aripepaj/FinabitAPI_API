using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.EventDetails
{
    public class EventsDetails : BaseClass
    {
        #region Class_Methods

        /// <summary>
        /// The ID field in the DB
        /// </summary>
        public new int ID { get; set; }

        /// <summary>
        /// The EventID field in the DB
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// The EventActivityID field in the DB
        /// </summary>
        public int EventActivityID { get; set; }

        /// <summary>
        /// The ActivityDescription field in the DB
        /// </summary>
        public string? ActivityDescription { get; set; }

        /// <summary>
        /// The Date field in the DB
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The FromTime field in the DB
        /// </summary>
        public string? FromTime { get; set; }

        /// <summary>
        /// The ToTime field in the DB
        /// </summary>
        public string? ToTime { get; set; }

        /// <summary>
        /// The ShowInInvoice field in the DB
        /// </summary>
        public bool ShowInInvoice { get; set; }

/// <summary>
/// The DepartmentID field in the DB
/// </summary>
public int DepartmentID { get; set; }

/// <summary>
/// The Quantity field in the DB
/// </summary>
public decimal Quantity { get; set; }

/// <summary>
/// The Price field in the DB
/// </summary>
public decimal Price { get; set; }

/// <summary>
/// The Value field in the DB
/// </summary>
public decimal Value { get; set; }

/// <summary>
/// The HasCoupon field in the DB
/// </summary>
public bool HasCoupon { get; set; }

        public int MODE { get; set; }
        public string? Account { get; set; }

        public int? SetupID { get; set; }
        public string? MenuID { get; set; }

        public string? SetupDescription { get; set; }
        public string? AudioDescription { get; set; }

        #endregion 
    }
}

using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.HAgencies
{
    public class HAgencies : BaseClass
    {
        #region Class_Methods

        /// <summary>
        /// The ID field in the DB
        /// </summary>
        public new int ID { get; set; }

        /// <summary>
        /// The AgencyName field in the DB
        /// </summary>
        public string? AgencyName { get; set; }

        public int PartnerID { get; set; }

        #endregion 
    }
}
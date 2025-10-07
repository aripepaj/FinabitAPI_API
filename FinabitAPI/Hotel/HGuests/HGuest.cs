using FinabitAPI.Core.Global.dto;

namespace FinabitAPI.Hotel.HGuests
{
    public class HGuest : BaseClass
{ 
   #region Class_Methods


        /// Methods
        /// <summary>
        /// The ID field in the DB
        /// </summary>
        public new int ID
        {
            get;
            set;
        }

        /// <summary>
        /// The IDNo field in the DB
        /// </summary>
        public string? IDNo
        {
            get;
            set;
        }

        /// <summary>
        /// The Name field in the DB
        /// </summary>
        public string? Name
        {
            get;
            set;
        }

        /// <summary>
        /// The Surname field in the DB
        /// </summary>
        public string? Surname
        {
            get;
            set;
        }

        /// <summary>
        /// The Gender field in the DB
        /// </summary>
        public Int16 Gender
        {
            get;
            set;
        }

        /// <summary>
        /// The Company field in the DB
        /// </summary>
        public string? Company
        {
            get;
            set;
        }

        /// <summary>
        /// The Profession field in the DB
        /// </summary>
        public string? Profession
        {
            get;
            set;
        }

        /// <summary>
        /// The Email field in the DB
        /// </summary>
        public string? Email
        {
            get;
            set;
        }

        /// <summary>
        /// The Phone1 field in the DB
        /// </summary>
        public string? Phone1
        {
            get;
            set;
        }

        /// <summary>
        /// The Phone2 field in the DB
        /// </summary>
        public string? Phone2
        {
            get;
            set;
        }

        /// <summary>
        /// The GuestTypeID field in the DB
        /// </summary>
        public int GuestTypeID
        {
            get;
            set;
        }

        /// <summary>
        /// The GuestType field in the DB
        /// </summary>
        public string? GuestType
        {
            get;
            set;
        }

        /// <summary>GuestType
        /// 
        /// The CountryID field in the DB
        /// </summary>
        public int CountryID
        {
            get;
            set;
        }

        /// <summary>
        /// The CityID field in the DB
        /// </summary>
        public int CityID
        {
            get;
            set;
        }

        /// <summary>
        /// The Address field in the DB
        /// </summary>
        public string? Address
        {
            get;
            set;
        }

        /// <summary>
        /// The Description field in the DB
        /// </summary>
        public new string? Description
        {
            get;
            set;
        }
        /// <summary>
        /// The Guest field in the DB
        /// </summary>
        public string? Guest
        {
            get;
            set;
        }
        public DateTime Birthday
        {
            get;
            set;
        }


        #endregion


    }
}
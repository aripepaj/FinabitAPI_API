using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Transaction.dto
{
    public class Termins : BaseClass
    {

        #region Class_Members


        /// Members
        private int _mEmployeeID;
        private int _mCashJournalPOSID;
        private DateTime _mBeginningDate;
        private DateTime? _mEndingDate;
        private int _mStatus;
        public string StatusName { get; set; }
        public DateTime BeginningH { get; set; }
        public DateTime EndingH { get; set; }
        public string EmployeeName { get; set; }
        public string TerminName { get; set; }
        public List<TerminDetails> DetailsColl { get; set; }


        #endregion

        #region Class_Methods


        /// Methods
        /// <summary>
        /// The EmployeeID field in the DB
        /// </summary>
        public int EmployeeID
        {
            get { return _mEmployeeID; }
            set { _mEmployeeID = value; }
        }

        /// <summary>
        /// The BeginningDate field in the DB
        /// </summary>
        public DateTime BeginningDate
        {
            get { return _mBeginningDate; }
            set { _mBeginningDate = value; }
        }

        /// <summary>
        /// The EndingDate field in the DB
        /// </summary>
        public DateTime? EndingDate
        {
            get { return _mEndingDate; }
            set { _mEndingDate = value; }
        }

        /// <summary>
        /// The Status field in the DB
        /// </summary>
        public int Status
        {
            get { return _mStatus; }
            set { _mStatus = value; }
        }

        /// <summary>
        /// The CashJournalPOSID field in the DB
        /// </summary>
        public int CashJournalPOSID
        {
            get { return _mCashJournalPOSID; }
            set { _mCashJournalPOSID = value; }
        }
        #endregion

        public int DepartmentID { get; set; }
      //  public List<DeviceCard> DevicesColl = new List<DeviceCard>();
        public bool IsSynchronised { get; set; }
        public decimal JournalInititalState { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class DocumentPrice
    {

        private decimal _DocPrice;

        private string _DocPriceName;

        private int _HomologationTypeID;

        private string _HomologationTypeName;

        private decimal _QtyFrom;

        private decimal _QtyTo;

        private int _VehicleTypeID;

        private string _VehicleTypeName;
        private decimal _feePrice;
        public string Account { get; set; }

        public decimal DocPrice
        {

            get
            {
                return _DocPrice;
            }

            set
            {
                _DocPrice = value;
            }
        }

        public string DocPriceName
        {

            get
            {
                return _DocPriceName;
            }

            set
            {
                _DocPriceName = value;
            }
        }

        public int HomologationTypeID
        {

            get
            {
                return _HomologationTypeID;
            }

            set
            {
                _HomologationTypeID = value;
            }
        }

        public string HomologationTypeName
        {

            get
            {
                return _HomologationTypeName;
            }

            set
            {
                _HomologationTypeName = value;
            }
        }

        public decimal QtyFrom
        {

            get
            {
                return _QtyFrom;
            }

            set
            {
                _QtyFrom = value;
            }
        }

        public decimal QtyTo
        {

            get
            {
                return _QtyTo;
            }

            set
            {
                _QtyTo = value;
            }
        }

        public int VehicleTypeID
        {

            get
            {
                return _VehicleTypeID;
            }

            set
            {
                _VehicleTypeID = value;
            }
        }

        public string VehicleTypeName
        {

            get
            {
                return _VehicleTypeName;
            }

            set
            {
                _VehicleTypeName = value;
            }
        }


        public decimal FeePrice
        {
            get { return _feePrice; }
            set { _feePrice = value; }
        }

        public bool HasTarifsForEurolab { get; set; }

        public bool Active { get; set; }
        public bool IsPeriodic { get; set; }
        public decimal TarifaGazerave { get; set; }
        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public object ID { get; set; }
    }
}
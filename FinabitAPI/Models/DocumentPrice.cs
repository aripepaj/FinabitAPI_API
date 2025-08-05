using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finabit_API.Models
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
                return this._DocPrice;
            }

            set
            {
                this._DocPrice = value;
            }
        }

        public string DocPriceName
        {

            get
            {
                return this._DocPriceName;
            }

            set
            {
                this._DocPriceName = value;
            }
        }

        public int HomologationTypeID
        {

            get
            {
                return this._HomologationTypeID;
            }

            set
            {
                this._HomologationTypeID = value;
            }
        }

        public string HomologationTypeName
        {

            get
            {
                return this._HomologationTypeName;
            }

            set
            {
                this._HomologationTypeName = value;
            }
        }

        public decimal QtyFrom
        {

            get
            {
                return this._QtyFrom;
            }

            set
            {
                this._QtyFrom = value;
            }
        }

        public decimal QtyTo
        {

            get
            {
                return this._QtyTo;
            }

            set
            {
                this._QtyTo = value;
            }
        }

        public int VehicleTypeID
        {

            get
            {
                return this._VehicleTypeID;
            }

            set
            {
                this._VehicleTypeID = value;
            }
        }

        public string VehicleTypeName
        {

            get
            {
                return this._VehicleTypeName;
            }

            set
            {
                this._VehicleTypeName = value;
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
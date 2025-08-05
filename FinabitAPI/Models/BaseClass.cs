/////////////////////////////////////////////////////////// 
using System;
using System.Collections.Generic;
using System.Text;

namespace Finabit_API.Models
{
    [Serializable]
    public class BaseClass
    {
        public BaseClass()
        {
            ID = 0;
            ErrorID = 0;
            ErrorDescription = "";
            Description = "";
            LUN = 0;
            LUB = 0;
            LUD = DateTime.Now;
            InsBy = 0;
            InsDate = DateTime.Now;
            rowguid= new Guid();
        }

        #region Class_Methods


        /// Methods
        public int ID
        {
            get;
            set;
        }

        public int ErrorID
        {
            get;
            set;
        }

        public string ErrorDescription
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int LUN
        {
            get;
            set;
        }

        public int LUB
        {
            get;
            set;
        }

        public DateTime LUD
        {
            get;
            set;
        }

        public Guid rowguid
        {
            get;
            set;
        }

        public int InsBy
        {
            get;
            set;
        }

        public DateTime InsDate
        {
            get;
            set;
        }


        #endregion
    }

}
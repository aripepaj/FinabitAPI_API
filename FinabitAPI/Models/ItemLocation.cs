using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class ItemLocation : BaseClass
    {

        #region Class_Methods


        /// Methods
        /// <summary>
        /// The ID field in the DB
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// The Name field in the DB
        /// </summary>
        public string Name
        {
            get;
            set;
        }


        #endregion
    }
}
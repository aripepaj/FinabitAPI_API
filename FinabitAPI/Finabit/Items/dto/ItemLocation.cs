using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Items.dto
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
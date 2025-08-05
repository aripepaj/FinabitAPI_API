using Finabit_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoBit_WebInvoices.Models
{
    public class PartnersType : BaseClass
    {
        private string _mPartnerTypeName = "";



        public string PartnerTypeName
        {
            get { return _mPartnerTypeName; }
            set { _mPartnerTypeName = value; }
        }

        public override string ToString()
        {
            return this.PartnerTypeName;
        }

    }
}
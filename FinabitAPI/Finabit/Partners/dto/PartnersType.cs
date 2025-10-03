using FinabitAPI.Core.Global.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Finabit.Partner.dto
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
            return PartnerTypeName;
        }

    }
}
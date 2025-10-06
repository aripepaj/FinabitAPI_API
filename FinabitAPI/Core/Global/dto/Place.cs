using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class Place : BaseClass
    {
        public string PlaceName { get; set; }

        public override string ToString()
        {
            return PlaceName;
        }

        public Place()
        {
            PlaceName = "";
        }

    }
}
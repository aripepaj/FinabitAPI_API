using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class State : BaseClass
    {
        public string StateName { get; set; }

        public override string ToString()
        {
            return StateName;
        }
        public State()
        {
            StateName = "";
        }
    }
}
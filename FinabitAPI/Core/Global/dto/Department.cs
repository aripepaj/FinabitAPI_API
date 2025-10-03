using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FinabitAPI.Core.Global.dto
{
    public class Department:BaseClass
    {
        public Department()
        {
            DepartmentName = "";
            Account = "";
            AllowNegative = false;
            PriceMenuID = 0;
            PriceMenuName = "";
            CompanyID = 0;
            POSSeparate = false;
        }

       public string DepartmentName { get ; set; }
       public string Account { get; set; }
       public bool AllowNegative { get; set; }
       //public DataTable Users { get; set; }
       //public DataTable Regions { get; set; }
       public int PriceMenuID { get; set; }
       public string PriceMenuName { get; set; }
       public int CompanyID { get; set; }
       public bool POSSeparate { get; set; }
    }
}

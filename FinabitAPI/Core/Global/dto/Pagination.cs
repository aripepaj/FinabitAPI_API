using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinabitAPI.Core.Global.dto
{
    public class PaginationResult<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }




    }

 
}
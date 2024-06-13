using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    /// <summary>
    /// Filter Object for Reviews to define how to filter them by using GetAll() in the controller
    /// </summary>
    public class QueryObject
    {  
        public string? Artist {get; set;} = null;

        public string? Venue {get; set;} = null;

        public string? Genre {get; set;} = null;

        public string? SortBy { get; set; } = null;

        public bool IsDescending { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
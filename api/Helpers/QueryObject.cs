using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        
        public string? Title {get; set;} = null;

        public string? Venue {get; set;} = null;

        public string? Genre {get; set;} = null;

        public string? SortBy { get; set; } = null;

        public bool IsDescending { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        // public int Rating {get; set;}
    }
}
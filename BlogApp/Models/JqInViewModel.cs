using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class JqInViewModel
    {
        // No. of records to fetch
        public int rows { get; set; }

        // The page index
        public int page { get; set; }

        // Sort column name
        public string sidx { get; set; }

        // Sort order "asc" of "desc"
        public string sord { get; set; }
    }
}
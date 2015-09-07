using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS.Web.Models
{
    public class ItemPaging
    {
        public int ItemsPerPage { get; set; }
        public int NumOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
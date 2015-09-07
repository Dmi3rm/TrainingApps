using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FS.Web.Models.Home
{
    public class SNovelty
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string htmlcode { get; set; }
    }
}
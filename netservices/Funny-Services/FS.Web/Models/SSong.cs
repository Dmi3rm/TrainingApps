using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FS.Web.Models
{
    public class SSong
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte[] SongData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string SongMimeType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FS.Web.Models
{
    public class SPicture
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte[] PictureData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string PictureMimeType { get; set; }
    }
}
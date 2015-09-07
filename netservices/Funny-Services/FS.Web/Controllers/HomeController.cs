using FS.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FS.Web.Models.Home;
using FS.Web.HelpClasses;
using FS.Web.Models;

namespace FS.Web.Controllers
{
    public class HomeController : Controller
    {
        FSContext context = new FSContext();
        //XmlWorker xmlWorker = new XmlWorker();

        public ActionResult Index()
        {
            //string City = xmlWorker.GetInfo("http://weather.yandex.ru/static/cities.xml", "country");
            //ViewBag.City = City;

            return View();
        }


        public ActionResult GetAllSNoveltys ()
        {
            return PartialView("SNoveltysPartial", context.GetAllSNoveltys());
        }


        public ActionResult GetGreetings ()
        {
            return PartialView("SNoveltysPartial", context.GetGreetings());
        }


        public ActionResult GetNovations()
        {
            return PartialView("SNoveltysPartial", context.GetNovations());
        }


        public ActionResult GetNotes()
        {
            return PartialView("SNoveltysPartial", context.GetNotes());
        }


        public FileContentResult pic(string name)
        {
            SPicture spicture = context.GetSPictureByName(name);
            return File(spicture.PictureData, spicture.PictureMimeType);
        }


        public FileContentResult son(string name)
        {
            SSong ssong = context.GetSSongByName(name);
            return File(ssong.SongData, ssong.SongMimeType);
        }


        public FileContentResult vid(string name)
        {
            SVideo svideo = context.GetSVideoByName(name);
            return File(svideo.VideoData, svideo.VideoMimeType);
        }
    }
}
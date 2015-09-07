using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FS.Web.Controllers
{
    public class AppsController : Controller
    {
        //
        // GET: /Apps/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LogicSchemeBuilder()
        {
            return View("LogicSchemeBuilder");
        }


        public ActionResult LSBHelp()
        {
            return View();
        }


        public ActionResult Graphs()
        {
            return View("3dgraphs");
        }

        public ActionResult WorldWar5()
        {
            return View();
        }
	}
}
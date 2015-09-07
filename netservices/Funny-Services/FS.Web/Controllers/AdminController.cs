using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FS.Web.Models.Home;
using FS.Web.Models;
using FS.Web.Models.Admin;
using System.Web.Security;



namespace FS.Web.Controllers
{
    [Authorize(Roles = "HEAD")]
    public class AdminController : Controller
    {
        FSContext context = new FSContext();

        public ActionResult Index()
        {
            return View();
        }



        #region SNoveltys

        public ActionResult SNoveltysEditPage()
        {
            return View(context.GetAllSNoveltys());
        }



        [HttpGet]
        [ValidateInput(false)]
        public ActionResult EditSNovelty(int SNoveltyId)
        {
            SNovelty snovelty = context.GetSNoveltyById(SNoveltyId);
            return View(snovelty);
        }



        public ActionResult AddSNovelty()
        {
            return View("EditSNovelty", new SNovelty());
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSNovelty(SNovelty snovelty)
        {
            if (ModelState.IsValid)
            {
                SNovelty SNovelty = null;

                if (snovelty.Id == 0)
                {
                    SNovelty = snovelty;
                    context.AddSNovelty(SNovelty);
                }
                else
                {
                    SNovelty = context.GetSNoveltyById(snovelty.Id);
                    SNovelty.Name = snovelty.Name;
                    SNovelty.Type = snovelty.Type;
                    SNovelty.htmlcode = snovelty.htmlcode;
                }

                context.SaveChanges();
            }
            return RedirectToAction("SNoveltysEditPage");
        }



        public ActionResult RemoveSNovelty(int SNoveltyId)
        {
            context.RemoveSNovelty(SNoveltyId);
            return RedirectToAction("SNoveltysEditPage");
        }



        public ActionResult RemoveOldSNovelties()
        {
            return View();
        }

        #endregion




        #region SPictures

        public ActionResult SPicturesEditPage()
        {
            return View(context.GetAllSPictures());
        }



        public ActionResult SPicturesPartialEditPage(List<SPicture> spictures)
        {
            return PartialView(spictures);
        }



        public ActionResult AddSPicture ()
        {
            return PartialView("EditSPicture", new SPicture());
        }



        [HttpGet]
        public ActionResult EditSPicture (int spictureId)
        {
            SPicture spicture = context.GetSPictureById(spictureId);
            return PartialView(spicture);
        }



        [HttpPost]
        public ActionResult EditSPicture (SPicture spicture, HttpPostedFileBase image)
        {
            if (ModelState.IsValid && image != null && spicture.Name != "")
            {
                SPicture SPicture = null;

                if (spicture.Id == 0)
                {
                    SPicture = spicture;
                    SPicture.PictureMimeType = image.ContentType;
                    SPicture.PictureData = new byte[image.ContentLength];
                    image.InputStream.Read(SPicture.PictureData, 0, image.ContentLength);
                    context.AddSPicture(SPicture);
                }
                else
                {
                    SPicture = context.GetSPictureById(spicture.Id);
                    SPicture.Name = spicture.Name;
                    SPicture.PictureMimeType = image.ContentType;
                    SPicture.PictureData = new byte[image.ContentLength];
                    image.InputStream.Read(SPicture.PictureData, 0, image.ContentLength);
                }

                context.SaveChanges();
            }
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }



        public ActionResult RemoveSPicture(int spictureId)
        {
            context.RemoveSPicture(spictureId);
            return RedirectToAction("SPicturesEditPage");
        }

        public ActionResult PicsByNames(string names)
        {
            string[] picNames = names.Split(new Char[] {'!'});
            List<SPicture> spictures = new List<SPicture>();
            foreach (string name in picNames)
            {
                try
                {
                    spictures.Add(context.GetSPictureByName(name));
                }
                catch
                {

                }
            }

            return PartialView("SPicturesPartialEditPage", spictures);
        }

        #endregion




        #region SSongs

        public ActionResult SSongsEditPage()
        {
            return View(context.GetAllSSongs());
        }



        public ActionResult SSongsPartialEditPage(List<SSong> ssongs)
        {
            return PartialView(ssongs);
        }



        public ActionResult AddSSong()
        {
            return PartialView("EditSSong", new SSong());
        }



        [HttpGet]
        public ActionResult EditSSong(int ssongId)
        {
            SSong ssong = context.GetSSongById(ssongId);
            return PartialView(ssong);
        }



        [HttpPost]
        public ActionResult EditSSong(SSong ssong, HttpPostedFileBase song)
        {
            if (ModelState.IsValid && song != null && ssong.Name != "")
            {
                SSong SSong = null;

                if (ssong.Id == 0)
                {
                    SSong = ssong;
                    SSong.SongMimeType = song.ContentType;
                    SSong.SongData = new byte[song.ContentLength];
                    song.InputStream.Read(SSong.SongData, 0, song.ContentLength);
                    context.AddSSong(SSong);
                }
                else
                {
                    SSong = context.GetSSongById(ssong.Id);
                    SSong.SongMimeType = song.ContentType;
                    SSong.SongData = new byte[song.ContentLength];
                    song.InputStream.Read(SSong.SongData, 0, song.ContentLength);
                }

                context.SaveChanges();
            }
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }



        public ActionResult RemoveSSong(int ssongId)
        {
            context.RemoveSSong(ssongId);
            return RedirectToAction("SSongsEditPage");
        }

        public ActionResult SonsByNames(string names)
        {
            string[] songNames = names.Split(new Char[] { '!' });
            List<SSong> ssongs = new List<SSong>();
            foreach (string name in songNames)
            {
                try
                {
                    ssongs.Add(context.GetSSongByName(name));
                }
                catch
                {

                }
            }

            return PartialView("SSongsPartialEditPage", ssongs);
        }

        #endregion




        #region SVideos

        public ActionResult SVideosEditPage()
        {
            return View(context.GetAllSVideos());
        }



        public ActionResult SVideosPartialEditPage(List<SVideo> svideos)
        {
            return PartialView(svideos);
        }



        public ActionResult AddSVideo()
        {
            return PartialView("EditSVideo", new SVideo());
        }



        [HttpGet]
        public ActionResult EditSVideo(int svideoId)
        {
            SVideo svideo = context.GetSVideoById(svideoId);
            return PartialView(svideo);
        }



        [HttpPost]
        public ActionResult EditSVideo(SVideo svideo, HttpPostedFileBase movie)
        {
            if (ModelState.IsValid && movie != null && svideo.Name != "")
            {
                SVideo SVideo = null;

                if (svideo.Id == 0)
                {
                    SVideo = svideo;
                    SVideo.VideoMimeType = movie.ContentType;
                    SVideo.VideoData = new byte[movie.ContentLength];
                    movie.InputStream.Read(SVideo.VideoData, 0, movie.ContentLength);
                    context.AddSVideo(SVideo);
                }
                else
                {
                    SVideo = context.GetSVideoById(svideo.Id);
                    SVideo.VideoMimeType = movie.ContentType;
                    SVideo.VideoData = new byte[movie.ContentLength];
                    movie.InputStream.Read(SVideo.VideoData, 0, movie.ContentLength);
                }

                context.SaveChanges();
            }
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }



        public ActionResult RemoveSVideo(int svideoId)
        {
            context.RemoveSVideo(svideoId);
            return RedirectToAction("SVideosEditPage");
        }

        public ActionResult VidsByNames(string names)
        {
            string[] videosNames = names.Split(new Char[] { '!' });
            List<SVideo> svideos = new List<SVideo>();
            foreach (string name in videosNames)
            {
                try
                {
                    svideos.Add(context.GetSVideoByName(name));
                }
                catch
                {

                }
            }

            return PartialView("SVideosPartialEditPage", svideos);
        }

        #endregion




        #region Users

        public ActionResult UsersEditList(int currentPage = 0)
        {
            int UsersPerPage = 15;
            List<User> Users = context.GetAllUsers().OrderBy(u => u.Login).ToList();
            UserPaging userPaging = new UserPaging(Users, currentPage, UsersPerPage, Users.Count);
            return View(userPaging);
        }


        [HttpGet]
        public ActionResult SetUserRole(int Page, int UserId, string RoleId, int code)
        {
            ViewData["Page"] = Page; ViewData["UserId"] = UserId; ViewData["RoleId"] = RoleId; ViewData["UserName"] = context.GetUserById(UserId).Login;
            return View();
        }

        [HttpPost]
        public ActionResult SetUserRole (int page, int userId, string roleId)
        {
            int RoleId;
            if (Int32.TryParse(roleId, out RoleId))
            {
                context.GetUserById(userId).RoleId = RoleId;
                context.SaveChanges();
            }
            return RedirectToAction("UsersEditList", new { currentPage = page });
        }


        public ActionResult RemoveUser (int userId, int page)
        {
            if (userId != context.GetUserByName(HttpContext.User.Identity.Name).Id)
            {
                context.RemoveUser(userId);
            }
            return RedirectToAction("UsersEditList", new { currentPage = page });
        }


        public ActionResult LogAs (int userId)
        {
            LogOnModel model = new LogOnModel();
            User user = context.GetUserById(userId);
            model.UserName = user.Login;
            model.Password = user.Password;
            model.RememberMe = false;

            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
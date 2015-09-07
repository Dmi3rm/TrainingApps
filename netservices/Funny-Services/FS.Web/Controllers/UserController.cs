using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FS.Web.Models;

namespace FS.Web.Controllers
{
    public class UserController : Controller
    {
        FSContext context = new FSContext();



        #region Personal
        [Authorize]
        public ActionResult UserPage ()
        {
            User currentUser = context.GetUserByName(HttpContext.User.Identity.Name);
            return View(currentUser);
        }



        [HttpPost]
        [Authorize]
        public ActionResult ChangeAva(HttpPostedFileBase image)
        {
            User currentUser = context.GetUserByName(HttpContext.User.Identity.Name);
            if (image != null)
            {
                currentUser.ImageMimeType = image.ContentType;
                currentUser.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(currentUser.ImageData, 0, image.ContentLength);
                context.SaveChanges();
            }
            return RedirectToAction("UserPage", "User");
        }



        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            User currentUser = context.GetUserByName(HttpContext.User.Identity.Name);
            if (currentUser.Password == oldPassword)
            {
                if (newPassword == confirmPassword)
                {
                    currentUser.Password = newPassword;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("UserPage", "User");
        }



        [Authorize]
        public ActionResult QuickUserData()
        {
            User currentUser = context.GetUserByName(HttpContext.User.Identity.Name);
            return View(currentUser);
        }
        #endregion


        public ActionResult AnotherUserPage(int userId)
        {
            User AnotherUser = context.GetUserById(userId);
            return View(AnotherUser);
        }


        #region GetImage

        public FileContentResult GetImage()
        {
            User currentUser = context.GetUserByName(HttpContext.User.Identity.Name);
            return File(currentUser.ImageData, currentUser.ImageMimeType);
        }



        public FileContentResult GetImageByUserId(int userId)
        {
            User currentUser = context.GetUserById(userId);
            return File(currentUser.ImageData, currentUser.ImageMimeType);
        }

        #endregion

    }
}
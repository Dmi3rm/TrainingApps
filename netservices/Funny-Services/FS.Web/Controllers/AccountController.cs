using System;
using System.Web;
using System.Web.Mvc;
using FS.Web.Models;
using System.Web.Security;
using FS.Web.Providers;
using System.IO;
using Microsoft.Win32;
using FS.Web.HelpClasses;
using System.Drawing.Imaging;

namespace CustomAuthorization.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        FSContext context = new FSContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }




        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }




        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (model.Captcha != (string)Session["code"])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            }
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(model.UserName, model.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    User currentUser = context.GetUserByName(model.UserName);

                    try
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + "Content/images/chelovek.jpg";
                        FileStream imageStream = new FileStream(path, FileMode.Open);
                        currentUser.ImageMimeType = System.Web.MimeMapping.GetMimeMapping(path);
                        currentUser.ImageData = new byte[imageStream.Length];
                        imageStream.Read(currentUser.ImageData, 0, Convert.ToInt32(imageStream.Length));
                        context.SaveChanges();
                    }
                    catch { }
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View(model);
        }



        public ActionResult Captcha()
        {
            string code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            Session["code"] = code;
            CaptchaImage captcha = new CaptchaImage(code, 80, 40);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            captcha.Dispose();
            return null;
        }



        string GetMimeType(FileInfo fileInfo)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(
                fileInfo.Extension.ToLower()
                );

            if (regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType != null)
                    mimeType = contentType.ToString();
            }

            return mimeType;
        }
    }
}
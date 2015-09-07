using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FS.Web.Models;
using System.Web.Security;


namespace FS.Web.Controllers
{
    public class ForumController : Controller
    {
        FSContext context = new FSContext();


        #region FMessages

        public ActionResult FMessages(int sectionId, int subsectionId, int topicId, int currentPage = 0, int NewMsg = 0)
        {
            if (currentPage < 0)
            { currentPage = 1; }
            if ((currentPage == 0) && (NewMsg == 0))
            {
                context.GetFTopicById(topicId).NumViews++;
                context.SaveChanges();
            }

            int MsgsPerPage = 10;
            List<FMessage> AllFMessages = context.GetFMessagesByFTopicId(topicId).ToList();

            FMsgPaging fmsgPaging = new FMsgPaging(AllFMessages, currentPage, MsgsPerPage, AllFMessages.Count);

            FSection section = context.GetFSectionById(sectionId);
            if (section.Closed == 0) { ViewData["FSectionClosed"] = 0; } //Закрыт ли раздел
            else { ViewData["FSectionClosed"] = 1; }

            FSubsection subsection = context.GetFSubsectionById(subsectionId); //Закрыта ли секция
            if (subsection.Closed == 0) { ViewData["FSubsectionClosed"]  = 0; }
            else { ViewData["FSubsectionClosed"] = 1; }

            FTopic topic = context.GetFTopicById(topicId);
            if (topic.Closed == 0) { ViewData["FTopicClosed"] = 0; }
            else { ViewData["FTopicClosed"] = 1; }

            ViewData["FTopicName"] = topic.Name;
            ViewData["Role"] = GetCurrentUserRole();
            ViewData["FSectionId"] = sectionId;
            ViewData["FSubsectionId"] = subsectionId;
            ViewData["FTopicId"] = topicId;
            return View("FMessages", fmsgPaging);
        }

        [HttpPost]
        [Authorize]
        public ActionResult NewFMessage(string Text, int sectionId, int subsectionId, int topicId)
        {
            if ((Text != "") && (Text.Length < 3501) && (((context.GetFTopicById(topicId).Closed == 0) && (context.GetFSubsectionById(subsectionId).Closed == 0) && (context.GetFSectionById(sectionId).Closed == 0) ) || (GetCurrentUserRole() == 2)  ))
            {
                FMessage fmessage = new FMessage();
                fmessage.Text = Text;
                fmessage.FTopicId = topicId;
                User currentUser = context.GetUserByName(HttpContext.User.Identity.Name);
                fmessage.UserId = currentUser.Id;
                fmessage.WriteDate = DateTime.Now;
                

                context.AddFMessage(fmessage, topicId, subsectionId, sectionId);
            }

            return RedirectToAction("FMessages", new {topicId = topicId, subsectionId = subsectionId, sectionId = sectionId, NewMsg = 1});
        }

        [Authorize]
        public void DeleteFMessage(int messageId, int topicId, int subsectionId, int sectionId)
        {
            if ((GetCurrentUserRole() == 2) || (User.Identity.Name == context.GetFMessageById(messageId).User.Login))
            {
                context.RemoveFMessageUpdate(messageId, topicId, subsectionId, sectionId);
            }
        }


        [Authorize]
        [HttpGet]
        public string EditFMessage(int messageId)
        {
            if ((GetCurrentUserRole() == 2) || (User.Identity.Name == context.GetFMessageById(messageId).User.Login))
            {
                return context.GetFMessageById(messageId).Text;
            }
            return "У вас нет прав";
        }


        [Authorize]
        [HttpPost]
        public void EditFMessage(int messageId, string Text)
        {
            if (((GetCurrentUserRole() == 2) || (User.Identity.Name == context.GetFMessageById(messageId).User.Login)) && (Text != "") && (Text.Length < 3501))
            {
                context.GetFMessageById(messageId).Text = Text;
                context.SaveChanges();
            }
        }

        #endregion




        #region Topics

        public ActionResult FTopics(int subsectionId, int sectionId, int currentPage = 1)
        {
            if (currentPage < 0)
            { currentPage = 1; }
            int TopicsPerPage = 10;
            List<FTopic> allFTopics = context.GetFTopicsByFSubsectionId(subsectionId).OrderByDescending(s => s.Fix).ThenByDescending(s => s.LastFMsgDate).ToList();

            FTpcPaging ftpcPaging = new FTpcPaging(allFTopics, currentPage, TopicsPerPage, allFTopics.Count);

            FSection section = context.GetFSectionById(sectionId);
            if (section.Closed == 0) { ViewData["FSectionClosed"] = 0; } //Закрыт ли раздел
            else { ViewData["FSectionClosed"] = 1; }

            FSubsection subsection = context.GetFSubsectionById(subsectionId); //Закрыта ли секция
            if (subsection.Closed == 0) { ViewData["FSubsectionClosed"] = 0; } 
            else { ViewData["FSubsectionClosed"] = 1; }
            ViewData["FSubsectionName"] = subsection.Name;
            ViewData["Role"] = GetCurrentUserRole();
            ViewData["FSubsectionId"] = subsectionId;
            ViewData["FSectionId"] = sectionId;
            return View("FTopics", ftpcPaging);
        }

        [Authorize]
        public ActionResult NewFTopic(int subsectionId, int sectionId)
        {
            ViewData["FSubsectionId"] = subsectionId;
            ViewData["FSectionId"] = sectionId;
            ViewData["Role"] = GetCurrentUserRole();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult NewFTopic(string tName, int subsectionId, int sectionId)
        {
            if ((tName != "") && (tName.Length < 51) && (((context.GetFSubsectionById(subsectionId).Closed == 0) && (context.GetFSectionById(sectionId).Closed == 0))  || (GetCurrentUserRole() == 2)  ))
            {
                FTopic topic = new FTopic();
                topic.FSubsectionId = subsectionId;
                topic.Name = tName;
                topic.Closed = 0;
                topic.Fix = 0;
                topic.LastFMsgDate = DateTime.Now;
                topic.UserId = 0;
                topic.NumFMsgs = 0;
                topic.NumViews = 0;

                context.AddFTopic(topic, subsectionId, sectionId);
                context.SaveChanges();
            }

            return RedirectToAction("FTopics", new { subsectionId = subsectionId, sectionId = sectionId });
        }

        [Authorize(Roles = "ADMIN")]
        public void DeleteFTopic(int topicId, int subsectionId, int sectionId)
        {
            context.RemoveFTopicUpdate(topicId, subsectionId, sectionId);
            context.SaveChanges();
        }
        
        #endregion




        #region FSubsections

        public ActionResult FSubsections(int sectionId)
        {
            List<FSubsection> fsubsections = context.GetFSubsectionsByFSectionId(sectionId).ToList() ;
            fsubsections.Sort(delegate(FSubsection fsubsection1, FSubsection fsubsection2)
            { return fsubsection1.OrderNum.CompareTo(fsubsection2.OrderNum); });

            if ((Session["ReplacedItemType"] != null) && ((int)Session["ReplacedItemType"] == 3)) //Возможно ли перемещение сюда части форума
            { ViewData["AllowInput"] = 1;}
            else { ViewData["AllowInput"] = 0; }

            FSection section = context.GetFSectionById(sectionId);
            if (section.Closed == 0) { ViewData["FSectionClosed"] = 0; } //Закрыта ли секция
            else { ViewData["FSectionClosed"] = 1; }
            ViewData["FSectionName"] = section.Name;
            ViewData["Role"] = GetCurrentUserRole();
            ViewData["FSectionId"] = sectionId;
            return View("FSubsections", fsubsections);
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult NewFSubsection(int sectionId)
        {
            ViewData["FSectionId"] = sectionId;
            ViewBag.Role = GetCurrentUserRole();
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult NewFSubsection(string sName, int sectionId)
        {
            if ((sName != "") && (sName.Length < 51))
            {
                FSubsection subsection = new FSubsection();
                subsection.FSectionId = sectionId;
                subsection.Name = sName;
                subsection.Closed = 0;
                subsection.LastFMsgDate = DateTime.Now;
                subsection.FTopicId = 0;
                subsection.UserId = 0;
                subsection.NumFMsgs = 0;
                subsection.NumFTopics = 0;
                subsection.OrderNum = 0;

                context.AddFSubsection(subsection, sectionId);
            }

            ViewData["Role"] = GetCurrentUserRole();
            ViewData["FSectionId"] = sectionId;

            return RedirectToAction("FSubsections", new { sectionId = sectionId});
        }

        [Authorize(Roles = "ADMIN")]
        public void DeleteFSubsection(int subsectionId, int sectionId)
        {
            context.RemoveFSubsectionUpdate(subsectionId, sectionId);
        }

        #endregion




        #region FSections

        public ActionResult FSections()
        {
            ModelState.Clear();
            ViewData["Role"] = GetCurrentUserRole();
            if ((Session["ReplacedItemType"] != null) && ((int)Session["ReplacedItemType"] == 2))
            {
                ViewData["AllowInput"] = 1;
            }
            else
            {
                ViewData["AllowInput"] = 0;
            }

            List<FSection> fsections = context.GetAllFSections().ToList();
            fsections.Sort(delegate(FSection fsection1, FSection fsection2)
            { return fsection1.OrderNum.CompareTo(fsection2.OrderNum); });
            return View(fsections);
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult NewFSection()
        {
            ViewBag.Role = GetCurrentUserRole();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult NewFSection(string sName)
        {
            if ((sName != "") && (sName.Length < 51))
            {
                FSection section = new FSection();
                section.Name = sName;
                section.Closed = 0;
                section.NumFMsgs = 0;
                section.NumFSubsections = 0;
                section.NumFTopics = 0;
                section.OrderNum = 1;


                context.AddFSection(section);
                foreach (FSection fsection in context.GetAllFSections())
                {
                    fsection.OrderNum++;
                }
                context.SaveChanges();
            }
            return RedirectToAction("FSections");
        }

        [Authorize(Roles="ADMIN")]
        public void DeleteFSection(int sectionId)
        {
            int OrderNumOfDelSec = context.GetAllFSections().Where(s => s.Id == sectionId).First().OrderNum;
            foreach (FSection fsection in context.GetAllFSections().Where(s => s.OrderNum > OrderNumOfDelSec))
            {
                fsection.OrderNum--;
            }
            context.RemoveFSection(sectionId);
            context.SaveChanges();
        }
        #endregion




        #region Order functions
        
        public void SetNewFSectionsOrder (string arr)
        {
            if (GetCurrentUserRole() == 2)
            {
                string[] split = arr.Split(new char [] {','});

                for (int i = 0; i < split.Length - 1; i++)
                {
                    FSection fsection = context.GetAllFSections().Where(s => s.Id == Convert.ToInt32(split[i])).First();
                    fsection.OrderNum = i;
                }
                context.SaveChanges();
            }
        }


        
        public void SetNewFSubsectionsOrder (int fsectionId, string arr)
        {
            if (GetCurrentUserRole() == 2)
            {
                string[] split = arr.Split(new char[] { ',' });

                for (int i = 0; i < split.Length - 1; i++)
                {
                    FSubsection fsubsection = context.GetFSubsectionsByFSectionId(fsectionId).Where(s => s.Id == Convert.ToInt32(split[i])).First();
                    fsubsection.OrderNum = i;
                }

                context.SaveChanges();
            }
        }


        [Authorize(Roles = "ADMIN")]
        public void FixFTopic (int topicId)
        {
            FTopic topic = context.GetFTopicById(topicId);
            if (topic.Fix == 0) { topic.Fix = 1; }
            else { topic.Fix = 0; }
            context.SaveChanges();
        }

        #endregion



        public int? GetCurrentUserRole()
        {
            int? RoleId = 0;
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                RoleId = context.GetUserByName(HttpContext.User.Identity.Name).RoleId;
            }
            else { RoleId = 0; }
            return RoleId;
        }



        #region Admin Edit Functions

        [Authorize(Roles = "ADMIN")]
        public void RenameFItem(int type, int itemId, string name)
        {
            if ((name != "") && (name.Length < 51) )
            {
                try
                {
                    if (type == 1)
                    {
                        context.GetFSectionById(itemId).Name = name;
                    }
                    if (type == 2)
                    {
                        context.GetFSubsectionById(itemId).Name = name;
                    }
                    if (type == 3)
                    {
                        context.GetFTopicById(itemId).Name = name;
                    }
                    context.SaveChanges();
                }
                catch { }
            }
        }


        [Authorize(Roles = "ADMIN")]
        public void RememberFItem(int type, int itemId)
        {
            Session["ReplacedItemType"] = type;
            Session["ReplacedItemId"] = itemId;
        }


        [Authorize(Roles = "ADMIN")]
        public void InputFItem(int inputPlaceId)
        {
            try
            {
                if (Convert.ToInt32(Session["ReplacedItemType"]) == 2) //секцию в раздел
                {
                    FSection section = context.GetFSectionById(inputPlaceId);
                    FSubsection subsection = context.GetFSubsectionById(Convert.ToInt32(Session["ReplacedItemId"]));
                    int prevSectionId = subsection.FSectionId;

                    subsection.FSectionId = section.Id; //Перемещение

                    section.NumFMsgs = section.NumFMsgs + subsection.NumFMsgs; //Обновление счетчика
                    section.NumFTopics = section.NumFTopics + subsection.NumFTopics;
                    section.NumFSubsections++;
                    FSection prevSection = context.GetFSectionById(prevSectionId);
                    prevSection.NumFMsgs = prevSection.NumFMsgs - subsection.NumFMsgs;
                    prevSection.NumFTopics = prevSection.NumFTopics - subsection.NumFTopics;
                    prevSection.NumFSubsections--;
                }
                if ((int)Session["ReplacedItemType"] == 3) //тему в секцию
                {
                    FSubsection subsection = context.GetFSubsectionById(inputPlaceId);
                    FTopic topic = context.GetFTopicById((int)Session["ReplacedItemId"]);
                    int prevSubsectionId = topic.FSubsectionId;

                    topic.FSubsectionId = subsection.Id; //Перемещение

                    subsection.NumFMsgs = subsection.NumFMsgs + topic.NumFMsgs; //Обновление счетчика
                    subsection.NumFTopics++;
                    subsection.FSection.NumFMsgs = subsection.FSection.NumFMsgs + topic.NumFMsgs;
                    subsection.FSection.NumFTopics++;

                    FSubsection prevSubsection = context.GetFSubsectionById(prevSubsectionId);
                    prevSubsection.NumFMsgs = prevSubsection.NumFMsgs - topic.NumFMsgs;
                    prevSubsection.NumFTopics--;
                    prevSubsection.FSection.NumFMsgs = prevSubsection.FSection.NumFMsgs - topic.NumFMsgs;
                    prevSubsection.FSection.NumFTopics--;

                    context.SetFSubsectionLastMsg(inputPlaceId);
                    context.SetFSubsectionLastMsg(prevSubsectionId);
                }
                Session["ReplacedItemType"] = null;
                Session["ReplacedItemId"] = null;
            }
            catch { }
            context.SaveChanges();
        }


        [Authorize(Roles = "ADMIN")]
        public void Lock(int type, int itemId)
        {
            switch (type)
            {
                case 3:
                    FTopic topic = context.GetFTopicById(itemId);
                    if (topic.Closed == 0)
                    {
                        topic.Closed = 1;
                    }
                    else
                    {
                        topic.Closed = 0;
                    }
                    break;
                case 2:
                    FSubsection subsection = context.GetFSubsectionById(itemId);
                    if (subsection.Closed == 0)
                    {
                        subsection.Closed = 1;
                    }
                    else
                    {
                        subsection.Closed = 0;
                    }
                    break;
                case 1:
                    FSection section = context.GetFSectionById(itemId);
                    if (section.Closed == 0)
                    {
                        section.Closed = 1;
                    }
                    else
                    {
                        section.Closed = 0;
                    }
                    break;
                default: break;
            }

            context.SaveChanges();
        }


        #endregion



        #region Search

        public ActionResult FSearch (int type, int itemId, string ask)
        {
            if (ask.Length < 150)
            {
                FSearch FSearchResult = new FSearch(type, itemId, ask);
                Session["FSearchResult"] = FSearchResult;
                FoundFMsgPaging foundFMsgPaging = new FoundFMsgPaging(FSearchResult.FoundFMessages, FSearchResult.FoundFMessages.Count);
                FoundFTpcPaging foundFTpcPaging = new FoundFTpcPaging(FSearchResult.FoundFTopics, FSearchResult.FoundFTopics.Count);
                SearchView searchView = new SearchView(FSearchResult.PlaceType, FSearchResult.PlaceName, ask, FSearchResult.LinkBackHref,
                    FSearchResult.LinkBackText, foundFTpcPaging, foundFMsgPaging);
                ViewData["Role"] = GetCurrentUserRole();
                return View("FSearch", searchView);
            }
            else return RedirectToAction("Index", "Home");
        }


        
        public PartialViewResult FoundFTopicsChangePage (int page)
        {
            if (page < 0)
            { page = 1; }
            FSearch FSearchResult = (FSearch)Session["FSearchResult"];
            FoundFTpcPaging foundFTpcPaging = new FoundFTpcPaging(FSearchResult.FoundFTopics, FSearchResult.FoundFTopics.Count, page);
            ViewData["Role"] = GetCurrentUserRole();
            return PartialView("Partials/FoundFTopicsChangePage",foundFTpcPaging);
        }



        public PartialViewResult FoundFMessagesChangePage(int page)
        {
            if (page < 0)
            { page = 1; }
            FSearch FSearchResult = (FSearch)Session["FSearchResult"];
            FoundFMsgPaging foundFMsgPaging = new FoundFMsgPaging(FSearchResult.FoundFMessages, FSearchResult.FoundFMessages.Count, page);
            ViewData["Role"] = GetCurrentUserRole();
            return PartialView("Partials/FoundFMessagesChangePage",foundFMsgPaging);
        }

        #endregion
    }
}
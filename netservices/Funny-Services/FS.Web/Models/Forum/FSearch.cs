using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace FS.Web.Models
{
    public class FoundFTopic
    {
        public FTopic FTopic { get; set; }
        public FSubsection FSubsection { get; set; }
        public FSection FSection { get; set; }
        public int Page { get; set; }
        public FoundFTopic (FTopic topic, int page, FSection section, FSubsection subsection)
        {
            FTopic = topic;
            Page = page;
            FSection = section;
            FSubsection = subsection;
        }
    }



    public class FoundFTpcPaging: ItemPaging
    {
        public List<FoundFTopic> FoundFTopics { get; set; }

        public FoundFTpcPaging(List<FoundFTopic> allFoundFTopics, int numOfAllFTpcs, int currentPage = 0, int ftpcsPerPage = 10 )
        {
            ItemsPerPage = ftpcsPerPage;
            double a = (double)numOfAllFTpcs / (double)ItemsPerPage;
            NumOfPages = (int)Math.Ceiling(a);
            if (currentPage != 0)
            {
                if (currentPage < NumOfPages)
                {
                    CurrentPage = currentPage;
                }
                else
                {
                    CurrentPage = NumOfPages;
                }
            }
            else
            {
                CurrentPage = NumOfPages;
            }

            FoundFTopics = allFoundFTopics.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        }
    }



    public class FoundFMessage
    {
        public FMessage FMessage { get; set; }
        public FTopic FTopic { get; set; }
        public FSubsection FSubsection { get; set; }
        public FSection FSection { get; set; }
        public int Page { get; set; }
        public FoundFMessage (FMessage message, int page, FSection section, FSubsection subsection, FTopic topic)
        {
            FMessage = message;
            FSection = section;
            FSubsection = subsection;
            FTopic = topic;
            Page = page;
            
        }
    }



    public class FoundFMsgPaging : ItemPaging
    {
        public List<FoundFMessage> FoundFMessages { get; set; }

        public FoundFMsgPaging(List<FoundFMessage> allFoundFMessages, int numOfAllFMsgs, int currentPage = 0, int fmsgsPerPage = 10)
        {
            ItemsPerPage = fmsgsPerPage;
            double a = (double)numOfAllFMsgs / (double)ItemsPerPage;
            NumOfPages = (int)Math.Ceiling(a);
            if (currentPage != 0)
            {
                if (currentPage < NumOfPages)
                {
                    CurrentPage = currentPage;
                }
                else
                {
                    CurrentPage = NumOfPages;
                }
            }
            else
            {
                CurrentPage = NumOfPages;
            }

            FoundFMessages = allFoundFMessages.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        }
    }



    public class FSearch
    {
        public FSContext context { get; set; }
        public string Ask { get; set; }
        public string PlaceType { get; set; }
        public string PlaceName { get; set; }
        public int TpcsPerPage { get; set; }
        public int MsgsPerPage { get; set; }
        public string LinkBackHref { get; set; }
        public string LinkBackText { get; set; }

        public List<FoundFTopic> FoundFTopics { get; set; }
        public List<FoundFMessage> FoundFMessages { get; set; }

        public FSearch(int type, int itemId, string ask, int tpcsPerPage = 10, int msgsPerPage = 10)
        {
            context = new FSContext();
            FoundFTopics = new List<FoundFTopic>();
            FoundFMessages = new List<FoundFMessage>();
            TpcsPerPage = tpcsPerPage;
            MsgsPerPage = msgsPerPage;
            Ask = ask;

            switch (type)
            {
                case 0:
                    PlaceType = "везде";
                    PlaceName = "";
                    LinkBackHref = "/Forum/FSections";
                    LinkBackText = " << Разделы << ";
                    CheckFSections(); break;
                case 1:
                    PlaceType = "в разделе";
                    FSection section = context.GetFSectionById(itemId);
                    PlaceName = section.Name;
                    LinkBackHref = "/Forum/FSubsections?sectionId=" + section.Id;
                    LinkBackText = " << " + section.Name + " << ";
                    CheckFSubsections(section); break;
                case 2:
                    FSubsection subsection = context.GetFSubsectionById(itemId);
                    PlaceType = "в секции";
                    PlaceName = subsection.Name;
                    LinkBackHref = "/Forum/FTopics?sectionId=" + subsection.FSectionId + "&&subsectionId=" + subsection.Id;
                    LinkBackText = " << " + subsection.Name + " << ";
                    CheckFTopics(subsection.FSection, subsection); break;
                case 3:
                    FTopic topic = context.GetFTopicById(itemId);
                    PlaceType = "в теме";
                    PlaceName = topic.Name;
                    LinkBackHref = "/Forum/FMessages?sectionId=" + topic.FSubsection.FSectionId + "&&subsectionId=" + topic.FSubsectionId + "&&topicId=" + topic.Id;
                    LinkBackText = " << " + topic.Name + " << ";
                    FindFMessages(topic, topic.FSubsection, topic.FSubsection.FSection); break;
                default:
                    break;
            }
        }

        public void CheckFSections()
        {
            List<FSection> FSections = context.GetAllFSections();
            foreach (FSection section in FSections)
            {
                CheckFSubsections(section);
            }
        }



        public void CheckFSubsections(FSection section)
        {
            List<FSubsection> FSubsections = context.GetFSubsectionsByFSectionId(section.Id);
            foreach (FSubsection subsection in FSubsections)
            {
                CheckFTopics(section, subsection);
            }
        }



        public void CheckFTopics(FSection section, FSubsection subsection)
        {
            List<FTopic> FTopics = context.GetFTopicsByFSubsectionId(subsection.Id);
            for (int TpcNum = 0; TpcNum < FTopics.Count; TpcNum++)
            {
                FTopic topic = FTopics[TpcNum];

                CheckFTopic(topic, TpcNum, section, subsection); //Проверка темы и добавление в случае совпадения
                FindFMessages(topic, subsection, section); //Добавление подходящих сообщений
            }
        }



        public void CheckFTopic(FTopic topic, int TpcNum, FSection section, FSubsection subsection)
        {
            if (HasAnswer(topic.Name))
            {
                double a = (double)(FoundFMessages.Count + 1) / (double)TpcsPerPage;
                int Page = (int)Math.Ceiling(a);

                FoundFTopics.Add(new FoundFTopic(topic, Page, section, subsection));
            }
        }




        public void FindFMessages(FTopic topic, FSubsection subsection, FSection section)
        {
            List<FMessage> FMessages = context.GetFMessagesByFTopicId(topic.Id);
            for (int MsgNum = 0; MsgNum < FMessages.Count; MsgNum++)
            {
                FMessage message = FMessages[MsgNum];
                if (HasAnswer(message.Text))
                {
                    double a = (double)(FoundFMessages.Count + 1) / (double)MsgsPerPage;
                    int Page = (int)Math.Ceiling(a);

                    FoundFMessages.Add(new FoundFMessage(message, Page, section, subsection, topic));
                }
            }
        }




        public bool HasAnswer(string text)
        {
            text.ToLower();
            string ask = Ask.ToLower();
            string [] words = ask.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int Suits = 0;

            for (int i = 0; i < words.Length; i++ )
            {
                string word = words[i];
                if (word.Length > 7)
                {
                    word = word.Substring(0, word.Length - 2);
                }
                else
                {
                    if (word.Length > 5)
                    {
                        word = word.Substring(0, word.Length - 1);
                    }
                }

                if (word.Length > 2)
                {
                    if (text.IndexOf(word) > -1)
                    {
                        Suits++;
                    }
                }

            }

            double SuitsStat = 0;
            double NumOfWords = 0;

            for (int i = 0; i < words.Length; i++ )
            {
                if (words[i].Length > 2)
                {
                    NumOfWords++;
                }
            }


            if (NumOfWords > 0)
            {
                 SuitsStat = Suits / NumOfWords;
            }
            if (SuitsStat > 0.6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    


    public class SearchView
    {
        public string Ask { get; set; }
        public string PlaceType { get; set; }
        public string PlaceName { get; set; }
        public string LinkBackHref { get; set; }
        public string LinkBackText { get; set; }

        public FoundFTpcPaging FoundFTpcPaging { get; set; }
        public FoundFMsgPaging FoundFMsgPaging { get; set; }

        public SearchView (string placeType, string placeName, string ask, string linkBackHref, string linkBackText, 
            FoundFTpcPaging foundFTpcPaging, FoundFMsgPaging foundFMsgPaging)
        {
            PlaceType = placeType;
            PlaceName = placeName;
            Ask = ask;
            LinkBackHref = linkBackHref;
            LinkBackText = linkBackText;
            FoundFTpcPaging = foundFTpcPaging;
            FoundFMsgPaging = foundFMsgPaging;
        }
    }
}
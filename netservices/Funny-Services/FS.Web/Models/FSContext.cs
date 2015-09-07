using FS.Web.Models;
using FS.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FS;

namespace FS.Web.Models
{
    public class FSContext : DbContext
    {
        public FSContext() : base("FSConnection") { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FSection> FSections { get; set; }
        public DbSet<FSubsection> FSubsections {get; set;}
        public DbSet<FTopic> FTopics { get; set; }
        public DbSet<FMessage> FMessages { get; set; }
        public DbSet<SNovelty> SNovelties { get; set; }
        public DbSet<SPicture> SPictures { get; set; }
        public DbSet<SSong> SSongs { get; set; }
        public DbSet<SVideo> SVideos { get; set; }


        #region Roles
        public List<Role> GetAllRoles()
        { return Roles.ToList(); }
        #endregion



        #region Users

        public List<User> GetAllUsers()
        { return Users.ToList(); }

        public User GetUserByName(string username)
        {
            return Users.Where(user => user.Login == username).First();
        }


        public User GetUserById(int UserId)
        {
            return Users.Where(user => user.Id == UserId).First();
        }


        public void RemoveUser(int userId)
        {
            Users.Remove(GetUserById(userId));
            SaveChanges();
        }

        #endregion



//---------------------------------------------------------------Форум-------------------------------------------------------------------
        #region FMessages

        public List<FMessage> GetFMessagesByFTopicId(int topicId)
        {
            try
            {
                return FMessages.Where(fmessage => fmessage.FTopicId == topicId).ToList();
            }
            catch 
            {
                return null;
            }
        }

        public FMessage GetFMessageById(int fmessageId)
        {
            return FMessages.Where(m => m.Id == fmessageId).First();
        }

        public void AddFMessage(FMessage fmessage, int topicId, int subsectionId, int sectionId)
        {
            GetFSectionById(sectionId).NumFMsgs++;

            FSubsection subsection = GetFSubsectionById(subsectionId);
            FTopic topic = GetFTopicById(topicId);

            subsection.NumFMsgs++;
            subsection.LastFMsgDate = fmessage.WriteDate;
            subsection.UserId = fmessage.UserId;

            topic.NumFMsgs++;
            topic.LastFMsgDate = fmessage.WriteDate;
            topic.UserId = fmessage.UserId;

            FMessages.Add(fmessage);
            SaveChanges();
        }

        public void RemoveFMessageSimple(int messageId)
        {
            FMessage fmessage = FMessages.Find(messageId);
            FMessages.Remove(fmessage);
            SaveChanges();
        }

        public void RemoveFMessageUpdate(int messageId, int topicId, int subsectionId, int sectionId)
        {
            FSection section = this.GetFSectionById(sectionId);
            FSubsection subsection = this.GetFSubsectionById(subsectionId);
            FTopic topic = this.GetFTopicById(topicId);

            section.NumFMsgs--;
            subsection.NumFMsgs--;
            topic.NumFMsgs--;

            FMessage fmessage = FMessages.Find(messageId);
            DateTime WriteDate = fmessage.WriteDate;
            FMessages.Remove(fmessage);

            if (WriteDate == topic.LastFMsgDate)
            {
                SetFTopicLastMsg(topicId);
                if (WriteDate == subsection.LastFMsgDate)
                {
                    SetFSubsectionLastMsg(subsectionId);
                }
            }

            SaveChanges();
        }

        #endregion




        #region FTopics

        public List<FTopic> GetFTopicsByFSubsectionId(int FSubsectionId)
        {
            return FTopics.Where(topic => topic.FSubsectionId == FSubsectionId).ToList();
        }

        public FTopic GetFTopicById(int topicId)
        {
            return FTopics.Where(t => t.Id == topicId).First();
        }

        public void AddFTopic(FTopic topic, int subsectionId, int sectionId)
        {
            GetFSectionById(sectionId).NumFTopics++;
            GetFSubsectionById(subsectionId).NumFTopics++;
            FTopics.Add(topic);
            SaveChanges();
        }

        public void RemoveFTopicSimple(int topicId)
        {
            foreach (FMessage message in GetFMessagesByFTopicId(topicId))
            {
                RemoveFMessageSimple(message.Id);
            }
            FTopics.Remove(GetFTopicById(topicId));
            SaveChanges();
        }

        public void RemoveFTopicUpdate(int topicId, int subsectionId, int sectionId)
        {
            FSection section = this.GetFSectionById(sectionId);
            FSubsection subsection = this.GetFSubsectionById(subsectionId);
            FTopic topic = FTopics.Find(topicId);

            section.NumFMsgs = section.NumFMsgs - topic.NumFMsgs;
            subsection.NumFMsgs = subsection.NumFMsgs - topic.NumFMsgs;
            section.NumFTopics--;
            subsection.NumFTopics--;

            DateTime lastMsgDate = topic.LastFMsgDate;

            foreach (FMessage fmessage in GetFMessagesByFTopicId(topicId))
            {
                RemoveFMessageSimple(fmessage.Id);
            }

            FTopics.Remove(topic);
            if (lastMsgDate == subsection.LastFMsgDate)
            {
                SetFSubsectionLastMsg(subsectionId);
            }

            SaveChanges();
        }

        public void SetFTopicLastMsg(int topicId)
        {
            FTopic topic = this.GetFTopicById(topicId);
            List<FMessage> fmessages = GetFMessagesByFTopicId(topicId).ToList();
            if (fmessages.Count > 0)
            {
                fmessages.Sort(delegate(FMessage msg1, FMessage msg2)
                {
                    return (msg1.WriteDate.CompareTo(msg2.WriteDate));
                });
                FMessage lastMessage = fmessages.Last();
                topic.LastFMsgDate = lastMessage.WriteDate;
                topic.UserId = lastMessage.UserId;
                SaveChanges();
            }
            else
            {
                
            }
        }

        #endregion




        #region FSubsections

        public List<FSubsection> GetFSubsectionsByFSectionId(int FSectionId)
        {
            return FSubsections.Where(subsection => subsection.FSectionId == FSectionId).ToList();
        }

        public FSubsection GetFSubsectionById (int subsectionId)
        {
            return FSubsections.Where(s => s.Id == subsectionId).First();
        }

        public void AddFSubsection(FSubsection subsection, int sectionId)
        {
            GetFSectionById(sectionId).NumFSubsections++;
            FSubsections.Add(subsection);
            SaveChanges();
        }

        public void RemoveFSubsectionSimple(int subsectionId)
        {
            foreach (FTopic topic in GetFTopicsByFSubsectionId(subsectionId))
            {
                RemoveFTopicSimple(topic.Id);
            }

            FSubsections.Remove(GetFSubsectionById(subsectionId));
            SaveChanges();
        }

        public void RemoveFSubsectionUpdate(int subsectionId, int sectionId)
        {
            FSection section = this.GetFSectionById(sectionId);
            FSubsection subsection = this.GetFSubsectionById(subsectionId);

            section.NumFMsgs = section.NumFMsgs - subsection.NumFMsgs;
            section.NumFTopics = section.NumFTopics - subsection.NumFTopics;
            section.NumFSubsections--;

            foreach (FTopic topic in GetFTopicsByFSubsectionId(subsectionId))
            {
                RemoveFTopicSimple(topic.Id);
            }

            FSubsections.Remove(subsection);
            SaveChanges();
        }

        public void SetFSubsectionLastMsg(int subsectionId)
        {
            FSubsection subsection = this.GetFSubsectionById(subsectionId);
            List<FTopic> topics = GetFTopicsByFSubsectionId(subsectionId).ToList();

            if (topics.Count > 0)
            {
                topics.Sort(delegate(FTopic tpc1, FTopic tpc2)
                {
                    return (tpc1.LastFMsgDate.CompareTo(tpc2.LastFMsgDate));
                });
                FTopic lastTopic = topics.Last();
                subsection.LastFMsgDate = lastTopic.LastFMsgDate;
                subsection.UserId = lastTopic.UserId;
                subsection.FTopicId = lastTopic.Id;
                SaveChanges();
            }
            else
            {

            }
        }

        #endregion




        #region FSections

        public List<FSection> GetAllFSections()
        {
            return FSections.ToList();
        }

        public FSection GetFSectionById(int sectionId)
        {
            return FSections.Where(s => s.Id == sectionId).First();
        }

        public void AddFSection(FSection section)
        {
            FSections.Add(section);
            SaveChanges();
        }

        public void RemoveFSection(int Id)
        {
            foreach (FSubsection subsection in GetFSubsectionsByFSectionId(Id))
            {
                RemoveFSubsectionSimple(subsection.Id);
            }

            FSection section = FSections.Find(Id);
            FSections.Remove(section);
            SaveChanges();
        }

        #endregion





//----------------------------------------------------Новости на главной-------------------------------------------------------------------
        #region SNoveltys

        public List<SNovelty> GetAllSNoveltys()
        {
            return SNovelties.ToList();
        }

        public List<SNovelty> GetGreetings()
        {
            return SNovelties.Where(n => n.Type == 1).ToList();
        }

        public List<SNovelty> GetNovations()
        {
            return SNovelties.Where(n => n.Type == 2).ToList();
        }

        public List<SNovelty> GetNotes()
        {
            return SNovelties.Where(n => n.Type == 3).ToList();
        }

        public SNovelty GetSNoveltyById(int SNoveltyId)
        {
            SNovelty snovelty = SNovelties.First(n => n.Id == SNoveltyId);
            return snovelty;
        }

        public void AddSNovelty(SNovelty snovelty)
        {
            SNovelties.Add(snovelty);
            SaveChanges();
        }

        public void RemoveSNovelty(int SNoveltyId)
        {
            SNovelties.Remove(GetSNoveltyById(SNoveltyId));
            SaveChanges();
        }

        public void RemoveSNovelty(SNovelty snovelty)
        {
            SNovelties.Remove(snovelty);
            SaveChanges();
        }

        #endregion



        #region SPictures

        public List<SPicture> GetAllSPictures()
        {
            return SPictures.ToList();
        }

        public SPicture GetSPictureById(int SPictureId)
        {
            return SPictures.First(p => p.Id == SPictureId);
        }

        public SPicture GetSPictureByName(string SPictureName)
        {
            return SPictures.First(p => p.Name == SPictureName);
        }

        public void AddSPicture(SPicture spicture)
        {
            SPictures.Add(spicture);
            SaveChanges();
        }

        public void RemoveSPicture(int SPictureId)
        {
            SPictures.Remove(GetSPictureById(SPictureId));
            SaveChanges();
        }

        public void RemoveSPicture(SPicture spicture)
        {
            SPictures.Remove(spicture);
            SaveChanges();
        }

        #endregion



        #region SSongs

        public List<SSong> GetAllSSongs()
        {
            return SSongs.ToList();
        }

        public SSong GetSSongById(int SSongId)
        {
            return SSongs.First(p => p.Id == SSongId);
        }

        public SSong GetSSongByName(string SSongName)
        {
            return SSongs.First(p => p.Name == SSongName);
        }

        public void AddSSong(SSong ssong)
        {
            SSongs.Add(ssong);
            SaveChanges();
        }

        public void RemoveSSong(int SSongId)
        {
            SSongs.Remove(GetSSongById(SSongId));
            SaveChanges();
        }

        public void RemoveSSong(SSong ssong)
        {
            SSongs.Remove(ssong);
            SaveChanges();
        }

        #endregion



        #region SVideos

        public List<SVideo> GetAllSVideos()
        {
            return SVideos.ToList();
        }

        public SVideo GetSVideoById(int SVideoId)
        {
            return SVideos.First(p => p.Id == SVideoId);
        }

        public SVideo GetSVideoByName(string SVideoName)
        {
            return SVideos.First(p => p.Name == SVideoName);
        }

        public void AddSVideo(SVideo svideo)
        {
            SVideos.Add(svideo);
            SaveChanges();
        }

        public void RemoveSVideo(int SVideoId)
        {
            SVideos.Remove(GetSVideoById(SVideoId));
            SaveChanges();
        }

        public void RemoveSVideo(SVideo svideo)
        {
            SVideos.Remove(svideo);
            SaveChanges();
        }

        #endregion

    }
}

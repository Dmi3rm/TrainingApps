using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Web.Models
{
    public class FTopic
    {
        [Key]
        public int Id { get; set; }

        public int FSubsectionId { get; set; }
        public virtual FSubsection FSubsection { get; set; }

        public string Name { get; set; }
        public int Closed { get; set; }
        public int Fix { get; set; }
        public int NumFMsgs { get; set; }
        public int NumViews { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime LastFMsgDate { get; set; }
    }
}
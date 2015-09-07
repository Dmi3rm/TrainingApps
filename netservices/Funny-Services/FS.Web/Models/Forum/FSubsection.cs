using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS.Web.Models
{
    public class FSubsection
    {
        [Key]
        public int Id { get; set; }

        public int FSectionId { get; set; }
        public virtual FSection FSection { get; set; }

        public int OrderNum { get; set; }
        public string Name { get; set; }
        public int Closed { get; set; }
        public int NumFTopics { get; set; }
        public int NumFMsgs { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime LastFMsgDate { get; set; }

        public int FTopicId { get; set; }
    }
}
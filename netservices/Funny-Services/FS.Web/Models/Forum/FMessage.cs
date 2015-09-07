using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.Web.Models
{
    [Table("FMessages")]
    public class FMessage
    {
        [Key]
        public int Id { get; set; }

        public int FTopicId { get; set; }
        public virtual FTopic FTopic { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime WriteDate { get; set; }

        public string Text { get; set; }
    }
}
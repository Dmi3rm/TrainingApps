using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.Web.Models
{
    public class FSection
    {
        public int Id { get; set; }
        public int OrderNum { get; set; }
        public string Name { get; set; }
        public int Closed { get; set; }
        public int NumFTopics { get; set; }
        public int NumFMsgs { get; set; }
        public int NumFSubsections { get; set; }
    }
}
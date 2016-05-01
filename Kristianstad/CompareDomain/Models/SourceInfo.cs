using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Models
{
    public class SourceInfo
    {
        public string SourceName { get; set; }

        public string SourceId { get; set; }

        public string Title { get; set; } 

        public DateTime? InfoReadAt { get; set; }
    }
}

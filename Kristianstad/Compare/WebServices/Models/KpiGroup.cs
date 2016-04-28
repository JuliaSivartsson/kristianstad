using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.WebServices.Models
{
    public class KpiGroup
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<KpiQuestion> Members { get; set; }
    }
}

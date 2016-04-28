using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.WebServices.Models
{
    public class KpiAnswer
    {
        public string Kpi { get; set; }
        public string Ou { get; set; }
        public int Period { get; set; }
        public List<KpiAnswerValues> Values { get; set; }
    }
}

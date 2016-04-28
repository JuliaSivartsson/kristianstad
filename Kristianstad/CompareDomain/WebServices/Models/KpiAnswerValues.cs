using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.WebServices.Models
{
    public class KpiAnswerValues
    {
        public int Count { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public float? Value { get; set; }
    }
}

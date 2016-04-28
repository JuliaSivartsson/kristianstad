using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.WebServices.Models
{
    public class KpiAnswers
    {
        public int Count { get; set; }

        [JsonProperty("values")]
        public List<KpiAnswer> Values { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.WebServices.Models
{
    public class OUs
    {
        public int Count { get; set; }

        [JsonProperty("values")]
        public List<OU> Values { get; set; }

    }
}

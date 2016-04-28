using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Models
{
    public class PropertyQueryResultForPeriod
    {
        public int Period { get; set; }

        public List<PropertyQueryResultValue> Values { get; set; }
        
        public PropertyQueryResultValue ValueToUse { get; set; }


        //Constructors
        public PropertyQueryResultForPeriod()
        {
            // Empty
        }
        public PropertyQueryResultForPeriod(int period, List<PropertyQueryResultValue> values)
        {
            Period = period;
            Values = values;
            ValueToUse = Values.Where(v => v.Gender.ToLower() == "t").FirstOrDefault() ?? Values.FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.Models
{
    public class PropertyQueryResult
    {
        public string OrganisationalUnitId { get; set; }
        
        public List<PropertyQueryResultForPeriod> PeriodValues { get; set; }

        public PropertyQueryResultForPeriod PeriodToUse { get; set; }


        //Constructors
        public PropertyQueryResult()
        {
            // Empty
        }
        public PropertyQueryResult(string organisationalUnitId, List<PropertyQueryResultForPeriod> periodValues, int? periodToUse = null)
        {
            OrganisationalUnitId = organisationalUnitId;
            PeriodValues = periodValues;
            PeriodToUse = PeriodValues.Where(p => p.Period == periodToUse && p.Values.Any(v => v.Value != null)).FirstOrDefault() ?? 
                          PeriodValues.OrderByDescending(p => p.Period).Where(p => p.Values.Any(v => v.Value != null)).FirstOrDefault();
        }
    }
}

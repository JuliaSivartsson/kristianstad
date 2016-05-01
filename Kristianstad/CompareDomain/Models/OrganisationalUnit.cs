using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Models
{
    public class OrganisationalUnit : SourceInfo
    {
        /*
        public string WebServiceName { get; set; }

        public string OrganisationalUnitId { get; set; } // Like OU id if using Kolada

        public string Name { get; set; }
        
        public DateTime? InfoReadAt { get; set; }
        */

        //Constructors
        public OrganisationalUnit()
        {
            // Empty
        }
        /*
        public OrganisationalUnit(string webServiceName, string organisationalUnitId, string name, DateTime? infoReadAt = null)
        {
            WebServiceName = webServiceName;
            OrganisationalUnitId = organisationalUnitId;
            Name = name;
            InfoReadAt = infoReadAt;
        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Models
{
    public class OrganisationalUnit
    {
        public string WebServiceName { get; set; }

        public string OrganisationalUnitId { get; set; } // Like OU id if using Kolada

        public string Name { get; set; }

        //Constructors
        public OrganisationalUnit()
        {
            // Empty
        }
        public OrganisationalUnit(string webServiceName, string organisationalUnitId, string name)
        {
            WebServiceName = webServiceName;
            OrganisationalUnitId = organisationalUnitId;
            Name = name;
        }
    }
}

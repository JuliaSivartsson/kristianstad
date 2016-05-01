using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Models
{
    public class PropertyQueryGroup : SourceInfo
    {
        /*
        public string WebServiceName { get; set; }

        public string QueryGroupId { get; set; } // Kpi group id if using Kolada

        public string Title { get; set; } // name/title of the query
        */

        public List<PropertyQuery> Queries { get; set; }


        //Constructors
        public PropertyQueryGroup()
        {
            Queries = new List<PropertyQuery>();
        }
        /*
        public PropertyQueryGroup(string webServiceName, string queryGroupId, string title, List<PropertyQuery> queries)
        {
            WebServiceName = webServiceName;
            QueryGroupId = queryGroupId;
            Title = title;
            Queries = queries;
        }
        */
    }
}

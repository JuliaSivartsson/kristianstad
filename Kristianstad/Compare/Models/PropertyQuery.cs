using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.Models
{
    public class PropertyQuery
    {
        public static readonly string TYPE_STANDARD = "standard";
        public static readonly string TYPE_YESNO = "yesno";
        public static readonly string TYPE_PERCENT = "percent";
        public static readonly string TYPE_PERCENTAGE = "percentage";
        // if/when adding more TYPE's, also add to property AllTypes in TownComparisons.MVC.ViewModels.Admin.PropertyQueryInfoViewModel


        public string WebServiceName { get; set; }

        public string QueryId { get; set; } // Kpi id if using Kolada

        public string Title { get; set; } // name/title of the query

        public string Type { get; set; }
        

        //Constructors
        public PropertyQuery()
        {
            // Empty
        }
        public PropertyQuery(string webServiceName, string queryId, string title, string type)
        {
            WebServiceName = webServiceName;
            QueryId = queryId;
            Title = title;
            Type = type;
        }
    }
}

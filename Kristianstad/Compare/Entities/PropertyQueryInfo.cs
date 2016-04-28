using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.Entities
{
    public class PropertyQueryInfo
    {
        public int Id { get; set; } //just the database table id

        public string WebServiceName { get; set; }

        public string QueryId { get; set; } // this is the external ID from the web service (like Kolada)

        public string OriginalTitle { get; set; } // (original) name/title of the query

        public string Title { get; set; } // (custom) name/title of the query
        
        public string Type { get; set; }

        public int? Period { get; set; } //which period (year) that should be used, default should be latest


        public virtual Category Category { get; set; }  // which category this is in

    }
}

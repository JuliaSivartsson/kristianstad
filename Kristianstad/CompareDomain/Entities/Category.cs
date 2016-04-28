using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public virtual GroupCategory GroupCategory { get; set; }

        public virtual ICollection<PropertyQueryInfo> Queries { get; set; }

        public virtual ICollection<OrganisationalUnitInfo> OrganisationalUnits { get; set; }
        

        //Constructors
        public Category()
        {
            Queries = new HashSet<PropertyQueryInfo>();
            OrganisationalUnits = new HashSet<OrganisationalUnitInfo>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.Entities
{
    public class GroupCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }


        public bool TempJustToChangeDatabase { get; set; }


        //Constructors
        public GroupCategory()
        {
            Categories = new HashSet<Category>();
        }
    }
}

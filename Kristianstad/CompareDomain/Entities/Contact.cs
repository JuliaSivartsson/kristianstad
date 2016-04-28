using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public virtual OrganisationalUnitInfo OrganisationalUnitInfo { get; set; }
    }
}

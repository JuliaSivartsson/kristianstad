using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kstistianstad.Compare.Entities;
using Kstistianstad.Compare.Models;
using Kstistianstad.Compare.WebServices.Models;

namespace Kstistianstad.Compare.Abstract
{
    public interface ITownWebService : IDisposable
    {
        string GetName();

        List<OrganisationalUnit> GetAllOrganisationalUnits(string municipalityId);
        OrganisationalUnit GetOrganisationalUnit(string id);
        List<PropertyQueryWithResults> GetPropertyResults(List<PropertyQueryInfo> queries, List<OrganisationalUnitInfo> organisationalUnits);
        List<PropertyQueryGroup> GetAllPropertyQueries();
    }
}

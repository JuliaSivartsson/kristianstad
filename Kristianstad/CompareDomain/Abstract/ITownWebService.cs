using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kristianstad.CompareDomain.Entities;
using Kristianstad.CompareDomain.Models;
using Kristianstad.CompareDomain.WebServices.Models;

namespace Kristianstad.CompareDomain.Abstract
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

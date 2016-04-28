using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kristianstad.CompareDomain.Entities;
using Kristianstad.CompareDomain.Models;
using Kristianstad.CompareDomain.WebServices.Models;

namespace Kristianstad.CompareDomain.Abstract
{
    public abstract class TownWebServiceBase : ITownWebService
    {
        public abstract string GetName();
        public abstract List<OrganisationalUnit> GetAllOrganisationalUnits(string municipalityId);
        public abstract OrganisationalUnit GetOrganisationalUnit(string id);
        public abstract List<PropertyQueryWithResults> GetPropertyResults(List<PropertyQueryInfo> queries, List<OrganisationalUnitInfo> organisationalUnits);
        public abstract List<PropertyQueryGroup> GetAllPropertyQueries();


        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

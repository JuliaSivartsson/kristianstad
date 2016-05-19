using System.Collections.Generic;
using EPiServer.Core;
using Kristianstad.CompareDomain.Models;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CompareResultsModel" /> class.
    /// </summary>
    public class CompareResultsModel
    {
        public CompareResultsModel()
        {
            OrganisationalUnits = new List<OrganisationalUnitModel>();
            QueriesWithResults = new List<PropertyQueryWithResults>();
        }

        public List<OrganisationalUnitModel> OrganisationalUnits { get; set; }

        public List<PropertyQueryWithResults> QueriesWithResults { get; set; }
    }
}
using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using EPiCore.ViewModels.Pages;
using Kristianstad.Models.Pages.Compare;
using Kristianstad.CompareDomain.Models;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CompareResultPageModel" /> class. Defines elements to be used to present a category page.
    /// </summary>
    public class CompareResultPageModel : IPageViewModel<CompareResultPage>
    {
        public CompareResultPageModel(CompareResultPage currentPage)
        {
            CurrentPage = currentPage;
            // OrganisationalUnits = new List<OrganisationalUnitModel>();
            PropertyQueryGroupsFromSources = new List<PropertyQueryGroupModel>();
            // QueriesWithResults = new List<PropertyQueryWithResults>();
        }

        public CompareResultPage CurrentPage { get; set; }

        public List<PropertyQueryGroupModel> PropertyQueryGroupsFromSources { get; set; }

        // public List<OrganisationalUnitModel> OrganisationalUnits { get; set; }

        public DistanceFromAddressModel DistanceList { get; set; }

        // public List<PropertyQueryWithResults> QueriesWithResults { get; set; }
    }
}
using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using EPiCore.ViewModels.Pages;
using Kristianstad.Models.Pages.Compare;

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
            ResultQueryGroupsFromSources = new List<ResultQueryGroupModel>();
        }

        public CompareResultPage CurrentPage { get; set; }
        
        public List<ResultQueryGroupModel> ResultQueryGroupsFromSources { get; set; }

        public List<OrganisationalUnitModel> OrganisationalUnits { get; set; }

        public DistanceFromAddressModel DistanceList { get; set; }
    }
}
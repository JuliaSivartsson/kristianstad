using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using Kristianstad.Models.Pages.Compare;
using EPiCore.ViewModels.Pages;
using Kristianstad.Business.Models.Blocks.Shared;
using System.ComponentModel.DataAnnotations;
using EPiServer.Web;
using Kristianstad.HtmlHelpers;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitPageModel : IPageViewModel<OrganisationalUnitPage>
    {
        public OrganisationalUnitPageModel(OrganisationalUnitPage currentPage)
        {
            CurrentPage = currentPage;
            URL = CompareHelper.GetExternalUrl(currentPage.ContentLink);
        }

        public OrganisationalUnitPage CurrentPage { get; set; }
        public string URL { get; set; }

        public bool HasComparePage { get; set; }
        public bool InCompareAlready { get; set; }

        // [UIHint(UIHint.Block)]
        // public OrganisationalUnitBlock OrganisationalUnitBlock { get; set; }

        // public IEnumerable<CategoryItemModel> Categories { get; set; }
     
    }
}
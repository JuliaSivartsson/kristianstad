using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using Kristianstad.Models.Pages.Compare;
using EPiCore.ViewModels.Pages;
using Kristianstad.Business.Models.Blocks.Shared;
using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitPageModel : IPageViewModel<OrganisationalUnitPage>
    {
        public OrganisationalUnitPageModel(OrganisationalUnitPage currentPage)
        {
            CurrentPage = currentPage;
        }

        public OrganisationalUnitPage CurrentPage { get; set; }

        // [UIHint(UIHint.Block)]
        // public OrganisationalUnitBlock OrganisationalUnitBlock { get; set; }

        // public IEnumerable<CategoryItemModel> Categories { get; set; }
     
    }
}
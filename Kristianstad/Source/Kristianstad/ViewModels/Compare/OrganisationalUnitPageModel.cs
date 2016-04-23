using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using Kristianstad.Models.Pages;
using EPiCore.ViewModels.Pages;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitPageModel : IPageViewModel<OrganisationalUnitPage>
    {
        public OrganisationalUnitPageModel(OrganisationalUnitPage currentPage)
        {
            CurrentPage = currentPage;
        }

        public OrganisationalUnitPage CurrentPage { get; set; }

        public IEnumerable<CategoryItemModel> Categories { get; set; }
     
        public string PreviewText { get; set; }
        public DateTime StartPublish { get; set; }
        public XhtmlString MainBody { get; set; }

        //public bool ShowPublishDate { get; set; }
        //public bool ShowIntroduction { get; set; }

        public CategoryList Category { get; set; }

    }
}
using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using EPiCore.ViewModels.Pages;
using Kristianstad.Models.Pages.Compare;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CategoryPageModel" /> class. Defines elements to be used to present a category page.
    /// </summary>
    public class CategoryPageModel : IPageViewModel<CategoryPage>
    {
        public CategoryPageModel(CategoryPage currentPage)
        {
            CurrentPage = currentPage;
            AddOrganisationalUnits = new AddOrganisationalUnitsFormModel();
            DistanceList = new DistanceFromAddressModel();
        }

        public CategoryPage CurrentPage { get; set; }

        public AddOrganisationalUnitsFormModel AddOrganisationalUnits { get; set; }

        // public IEnumerable<OrganisationalUnitPage> ListOfExistingOU { get; set; }

        public DistanceFromAddressModel DistanceList { get; set; }
    }
}
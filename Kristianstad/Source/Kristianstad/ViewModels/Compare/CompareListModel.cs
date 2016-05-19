using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CompareListModel" /> class. Defines elements to be used to present a category list.
    /// </summary>
    public class CompareListModel
    {
        public CompareListModel()
        {
            OrganisationalUnits = new List<OrganisationalUnitCompareModel>();
        }

        /*
        public List<CompareModel> OrganisationalUnits { get; set; }
        public string ClearLink { get; set; }
        public string CompareLink { get; set; }
        public string CurrentLink { get; set; }
        public int CategoryId { get; set; }
        */
        public string Header { get; set; }

        public string ComparePageUrl { get; set; }

        public List<OrganisationalUnitCompareModel> OrganisationalUnits { get; set; }
    }
}
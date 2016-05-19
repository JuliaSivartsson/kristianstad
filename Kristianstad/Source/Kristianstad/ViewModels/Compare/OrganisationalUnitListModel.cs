using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitListModel
    {
        public OrganisationalUnitListModel()
        {
            OrganisationalUnits = new List<OrganisationalUnitPageModel>();
        }

        public PageData CurrentPage { get; set; }

        public List<OrganisationalUnitPageModel> OrganisationalUnits { get; set; }
    }
}
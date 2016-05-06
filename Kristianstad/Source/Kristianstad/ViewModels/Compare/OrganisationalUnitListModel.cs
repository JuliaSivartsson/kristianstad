using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitListModel
    {
        public OrganisationalUnitListModel()
        {
            // Empty
        }

        public PageData CurrentPage { get; set; }

        public List<PageData> OrganisationalUnits { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class CompareAdminPluginModel
    {
        public List<OrganisationalUnitModel> OrganisationalUnitsFromSources { get; set; }


        public CompareAdminPluginModel()
        {
            OrganisationalUnitsFromSources = new List<OrganisationalUnitModel>();
        }
    }
}
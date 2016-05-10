using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class AddOrganisationalUnitsFormModel
    {
        public List<OrganisationalUnitsSourceModel> OrganisationalUnitsSources { get; set; }

        public OrganisationalUnitModel Custom { get; set; }

        public AddOrganisationalUnitsFormModel()
        {
            OrganisationalUnitsSources = new List<OrganisationalUnitsSourceModel>();
        }
    }
}
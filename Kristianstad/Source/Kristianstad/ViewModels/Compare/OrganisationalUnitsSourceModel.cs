using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitsSourceModel
    {
        public string SourceName { get; set; }

        public List<OrganisationalUnitModel> OrganisationalUnits { get; set; }

        public OrganisationalUnitsSourceModel()
        {
            OrganisationalUnits = new List<OrganisationalUnitModel>();
        }

    }
}
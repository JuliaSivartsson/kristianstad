using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.Collections.Generic;

namespace Kristianstad.Business.Models.Blocks.Compare
{
    [ContentType(DisplayName = "AddOrganisationalUnitsBlock", GUID = "bde1b1c7-91b2-4f4a-8083-92fc20c1524b", Description = "")]
    public class AddOrganisationalUnitsBlock : BlockData
    {
        /*
        [Display(
            Name = "OrganisationalUnits",
            Description = "",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual List<NewOrganisationalUnit> OrganisationalUnits { get; set; }
         */
    }

    /*
    public class NewOrganisationalUnit
    {
        public bool Add { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
    */
}
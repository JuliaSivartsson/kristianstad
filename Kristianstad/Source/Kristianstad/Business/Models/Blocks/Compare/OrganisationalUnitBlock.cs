using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Kristianstad.Business.Compare;

namespace Kristianstad.Business.Models.Blocks.Compare
{
    [ContentType(
        DisplayName = "OrganisationalUnitBlock", 
        GUID = "bf55f223-9e75-4bdb-9775-698ec9d6ad59", 
        Description = "")]
    public class OrganisationalUnitBlock : BlockData
    {
        public OrganisationalUnitBlock()
        {
            OrganisationalUnit = new OrganisationalUnit();
        }

        [Ignore]
        public OrganisationalUnit OrganisationalUnit { get; set; }

        [Display(
            Name = "Name",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        [Required]
        public virtual string Name { get; set; }

        [Display(
            Name = "Description",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual string Description { get; set; }
    }
}
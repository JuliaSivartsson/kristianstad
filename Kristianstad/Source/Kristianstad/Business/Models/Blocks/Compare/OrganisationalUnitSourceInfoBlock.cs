using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Kristianstad.Business.Compare;

namespace Kristianstad.Business.Models.Blocks.Compare
{
    [ContentType(
        DisplayName = "Organisational Unit Block",
        GUID = "bf55f223-9e75-4bdb-9775-698ec9d6ad59",
        Description = "")]
    public class OrganisationalUnitSourceInfoBlock : BlockData
    {
        [Display(
            Name = "WebServiceName",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual string WebServiceName { get; set; }

        [Display(
            Name = "OrganisationalUnitId",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual string OrganisationalUnitId { get; set; }

        [Display(
            Name = "Name",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        public virtual string Name { get; set; }

        [Display(
            Name = "InfoReadAt",
            GroupName = SystemTabNames.Content,
            Order = 400)]
        public virtual DateTime? InfoReadAt { get; set; }

    }
}
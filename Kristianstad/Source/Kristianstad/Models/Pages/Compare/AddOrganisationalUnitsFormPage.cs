using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiCore.Content.Models.Pages;

namespace Kristianstad.Models.Pages.Compare
{
    [ContentType(
        DisplayName = "AddOrganisationalUnitsFormPage",
        GroupName = "Form Page",
        Description = "AddOrganisationalUnitsFormPage",
        GUID = "1c3fb17c-d4f5-4190-8bc5-821713ac8107")]
    public class AddOrganisationalUnitsFormPage : StandardPage
    {
        [Display(
            Name = "Contenet Area",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual ContentArea MainContentArea { get; set; }
    }
}
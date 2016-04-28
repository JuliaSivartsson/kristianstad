using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Compare;
using EPiServer.Filters;
using Kristianstad.Models.Pages.Compare;

namespace Kristianstad.Models.Pages
{
    [ContentType(
        GroupName = "Compare",
        GUID = "8f421f96-a21b-4946-bbc1-046c914a9ad7", 
        DisplayName = "Compare start page", 
        Description = "The start page for the compare service, listing all the categories")]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(CategoryPage), typeof(OrganisationalUnitFolderPage), typeof(AddOrganisationalUnitsFormPage) })]  // Pages we can create under the start page...
  
    public class CompareStartPage : ContentPage
    {
        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Heading { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Author { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual CategoryListBlock CategoryList { get; set; }
        
        [Display(GroupName = SystemTabNames.Content)]
        public virtual ContentArea RightContentArea { get; set; }

        /// <inheritdoc/>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            this[MetaDataProperties.PageChildOrderRule] = FilterSortOrder.Index;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Compare;
using EPiServer.Shell.ObjectEditing;
using Kristianstad.Business.Compare.EditorDescriptors;

namespace Kristianstad.Models.Pages
{
    [ContentType(
        GroupName = "Compare",
        GUID = "5f852296-61b7-4364-bb16-3a1a9567b1db", 
        DisplayName = "Category page", 
        Description = "A category page for comparison service")]
    [AvailableContentTypes(
       Availability.Specific,
       Include = new[] { typeof(OrganisationalUnitPage) })]

    public class CategoryPage : ContentPage
    {
        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Heading { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual OrganisationalUnitListBlock OrganisationalUnitList { get; set; }

        // public virtual AddOrganisationalUnitsBlock AddOrganisationalUnits { get; set; }

        // public virtual AddOrganisationalUnitsFormPage AddOrganisationalUnitsForm { get; set; }
        
        /*
        [Display(GroupName = SystemTabNames.Content)]
        public virtual PropertyCheckBoxList NewOrganisationalUnits { get; set; }
        */

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Author { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual ContentArea RightContentArea { get; set; }
        
        [Display(GroupName = SystemTabNames.Content)]
        [SelectMany(SelectionFactoryType = typeof(OrganisationalUnitSelectionFactory))]
        public virtual string NewOrganisationalUnits { get; set; }


        #region IInitializableContent

        /// <summary>
        /// Sets the default property values on the content data.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            //OrganisationalUnitList.PageTypeFilter = typeof(OrganisationalUnitPage).GetPageType();
            //OrganisationalUnitList.Recursive = true;
        }

        #endregion
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Compare;
using EPiServer.Shell.ObjectEditing;
using Kristianstad.Business.Models.Blocks;

namespace Kristianstad.Models.Pages.Compare
{
    [ContentType(
        GroupName = "Compare",
        GUID = "5f852296-61b7-4364-bb16-3a1a9567b1db",
        DisplayName = "Category page",
        Description = "A category page for comparison service")]
    [AvailableContentTypes(
       Availability.Specific,
       Include = new[] { typeof(OrganisationalUnitPage), typeof(CompareResultPage) })]

    public class CategoryPage : ContentPage
    {
        [Display(GroupName = SystemTabNames.Content, Order = 1)]
        public virtual string Description { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 2)]
        public virtual OrganisationalUnitListBlock OrganisationalUnitList { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 3)]
        public virtual CompareListBlock CompareListBlock { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 5)]
        public virtual ContentArea RightContentArea { get; set; }
        
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
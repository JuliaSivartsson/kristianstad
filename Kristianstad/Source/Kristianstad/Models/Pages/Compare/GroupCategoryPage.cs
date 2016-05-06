using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Compare;
using EPiServer.Shell.ObjectEditing;

namespace Kristianstad.Models.Pages.Compare
{
    [ContentType(
        GroupName = "Compare",
        GUID = "c9a23f63-4f11-41f3-b4ed-69bc4c1cb112",
        DisplayName = "Group category page",
        Description = "A group category page containing the actual (sub)categories")]
    [AvailableContentTypes(
       Availability.Specific,
       Include = new[] { typeof(CategoryPage) })]

    public class GroupCategoryPage : ContentPage
    {
        [Display(GroupName = SystemTabNames.Content, Order = 1)]
        public virtual string Description { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual CategoryListBlock CategoryList { get; set; }

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
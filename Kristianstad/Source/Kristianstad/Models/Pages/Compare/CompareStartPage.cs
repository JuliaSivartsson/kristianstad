using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Compare;

namespace Kristianstad.Models.Pages
{
    [ContentType(
        GroupName = "Compare",
        GUID = "8f421f96-a21b-4946-bbc1-046c914a9ad7", 
        DisplayName = "Compare start page", 
        Description = "The start page for the compare service, listing all the categories")]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(CategoryPage), typeof(OrganisationalUnitFolderPage) })]  // Pages we can create under the start page...
  
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


        #region IInitializableContent

        /// <summary>
        /// Sets the default property values on the content data.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            //CategoryList.PageTypeFilter = typeof(CategoryPage).GetPageType();
            //CategoryList.Recursive = true;
        }

        #endregion
    }
}
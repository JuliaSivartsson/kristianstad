using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Kristianstad.Models.Pages
{
    [ContentType(
        GroupName = "Compare",
        GUID = "2a9d3fd5-627a-4503-b026-04ab66f76439",
        DisplayName = "Organisational Unit Folder Page",
        Description = "(Do not create this page manually, it's created automatically) A folder page containing all the organisational unit pages for the comparison service")]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(OrganisationalUnitPage) })]

    public class OrganisationalUnitFolderPage : ContentPage
    {

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
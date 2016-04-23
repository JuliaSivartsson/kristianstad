using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel;
using EPiServer.Filters;
using EPiCore.Content.Models.Misc;

namespace Kristianstad.Business.Models.Blocks.Compare
{
    [ContentType(
        GUID = "fa50f9a5-6b51-4422-95cd-384a4c435025",
        GroupName = GroupNames.Content,
        DisplayName = "Organisational Unit List Block")]
    public class OrganisationalUnitListBlock : BlockData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 4,
            Name = "Sort Order")]
        [DefaultValue(FilterSortOrder.PublishedDescending)]
        [UIHint("SortOrder")]
        [BackingType(typeof(PropertyNumber))]
        public virtual FilterSortOrder SortOrder { get; set; }
        

        #region IInitializableContent

        /// <summary>
        /// Sets the default property values on the content data.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            /*
            Count = 3;
            IncludeIntroduction = true;
            IncludePublishDate = true;
            */
            SortOrder = FilterSortOrder.PublishedDescending;
            //Recursive = true;
        }

        #endregion
    }
}
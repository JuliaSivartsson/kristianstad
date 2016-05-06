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
        GUID = "f2349a48-d857-4148-94f1-c5f8508fb278",
        GroupName = "Compare",
        DisplayName = "Group Category List Block")]
    public class GroupCategoryListBlock : BlockData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 1,
            Name = "Show sub-categories (not only group categories)")]
        [DefaultValue(true)]
        public virtual bool ShowSubCategories { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 2,
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
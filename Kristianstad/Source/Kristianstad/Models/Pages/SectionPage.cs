// <copyright file="SectionPage.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Pages
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using EPiCore.Content.Models.Attributes;
    using EPiCore.Content.Models.Misc;
    using EPiCore.Models.Pages;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Filters;
    using EPiServer.Shell.ObjectEditing;
    using EPiServer.SpecializedProperties;
    using UI.Factories;

    /// <summary>
    /// The <see cref="SectionPage" /> class. Represents a section/tab in the Mega Menu.
    /// </summary>
    [ContentType(
        GUID = "A43AB9DB-D2A0-4749-B85C-B9AB1B7D5AE6",
        GroupName = GroupNames.Content,
        Order = 200)]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(ContentPage) })]
    [IncludeOnAToZ]
    public class SectionPage : BasePage
    {
        /// <summary>
        /// Gets or sets the icon to be used for the tab/section that this page represents.
        /// </summary>
        [Display(
            GroupName = TabNames.Content,
            Order = 400)]
        [Required]
        [SelectOne(SelectionFactoryType = typeof(MegaMenuIconSelectionFactory))]
        public virtual string Icon { get; set; }

        /// <summary>
        /// Gets or sets the...
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(
            GroupName = TabNames.Content,
            Order = 410)]
        [CultureSpecific]
        public virtual LinkItemCollection MenuLinkList { get; set; }

        /// <summary>
        /// Gets or sets the...
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(
            GroupName = TabNames.Content,
            Order = 420)]
        [DefaultValue(500)]
        [Range(0, 500)]
        public virtual int MaxNumberOfMenuEntries { get; set; }

        /// <inheritdoc/>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            this[MetaDataProperties.PageChildOrderRule] = FilterSortOrder.Index;
        }
    }
}
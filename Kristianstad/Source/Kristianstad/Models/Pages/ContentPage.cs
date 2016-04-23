// <copyright file="ContentPage.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Pages
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attributes;
    using EPiCore.Content.Models.Attributes;
    using EPiCore.Content.Models.Pages;
    using EPiCore.Models.Attributes;
    using EPiCore.Models.Blocks;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.SpecializedProperties;
    using EPiServer.Web;
    using Misc;
    using EPiCore.Content.Models.Pages;
    using EPiCore.Models.Blocks;

    /// <summary>
    /// The <see cref="ContentPage" /> class. This is the main page type for content pages.
    /// </summary>
    [ContentType(
        GUID = "FE7F629B-5053-4B24-84FA-D77178CC0DA2",
        GroupName = EPiCore.Content.Models.Misc.GroupNames.Content,
        Order = 100)]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(ContentPage) })]
    [IncludeOnRoot]
    [IncludeOnAToZ(false)]
    [IncludeInSearch("ContentPage")]
    public class ContentPage : StandardPage, ITeaser, ISEO, IEasyToRead
    {
        /// <summary>
        /// Gets or sets the menu title, used as the title of this page's representation in mega menu.
        /// </summary>
        [Required]
        [Display(
            GroupName = TabNames.MenuSettings,
            Order = 100)]
        [CultureSpecific]
        public virtual string MenuTitle { get; set; }

        /// <summary>
        /// Gets or sets the menu description, used in the mega menu to describe a page.
        /// </summary>
        [Required]
        [Display(
            GroupName = TabNames.MenuSettings,
            Order = 200)]
        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [StringLength(100)]
        public virtual string MenuDescription { get; set; }

        /// <summary>
        /// Gets or sets the preamble.
        /// </summary>
        /// <value>
        /// The preamble.
        /// </value>
        // ReSharper disable once Mvc.TemplateNotResolved
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
            Order = 210)]
        [CultureSpecific]
        public virtual string TitleImageCaption { get; set; }

        /// <summary>
        /// Gets or sets the related E services collection.
        /// </summary>
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
            Order = 810)]
        [CultureSpecific]
        public virtual LinkItemCollection RelatedEServices { get; set; }

        /// <summary>
        /// Gets or sets the related documents.
        /// </summary>
        [Display(
        GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
        Order = 820)]
        [CultureSpecific]
        [AllowedTypes(AllowedTypes = new[] { typeof(MediaData) })]
        public virtual IEnumerable<ContentReference> RelatedDocuments { get; set; }

        /// <summary>
        /// Gets or sets the related links collection.
        /// </summary>
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
            Order = 830)]
        [CultureSpecific]
        //[MaxRelatedLinksSetting] // TODO: Find out why the default setting (0 sadly) is being cached indefinately, fix it, and then use this attribute
        [MaxLinkItems(6)]
        public virtual LinkItemCollection RelatedLinks { get; set; }

        /// <summary>
        /// Gets or sets the top block area.
        /// </summary>
        /// <value>
        /// The top block area.
        /// </value>
        // ReSharper disable once Mvc.TemplateNotResolved
        [UIHint(UIHint.Block)]
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
            Order = 400)]
        [AllowedTypes(AllowedTypes = new[] { typeof(BaseBlock), typeof(ITeaser) })]
        public virtual ContentArea TopBlockArea { get; set; }

        /// <summary>
        /// Gets or sets the block area.
        /// </summary>
        /// <value>
        /// The block area.
        /// </value>
        // ReSharper disable once Mvc.TemplateNotResolved
        [UIHint(UIHint.Block)]
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
            Order = 600)]
        [AllowedTypes(AllowedTypes = new[] { typeof(BaseBlock), typeof(ITeaser) })]
        public virtual ContentArea BlockArea { get; set; }

        /// <summary>
        /// Gets or sets the easy to read page.
        /// </summary>
        /// <value>
        /// The easy to read page.
        /// </value>
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.Content,
            Order = 700)]
        [AllowedTypes(AllowedTypes = new[] { typeof(EasyToReadPage) })]
        public virtual PageReference EasyToReadPage { get; set; }

        /// <summary>
        /// Gets or sets the SEO description.
        /// </summary>
        /// <value>
        /// The SEO.
        /// </value>
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.SEO,
            Order = 100)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the right block area.
        /// </summary>
        /// <value>
        /// The right block area.
        /// </value>
        [AllowedTypes(AllowedTypes = new[] { typeof(BaseBlock), typeof(ITeaser) })]
        [Display(GroupName = "Information", Order = 300)]
        [UIHint("block")]
        public virtual ContentArea RightBlockArea { get; set; }
    }
}
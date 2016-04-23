// <copyright file="FooterLinkListBlock.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Blocks
{
    using System.ComponentModel.DataAnnotations;
    using EPiCore.Content.Models.Blocks;
    using EPiCore.Content.Models.Misc;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Shell.ObjectEditing;
    using EPiServer.SpecializedProperties;
    using UI.Factories;

    /// <summary>
    /// The <see cref="FooterLinkListBlock" /> class. Block used to display a list of links, internal or external,
    /// coupled with a list title and icon.
    /// </summary>
    [ContentType(
        GUID = "6E8FAF8B-F28F-493B-8F2A-16C1ABF09BC2",
        GroupName = GroupNames.Content,
        Order = 500)]
    public class FooterLinkListBlock : PreviewBlock
    {
        /// <summary>
        /// Gets or sets the icon to be used for the list of links.
        /// </summary>
        [Display(
            GroupName = TabNames.Content,
            Order = 100)]
        [Required]
        [SelectOne(SelectionFactoryType = typeof(FooterIconSelectionFactory))]
        public virtual string Icon { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Display(
            GroupName = TabNames.Content,
            Order = 200)]
        [Required]
        [CultureSpecific]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        [Display(
            GroupName = TabNames.Content,
            Order = 300)]
        [Required]
        public virtual LinkItemCollection Links { get; set; }

        /// <summary>
        /// Gets the supported display option tags.
        /// </summary>
        /// <value>
        /// The supported display option tags.
        /// </value>
        public override DisplayOptionTags[] SupportedDisplayOptionTags => new[] { DisplayOptionTags.Full, DisplayOptionTags.Half, DisplayOptionTags.OneThird };

        /// <summary>
        /// Gets the default display option tag.
        /// </summary>
        /// <value>
        /// The default display option tag.
        /// </value>
        public override DisplayOptionTags DefaultDisplayOptionTag => DisplayOptionTags.Full;
    }
}
// <copyright file="SettingsPage.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Pages
{
    using System.ComponentModel.DataAnnotations;
    using Blocks;
    using EPiCore.Content.Models.Attributes;
    using EPiCore.Models.Attributes;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;
    using Misc;
    using Attributes;

    /// <summary>
    /// The <see cref="SettingsPage" /> class used to configure site wide settings for kristianstad.se.
    /// </summary>
    [ContentType(
        GUID = "D090901E-4B41-41E8-83FC-87F8DD05537A",
        GroupName = EPiCore.Content.Models.Misc.GroupNames.Configuration,
        Order = 100,
        DisplayName = "Inställningssida")]
    [AvailableContentTypes(Availability.None)]
    [IncludeOnRoot]
    [IncludeOnAToZ(false)]
    public class SettingsPage : EPiCore.Content.Models.Pages.SettingsPage
    {
        /*
         * TODO: Add once cache issue has been solved (see ContentPage.cs)
        /// <summary>
        /// Gets or sets the maximum number of links allowed in ContentPage.RelatedLinks.
        /// </summary>
        [Display(
            GroupName = EPiCore.Content.Models.Misc.TabNames.GlobalSettings,
            Order = 410,
            Name = "Max antal Relaterade länkar",
            Description = "Maximalt antal Relaterade länkar som en innehållssida får ha")]
        [Range(0, 42)]
        [DefaultValue(6)]
        public virtual int MaxRelatedLinks { get; set; }
        */

        /// <summary>
        /// Gets or sets the adjust link.
        /// </summary>
        [Required]
        [Display(
            GroupName = TabNames.Header,
            Order = 510,
            Name = "Länk till tillgänglighetssida")]
        public virtual Url Adjust { get; set; }

        /// <summary>
        /// Gets or sets the E services link.
        /// </summary>
        [Required]
        [Display(
            GroupName = TabNames.Header,
            Order = 520,
            Name = "Länk till e-tjänster")]
        public virtual Url EServices { get; set; }

        /// <summary>
        /// Gets or sets the contact link.
        /// </summary>
        [Required]
        [Display(
            GroupName = TabNames.Header,
            Order = 530,
            Name = "Länk till kontaktsida")]
        public virtual Url Contact { get; set; }

        /// <summary>
        /// Gets or sets the ContentArea containing the lists of links.
        /// </summary>
        [UIHint(UIHint.Block)]
        [Display(
            GroupName = TabNames.Footer,
            Order = 610,
            Name = "Blockyta för sidfot")]
        [CultureSpecific]
        [MaxContentCount(4)]
        [AllowedTypes(AllowedTypes = new[] { typeof(FooterLinkListBlock) })]
        public virtual ContentArea LinkListArea { get; set; }

        /// <inheritdoc/>
        [Ignore]
        public override ContentArea FooterBlockArea { get; set; }

        /// <summary>
        /// Gets or sets the Facebook link.
        /// </summary>
        [Display(
            GroupName = TabNames.Footer,
            Order = 620)]
        public virtual Url Facebook { get; set; }

        /// <summary>
        /// Gets or sets the Twitter link.
        /// </summary>
        [Display(
            GroupName = TabNames.Footer,
            Order = 630)]
        public virtual Url Twitter { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn link.
        /// </summary>
        [Display(
            GroupName = TabNames.Footer,
            Order = 640)]
        public virtual Url LinkedIn { get; set; }

        /// <summary>
        /// Gets or sets the Flickr link.
        /// </summary>
        [Display(
            GroupName = TabNames.Footer,
            Order = 650)]
        public virtual Url Flickr { get; set; }

        /// <summary>
        /// Gets or sets the YouTube link.
        /// </summary>
        [Display(
            GroupName = TabNames.Footer,
            Order = 660)]
        public virtual Url YouTube { get; set; }

        /// <summary>
        /// Gets or sets the contact details used at the bottom of the footer.
        /// </summary>
        [Display(
            GroupName = TabNames.Footer,
            Order = 670,
            Name = "Informationsstext",
            Description = "Visas längst ner i sidfoten")]
        [CultureSpecific]
        public virtual XhtmlString ContactDetails { get; set; }
    }
}
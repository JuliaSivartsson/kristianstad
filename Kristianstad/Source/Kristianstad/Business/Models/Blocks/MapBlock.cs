// <copyright file="MapBlock.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Models.Blocks
{
    using System.ComponentModel.DataAnnotations;
    using EPiCore.Content.Models.Misc;
    using EPiCore.Models.Blocks;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Shell.ObjectEditing;
    using Map;

    /// <summary>
    /// The <see cref="MapBlock"/> class.
    /// </summary>
    [ContentType(
        GUID = "65ab3b7f-dfc2-4584-9431-c80d63e3f997",
        GroupName = GroupNames.Content)]
    public class MapBlock : BaseBlock
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Display(
            Order = 100,
            GroupName = TabNames.Content)]
        [AutoSuggestSelection(typeof(MapAddressSelectionQuery), AllowCustomValues = false)]
        [Required(AllowEmptyStrings = false)]
        public virtual string Address { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// Stored as EPSG:3008
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [Display(
            Order = 1000,
            GroupName = TabNames.Content)]
        [Required(AllowEmptyStrings = false)]
        public virtual double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// Stored as EPSG:3008
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [Display(
            Order = 1100,
            GroupName = TabNames.Content)]
        [Required(AllowEmptyStrings = false)]
        public virtual double Longitude { get; set; }
    }
}
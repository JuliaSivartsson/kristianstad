// <copyright file="VideoBlock.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Models.Blocks
{
    using System.ComponentModel.DataAnnotations;
    using EPiCore.Content.Models.Misc;
    using EPiCore.Models.Blocks;
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using EPiServer.Shell.ObjectEditing;
    using UI.Factories;

    /// <summary>
    /// Block for videos, a video file (uploaded to EPiServer), YouTube or Vimeo video.
    /// </summary>
    [ContentType(
        GUID = "90B60F39-D71A-4D52-BCF2-ABBA8C981941",
        GroupName = GroupNames.Content)]
    public class VideoBlock : BaseBlock
    {
        /// <summary>
        /// Gets or sets the type of the video.
        /// </summary>
        /// <value>
        /// The type of the video.
        /// </value>
        [Display(Order = 100)]
        [SelectOne(SelectionFactoryType = typeof(VideoTypeSelectionFactory))]
        public virtual int VideoType { get; set; }

        /// <summary>
        /// Gets or sets the local video file.
        /// </summary>
        /// <value>
        /// The video.
        /// </value>
        [Display(Order = 200)]
        [AllowedTypes(AllowedTypes = new[] { typeof(VideoData) })]
        [CultureSpecific]
        public virtual ContentReference Video { get; set; }

        /// <summary>
        /// Gets or sets the video link identifier.
        /// </summary>
        /// <value>
        /// The video link identifier.
        /// </value>
        [Display(Order = 300)]
        [CultureSpecific]
        public virtual string VideoLinkId { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data; otherwise, <c>false</c>.
        /// </value>
        [Ignore]
        public bool HasData
        {
            get
            {
                return Video != null || !string.IsNullOrWhiteSpace(VideoLinkId);
            }
        }
    }
}
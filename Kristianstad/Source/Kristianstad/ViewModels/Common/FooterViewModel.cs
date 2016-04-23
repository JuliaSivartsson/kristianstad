// <copyright file="FooterViewModel.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.ViewModels.Common
{
    using EPiServer;
    using EPiServer.Core;

    /// <summary>
    /// The <see cref="FooterViewModel" /> class. Defines elements to be used to present the footer.
    /// </summary>
    public class FooterViewModel
    {
        /// <summary>
        /// Gets or set the footer block area.
        /// </summary>
        public ContentArea LinkListArea { get; set; }

        /// <summary>
        /// Gets or sets the Facebook URL.
        /// </summary>
        public Url Facebook { get; set; }

        /// <summary>
        /// Gets or sets the Twitter URL.
        /// </summary>
        public Url Twitter { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn URL.
        /// </summary>
        public Url LinkedIn { get; set; }

        /// <summary>
        /// Gets or sets the Flickr URL.
        /// </summary>
        public Url Flickr { get; set; }

        /// <summary>
        /// Gets or sets the YouTube URL.
        /// </summary>
        public Url YouTube { get; set; }

        /// <summary>
        /// Gets or sets the contact details used at the bottom of the footer.
        /// </summary>
        public XhtmlString ContactDetails { get; set; }
    }
}
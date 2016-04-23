// <copyright file="HeaderViewModel.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.ViewModels.Common
{
    using EPiServer;

    /// <summary>
    /// The <see cref="HeaderViewModel" /> class. Defines elements to be used to present the header.
    /// </summary>
    public class HeaderViewModel
    {
        /// <summary>
        /// Gets or sets the URL to the adjust page.
        /// </summary>
        public Url Adjust { get; set; }

        /// <summary>
        /// Gets or sets the URL to the E services page.
        /// </summary>
        public Url EServices { get; set; }

        /// <summary>
        /// Gets or sets the URL to the contact page.
        /// </summary>
        public Url Contact { get; set; }
    }
}
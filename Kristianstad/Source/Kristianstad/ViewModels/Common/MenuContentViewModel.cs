// <copyright file="MenuContentViewModel.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.ViewModels.Common
{
    /// <summary>
    /// The <see cref="MenuContentViewModel" /> class. Defines elements to be used to present content pages in the main menu.
    /// </summary>
    public class MenuContentViewModel
    {
        /// <summary>
        /// Gets or sets the menu title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the menu description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string URL { get; set; }

        /// <summary>
        /// Determents wether or not a page should be presented as a link only.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [render as link only]; otherwise, <c>false</c>.
        /// </value>
        public bool RenderAsLinkOnly { get; set; }
    }
}
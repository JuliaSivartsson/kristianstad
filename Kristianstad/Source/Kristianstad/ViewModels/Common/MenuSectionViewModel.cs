// <copyright file="menuSectionViewModel.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.ViewModels.Common
{
    using EPiServer.SpecializedProperties;

    /// <summary>
    /// The <see cref="MenuSectionViewModel" /> class. Defines elements to be used to present a section page in the main menu.
    /// </summary>
    public class MenuSectionViewModel
    {
        /// <summary>
        /// Gets or sets the Content.Link.ID.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the section title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the sections icon value (css class).
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the quick links.
        /// </summary>
        /// <value>
        /// The quick links.
        /// </value>
        public LinkItemCollection QuickLinks { get; set; }
    }
}
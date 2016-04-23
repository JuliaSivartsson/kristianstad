// <copyright file="MegaMenuViewModel.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.ViewModels.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="MegaMenuViewModel" /> class. Defines elements to be used to present the mega menu.
    /// </summary>
    public class MegaMenuViewModel
    {
        /// <summary>
        /// Gets or sets the menu pages.
        /// </summary>
        /// <value>
        /// The menu pages.
        /// </value>
        public List<KeyValuePair<MenuSectionViewModel, List<MenuContentViewModel>>> MenuPages { get; set; }
    }
}
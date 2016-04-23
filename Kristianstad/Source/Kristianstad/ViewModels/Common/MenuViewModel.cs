// <copyright file="MenuViewModel.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.ViewModels.Common
{
    using System.Collections.Generic;
    using Models.Misc;

    /// <summary>
    /// The <see cref="MenuViewModel" /> class.
    /// </summary>
    public class MenuViewModel
    {
        /// <summary>
        /// The root of this menu.
        /// </summary>
        public MenuItem Root { get; set; }

        /// <summary>
        /// The path from the root of the menu, to the item representing the page for which this menu is being rendered.
        /// </summary>
        /*
         * In reality, we only need the nodes, so this could be a HashSet. However, the number of items is so small
         * that it doesn't really matter, and this way we are able to debug more easily
         */
        public List<int> Path { get; set; }

        /// <summary>
        /// The ID of the page for which this menu is being rendered.
        /// </summary>
        public int CurrentPageID { get; set; }
    }
}
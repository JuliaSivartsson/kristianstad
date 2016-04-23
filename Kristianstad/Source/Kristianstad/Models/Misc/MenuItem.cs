// <copyright file="MenuItem.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Misc
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="MenuItem" /> class.
    /// </summary>
    public class MenuItem : EPiCore.Content.ViewModels.Common.LinkViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        /// <param name="name">The name of this item</param>
        /// <param name="url">The URL of this item</param>
        public MenuItem(string name, string url)
            : base(name, url)
        {
            Children = new List<MenuItem>();
        }

        /// <summary>
        /// The children of this item.
        /// </summary>
        public List<MenuItem> Children { get; set; }

        /// <summary>
        /// The ID of this item.
        /// </summary>
        public int ID { get; set; }
    }
}
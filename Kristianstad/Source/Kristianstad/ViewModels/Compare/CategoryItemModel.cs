using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CategoryItemModel" /> class. Defines elements to be used to present categories.
    /// </summary>
    public class CategoryItemModel
    {
        /// <summary>
        /// Gets or sets the Title of the category.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Url of the category.
        /// </summary>
        public string Url { get; set; }
    }
}
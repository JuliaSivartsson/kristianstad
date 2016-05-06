using System.Collections.Generic;
using EPiServer.Core;
using Kristianstad.Business.Models.Blocks.Compare;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CategoryListModel" /> class. Defines elements to be used to present a category list.
    /// </summary>
    public class CategoryListModel
    {
        /// <summary>
        /// Gets or sets the Categories of the list.
        /// </summary>
        public List<PageData> Categories { get; set; }

        // public bool ShowIntroduction { get; set; }
        // public bool ShowPublishDate { get; set; }
    }
}
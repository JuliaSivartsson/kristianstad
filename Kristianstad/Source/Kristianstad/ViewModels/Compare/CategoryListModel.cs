using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CategoryListModel" /> class. Defines elements to be used to present a category list.
    /// </summary>
    public class CategoryListModel
    {
        // public string Heading { get; set; }

        /// <summary>
        /// Gets or sets the Categories of the list.
        /// </summary>
        public IEnumerable<PageData> Categories { get; set; }

        // public bool ShowIntroduction { get; set; }
        // public bool ShowPublishDate { get; set; }
    }
}
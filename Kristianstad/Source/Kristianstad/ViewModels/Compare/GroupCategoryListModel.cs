using System.Collections.Generic;
using EPiServer.Core;
using Kristianstad.Models.Pages.Compare;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="GroupCategoryListModel" /> class. Defines elements to be used to present a category list.
    /// </summary>
    public class GroupCategoryListModel
    {
        public bool ShowSubCategories { get; set; }

        /// <summary>
        /// Gets or sets the Group categories of the list.
        /// </summary>
        public List<GroupCategoryPage> GroupCategories { get; set; }
    }
}
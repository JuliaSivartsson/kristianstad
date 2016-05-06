using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using EPiCore.ViewModels.Pages;
using Kristianstad.Models.Pages.Compare;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CategoryPageModel" /> class. Defines elements to be used to present a category page.
    /// </summary>
    public class GroupCategoryPageModel : IPageViewModel<GroupCategoryPage>
    {
        public GroupCategoryPageModel(GroupCategoryPage currentPage)
        {
            CurrentPage = currentPage;
        }

        public GroupCategoryPage CurrentPage { get; set; }

        public bool ShowSubCategories { get; set; }
    }
}
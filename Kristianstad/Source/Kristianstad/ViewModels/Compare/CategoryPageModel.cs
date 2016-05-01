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
    public class CategoryPageModel : IPageViewModel<CategoryPage>
    {
        public CategoryPageModel(CategoryPage currentPage)
        {
            CurrentPage = currentPage;
        }

        public CategoryPage CurrentPage { get; set; }
        
        public List<ResultQueryGroupModel> ResultQueryGroups { get; set; }
    }
}
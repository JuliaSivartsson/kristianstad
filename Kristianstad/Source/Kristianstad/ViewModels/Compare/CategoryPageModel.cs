using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using EPiCore.ViewModels.Pages;
using Kristianstad.Models.Pages;

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

        // public IEnumerable<TagItem> Tags { get; set; }
     
        public string PreviewText { get; set; }
        public DateTime StartPublish { get; set; }
        public XhtmlString MainBody { get; set; }

        /*
        public bool ShowPublishDate { get; set; }

        public bool ShowIntroduction { get; set; }
        
        public CategoryList Category { get; set; }
        */

        /*
        public class TagItem
        {
            public string Title { get; set; }
            public string Url { get; set; }
        }
        */
    }
}
using System.Collections.Generic;
using System.Web;
using EPiServer.Core;
using System;
using EPiCore.ViewModels.Pages;
using Kristianstad.Models.Pages.Compare;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CompareResultPageModel" /> class. Defines elements to be used to present a category page.
    /// </summary>
    public class CompareResultPageModel : IPageViewModel<CompareResultPage>
    {
        public CompareResultPageModel(CompareResultPage currentPage)
        {
            CurrentPage = currentPage;
        }

        public CompareResultPage CurrentPage { get; set; }
        
        public List<ResultQueryGroupModel> ResultQueryGroups { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation;
using EPiServer.DataAbstraction;
using System.Text;
using System;
using System.Text.RegularExpressions;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages;
using Kristianstad.Business.Compare;

namespace Kristianstad.Controllers.Compare
{
    public class CategoryListBlockController : BlockController<CategoryListBlock>
    {
        public int PreviewTextLength { get; set; }

        private readonly Injected<IContentLoader> contentLoader; // private IContentLoader contentLoader;
        
        public override ActionResult Index(CategoryListBlock currentBlock)
        {
            var categories = FindPages(currentBlock);
            categories = Sort(categories, currentBlock.SortOrder);
            
            var model = new CategoryListModel() // (currentBlock)
            {
                Categories = categories
            };

            return PartialView(model);
        }
        
        public ActionResult Preview(PageData currentPage, CategoryListModel categoryListModel)
        {
            var pd = (CategoryPage)currentPage;
            PreviewTextLength = 200;

            var model = new CategoryPageModel(pd)
            {
                PreviewText = GetPreviewText(pd),
            };

            return PartialView("Preview", model);
        }

        protected string GetPreviewText(CategoryPage page)
        {
            if (PreviewTextLength <= 0)
            {
                return string.Empty;
            }

            string previewText = String.Empty;

            /*
            if (page.MainBody != null)
            {
                previewText = page.MainBody.ToHtmlString();
            }
            */

            if (String.IsNullOrEmpty(previewText))
            {
                return string.Empty;
            }

            //If the MainBody contains DynamicContents, replace those with an empty string
            StringBuilder regexPattern = new StringBuilder(@"<span[\s\W\w]*?classid=""");
            regexPattern.Append(DynamicContentFactory.Instance.DynamicContentId.ToString());
            regexPattern.Append(@"""[\s\W\w]*?</span>");
            previewText = Regex.Replace(previewText, regexPattern.ToString(), string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return TextIndexer.StripHtml(previewText, PreviewTextLength);
        }

        private IEnumerable<PageData> FindPages(CategoryListBlock currentBlock) //, Category categoryParameter)
        {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? contentLoader.Service.Get<PageData>(ContentReference.StartPage);
            PageReference listRoot = currentPage.PageLink;

            if (currentPage.PageTypeName == typeof(CompareStartPage).GetPageType().Name)
            {
                pages = contentLoader.Service.GetChildren<CategoryPage>(currentPage.ContentLink);
            }

            return pages ?? new List<PageData>();
        }

        private IEnumerable<PageData> Sort(IEnumerable<PageData> pages, FilterSortOrder sortOrder)
        {
            var asCollection = new PageDataCollection(pages);
            var sortFilter = new FilterSort(sortOrder);
            sortFilter.Sort(asCollection);
            return asCollection;
        }

      
    }
}

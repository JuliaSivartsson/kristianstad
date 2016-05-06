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
using Kristianstad.Models.Pages.Compare;
using Kristianstad.Business.Compare;

namespace Kristianstad.Controllers.Compare
{
    public class CategoryListBlockController : BlockController<CategoryListBlock>
    {
        private readonly Injected<IContentLoader> contentLoader;

        public override ActionResult Index(CategoryListBlock currentBlock)
        {
            var categories = FindPages(currentBlock);
            categories = Sort(categories, currentBlock.SortOrder);

            var model = new CategoryListModel
            {
                Categories = categories.ToList()
            };

            return PartialView(model);
        }

        public ActionResult Preview(PageData currentPage, CategoryListModel categoryListModel)
        {
            var pd = (CategoryPage)currentPage;
            var model = new CategoryPageModel(pd);

            return PartialView("Preview", model);
        }

        private IEnumerable<PageData> FindPages(CategoryListBlock currentBlock)
        {
            IEnumerable<PageData> pages = null;
            PageData groupCategoryPage = null;

            // Try to get the Group category page by parent controller context
            var parentPage = ControllerContext.ParentActionViewContext.RouteData.Values["currentPage"];
            if (parentPage is GroupCategoryPage)
            {
                groupCategoryPage = parentPage as GroupCategoryPage;
            }
            else
            {
                var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
                groupCategoryPage = pageRouteHelper.Page;
            }

            if (groupCategoryPage != null && groupCategoryPage is GroupCategoryPage)
            {
                // Load pages inside group category page
                pages = contentLoader.Service.GetChildren<CategoryPage>(groupCategoryPage.ContentLink);
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

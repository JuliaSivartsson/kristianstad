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
    public class GroupCategoryListBlockController : BlockController<GroupCategoryListBlock>
    {
        private readonly Injected<IContentLoader> contentLoader;

        public override ActionResult Index(GroupCategoryListBlock currentBlock)
        {
            var categories = FindPages(currentBlock);
            categories = Sort(categories, currentBlock.SortOrder);

            var model = new GroupCategoryListModel
            {
                ShowSubCategories = currentBlock.ShowSubCategories,
                GroupCategories = categories.ToList()
            };

            return PartialView(model);
        }

        public ActionResult Preview(PageData currentPage, GroupCategoryListModel groupCategoryListModel)
        {
            var pd = (GroupCategoryPage)currentPage;
            var model = new GroupCategoryPageModel(pd)
            {
                ShowSubCategories = groupCategoryListModel.ShowSubCategories
            };

            return PartialView("Preview", model);
        }

        private IEnumerable<GroupCategoryPage> FindPages(GroupCategoryListBlock currentBlock)
        {
            IEnumerable<GroupCategoryPage> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page;

            if (currentPage != null && currentPage is CompareStartPage)
            {
                pages = contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink).OfType<GroupCategoryPage>();
            }

            return pages ?? new List<GroupCategoryPage>();
        }

        private IEnumerable<GroupCategoryPage> Sort(IEnumerable<GroupCategoryPage> pages, FilterSortOrder sortOrder)
        {
            var asCollection = new PageDataCollection(pages);
            var sortFilter = new FilterSort(sortOrder);
            sortFilter.Sort(asCollection);
            return asCollection.Select(x => x as GroupCategoryPage);
        }
    }
}

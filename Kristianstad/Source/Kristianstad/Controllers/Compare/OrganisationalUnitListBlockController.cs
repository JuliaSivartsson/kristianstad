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
using Kristianstad.Models.Pages.Compare;
using Kristianstad.Business.Compare;
using Kristianstad.ViewModels.Compare;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitListBlockController : BlockController<OrganisationalUnitListBlock>
    {
        private readonly Injected<IContentLoader> contentLoader; // private IContentLoader contentLoader;

        public override ActionResult Index(OrganisationalUnitListBlock currentBlock)
        {
            var organisationalUnits = FindPages(currentBlock);
            organisationalUnits = Sort(organisationalUnits, currentBlock.SortOrder);

            var model = new OrganisationalUnitListModel()
            {
                OrganisationalUnits = organisationalUnits
            };

            return PartialView(model);
        }

        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;

            var model = new OrganisationalUnitPageModel(pd)
            {
                // Categories = CategoryHelper.GetCategoryViewModels(pd)
            };

            return PartialView("Preview", model);
        }

        /*
        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitListModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;

            var model = new OrganisationalUnitPageModel(pd)
            {
                // Categories = CategoryHelper.GetCategoryViewModels(pd)
            };

            return PartialView("Preview", model);
        }
        */

        private IEnumerable<PageData> FindPages(OrganisationalUnitListBlock currentBlock)
        {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
            var category = CategoryHelper.FindCompareCategory(categoryRepository, currentPage.Name);

            if (category != null && currentPage is CategoryPage)
            {
                pages = contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink).OfType<OrganisationalUnitPage>(); // .Where(o => o.Category.Contains(category.ID)).ToList();
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

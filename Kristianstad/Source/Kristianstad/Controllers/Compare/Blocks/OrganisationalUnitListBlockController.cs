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
using Newtonsoft.Json.Linq;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitListBlockController : BlockController<OrganisationalUnitListBlock>
    {
        private readonly Injected<IContentLoader> _contentLoader;
        private CookieHelper _cookieHelper;

        public OrganisationalUnitListBlockController()
        {
            _cookieHelper = new CookieHelper();
        }

        public override ActionResult Index(OrganisationalUnitListBlock currentBlock)
        {
            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page; // ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            var organisationalUnits = FindPages(currentBlock);
            organisationalUnits = Sort(organisationalUnits, currentBlock.SortOrder);

            var model = new OrganisationalUnitListModel()
            {
                CurrentPage = currentPage,
                OrganisationalUnits = organisationalUnits.ToList()
            };

            ViewData.Add("cookies", _cookieHelper.GetCookie(GetCategoryPageId(currentBlock)));

            return PartialView(model);
        }

        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;
            var model = new OrganisationalUnitPageModel(pd);

            return PartialView("Preview", model);
        }

        private IEnumerable<PageData> FindPages(OrganisationalUnitListBlock currentBlock)
        {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            if (currentPage is CategoryPage)
            {
                pages = _contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink).OfType<OrganisationalUnitPage>();
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

        private int GetCategoryPageId(OrganisationalUnitListBlock currentBlock)
        {
            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            if (currentPage.PageTypeName == typeof(OrganisationalUnitPage).GetPageType().Name)
            {
                return currentPage.ParentLink.ID;
            }

            if (currentPage.PageTypeName == typeof(CategoryPage).GetPageType().Name)
            {
                return currentPage.ContentLink.ID;
            }

            return currentPage.ParentLink.ID;
        }
    }
}

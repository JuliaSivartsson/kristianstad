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
using Kristianstad.HtmlHelpers;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitListBlockController : BlockController<OrganisationalUnitListBlock>
    {
        private readonly Injected<IContentLoader> contentLoader;
        private CookieHelper cookieHelper;

        public OrganisationalUnitListBlockController()
        {
            cookieHelper = new CookieHelper();
        }

        public override ActionResult Index(OrganisationalUnitListBlock currentBlock)
        {
            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page; // ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            var model = new OrganisationalUnitListModel()
            {
                CurrentPage = currentPage
            };

            var organisationalUnits = FindOrganisationalUnitPages(currentBlock);
            var sortedOrganisationalUnits = Sort(organisationalUnits, currentBlock.SortOrder).OfType<OrganisationalUnitPage>();

            foreach (var organisationalUnitPage in sortedOrganisationalUnits)
            {
                var organisationalUnitModel = new OrganisationalUnitPageModel(organisationalUnitPage);
                var compareResultPage = CompareHelper.GetCompareResultPage(contentLoader.Service, organisationalUnitPage);
                if (compareResultPage != null)
                {
                    // get organisational units in current ones compare page
                    var compareOrganisationalUnits = cookieHelper.GetOrganisationalUnitsInCompare(contentLoader.Service, compareResultPage);

                    // set compare info
                    organisationalUnitModel.HasComparePage = true;
                    organisationalUnitModel.InCompareAlready = compareOrganisationalUnits.Any(x => x.ID == organisationalUnitPage.ContentLink.ID);
                }

                model.OrganisationalUnits.Add(organisationalUnitModel);
            }

            return PartialView(model);
        }

        /*
        [HttpPost]
        public ActionResult IndexWithAddress(DistanceFromAddressModel distance, string address)
        {
            

            var model = new DistanceFromAddressModel()
            {
                MeasureFromAddress = address
            };

            model.DistanceList = new DistanceFromAddressModel()
            {
                MeasureFromAddress = address
            };

            // For Ajax request return partial view with form. Validation will work out of the box.
            return PartialView("~/Views/OrganisationalUnitListBlock/_AddressDistanceForm", model);


            return PartialView(model);
        }
    */

        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;
            var model = new OrganisationalUnitPageModel(pd);

            return PartialView("Preview", model);
        }

        private IEnumerable<PageData> FindOrganisationalUnitPages(OrganisationalUnitListBlock currentBlock)
        {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            if (currentPage is CategoryPage)
            {
                pages = contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink).OfType<OrganisationalUnitPage>();
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

        /*
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
        */
    }
}

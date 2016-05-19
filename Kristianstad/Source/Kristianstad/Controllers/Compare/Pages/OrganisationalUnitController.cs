using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Search;
using EPiServer.Web;
using EPiServer.Web.Hosting;
using EPiServer.Web.Mvc.Html;
using EPiServer.DataAbstraction;
using System;
using System.Text;
using System.Text.RegularExpressions;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.Web.Mvc;
using Kristianstad.Models.Pages.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Business.Compare;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiCore.ViewModels.Pages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Kristianstad.HtmlHelpers;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitController : PageController<OrganisationalUnitPage>
    {
        private const string CookieName = "compare";
        private readonly Injected<IContentLoader> contentLoader;
        private CookieHelper cookieHelper;

        public OrganisationalUnitController()
        {
            cookieHelper = new CookieHelper();
        }

        public ActionResult Full(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);
            return PartialView("Full", model);
        }

        public ActionResult Index(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);

            // Checks if the CurrentPage is in the CompareList.
            var compareResultPage = CompareHelper.GetCompareResultPage(contentLoader.Service, currentPage);
            if (compareResultPage != null)
            {
                var items = cookieHelper.GetOrganisationalUnitsInCompare(contentLoader.Service, compareResultPage);
                if (items.Any(x => x.ID == currentPage.ContentLink.ID))
                {
                    model.InCompareAlready = true;
                }
            }

            return View(model);
        }

        public ActionResult AddOrganisationalUnitToCompare(int id, string redirectBackTo = null) // OrganisationalUnitPage organisationalUnitPage
        {
            return AddOrRemoveOrganisationalUnitFromCompare(true, id, redirectBackTo);
        }

        public ActionResult RemoveOrganisationalUnitFromCompare(int id, string redirectBackTo = null) // OrganisationalUnitPage organisationalUnitPage
        {
            return AddOrRemoveOrganisationalUnitFromCompare(false, id, redirectBackTo);
        }

        private ActionResult AddOrRemoveOrganisationalUnitFromCompare(bool addNotRemove, int id, string redirectBackTo = null)
        {
            var page = contentLoader.Service.Get<PageData>(new ContentReference(id));
            if (page != null && page is OrganisationalUnitPage)
            {
                var organisationalUnitPage = page as OrganisationalUnitPage;

                var compareResultPage = CompareHelper.GetCompareResultPage(contentLoader.Service, organisationalUnitPage);
                if (compareResultPage != null)
                {
                    if (addNotRemove)
                    {
                        cookieHelper.AddOrganisationalUnitToCompare(compareResultPage, organisationalUnitPage);
                    }
                    else
                    {
                        cookieHelper.RemoveOrganisationalUnitFromCompare(compareResultPage, organisationalUnitPage);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(redirectBackTo))
            {
                return Redirect(redirectBackTo);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}

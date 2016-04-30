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
using Kristianstad.Models.Pages;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Business.Compare;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiCore.ViewModels.Pages;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitController : PageController<OrganisationalUnitPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;
        
        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;

            var model = new OrganisationalUnitPageModel(pd)
            {
                // Categories = CategoryHelper.GetCategoryViewModels(pd)
            };

            return PartialView("Preview", model);
        }

        public ActionResult Full(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage)
            {
                // Category = currentPage.Category,
                // Categories = CategoryHelper.GetCategoryViewModels(currentPage)
            };

            var editHints = ViewData.GetEditHints<OrganisationalUnitPageModel, OrganisationalUnitPage>();
            // editHints.AddConnection(m => m.Category, p => p.Category);
            // editHints.AddFullRefreshFor(p => p.Category);
            // editHints.AddFullRefreshFor(p => p.StartPublish);

            return PartialView("Full", model);
        }

        public ActionResult Index(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);

            // Connect the view models logotype property to the start page's to make it editable
            // var editHints = ViewData.GetEditHints<OrganisationalUnitPageModel, OrganisationalUnitPage>();
            // editHints.AddConnection(m => m.Category, p => p.Category);

            return View(model);
        }
    }
}

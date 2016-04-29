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
using Kristianstad.Models.Pages;
using Kristianstad.Business.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages.Compare;
using EPiCore.ViewModels.Pages;

namespace Kristianstad.Controllers.Compare
{
    public class AddOrganisationalUnitsFormController : PageController<AddOrganisationalUnitsFormPage>
    {
        public ActionResult Index(AddOrganisationalUnitsFormPage currentPage)
        {
            var model = new PageViewModel<AddOrganisationalUnitsFormPage>(currentPage);
            return View(model);
        }

        public ActionResult Save(AddOrganisationalUnitsFormPage currentPage, OrganisationalUnitModel organisationalUnit)
        {
            if (currentPage.MainContentArea != null || currentPage.MainContentArea.Items.Any())
            {
                var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

                foreach (var item in currentPage.MainContentArea.Items)
                {
                    var block = contentLoader.Get<OrganisationalUnitBlock>(item.ContentLink);
                    if (block != null)
                    {
                        block.OrganisationalUnit = organisationalUnit;
                    }
                }
            }
            return null;
        }
    }
}

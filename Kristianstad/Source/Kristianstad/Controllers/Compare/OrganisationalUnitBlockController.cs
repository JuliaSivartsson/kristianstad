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

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitBlockController : ContentController<OrganisationalUnitBlock>
    {
        // private readonly Injected<IContentLoader> _contentLoader;
        
        public ActionResult Index(OrganisationalUnitBlock currentBlock)
        {
            var model = new OrganisationalUnitBlockModel(currentBlock);
            return PartialView(model);
        }

        public ActionResult Preview(PageData currentPage, OrganisationalUnitBlock currentBlock)
        {
            var model = new OrganisationalUnitBlockModel(currentBlock);
            return PartialView("Preview", model);
        }


    }
}

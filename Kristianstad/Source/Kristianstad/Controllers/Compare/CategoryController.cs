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
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages.Compare;
using EPiServer.Editor;
using Kristianstad.Business.Models.Blocks.Compare;
using EPiServer;
using Kristianstad.CompareDomain;

namespace Kristianstad.Controllers.Compare
{
    public class CategoryController : PageController<CategoryPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;

        public ActionResult Index(CategoryPage currentPage)
        {
            var model = new CategoryPageModel(currentPage);
            return View(model);
        }

        public ActionResult SaveResultQueries(List<ResultQueryGroupModel> resultQueryGroups)
        {
            resultQueryGroups = resultQueryGroups;
            return null;
        }
    }
}

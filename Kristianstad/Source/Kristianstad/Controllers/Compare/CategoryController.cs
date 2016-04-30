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
using Kristianstad.Models.Pages;

namespace Kristianstad.Controllers.Compare
{
    public class CategoryController : PageController<CategoryPage>
    {
        public int PreviewTextLength { get; set; }
        
        public ActionResult Full(CategoryPage currentPage)
        {
            PreviewTextLength = 200;

            var model = new CategoryPageModel(currentPage)
            {

            };

            var editHints = ViewData.GetEditHints<CategoryPageModel, CategoryPage>();
            editHints.AddFullRefreshFor(p => p.StartPublish);

            return PartialView("Full", model);
        }

        public ActionResult Index(CategoryPage currentPage)
        {
            var model = new CategoryPageModel(currentPage); //PageViewModel.Create(currentPage);

            //Connect the view models logotype property to the start page's to make it editable
            var editHints = ViewData.GetEditHints<CategoryPageModel, CategoryPage>();
            //editHints.AddConnection(m => m.Category, p => p.Category);
            editHints.AddConnection(m => m.StartPublish, p => p.StartPublish);

            return View(model);
        }
    }
}

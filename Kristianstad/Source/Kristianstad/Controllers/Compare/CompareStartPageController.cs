using EPiCore.ViewModels.Pages;
using EPiServer;
using Kristianstad.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kristianstad.Controllers.Compare
{
    public class CompareStartPageController : Controller
    {
        public ActionResult Index(CompareStartPage currentPage)
        {
            var model = new PageViewModel<CompareStartPage>(currentPage);
            return View(model);
        }
    }
}
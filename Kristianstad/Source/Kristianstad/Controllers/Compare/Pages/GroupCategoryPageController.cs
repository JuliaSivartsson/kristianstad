using EPiCore.ViewModels.Pages;
using EPiServer;
using EPiServer.Web.Mvc;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kristianstad.Controllers.Compare
{
    public class GroupCategoryPageController : PageController<GroupCategoryPage>
    {
        public ActionResult Index(GroupCategoryPage currentPage)
        {
            var model = new PageViewModel<GroupCategoryPage>(currentPage);
            return View(model);
        }
    }
}
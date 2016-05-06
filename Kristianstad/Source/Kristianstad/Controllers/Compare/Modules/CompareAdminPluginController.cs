using EPiServer.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Kristianstad.Controllers.Compare
{
    [GuiPlugIn(
        Area = PlugInArea.AdminMenu,
        Url = "/CompareAdminPlugin",
        DisplayName = "Compare admin plugin")]
    [System.Web.Mvc.Authorize(Roles = "CmsAdmins")]
    public class CompareAdminPluginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

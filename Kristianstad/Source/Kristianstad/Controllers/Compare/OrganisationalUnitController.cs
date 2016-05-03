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

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitController : PageController<OrganisationalUnitPage>
    {
        private const string CookieName = "compare";
        private readonly Injected<IContentLoader> _contentLoader;

        public int PreviewTextLength { get; set; }

        public ActionResult Full(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);
            var editHints = ViewData.GetEditHints<OrganisationalUnitPageModel, OrganisationalUnitPage>();

            return PartialView("Full", model);
        }

        public ActionResult Index(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);

            // Connect the view models logotype property to the start page's to make it editable

            var editHints = ViewData.GetEditHints<OrganisationalUnitPage, OrganisationalUnitPage>();
            editHints.AddConnection(m => m.Category, p => p.Category);
            editHints.AddConnection(m => m.StartPublish, p => p.StartPublish);

            // Checks if the CurrentPage is in the CompareList.
            foreach (int item in GetCookie(CookieName + currentPage.ParentLink.ID))
            {
                if (item == currentPage.ContentLink.ID)
                {
                    ViewData.Add("cookies", currentPage.ContentLink.ID);
                }
            }

            return View(model);
        }

        protected string GetPreviewText(OrganisationalUnitPage page)
        {
            if (PreviewTextLength <= 0)
            {
                return string.Empty;
            }

            string previewText = String.Empty;

            if (String.IsNullOrEmpty(previewText))
            {
                return string.Empty;
            }

            // If the MainBody contains DynamicContents, replace those with an empty string
            StringBuilder regexPattern = new StringBuilder(@"<span[\s\W\w]*?classid=""");
            regexPattern.Append(DynamicContentFactory.Instance.DynamicContentId.ToString());
            regexPattern.Append(@"""[\s\W\w]*?</span>");
            previewText = Regex.Replace(previewText, regexPattern.ToString(), string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return TextIndexer.StripHtml(previewText, PreviewTextLength);
        }

        public ActionResult AddOuToCompare(int id, PageData currentPage)
        {
            AddCookie(CookieName + currentPage.ParentLink.ID, id);

            return RedirectToAction("Index");
        }

        private void AddCookie(string cookieName, int id)
        {
            List<int> cookieCollection = GetCookie(cookieName);

            bool isAlreadyInCookie = false;
            foreach (int item in cookieCollection)
            {
                if (item == id)
                {
                    isAlreadyInCookie = true;
                    break;
                }
            }

            List<int> newCookieCollection = new List<int>();
            if (isAlreadyInCookie)
            {
                foreach (int item in cookieCollection)
                {
                    if (item != id)
                    {
                        newCookieCollection.Add(item);
                    }
                }
            }
            else
            {
                newCookieCollection = cookieCollection;
                newCookieCollection.Add(id);
            }

            Response.Cookies[cookieName].Value = JsonConvert.SerializeObject(newCookieCollection);
        }

        private List<int> GetCookie(string cookieName)
        {
            JArray cookie;
            try
            {
                cookie = JArray.Parse(Request.Cookies[cookieName].Value);
            }
            catch
            {
                cookie = new JArray();
            }

            return cookie.Select(o => (int)o).ToList();
        }
    }
}

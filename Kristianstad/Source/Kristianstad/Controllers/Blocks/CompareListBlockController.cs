using System;
using System.Web.Mvc;
using EPiCore.Controllers;
using EPiCore.ViewModels.Blocks;
using EPiServer;
using Kristianstad.Business.Models.Blocks;
using EPiServer.Core;
using Kristianstad.Models.Pages;
using System.Collections.Generic;
using EPiServer.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using EPiServer.Web.Routing;
using Kristianstad.Business.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages.Compare;

namespace Kristianstad.Controllers.Blocks
{
    public class CompareListBlockController : BaseBlockController<CompareListBlock>
    {
        private readonly Injected<IContentLoader> _contentLoader;

        public override ActionResult Index(CompareListBlock currentBlock)
        {
            CompareListModel compareOUs = GetCompareObjects(currentBlock);

            return PartialView("~/Views/CompareList/Index.cshtml", compareOUs);
        }

        public ActionResult AddOuToCompare(int id, PageData currentPage)
        {
            AddCookie("grundskola", id);
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

        private IEnumerable<PageData> FindOrganisationalUnits(CompareListBlock currentBlock)
       {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);
            PageReference listRoot = currentPage.PageLink;

            if (currentPage.PageTypeName == typeof(OrganisationalUnitPage).GetPageType().Name)
            {
                pages = _contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ParentLink);
            }

            if (currentPage.PageTypeName == typeof(CategoryPage).GetPageType().Name)
            {
                pages = _contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ContentLink);
            }

            return pages ?? new List<PageData>();
        }

        private CompareListModel GetCompareObjects(CompareListBlock currentBlock)
        {
            List<PageData> ouPages = FindOrganisationalUnits(currentBlock).ToList();
            CompareListModel comparePages = new CompareListModel();

            foreach (int ou in GetCookie("grundskola"))
            {
                PageData page = ouPages.Where(o => o.ContentLink.ID == ou).First();
                if (page != null)
                {
                    CompareModel cm = new CompareModel
                    {
                        Name = page.Name,
                        ID = page.ContentLink.ID,
                        URL = GetExternalUrl(page)
                    };
                    comparePages.OrganisationalUnits.Add(cm);
                }
            }

            return comparePages;
        }

        public static string GetExternalUrl(IContent content)
        {
            var internalUrl = UrlResolver.Current.GetUrl(content.ContentLink);

            var url = new UrlBuilder(internalUrl);
            Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.Encoding.UTF8);

            var friendlyUrl = UriSupport.AbsoluteUrlBySettings(url.ToString());
            return friendlyUrl;
        }
    }
}

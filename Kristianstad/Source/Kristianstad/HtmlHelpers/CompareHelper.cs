using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kristianstad.HtmlHelpers
{
    public static class CompareHelper
    {
        public static void RenderContentReference(this HtmlHelper html, ContentReference contentReference)
        {
            IContentData contentData =
                ServiceLocator.Current.GetInstance<IContentRepository>().Get<IContent>(contentReference);
            IContentDataExtensions.RenderContentData(html, contentData, false, html.ViewContext.ViewData["tag"] as string);
        }

        public static string GetExternalUrl(IContent content)
        {
            var internalUrl = UrlResolver.Current.GetUrl(content.ContentLink);

            var url = new UrlBuilder(internalUrl);
            Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.Encoding.UTF8);

            var friendlyUrl = UriSupport.AbsoluteUrlBySettings(url.ToString());
            return friendlyUrl;
        }

        public static PageData GetCurrentPage()
        {
            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            return pageRouteHelper.Page;
        }
    }
}
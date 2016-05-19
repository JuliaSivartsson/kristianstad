using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Kristianstad.Models.Pages.Compare;
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

        public static string GetExternalUrl(ContentReference contentReference)
        {
            var internalUrl = UrlResolver.Current.GetUrl(contentReference);

            var url = new UrlBuilder(internalUrl);
            Global.UrlRewriteProvider.ConvertToExternal(url, null, System.Text.Encoding.UTF8);

            var friendlyUrl = UriSupport.AbsoluteUrlBySettings(url.ToString());
            return friendlyUrl;
        }

        public static ContentReference GetCompareResultPage(IContentLoader contentLoader, OrganisationalUnitPage organisationalUnitPage)
        {
            if (organisationalUnitPage.CompareListBlock.CompareResultPage != null)
            {
                return organisationalUnitPage.CompareListBlock.CompareResultPage;
            }

            var parentPage = contentLoader.Get<PageData>(organisationalUnitPage.ParentLink);
            if (parentPage != null && parentPage is CategoryPage)
            {
                var categoryPage = parentPage as CategoryPage;
                return categoryPage.CompareListBlock.CompareResultPage;
            }

            return null;
        }
    }
}
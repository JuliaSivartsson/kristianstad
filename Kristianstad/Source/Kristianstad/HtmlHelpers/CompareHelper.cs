using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
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
    }
}
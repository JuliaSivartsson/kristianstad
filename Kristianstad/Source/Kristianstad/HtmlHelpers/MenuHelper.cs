// <copyright file="MenuHelper.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.HtmlHelpers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using EPiCore.Services.Content;
    using EPiCore.Services.Content.Interfaces;
    using EPiServer;

    /// <summary>
    /// The <see cref="MenuHelper"/> class.
    /// </summary>
    public static class MenuHelper
    {
        private const string Menu = "menu";
        private static IPagePropertyService serv = new PagePropertyService();

        /// <summary>
        /// Determines whether the mega menu should be open.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>True if the mega menu should be open.</returns>
        public static bool MegaMenuOpen(this HtmlHelper helper)
        {
            return IsMegaMenuOpen(helper);
        }

        /// <summary>
        /// Get the correct url to open or close the mega menu.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>The url to open or close the mega menu.</returns>
        public static string MegaMenuUrl(this HtmlHelper helper)
        {
            var currentUrl = new UriBuilder(helper.ViewContext.RequestContext.HttpContext.Request.Url);
            var query = HttpUtility.ParseQueryString(currentUrl.Query);
            if (IsMegaMenuOpen(helper))
            {
                query.Remove(Menu);
            }
            else
            {
                query[Menu] = "open";
            }

            currentUrl.Query = query.ToString();
            return currentUrl.ToString();
        }

        /// <summary>
        /// True if the mega menu should be open.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>True if the mega menu should be open.</returns>
        private static bool IsMegaMenuOpen(HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.QueryString[Menu] != null;
        }

        /// <summary>
        /// Adjust link from settings.
        /// </summary>
        /// <param name="helper">helper</param>
        /// <returns>URL of Adjust link in settings</returns>
        public static Url Adjust(this HtmlHelper helper)
        {
            return serv.GetSettingsPageProperty<Url>("Adjust").ToString();
        }

        /// <summary>
        /// EServices link from settings.
        /// </summary>
        /// <param name="helper">helper</param>
        /// <returns>URL of EServices link in settings</returns>
        public static Url EServices(this HtmlHelper helper)
        {
            return serv.GetSettingsPageProperty<Url>("EServices");
        }

        /// <summary>
        /// Contact link from settings.
        /// </summary>
        /// <param name="helper">helper</param>
        /// <returns>URL of Contact link in settings</returns>
        public static Url Contact(this HtmlHelper helper)
        {
            return serv.GetSettingsPageProperty<Url>("Contact");
        }
    }
}
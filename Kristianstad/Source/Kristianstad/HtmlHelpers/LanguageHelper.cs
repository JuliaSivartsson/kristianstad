// <copyright file="LanguageHelper.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Routing;

    /// <summary>
    /// The <see cref="LanguageHelper"/> class, with language related utility methods.
    /// </summary>
    public static class LanguageHelper
    {
        private static readonly Injected<ILanguageBranchRepository> _langBranchRepo;

        /// <summary>
        /// Gets the available languages for the current page, except for the language for which the page is currently being displayed.
        /// </summary>
        /// <param name="helper">The helper</param>
        /// <param name="currentPage">The current page</param>
        /// <returns>All other languages of currentPage, where each item is a Tuple(language, URL)</returns>
        public static IList<Tuple<string, string>> GetOtherLanguages(this HtmlHelper helper, PageData currentPage)
        {
            if (currentPage == null)
            {
                throw new ArgumentNullException(
                    nameof(currentPage),
                    "Current page could not be found. Has the website been properly configured (CMS > Admin > Manage Websites)?");
            }

            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var languages = currentPage.ExistingLanguages.Except(new[] { currentPage.Language });

            return languages
                .Select(x => new Tuple<string, string>(_langBranchRepo.Service.Load(x).Name, urlResolver.GetUrl(currentPage.ContentLink, x.Name)))
                .OrderBy(x => x.Item1)
                .ToList();
        }
    }
}
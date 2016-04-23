// <copyright file="MenuKrController.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Controllers.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using EPiCore.Caching;
    using EPiCore.Controllers;
    using EPiCore.Services.Content.Interfaces;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Framework.Cache;
    using EPiServer.Globalization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Routing;
    using Models.Misc;
    using Models.Pages;
    using ViewModels.Common;
    using EPiServer.Editor;

    /// <summary>
    /// The <see cref="MenuKrController" /> class.
    /// </summary>
    public class MenuKrController : BaseController
    {
        private readonly Injected<IPageSource> _pageSource;
        private readonly Injected<IFilterService> _filterService;
        private readonly Injected<IContentTypeService> _contentTypeService;
        private readonly Injected<UrlResolver> _urlResolver;
        private readonly Injected<ISynchronizedObjectInstanceCache> _cache;
        private readonly Injected<IContentLoader> _contentLoader;
        private readonly string _languageBranch;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuKrController"/> class.
        /// </summary>
        public MenuKrController()
            : this(ContentLanguage.PreferredCulture.Name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuKrController" /> class.
        /// </summary>
        /// <param name="languageBranch">The language branch.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="languageBranch" /> is <c>null</c> or white space.</exception>
        public MenuKrController(string languageBranch)
        {
            if (string.IsNullOrWhiteSpace(languageBranch))
            {
                throw new ArgumentException("Language branch cannot be null or white space.", nameof(languageBranch));
            }

            _languageBranch = languageBranch;
        }

        /// <summary>
        /// Handles construction of bread crumbs for descendants of section pages.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The path from the root section page, to the currentPage, where each node is a Tuple(name, URL)</returns>
        [ChildActionOnly]
        public PartialViewResult Breadcrumbs(PageData currentPage)
        {
            if (currentPage == null)
            {
                throw new ArgumentNullException(
                    nameof(currentPage),
                    "Current page could not be found, and a menu could therefore not be built. Has the website been properly configured (CMS > Admin > Manage Websites)?");
            }

            var breadcrumbsRootPageType = _contentTypeService.Service.GetPageTypesByModelType(typeof(SectionPage)).SingleOrDefault();
            if (breadcrumbsRootPageType == null)
            {
                throw new ContentNotFoundException("Breadcrumbs root page type could not be found, and breadcrumbs could therefore not be built.");
            }

            var path = _contentLoader.Service.GetAncestors(currentPage.ContentLink)
                .Reverse()
                .SkipWhile(x => !ContentReference.IsNullOrEmpty(x.ContentLink) && x.ContentTypeID != breadcrumbsRootPageType.ID)
                .OfType<PageData>()
                .Select(x => new Tuple<string, string>(x.Name, _urlResolver.Service.GetUrl(x)))
                .ToList();
            path.Add(new Tuple<string, string>(currentPage.Name, _urlResolver.Service.GetUrl(currentPage)));

            return PartialView("_Breadcrumbs", path);
        }

        /// <summary>
        /// Handles construction of a menu for every descendant of a section page.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The rendered section menu.</returns>
        [ChildActionOnly]
        public PartialViewResult SectionMenu(PageData currentPage)
        {
            if (currentPage == null)
            {
                throw new ArgumentNullException(
                    nameof(currentPage),
                    "Current page could not be found, and a menu could therefore not be built. Has the website been properly configured (CMS > Admin > Manage Websites)?");
            }

            var menuRootPageType = _contentTypeService.Service.GetPageTypesByModelType(typeof(SectionPage)).SingleOrDefault();
            if (menuRootPageType == null)
            {
                throw new ContentNotFoundException("Menu root page type could not be found, and a menu could therefore not be built.");
            }

            return PartialView("_SectionMenu", Menu(currentPage, menuRootPageType.ID));
        }

        /// <summary>
        /// Handles construction of a menu for every descendant of start page.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The rendered full menu.</returns>
        [ChildActionOnly]
        public PartialViewResult FullMenu(PageData currentPage)
        {
            if (currentPage == null)
            {
                throw new ArgumentNullException(
                    nameof(currentPage),
                    "Current page could not be found, and a menu could therefore not be built. Has the website been properly configured (CMS > Admin > Manage Websites)?");
            }

            var menuRootPageType = _contentTypeService.Service.GetPageTypesByModelType(typeof(StartPage)).SingleOrDefault();
            if (menuRootPageType == null)
            {
                throw new ContentNotFoundException("Menu root page type could not be found, and a menu could therefore not be built.");
            }

            return PartialView("_FullMenu", Menu(currentPage, menuRootPageType.ID));
        }

        /// <summary>
        /// Handles construction of a menu, consisting of a menu root page and all descendants of that page.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="menuRootTypeId">The type ID of the page type that shall be the root of this menu.</param>
        /// <returns>A model with the root of the menu tree, the ID of the current page, and a path from root of tree to current page</returns>
        private MenuViewModel Menu(PageData currentPage, int menuRootTypeId)
        {
            var pathToMenuRoot = GetPathToMenuRoot(currentPage, menuRootTypeId);

            // Return menu with no children if section menu is being requested for start/root
            // Current page is neither a descendant of a, nor a, section menu root page
            if (pathToMenuRoot == null)
            {
                return new MenuViewModel
                {
                    Root = new MenuItem(currentPage.Name, string.Empty),
                    Path = { currentPage.ContentLink.ID },
                    CurrentPageID = currentPage.ContentLink.ID
                };
            }

            var menuRoot = GetMenu(pathToMenuRoot.Last());

            return new MenuViewModel
            {
                Root = menuRoot,
                CurrentPageID = currentPage.ContentLink.ID,
                Path = pathToMenuRoot.Select(n => n.ContentLink.ID).ToList()
            };
        }

        /// <summary>
        /// Constructs a pth from the given page to the closest ancestor of the given menu root page type ID.
        /// The path includes the page itself and the section menu root page.
        /// </summary>
        /// <param name="pageData">The page for which to construct a path to an ancestor</param>
        /// <param name="menuRootTypeId">The page type ID of the root of the path to construct</param>
        /// <returns>
        /// A path from the given page, to the first page of the given page that is encountered, including the given page and the root.
        /// Null is returned if no ancestor with the given page type ID is found, and the given page is not of the given menu root page type ID
        /// </returns>
        private List<PageData> GetPathToMenuRoot(PageData pageData, int menuRootTypeId)
        {
            if (pageData == null)
            {
                throw new ArgumentNullException(nameof(pageData));
            }

            Func<PageData, bool> isRootPage = page => ContentReference.RootPage.CompareToIgnoreWorkID(page.ContentLink);
            Func<PageData, bool> isMenuRootPageType = page => page.ContentTypeID == menuRootTypeId;

            var path = new List<PageData>();
            while (!isRootPage(pageData))
            {
                path.Add(pageData);
                if (isMenuRootPageType(pageData))
                {
                    return path;
                }

                pageData = _pageSource.Service.GetPage(pageData.ParentLink);
            }

            // None of the ancestors are section menu roots
            return null;
        }

        /// <summary>
        /// Attempts to retrieve menu for the given menu root page from cache.
        /// If no such menu is found in cache, it is constructed and cached.
        /// </summary>
        /// <param name="menuRootPage">a</param>
        /// <returns>A menu item representing the given menu root page, and all its descendants</returns>
        private MenuItem GetMenu(PageData menuRootPage)
        {
            var key = "Menu_" + menuRootPage.ContentLink.ID + _languageBranch;
            var menu = _cache.Service.Get(key) as MenuItem;

            if (menu == null)
            {
                menu = BuildMenu(menuRootPage);

                if (!PageEditing.PageIsInEditMode)
                {
                    _cache.Service.Insert(
                        key,
                        menu,
                        new CacheEvictionPolicy(null, null, new string[] { CacheKeys.ContentMasterKey }, TimeSpan.FromHours(1), CacheTimeoutType.Sliding));
                }
            }

            return menu;
        }

        /// <summary>
        /// Constructs a tree of items representing a menu with the given page as root.
        /// </summary>
        /// <param name="rootPage">The page that shall be the root of the menu</param>
        /// <returns>A menu item representing the given menu root page, and all its descendants</returns>
        private MenuItem BuildMenu(PageData rootPage)
        {
            var rootItem = new MenuItem(rootPage.Name, _urlResolver.Service.GetUrl(rootPage.ContentLink)) { ID = rootPage.ContentLink.ID };

            var children = _pageSource.Service.GetChildren(rootPage.ContentLink.ToPageReference())
                .Where(_filterService.Service.IsVisible);
            foreach (var child in children)
            {
                var childItem = BuildMenu(child);
                rootItem.Children.Add(childItem);
            }

            return rootItem;
        }
    }
}

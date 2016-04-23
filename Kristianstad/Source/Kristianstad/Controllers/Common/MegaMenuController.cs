// <copyright file="MegaMenuController.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Controllers.Controllers
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
    using EPiServer.Editor;
    using EPiServer.Framework.Cache;
    using EPiServer.Globalization;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Routing;
    using Models.Pages;
    using ViewModels.Common;

    /// <summary>
    /// The <see cref="MegaMenuController" /> class. Controller for rendering the mega menu.
    /// </summary>
    [ChildActionOnly]
    public class MegaMenuController : BaseController
    {
        private const string Cachekey = "MegaMenu_";
        private const int MaxNumberOfSectionPages = 7;

        private readonly Injected<IContentLoader> _contentLoader;
        private readonly Injected<ISynchronizedObjectInstanceCache> _cache;
        private readonly Injected<IFilterService> _filterService;
        private readonly Injected<UrlResolver> _urlResolver;

        private readonly string _languageBranch;

        /// <summary>
        /// Initializes a new instance of the <see cref="MegaMenuController"/> class.
        /// </summary>
        public MegaMenuController()
            : this(ContentLanguage.PreferredCulture.Name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MegaMenuController" /> class.
        /// </summary>
        /// <param name="languageBranch">The language branch.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="languageBranch" /> is <c>null</c> or white space.</exception>
        public MegaMenuController(string languageBranch)
        {
            if (string.IsNullOrWhiteSpace(languageBranch))
            {
                throw new ArgumentException("Language branch cannot be null or white space.", nameof(languageBranch));
            }

            _languageBranch = languageBranch;
        }

        /// <summary>
        /// Loads the mega menu content.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The rendered mega menu.</returns>
        public PartialViewResult Index(PageData currentPage)
        {
            // Skip caching in edit mode.
            if (PageEditing.PageIsInEditMode)
            {
                return PartialView("_MegaMenu", GetMenuViewModel());
            }

            // Try to get menu from cache, else generate new menu.
            var key = Cachekey + _languageBranch;
            var model = _cache.Service.Get(key) as MegaMenuViewModel;

            if (model == null)
            {
                model = GetMenuViewModel();
                _cache.Service.Insert(key, model, new CacheEvictionPolicy(null, null, new string[] { CacheKeys.ContentMasterKey }, TimeSpan.FromHours(1), CacheTimeoutType.Sliding));
            }

            return PartialView("_MegaMenu", model);
        }

        /// <summary>
        /// Gets the menu view model.
        /// </summary>
        /// <returns>MegaMenuViewModel</returns>
        private MegaMenuViewModel GetMenuViewModel()
        {
            var sectionPages = _contentLoader.Service.GetChildren<SectionPage>(ContentReference.StartPage, LanguageSelector.AutoDetect(), 0, MaxNumberOfSectionPages)
                .Where(page => page.VisibleInMenu).ToList();
            var menuPages = CreateMenuList(sectionPages);

            return new MegaMenuViewModel
            {
                MenuPages = menuPages
            };
        }

        /// <summary>
        /// Creates the menu list.
        /// </summary>
        /// <param name="sectionPages">The section pages.</param>
        /// <returns>A list of KeyValuePairs contaning section page </returns>
        private List<KeyValuePair<MenuSectionViewModel, List<MenuContentViewModel>>> CreateMenuList(List<SectionPage> sectionPages)
        {
            var menuPages = new List<KeyValuePair<MenuSectionViewModel, List<MenuContentViewModel>>>();

            foreach (var page in sectionPages)
            {
                var sectionViewModel = new MenuSectionViewModel
                {
                    ID = page.ContentLink.ID,
                    Title = page.Name,
                    Icon = page.Icon,
                    QuickLinks = page.MenuLinkList
                };

                var children = GetChildrenViewModels(page);

                menuPages.Add(new KeyValuePair<MenuSectionViewModel, List<MenuContentViewModel>>(sectionViewModel, children));
            }

            return menuPages;
        }

        /// <summary>
        /// Collects the children pages of a section page and map them to a list of MenuContentViewModel objects.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A list of MenuContentViewModel objects</returns>
        private List<MenuContentViewModel> GetChildrenViewModels(SectionPage parent)
        {
            var childrenViewModels = new List<MenuContentViewModel>();
            var maxNumberOfChildren = parent.MaxNumberOfMenuEntries >= 0 ? parent.MaxNumberOfMenuEntries : 500;
            var children = _contentLoader.Service.GetChildren<ContentPage>(parent.ContentLink, LanguageSelector.AutoDetect(), 0, 500).
                    Where(sp => _filterService.Service.IsVisible(sp)).ToList();

            foreach (var child in children)
            {
                childrenViewModels.Add(
                    new MenuContentViewModel
                    {
                        Title = child.MenuTitle,
                        Description = child.MenuDescription,
                        URL = _urlResolver.Service.GetUrl(child),
                        RenderAsLinkOnly = children.IndexOf(child) + 1 > maxNumberOfChildren
                    });
            }

            return childrenViewModels;
        }
    }
}
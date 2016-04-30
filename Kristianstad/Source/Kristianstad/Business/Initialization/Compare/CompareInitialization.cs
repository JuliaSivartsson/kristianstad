﻿using System;
using System.Linq;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.BaseLibrary;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.DataAbstraction;
using System.Web.Routing;
using EPiServer.Web.Routing;
using EPiServer;
using Kristianstad.Business.Compare;
using Kristianstad.Models.Pages;
using EPiServer.DataAbstraction.RuntimeModel;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiServer.Globalization;
using Kristianstad.CompareDomain.Abstract;
using System.Collections.Generic;
using Kristianstad.CompareDomain.Models;
using Kristianstad.CompareDomain;
using Kristianstad.Business.Models.Blocks.Compare;

namespace Kristianstad.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class CompareInitialization : IInitializableModule
    {
        private const string ORGANISATIONAL_UNIT_FOLDER_NAME = "OrganisationalUnits";

        public void Initialize(InitializationEngine context)
        {
            DataFactory.Instance.CreatingPage += Instance_CreatingPage;
            DataFactory.Instance.SavingContent += Instance_SavingContent;
            DataFactory.Instance.SavedContent += Instance_SavedContent;
        }

        public void Uninitialize(InitializationEngine context)
        {
            DataFactory.Instance.CreatingPage -= Instance_CreatingPage;
            DataFactory.Instance.SavingContent -= Instance_SavingContent;
            DataFactory.Instance.SavedContent -= Instance_SavedContent;
        }

        // Returns if we are doing an import or mirroring
        public bool IsImport()
        {
            return ContextCache.Current["CurrentITransferContext"] != null;
        }

        void Instance_SavedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is CategoryPage)
            {
                var page = e.Content as CategoryPage;
                if (!string.IsNullOrWhiteSpace(page.CreateNewOrganisationalUnits))
                {
                    // changed organisational units, if any added then create new pages for them
                    string[] list = page.CreateNewOrganisationalUnits.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Get a list of organisational units to add (web service and id)
                    List<OrganisationalUnit> addedOrganisationalUnitWebServiceInfo = OrganisationalUnitHelper.GetModelsFromCheckboxValues(list);

                    // Compare against existing OU pages
                    var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                    var ouPagesInCategory = contentRepository.GetChildren<PageData>(page.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();

                    // Get organisational unit info from web service(s)
                    var organisationalUnits = CompareServiceFactory.Instance.GetWebServiceOrganisationalUnits();

                    // Create list of the OU to add
                    var organisationalUnitsToAdd = organisationalUnits.Where(x => addedOrganisationalUnitWebServiceInfo.Any(x2 => x.WebServiceName == x2.WebServiceName && x.OrganisationalUnitId == x2.OrganisationalUnitId));

                    // Create new pages
                    foreach (var newOU in organisationalUnitsToAdd)
                    {
                        var existingPage = ouPagesInCategory.Where(x => x.Name.ToLower() == newOU.Name.ToLower()).FirstOrDefault();
                        if (existingPage != null)
                        {
                            // e.CancelReason = "A organisational unit page with the name \"" + existingPage.Name + "\" already exists.";
                            // e.CancelAction = true;
                            existingPage = existingPage;
                        }
                        else
                        {
                            // Create a new organisational unit page
                            var newPage = contentRepository.GetDefault<OrganisationalUnitPage>(page.ContentLink);
                            newPage.Name = newOU.Name;
                            newPage.MenuTitle = newOU.Name;
                            newPage.MenuDescription = newOU.Name;

                            // Add a source info block
                            var sourceInfoBlock = contentRepository.GetDefault<OrganisationalUnitSourceInfoBlock>(page.ContentLink);
                            sourceInfoBlock.Name = newOU.Name;
                            sourceInfoBlock.WebServiceName = newOU.WebServiceName;
                            sourceInfoBlock.OrganisationalUnitId = newOU.OrganisationalUnitId;
                            sourceInfoBlock.InfoReadAt = newOU.InfoReadAt;

                            // Save block to the page
                            newPage.SourceInfo = sourceInfoBlock;

                            // Save the page
                            contentRepository.Save(newPage, SaveAction.Save);
                        }
                    }
                }
            }
        }

        void Instance_SavingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is CategoryPage)
            {
                var savingPage = e.Content as CategoryPage;

                // get current saved page version
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                var page = contentRepository.Get<CategoryPage>(e.ContentLink);

                if (page.Name != savingPage.Name)
                {
                    // renaming name of the page, do the same for the category
                    string oldName = page.Name;
                    string newName = savingPage.Name;

                    var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                    var category = CategoryHelper.FindCompareCategory(categoryRepository, oldName);
                    if (category != null)
                    {
                        category = category.CreateWritableClone();
                        category.Name = newName;
                        categoryRepository.Save(category);
                    }
                    else
                    {
                        CategoryHelper.SaveCompareCategory(categoryRepository, newName, newName);
                    }
                }
            }
        }

        void Instance_CreatingPage(object sender, PageEventArgs e)
        {
            if (IsImport() || e.Content == null)
            {
                return;
            }

            if (string.Equals(e.Page.PageTypeName, typeof(CompareStartPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {

            }
            else if (string.Equals(e.Page.PageTypeName, typeof(CategoryPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                var category = CategoryHelper.FindCompareCategory(categoryRepository, e.Page.Name);
                if (category == null)
                {
                    CategoryHelper.SaveCompareCategory(categoryRepository, e.Page.Name, e.Page.Name);
                }
            }
            else if (string.Equals(e.Page.PageTypeName, typeof(OrganisationalUnitPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
                var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();

                var newOrganisationalUnitName = e.Page.Name;

                PageData ancestorPage = contentRepository.Get<PageData>(e.Page.ParentLink);
                string categoryName = null;
                if (ancestorPage is CategoryPage)
                {
                    categoryName = ancestorPage.Name;
                    bool existingOuPage = contentRepository.GetChildren<OrganisationalUnitPage>(ancestorPage.ContentLink, LanguageSelector.AutoDetect(true)).Where(x => x.Name.ToLower() == newOrganisationalUnitName.ToLower()).Any();
                    if (!existingOuPage)
                    {
                        if (!string.IsNullOrWhiteSpace(categoryName))
                        {
                            var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                            Category category = CategoryHelper.FindCompareCategory(categoryRepository, categoryName);
                            if (category == null)
                            {
                                category = CategoryHelper.SaveCompareCategory(categoryRepository, categoryName, categoryName);
                            }

                            // add category to the ou page
                            e.Page.Category.Add(category.ID);

                        }
                    }
                    else
                    {
                        // cancel, ou page already exists in this comparison content
                        e.CancelReason = "An organisational unit with the name " + e.Page.Name + " already exists. Please choose another name or edit the existing one.";
                        e.CancelAction = true;
                    }
                }
            }
        }

        public void Preload(string[] parameters) { }
    }
}
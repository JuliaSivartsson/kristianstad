using System;
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
using Kristianstad.Models.Pages.Compare;
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
        private readonly Injected<IContentLoader> _contentLoader;

        public void Initialize(InitializationEngine context)
        {
            DataFactory.Instance.CreatingPage += Instance_CreatingPage;
            DataFactory.Instance.SavingContent += Instance_SavingContent;
            DataFactory.Instance.SavedContent += Instance_SavedContent;
            DataFactory.Instance.CreatedPage += Instance_CreatedPage;
            DataFactory.Instance.PublishingPage += Instance_PublishingPage;
        }

        public void Uninitialize(InitializationEngine context)
        {
            DataFactory.Instance.CreatingPage -= Instance_CreatingPage;
            DataFactory.Instance.SavingContent -= Instance_SavingContent;
            DataFactory.Instance.SavedContent -= Instance_SavedContent;
            DataFactory.Instance.CreatedPage += Instance_CreatedPage;
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
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                PageData ancestorPage = contentRepository.Get<PageData>(e.ContentLink);
                var test = contentRepository.GetChildren<CategoryPage>(ancestorPage.ContentLink, LanguageSelector.AutoDetect(true));
                /*
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
                    var organisationalUnitsToAdd = organisationalUnits.Where(x => addedOrganisationalUnitWebServiceInfo.Any(x2 => x.SourceName == x2.SourceName && x.SourceId == x2.SourceId));

                    // Create new pages
                    foreach (var newOU in organisationalUnitsToAdd)
                    {
                        var existingPage = ouPagesInCategory.Where(x => x.Name.ToLower() == newOU.Title.ToLower()).FirstOrDefault();
                        if (existingPage != null)
                        {
                            // e.CancelReason = "A organisational unit page with the name \"" + existingPage.Name + "\" already exists.";
                            // e.CancelAction = true;
                            existingPage = existingPage;
                        }
                        else
                        {
                            // Create a new organisational unit page
                            var newPage = contentRepository.GetDefault<OrganisationalUnitPage>(ContentReference.GlobalBlockFolder);
                            newPage.Name = newOU.Title;
                            newPage.MenuTitle = newOU.Title;
                            newPage.MenuDescription = newOU.Title;

                            // Add a source info block
                            var sourceInfoBlock = contentRepository.GetDefault<SourceInfoBlock>(ContentReference.GlobalBlockFolder);
                            sourceInfoBlock.Name = newOU.Title;
                            sourceInfoBlock.SourceName = newOU.SourceName;
                            sourceInfoBlock.SourceId = newOU.SourceId;
                            sourceInfoBlock.InfoReadAt = newOU.InfoReadAt;

                            // Save block to the page
                            newPage.SourceInfo = sourceInfoBlock;

                            // Save the page
                            contentRepository.Save(newPage, SaveAction.Save);
                        }
                    }
                }
                */
            }
        }

        void Instance_SavingContent(object sender, ContentEventArgs e)
        {
            if (e.Content is CategoryPage)
            {
                /*
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
                */
            }
        }

        void Instance_CreatedPage(object sender, PageEventArgs e)
        {
            if (string.Equals(e.Page.PageTypeName, typeof(CategoryPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                CreateCompareResultPage(contentRepository, e.ContentLink);
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
                // Empty
            }
            if (string.Equals(e.Page.PageTypeName, typeof(CompareResultPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                PageData ancestorPage = contentRepository.Get<PageData>(e.Page.ParentLink);
                List<CompareResultPage> test = contentRepository.GetChildren<CompareResultPage>(ancestorPage.ContentLink, LanguageSelector.AutoDetect(true)).ToList<CompareResultPage>();
                if (test.Count > 0)
                {
                    e.CancelAction = true;
                    e.CancelReason = "There is already a CompareResultPage linked to this CategoryPage.";
                }
            }
            else if (string.Equals(e.Page.PageTypeName, typeof(CategoryPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                //var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                //Category category = CategoryHelper.FindCompareCategory(categoryRepository, e.Page.Name);

                //if (category == null)
                //{
                //    Category newCategory = CategoryHelper.SaveCompareCategory(categoryRepository, e.Page.Name, e.Page.Name);
                //}

                //var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                //CreateCompareResultPage(contentRepository, e.ContentLink);

                //var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                //var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
                //var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();

                //var newCategoryName = e.Page.Name;

                //// Group Category Page
                //PageData ancestorPage = contentRepository.Get<PageData>(e.Page.ParentLink);
                //string groupCategoryName = null;
                //if (ancestorPage is GroupCategoryPage)
                //{
                //    groupCategoryName = ancestorPage.Name;
                //    bool existingOuPage = contentRepository.GetChildren<CategoryPage>(ancestorPage.ContentLink, LanguageSelector.AutoDetect(true)).Where(x => x.Name.ToLower() == newCategoryName.ToLower()).Any();
                //    if (!existingOuPage)
                //    {
                //        if (!string.IsNullOrWhiteSpace(newCategoryName))
                //        {
                //            var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                //            Category category = CategoryHelper.FindCompareCategory(categoryRepository, newCategoryName);
                //            if (category == null)
                //            {
                //                //category = CategoryHelper.SaveCompareCategory(categoryRepository, newCategoryName, newCategoryName);

                //                CategoryPage categoryPage = contentRepository.GetChildren<CategoryPage>(ancestorPage.ContentLink).Last();

                //                //var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
                //                //var contentReference = new ContentReference(category.ID);
                //                //CategoryPage cp = repository.Get<CategoryPage>(contentReference);

                //                //CreateCompareResultPage(contentRepository, cp.ContentLink);
                //            }

                //            // add category to the ou page
                //            //e.Page.Category.Add(category.ID);

                //        }
                //    }
                //    else
                //    {
                //        // cancel, ou page already exists in this comparison content
                //        e.CancelReason = "An organisational unit with the name " + e.Page.Name + " already exists. Please choose another name or edit the existing one.";
                //        e.CancelAction = true;
                //    }
                //}

            }
            /*
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
            */
        }

        void Instance_PublishingPage(object sender, PageEventArgs e)
        {
            if (string.Equals(e.Page.PageTypeName, typeof(OrganisationalUnitPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                PageData ancestorPage = contentRepository.Get<PageData>(e.Page.ParentLink);
                List<OrganisationalUnitPage> organisationalUnits = contentRepository.GetChildren<OrganisationalUnitPage>(ancestorPage.ContentLink, LanguageSelector.AutoDetect(true)).ToList<OrganisationalUnitPage>();

                foreach (OrganisationalUnitPage ouPage in organisationalUnits)
                {
                    if (ouPage.Name == e.Page.Name && ouPage.ContentLink.ID != e.Page.ContentLink.ID)
                    {
                        e.CancelAction = true;
                        e.CancelReason = "There is already a OrganisationalUnitPage named '" + e.Page.Name + "'.";
                    }
                }
            }
        }

        private void CreateCompareResultPage(IContentRepository contentRepository, ContentReference parentContentLink)
        {
            //CompareResultPage newPage = new CompareResultPage();
            var newPage = contentRepository.GetDefault<CompareResultPage>(parentContentLink);
            newPage.Name = "Jämför";
            newPage.MenuTitle = "Jämför";
            newPage.MenuDescription = "Jämför";
            //newPage.ParentLink.ID = parentContentLink.ID;

            // Save the page
            contentRepository.Save(newPage, SaveAction.Save);
        }

        public void Preload(string[] parameters) { }
    }
}
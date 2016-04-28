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
using Kristianstad.Models.Pages;
using EPiServer.DataAbstraction.RuntimeModel;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiServer.Globalization;
using Kristianstad.CompareDomain.Abstract;
using Compare;

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
            //DataFactory.Instance.CreatingContent += Instance_CreatingContent;

            DataFactory.Instance.SavingContent += Instance_SavingContent;


            DataFactory.Instance.CreatedContent += Instance_CreatedContent;
            
            /*
            var partialRouter = new BlogPartialRouter();

            RouteTable.Routes.RegisterPartialRouter<BlogStartPage, Category>(partialRouter);
            */


            //context.Locate.Advanced.GetInstance<SingleModelRegister<OrganisationalUnitBlock>>().RegisterType();
        }

        void Instance_PublishingPage(object sender, PageEventArgs e)
        {

        }

        void Instance_CreatedPage(object sender, PageEventArgs e)
        {

        }

        //Returns if we are doing an import or mirroring
        public bool IsImport()
        {
            return ContextCache.Current["CurrentITransferContext"] != null;
        }

        void Instance_CreatedContent(object sender, ContentEventArgs e)
        {
            if (e.Content is OrganisationalUnitPage)
            {
                // Create a contact
                var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();

                /*
                // ContactBlock contact = contentRepository.Get<ContactBlock>(new ContentReference(123));
                ContentAssetFolder assetFolder = contentAssetHelper.GetOrCreateAssetFolder(e.ContentLink);
                ContactBlock newContact = contentRepository.GetDefault<ContactBlock>(assetFolder.ContentLink);
                newContact.Email = "test@test.com";
                newContact.Name = e.Content.Name + " Svensson";
                contentRepository.Save(newContact, SaveAction.Publish, AccessLevel.NoAccess);
                */

                var compareStartPage = contentRepository.GetAncestors(e.Content.ParentLink).Where(x => x is CompareStartPage).FirstOrDefault();
                if (compareStartPage != null)
                {
                    /*
                    ContentAssetFolder compareAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(compareStartPage.ContentLink);
                    var newOUBlock = contentRepository.GetDefault<OrganisationalUnitBlock>(compareAssetFolder.ContentLink); //, blockType.ID, ContentLanguage.PreferredCulture);
                    newOUBlock.Name = e.Content.Name;
                    newOUBlock.Body = e.Content.Name;
                    contentRepository.Save(newOUBlock, SaveAction.Publish, AccessLevel.NoAccess);
                    
                    var page = e.Content as OrganisationalUnitPage;
                    var clonedPage = (OrganisationalUnitPage)page.CreateWritableClone();
                    //var ouPage = e.Content as OrganisationalUnitPage;
                    clonedPage.OrganisationalUnitBlock = newOUBlock.ContentLink;
                    contentRepository.Save(clonedPage, SaveAction.Publish);
                    */

                    /*
                    //OrganisationalUnitBlock ouBlock = contentRepository.Get<OrganisationalUnitBlock>(new ContentReference(123));
                    ContentAssetFolder compareAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(e.Page.ContentLink);
                    OrganisationalUnitBlock ouBlock = contentRepository.GetDefault<OrganisationalUnitBlock>(assetFolder.ContentLink);

                    //ouBlock.Rating = 5;
                    //ouBlock.Text = "This is such a nice product";
                    //ouBlock.UserDisplayName = "Arve Systad";
                    ouBlock.Name = newOrganisationalUnitName; // ouBlock.UserDisplayName + "(" + DateTime.Now.ToShortDateString() + ")";
                    contentRepository.Save(ouBlock, SaveAction.Publish, AccessLevel.FullAccess);
                     */

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
                    //renaming name of the page!
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

                if (savingPage.NewOrganisationalUnits != page.NewOrganisationalUnits)
                {
                    // Create new pages now (if needed)
                    
                // IService compareService = ServiceFactory.Instance;
                    // compareService.
                }
            }
        }

        void Instance_CreatingPage(object sender, PageEventArgs e)
        {
            if (this.IsImport() || e.Content == null)
            {
                return;
            }
            if (string.Equals(e.Page.PageTypeName, typeof(CompareStartPage).GetPageType().Name, StringComparison.OrdinalIgnoreCase))
            {
                //var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
                //CreateOrganisationalUnitsPage(contentRepository, e.Page.ContentLink);

                //var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
                //var contentAssetFolder = contentAssetHelper.GetOrCreateAssetFolder(e.Page.ContentLink);
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
                    //ancestorPage = contentRepository.Get<PageData>(ancestorPage.ParentLink);

                    bool existingOuPage = contentRepository.GetChildren<OrganisationalUnitPage>(ancestorPage.ContentLink, LanguageSelector.AutoDetect(true)).Where(x => x.Name.ToLower() == newOrganisationalUnitName.ToLower()).Any();
                    if (!existingOuPage)
                    {
                        //e.Page.ParentLink = GetOrganisationalUnitsPageRef(ancestorPage, contentRepository);

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

                /*if (ancestorPage is CompareStartPage)
                {
                    /*
                    //var blockType = contentTypeRepository.Load<OrganisationalUnitBlock>();
                    var block = contentRepository.GetDefault<OrganisationalUnitBlock>(ContentReference.GlobalBlockFolder); //, blockType.ID, ContentLanguage.PreferredCulture);
                    block.Name = newOrganisationalUnitName;
                    contentRepository.Save(block, SaveAction.Publish);
                    
                    //OrganisationalUnitBlock ouBlock = contentRepository.Get<OrganisationalUnitBlock>(new ContentReference(123));
                    ContentAssetFolder assetFolder = contentAssetHelper.GetOrCreateAssetFolder(e.Page.ContentLink);
                    OrganisationalUnitBlock ouBlock = contentRepository.GetDefault<OrganisationalUnitBlock>(assetFolder.ContentLink);

                    //ouBlock.Rating = 5;
                    //ouBlock.Text = "This is such a nice product";
                    //ouBlock.UserDisplayName = "Arve Systad";
                    ouBlock.Name = newOrganisationalUnitName; // ouBlock.UserDisplayName + "(" + DateTime.Now.ToShortDateString() + ")";
                    contentRepository.Save(ouBlock, SaveAction.Publish, AccessLevel.FullAccess);
                    
                    
                    var ouPage = e.Page as OrganisationalUnitPage;
                    ouPage.OrganisationalUnitBlock = block.ContentLink;
                }*/



                    /*var organisationalUnitFolderPage = GetOrganisationalUnitsPageRef(ancestorPage, contentRepository);
                    if (organisationalUnitFolderPage != null)
                    {
                        bool existingOuPage = contentRepository.GetChildren<OrganisationalUnitPage>(organisationalUnitFolderPage).Where(x => x.Name.ToLower() == newOrganisationalUnitName.ToLower()).Any();
                        if (!existingOuPage)
                        {
                            e.Page.ParentLink = GetOrganisationalUnitsPageRef(ancestorPage, contentRepository);

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
                    else
                    {
                        // cancel, ou page folder does not exist (and could not be created for some reason)
                        e.CancelReason = "Could not find or create an organisational unit folder within the compare service";
                        e.CancelAction = true;
                    }*/
                /*}
                else
                {
                    // cancel, no compare start page
                    e.CancelReason = "Could not find the compare start page";
                    e.CancelAction = true;
                }*/
            }
        }



        // in here we know that the page is a compare start page and now we must create the organisational unit page unless already created
        private static PageReference GetOrganisationalUnitsPageRef(PageData compareStart, IContentRepository contentRepository)
        {
            foreach (var childPage in contentRepository.GetChildren<PageData>(compareStart.ContentLink))
            {
                if (childPage is OrganisationalUnitFolderPage) // current.Name == ORGANISATIONAL_UNIT_FOLDER_NAME)
                {
                    return childPage.PageLink;
                }
            }

            return CreateOrganisationalUnitFolderPage(contentRepository, compareStart.ContentLink);
        }

        private static PageReference CreateOrganisationalUnitFolderPage(IContentRepository contentRepository, ContentReference parent)
        {
            OrganisationalUnitFolderPage defaultPageData = contentRepository.GetDefault<OrganisationalUnitFolderPage>(parent, typeof(OrganisationalUnitFolderPage).GetPageType().ID, LanguageSelector.AutoDetect().Language);
            defaultPageData.PageName = ORGANISATIONAL_UNIT_FOLDER_NAME;
            defaultPageData.URLSegment = UrlSegment.CreateUrlSegment(defaultPageData);
            defaultPageData.MenuDescription = "Containing all the organisational units for the compare service";
            defaultPageData.MenuTitle = "Organisational units for the compare service";
            defaultPageData.VisibleInMenu = false;
            return contentRepository.Save(defaultPageData, SaveAction.Publish, AccessLevel.Publish).ToPageReference();
        }

        /*
        // in here we know that the page is a blog start page and now we must create the date pages unless they are already created
        public PageReference GetDatePageRef(PageData compareStart, DateTime published, IContentRepository contentRepository)
        {

            foreach (var current in contentRepository.GetChildren<PageData>(compareStart.ContentLink))
            {
                if (current.Name == published.Year.ToString())
                {
                    PageReference result;
                    foreach (PageData current2 in contentRepository.GetChildren<PageData>(current.ContentLink))
                    {
                        if (current2.Name == published.Month.ToString())
                        {
                            result = current2.PageLink;
                            return result;
                        }
                    }
                    result = CreateDatePage(contentRepository, current.PageLink, published.Month.ToString(), new DateTime(published.Year, published.Month, 1));
                    return result;
            
                }
            }
            PageReference parent = CreateDatePage(contentRepository, compareStart.ContentLink, published.Year.ToString(), new DateTime(published.Year, 1, 1));
            return CreateDatePage(contentRepository, parent, published.Month.ToString(), new DateTime(published.Year, published.Month, 1));     
        }

        private PageReference CreateDatePage(IContentRepository contentRepository, ContentReference parent, string name, DateTime startPublish)
        {
            BlogListPage defaultPageData = contentRepository.GetDefault<BlogListPage>(parent, typeof(BlogListPage).GetPageType().ID, LanguageSelector.AutoDetect().Language);
            defaultPageData.PageName = name;
            defaultPageData.Heading = name;
            defaultPageData.StartPublish = startPublish;
            defaultPageData.URLSegment = UrlSegment.CreateUrlSegment(defaultPageData);
            return contentRepository.Save(defaultPageData, SaveAction.Publish, AccessLevel.Publish).ToPageReference();
        }
        */

        public void Preload(string[] parameters) { }

        public void Uninitialize(InitializationEngine context)
        {
            DataFactory.Instance.CreatingPage -= Instance_CreatingPage;
            DataFactory.Instance.SavingContent -= Instance_SavingContent;

        }
    }
}
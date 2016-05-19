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
            DataFactory.Instance.CreatedPage -= Instance_CreatedPage;
            DataFactory.Instance.PublishingPage -= Instance_PublishingPage;
        }

        // Returns if we are doing an import or mirroring
        public bool IsImport()
        {
            return ContextCache.Current["CurrentITransferContext"] != null;
        }

        void Instance_SavedContent(object sender, ContentEventArgs e)
        {

        }

        void Instance_SavingContent(object sender, ContentEventArgs e)
        {

        }

        void Instance_CreatedPage(object sender, PageEventArgs e)
        {
            if (e.Page is CategoryPage)
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

            /*
            if (e.Page is CompareResultPage)
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
            */
        }

        void Instance_PublishingPage(object sender, PageEventArgs e)
        {
            if (e.Page is OrganisationalUnitPage)
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
            var newPage = contentRepository.GetDefault<CompareResultPage>(parentContentLink);
            newPage.Name = "Jämför";
            newPage.MenuTitle = "Jämför";
            newPage.MenuDescription = "Jämför";

            // Save the page
            contentRepository.Save(newPage, SaveAction.Save);
        }

        public void Preload(string[] parameters) { }
    }
}
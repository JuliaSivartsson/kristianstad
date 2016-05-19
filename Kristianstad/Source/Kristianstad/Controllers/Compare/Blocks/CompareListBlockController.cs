using System;
using System.Web.Mvc;
using EPiCore.Controllers;
using EPiCore.ViewModels.Blocks;
using EPiServer;
using Kristianstad.Business.Models.Blocks;
using EPiServer.Core;
using Kristianstad.Models.Pages;
using System.Collections.Generic;
using EPiServer.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using EPiServer.Web.Routing;
using Kristianstad.Business.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages.Compare;
using Kristianstad.HtmlHelpers;

namespace Kristianstad.Controllers.Compare
{
    public class CompareListBlockController : BaseBlockController<CompareListBlock>
    {
        private const string CookieName = "compare";
        private readonly Injected<IContentLoader> contentLoader;
        private CookieHelper cookieHelper;

        public CompareListBlockController()
        {
            cookieHelper = new CookieHelper();
        }

        public override ActionResult Index(CompareListBlock currentBlock)
        {
            CompareListModel model = CreateModel(currentBlock);
            return PartialView(model);
        }
        
        private IEnumerable<PageData> FindOrganisationalUnits(CompareListBlock currentBlock)
        {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? contentLoader.Service.Get<PageData>(ContentReference.StartPage);
            PageReference listRoot = currentPage.PageLink;

            if (currentPage is OrganisationalUnitPage)
            {
                pages = contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ParentLink);
            }
            else if (currentPage is CategoryPage)
            {
                pages = contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink).OfType<OrganisationalUnitPage>();
            }

            return pages ?? new List<PageData>();
        }

        private CompareListModel CreateModel(CompareListBlock currentBlock)
        {
            CompareListModel model = new CompareListModel()
            {
                Header = currentBlock.Header
            };

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page;
            if (currentPage != null)
            {
                model.CurrentUrl = CompareHelper.GetExternalUrl(currentPage.ContentLink);

                var compareResultPage = currentBlock.CompareResultPage;
                if (compareResultPage == null || string.IsNullOrWhiteSpace(model.Header))
                {
                    // try to get from parent page
                    var parentPage = contentLoader.Service.Get<PageData>(currentPage.ParentLink);
                    if (parentPage != null && parentPage is CategoryPage)
                    {
                        var categoryPage = parentPage as CategoryPage;
                        if (compareResultPage == null)
                        {
                            // set compare result page from category compare list block
                            compareResultPage = categoryPage.CompareListBlock.CompareResultPage;
                        }

                        if (string.IsNullOrWhiteSpace(model.Header) && categoryPage.CompareListBlock != null)
                        {
                            // copy header from category compare list block
                            model.Header = categoryPage.CompareListBlock.Header;
                        }
                    }
                }

                if (compareResultPage != null)
                {
                    model.ComparePageId = compareResultPage.ID;
                    model.OrganisationalUnits = cookieHelper.GetOrganisationalUnitsInCompare(contentLoader.Service, compareResultPage);
                    model.ComparePageUrl = CompareHelper.GetExternalUrl(compareResultPage);
                }
            }

            return model;
        }

    }
}

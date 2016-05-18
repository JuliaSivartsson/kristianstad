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
        private readonly Injected<IContentLoader> _contentLoader;
        private CookieHelper _cookieHelper;

        public CompareListBlockController()
        {
            _cookieHelper = new CookieHelper();
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
            PageData currentPage = pageRouteHelper.Page ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);
            PageReference listRoot = currentPage.PageLink;

            if (currentPage.PageTypeName == typeof(OrganisationalUnitPage).GetPageType().Name)
            {
                pages = _contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ParentLink);
            }

            if (currentPage.PageTypeName == typeof(CategoryPage).GetPageType().Name)
            {
                pages = _contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ContentLink);
            }

            return pages ?? new List<PageData>();
        }

        private int GetCategoryId(CompareListBlock currentBlock)
        {
            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? _contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            if (currentPage.PageTypeName == typeof(OrganisationalUnitPage).GetPageType().Name)
            {
                return currentPage.ParentLink.ID;
            }

            if (currentPage.PageTypeName == typeof(CategoryPage).GetPageType().Name)
            {
                return currentPage.ContentLink.ID;
            }

            return currentPage.ParentLink.ID;
        }

        private CompareListModel CreateModel(CompareListBlock currentBlock)
        {
            CompareListModel model = new CompareListModel();
            model.CategoryId = GetCategoryId(currentBlock);
            model.CurrentLink = CompareHelper.GetExternalUrl(CompareHelper.GetCurrentPage());

            var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var contentReference = new ContentReference(model.CategoryId);
            model.ClearLink = CompareHelper.GetExternalUrl(repository.Get<CategoryPage>(contentReference));

            // Gets link to CompareResultPage, if there is one.
            if (repository.GetChildren<CompareResultPage>(contentReference).ToList().Count > 0)
            {
                CompareResultPage cPage = repository.GetChildren<CompareResultPage>(contentReference).ToList().First();
                model.CompareLink = CompareHelper.GetExternalUrl(cPage);
            }

            // Gets Organisational Units from Cookies and adds them to the ViewModel.
            foreach (int ou in _cookieHelper.GetCookie(model.CategoryId))
            {
                PageData page = FindOrganisationalUnits(currentBlock).ToList().Where(o => o.ContentLink.ID == ou).First();
                if (page != null)
                {
                    CompareModel cm = new CompareModel
                    {
                        Name = page.Name,
                        ID = page.ContentLink.ID,
                        URL = CompareHelper.GetExternalUrl(page)
                    };
                    model.OrganisationalUnits.Add(cm);
                }
            }

            return model;
        }
    }
}

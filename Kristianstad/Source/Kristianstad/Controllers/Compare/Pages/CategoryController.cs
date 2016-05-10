using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Search;
using EPiServer.Web;
using EPiServer.Web.Hosting;
using EPiServer.Web.Mvc.Html;
using EPiServer.DataAbstraction;
using System;
using System.Text;
using System.Text.RegularExpressions;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.Web.Mvc;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages.Compare;
using EPiServer.Editor;
using Kristianstad.Business.Models.Blocks.Compare;
using EPiServer;
using Kristianstad.CompareDomain;
using Kristianstad.CompareDomain.Models;
using EPiServer.DataAccess;
using Kristianstad.Business.Compare;
using Newtonsoft.Json.Linq;

namespace Kristianstad.Controllers.Compare
{
    public class CategoryController : PageController<CategoryPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;
        private const string CookieName = "compare";

        public ActionResult Index(CategoryPage currentPage, bool doFullRefresh = false, string address = null)
        {
            var model = new CategoryPageModel(currentPage);

            // Get existing organisational units (by child pages)
            var existingOUPages = _contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();

            // Get organisational unit info from web service(s)
            var organisationalUnits = CompareServiceFactory.Instance.GetWebServicesOrganisationalUnits();

            // Set organisational units from sources to view model
            model.AddOrganisationalUnits.OrganisationalUnitsSources = organisationalUnits.Select(s => new OrganisationalUnitsSourceModel()
            {
                SourceName = s.Key,
                OrganisationalUnits = s.Value.Select(o => new OrganisationalUnitModel()
                {
                    Name = o.Name,
                    Title = o.Name,
                    SourceName = o.SourceName,
                    SourceId = o.SourceId,
                    InfoReadAt = o.InfoReadAt,
                    Use = existingOUPages != null && existingOUPages.Any(eou => eou.SourceInfo.SourceName == o.SourceName && eou.SourceInfo.SourceId == o.SourceId),
                    UseBefore = existingOUPages != null && existingOUPages.Any(eou => eou.SourceInfo.SourceName == o.SourceName && eou.SourceInfo.SourceId == o.SourceId),
                    NameAlreadyExistsInCategory = existingOUPages != null && existingOUPages.Any(eou => eou.Name.ToLower() == o.Name.ToLower())
                }).ToList()
            }).ToList();

            model.ListOfExistingOU = _contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();
            model.DistanceList = new DistanceFromAddressModel()
            {
                MeasureFromAddress = address
            };

            ViewData.Add("cookies", GetCookie(CookieName + GetCategoryPageId(currentPage)));

            /*
            var testList = new OrganisationUnitListTestModel();
            testList.MeasureFromAddress = address;
            */

            /*
            // Add edit hints to refresh when organisational units have changed //TODO: Doesn't work
            var editingHints = ViewData.GetEditHints<CategoryPageModel, CategoryPage>();
            editingHints.AddFullRefreshFor(p => p.DoFullRefresh);
            */

            return View(model);
        }

        private List<int> GetCookie(string cookieName)
        {
            JArray cookie;
            try
            {
                cookie = JArray.Parse(Request.Cookies[cookieName].Value);
            }
            catch
            {
                cookie = new JArray();
            }

            return cookie.Select(o => (int)o).ToList();
        }

        private int GetCategoryPageId(CategoryPage currentBlock)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOrganisationalUnits(CategoryPage currentPage, AddOrganisationalUnitsFormModel addOrganisationalUnits)
        {
            bool anyChanges = false;

            // Get existing OU pages to check against later
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var existingOUPages = contentRepository.GetChildren<PageData>(currentPage.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();

            // Loop all the organisational units
            if (addOrganisationalUnits != null)
            {
                foreach (var source in addOrganisationalUnits.OrganisationalUnitsSources)
                {
                    foreach (var ouToAdd in source.OrganisationalUnits)
                    {
                        if (ouToAdd.Use != ouToAdd.UseBefore && ouToAdd.Use == true)
                        {
                            // Check if already exists a page with this title
                            var existingPage = existingOUPages.Where(x => x.Name.ToLower() == ouToAdd.Title.ToLower()).FirstOrDefault();
                            if (existingPage != null)
                            {
                                // e.CancelReason = "A organisational unit page with the name \"" + existingPage.Name + "\" already exists.";
                                // e.CancelAction = true;
                                existingPage = existingPage;
                            }
                            else
                            {
                                CreateNewOrganisationalUnit(contentRepository, currentPage.ContentLink, ouToAdd.Title, ouToAdd);
                                anyChanges = true;
                            }
                        }
                    }
                }

                if (addOrganisationalUnits.Custom.Use && !string.IsNullOrWhiteSpace(addOrganisationalUnits.Custom.Title))
                {
                    // Add a new custom organisational unit
                    string sourceName = CompareServiceFactory.Instance.GetCustomSourceName();
                    string sourceId = Guid.NewGuid().ToString();

                    CreateNewOrganisationalUnit(contentRepository, currentPage.ContentLink, addOrganisationalUnits.Custom.Title, new SourceInfoModel() { SourceName = sourceName, SourceId = sourceId, InfoReadAt = DateTime.Now });
                }
            }

            return RedirectToAction("Index");
        }

        private void CreateNewOrganisationalUnit(IContentRepository contentRepository, ContentReference parentContentLink, string title, SourceInfoModel sourceInfo)
        {
            var newPage = contentRepository.GetDefault<OrganisationalUnitPage>(parentContentLink);
            newPage.Name = title;
            newPage.MenuTitle = title;
            newPage.MenuDescription = title;

            // Set source info
            newPage.SourceInfo.Name = sourceInfo.Name;
            newPage.SourceInfo.SourceName = sourceInfo.SourceName;
            newPage.SourceInfo.SourceId = sourceInfo.SourceId;
            newPage.SourceInfo.InfoReadAt = sourceInfo.InfoReadAt;

            // Save the page
            contentRepository.Save(newPage, SaveAction.Save);
        }
    }
}

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

namespace Kristianstad.Controllers.Compare
{
    public class CategoryController : PageController<CategoryPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;

        public ActionResult Index(CategoryPage currentPage, bool doFullRefresh = false)
        {
            var model = new CategoryPageModel(currentPage);

            // Get existing organisational units (by child pages)
            var existingOUPages = _contentLoader.Service.GetChildren<PageData>(currentPage.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();

            // Get organisational unit info from web service(s)
            List<OrganisationalUnit> organisationalUnits = CompareServiceFactory.Instance.GetWebServiceOrganisationalUnits();

            // Set organisational units from sources to view model
            model.OrganisationalUnitsFromSources = organisationalUnits.Select(o => new OrganisationalUnitModel()
            {
                Name = o.Title,
                Title = o.Title,
                SourceName = o.SourceName,
                SourceId = o.SourceId,
                InfoReadAt = o.InfoReadAt,
                Use = existingOUPages != null && existingOUPages.Any(eou => eou.SourceInfo.SourceName == o.SourceName && eou.SourceInfo.SourceId == o.SourceId),
                UseBefore = existingOUPages != null && existingOUPages.Any(eou => eou.SourceInfo.SourceName == o.SourceName && eou.SourceInfo.SourceId == o.SourceId),
                NameAlreadyExistsInCategory = existingOUPages != null && existingOUPages.Any(eou => eou.Name.ToLower() == o.Title.ToLower())
            }).ToList();

            /*
            // Add edit hints to refresh when organisational units have changed //TODO: Doesn't work
            var editingHints = ViewData.GetEditHints<CategoryPageModel, CategoryPage>();
            editingHints.AddFullRefreshFor(p => p.DoFullRefresh);
            */

            return View(model);
        }

        //public ActionResult Index(CategoryPage currentPage)
        //{
        //    var model = new CategoryPageModel(currentPage); //PageViewModel.Create(currentPage);
            
        //    //Connect the view models logotype property to the start page's to make it editable
        //    var editHints = ViewData.GetEditHints<CategoryPage, CategoryPage>();
        //    editHints.AddConnection(m => m.Category, p => p.Category);
        //    editHints.AddConnection(m => m.StartPublish, p => p.StartPublish);

        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOrganisationalUnits(CategoryPage currentPage, List<OrganisationalUnitModel> organisationalUnitsFromSources)
        {
            bool anyChanges = false;

            // Get existing OU pages to check against later
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var existingOUPages = contentRepository.GetChildren<PageData>(currentPage.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();

            // Loop all the organisational units
            if (organisationalUnitsFromSources != null)
            {
                foreach (var ouToAdd in organisationalUnitsFromSources)
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
                            // Create a new organisational unit page
                            var newPage = contentRepository.GetDefault<OrganisationalUnitPage>(currentPage.ContentLink);
                            newPage.Name = ouToAdd.Title;
                            newPage.MenuTitle = ouToAdd.Title;
                            newPage.MenuDescription = ouToAdd.Title;

                            // Set source info
                            newPage.SourceInfo.Name = ouToAdd.Title;
                            newPage.SourceInfo.SourceName = ouToAdd.SourceName;
                            newPage.SourceInfo.SourceId = ouToAdd.SourceId;
                            newPage.SourceInfo.InfoReadAt = ouToAdd.InfoReadAt;

                            // Save the page
                            contentRepository.Save(newPage, SaveAction.Save);

                            anyChanges = true;
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}

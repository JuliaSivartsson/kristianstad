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
using EPiServer.DataAccess;
using Kristianstad.Business.Compare;
using Kristianstad.Business.Models.Blocks;
using Kristianstad.HtmlHelpers;

namespace Kristianstad.Controllers.Compare
{
    public class CompareResultPageController : PageController<CompareResultPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;
        private CookieHelper _cookieHelper;

        public CompareResultPageController()
        {
            _cookieHelper = new CookieHelper();
        }

        public ActionResult Index(CompareResultPage currentPage)
        {
            var model = new CompareResultPageModel(currentPage);

            // Get existing queries in category page (if found)
            List<ResultQueryBlock> existingQueries = GetExistingQueryBlocks(currentPage);

            // Get all web service queries
            var webServiceQueries = CompareServiceFactory.Instance.GetWebServicePropertyQueries();

            // Set queries to view model
            foreach (var source in webServiceQueries)
            {
                model.ResultQueryGroupsFromSources.AddRange(source.Value.Select(g => new ResultQueryGroupModel()
                {
                    Name = g.Name,
                    SourceName = g.SourceName,
                    SourceId = g.SourceId,
                    InfoReadAt = g.InfoReadAt,
                    ResultQueries = g.Queries.Select(q => new ResultQueryModel()
                    {
                        SourceName = q.SourceName,
                        SourceId = q.SourceId,
                        Name = q.Name,
                        InfoReadAt = q.InfoReadAt,
                        Use = existingQueries != null && existingQueries.Any(eq => eq.SourceInfo.SourceName == q.SourceName && eq.SourceInfo.SourceId == q.SourceId),
                        UseBefore = existingQueries != null && existingQueries.Any(eq => eq.SourceInfo.SourceName == q.SourceName && eq.SourceInfo.SourceId == q.SourceId)
                    }).ToList()
                }).ToList());
            }

            model.OrganisationalUnits = GetOrganisationalUnits(currentPage);

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveResultQueries(CompareResultPage currentPage, List<ResultQueryGroupModel> resultQueryGroupsFromSources)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
            ContentAssetFolder folder = contentAssetHelper.GetOrCreateAssetFolder(currentPage.ContentLink);

            bool anyChanges = false;

            // Create writable clone of the page to be able to update it
            var writablePage = (CompareResultPage)currentPage.CreateWritableClone();

            if (resultQueryGroupsFromSources != null)
            {
                foreach (var group in resultQueryGroupsFromSources)
                {
                    foreach (var query in group.ResultQueries)
                    {
                        if (query.Use != query.UseBefore)
                        {
                            if (query.Use)
                            {
                                anyChanges = true;

                                // Add a result query block..
                                var block = contentRepository.GetDefault<ResultQueryBlock>(folder.ContentLink);
                                block.Title = query.Name;

                                // .. with a source info block
                                block.SourceInfo.Name = query.Name;
                                block.SourceInfo.SourceName = query.SourceName;
                                block.SourceInfo.SourceId = query.SourceId;
                                block.SourceInfo.InfoReadAt = query.InfoReadAt;

                                // Save the block
                                var contentBlock = (IContent)block;
                                contentBlock.Name = query.Name;
                                contentRepository.Save(contentBlock, SaveAction.Publish);

                                // Make sure the page queries content area is created
                                if (writablePage.ResultQueries == null)
                                {
                                    writablePage.ResultQueries = new ContentArea();
                                }

                                // Add the new block to the page queries content area
                                writablePage.ResultQueries.Items.Add(new ContentAreaItem
                                {
                                    ContentLink = ((IContent)block).ContentLink
                                });
                            }
                            else if (!query.Use)
                            {
                                // Try to find the block
                                var existingBlock = currentPage.ResultQueries.Items.OfType<ResultQueryBlock>().Where(b => b.SourceInfo != null && b.SourceInfo.SourceName == query.SourceName && b.SourceInfo.SourceId == query.SourceId).FirstOrDefault();
                                if (existingBlock != null)
                                {
                                    anyChanges = true;

                                    // Remove the block from the page queries content area
                                    writablePage.ResultQueries.Items.Remove(new ContentAreaItem
                                    {
                                        ContentLink = ((IContent)existingBlock).ContentLink
                                    });
                                }
                            }
                        }
                    }
                }
            }

            if (anyChanges)
            {
                // Save the page
                contentRepository.Save((IContent)writablePage, SaveAction.Save);
            }
            
            return RedirectToAction("Index");
        }

        private List<ResultQueryBlock> GetExistingQueryBlocks(CompareResultPage currentPage)
        {
            List<ResultQueryBlock> values = new List<ResultQueryBlock>();
            if (currentPage.ResultQueries != null)
            {
                foreach (var item in currentPage.ResultQueries.Items)
                {
                    var content = item.GetContent();
                    var resultQueryBlock = content as ResultQueryBlock;
                    if (resultQueryBlock != null)
                    {
                        values.Add(resultQueryBlock);
                    }
                }
            }

            return values;
        }

        private List<OrganisationalUnitModel> GetOrganisationalUnits(CompareResultPage currentPage)
        {
            List<int> cookies = _cookieHelper.GetCookie(currentPage.ParentLink.ID);
            List<OrganisationalUnitModel> oUnitsList = new List<OrganisationalUnitModel>();

            foreach (int ou in cookies)
            {
                OrganisationalUnitPage page = _contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ParentLink).Where(o => o.ContentLink.ID == ou).First();
                OrganisationalUnitModel pageModel = new OrganisationalUnitModel
                {
                    Id = page.ContentLink.ID,
                    Title = page.Name,
                    Link = CompareHelper.GetExternalUrl(page),
                    TitleImage = page.TitleImage,
                    Adress = page.Address,
                    Telephone = page.Telephone,
                    Email = page.Email
                };

                oUnitsList.Add(pageModel);
            }

            return oUnitsList;
        }
    }
}

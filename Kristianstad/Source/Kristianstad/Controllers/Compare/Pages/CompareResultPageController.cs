﻿using System.Collections.Generic;
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
using Kristianstad.CompareDomain.Models;

namespace Kristianstad.Controllers.Compare
{
    public class CompareResultPageController : PageController<CompareResultPage>
    {
        private readonly Injected<IContentLoader> contentLoader;
        private CookieHelper cookieHelper;

        public CompareResultPageController()
        {
            cookieHelper = new CookieHelper();
        }

        public ActionResult Index(CompareResultPage currentPage, string address = null)
        {
            var model = new CompareResultPageModel(currentPage);

            // Get existing queries in category page (if found)
            List<ResultQueryBlock> existingQueryBlocks = GetExistingQueryBlocks(currentPage);

            // Get all web service queries
            var webServiceQueries = CompareServiceFactory.Instance.GetWebServicePropertyQueries();

            // Set queries to view model
            foreach (var source in webServiceQueries)
            {
                model.PropertyQueryGroupsFromSources.AddRange(source.Value.Select(g => new PropertyQueryGroupModel()
                {
                    Name = g.Name,
                    SourceName = g.SourceName,
                    SourceId = g.SourceId,
                    InfoReadAt = g.InfoReadAt,
                    PropertyQueries = g.Queries.Select(q => new PropertyQueryModel()
                    {
                        SourceName = q.SourceName,
                        SourceId = q.SourceId,
                        Name = q.Name,
                        InfoReadAt = q.InfoReadAt,
                        Use = existingQueryBlocks != null && existingQueryBlocks.Any(eq => eq.SourceInfo.SourceName == q.SourceName && eq.SourceInfo.SourceId == q.SourceId),
                        UseBefore = existingQueryBlocks != null && existingQueryBlocks.Any(eq => eq.SourceInfo.SourceName == q.SourceName && eq.SourceInfo.SourceId == q.SourceId)
                    }).ToList()
                }).ToList());
            }

            model.DistanceList = new DistanceFromAddressModel()
            {
                MeasureFromAddress = address
            };


            return View(model);
        }

        public ActionResult ClearList(CompareResultPage currentPage, string redirectBackTo = null)
        {
            if (currentPage != null)
            {
                cookieHelper.ClearCompare(currentPage.ContentLink);
            }

            if (!string.IsNullOrWhiteSpace(redirectBackTo))
            {
                return Redirect(redirectBackTo);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveResultQueries(CompareResultPage currentPage, List<PropertyQueryGroupModel> propertyQueryGroupsFromSources)
        {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
            ContentAssetFolder folder = contentAssetHelper.GetOrCreateAssetFolder(currentPage.ContentLink);

            bool anyChanges = false;

            // Create writable clone of the page to be able to update it
            var writablePage = (CompareResultPage)currentPage.CreateWritableClone();

            if (propertyQueryGroupsFromSources != null)
            {
                foreach (var group in propertyQueryGroupsFromSources)
                {
                    foreach (var query in group.PropertyQueries)
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
                                if (writablePage.CompareResults.PropertyQueries == null)
                                {
                                    writablePage.CompareResults.PropertyQueries = new ContentArea();
                                }

                                // Add the new block to the page queries content area
                                writablePage.CompareResults.PropertyQueries.Items.Add(new ContentAreaItem
                                {
                                    ContentLink = ((IContent)block).ContentLink
                                });
                            }
                            else if (!query.Use)
                            {
                                // Try to find the block
                                List<ResultQueryBlock> rqbList = GetExistingQueryBlocks(currentPage);
                                ResultQueryBlock existingBlock = rqbList.Where(b => b.SourceInfo != null && b.SourceInfo.SourceName == query.SourceName && b.SourceInfo.SourceId == query.SourceId).FirstOrDefault();

                                if (existingBlock != null)
                                {
                                    // Remove the block from the page queries content area
                                    anyChanges = true;
                                    ContentAreaItem cai = writablePage.CompareResults.PropertyQueries.Items.Where(o => o.ContentLink.ID == ((IContent)existingBlock).ContentLink.ID).FirstOrDefault();
                                    writablePage.CompareResults.PropertyQueries.Items.Remove(cai);
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
            if (currentPage.CompareResults.PropertyQueries != null)
            {
                foreach (var item in currentPage.CompareResults.PropertyQueries.Items)
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

    }
}

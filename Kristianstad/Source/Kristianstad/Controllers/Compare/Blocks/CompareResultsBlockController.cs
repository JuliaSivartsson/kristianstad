using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation;
using EPiServer.DataAbstraction;
using System.Text;
using System;
using System.Text.RegularExpressions;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages.Compare;
using Kristianstad.Business.Compare;
using Kristianstad.CompareDomain;
using Kristianstad.CompareDomain.Models;
using Kristianstad.HtmlHelpers;

namespace Kristianstad.Controllers.Compare
{
    public class CompareResultsBlockController : BlockController<CompareResultsBlock>
    {
        private readonly Injected<IContentLoader> contentLoader;
        private CookieHelper cookieHelper;

        public CompareResultsBlockController()
        {
            cookieHelper = new CookieHelper();
        }

        public override ActionResult Index(CompareResultsBlock currentBlock)
        {
            var model = CreateModel(currentBlock);
            return PartialView(model);
        }

        public ActionResult Preview(CompareResultsBlock compareResultsBlock)
        {
            var model = CreateModel(compareResultsBlock);
            return PartialView(model);
        }

        private CompareResultsModel CreateModel(CompareResultsBlock compareResultsBlock)
        {
            var model = new CompareResultsModel();

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            var currentPage = pageRouteHelper.Page;

            if (currentPage != null)
            {
                if (compareResultsBlock.PropertyQueries == null || compareResultsBlock.PropertyQueries.Items.Count <= 0)
                {
                    // try to get compare results block via parent category page
                    if (currentPage is OrganisationalUnitPage)
                    {
                        var organisationalUnitPage = currentPage as OrganisationalUnitPage;
                        var compareResultsPageReference = CompareHelper.GetCompareResultPage(contentLoader.Service, organisationalUnitPage);
                        if (compareResultsPageReference != null)
                        {
                            var compareResultsPage = contentLoader.Service.Get<CompareResultPage>(compareResultsPageReference);
                            if (compareResultsPage != null)
                            {
                                compareResultsBlock = compareResultsPage.CompareResults;
                            }
                        }
                    }
                }

                // Get existing queries in category page (if found)
                List<ResultQueryBlock> existingQueryBlocks = GetExistingQueryBlocks(compareResultsBlock);

                model.OrganisationalUnits = GetOrganisationalUnitModels(currentPage);

                if (existingQueryBlocks.Count > 0 && model.OrganisationalUnits.Count > 0)
                {
                    List<PropertyQuery> existingQueries = existingQueryBlocks.Select(b => new PropertyQuery()
                    {
                        SourceId = b.SourceInfo.SourceId,
                        SourceName = b.SourceInfo.SourceName,
                        Name = b.SourceInfo.Name
                    }).ToList();

                    // get query results for existing queries and organisational units
                    var organisationalUnits = model.OrganisationalUnits.Select(x => x.ToDomainModel()).ToList();
                    var webServiceQueriesResults = CompareServiceFactory.Instance.GetWebServicePropertyResults(existingQueries, organisationalUnits);
                    model.QueriesWithResults = webServiceQueriesResults;
                }
            }

            return model;
        }

        private List<OrganisationalUnitModel> GetOrganisationalUnitModels(PageData currentPage)
        {
            List<OrganisationalUnitModel> oUnitsList = new List<OrganisationalUnitModel>();
            if (currentPage != null)
            {
                if (currentPage is OrganisationalUnitPage)
                {
                    var organisationalUnitPage = currentPage as OrganisationalUnitPage;
                    oUnitsList.Add(new OrganisationalUnitModel(organisationalUnitPage));
                }
                else if (currentPage is CompareResultPage)
                {
                    var organisationalUnitsInCompare = cookieHelper.GetOrganisationalUnitsInCompare(contentLoader.Service, currentPage.ContentLink);

                    foreach (var organisationalUnit in organisationalUnitsInCompare)
                    {
                        var page = contentLoader.Service.Get<PageData>(new ContentReference(organisationalUnit.ID)); // (currentPage.ParentLink).Where(o => o.ContentLink.ID == ou).First();
                        OrganisationalUnitPage organisationalUnitPage = page != null && page is OrganisationalUnitPage ? page as OrganisationalUnitPage : null;
                        if (organisationalUnitPage != null)
                        {
                            oUnitsList.Add(new OrganisationalUnitModel(organisationalUnitPage));
                        }
                    }
                }
            }

            return oUnitsList;
        }

        /*
        private List<ResultQueryBlock> GetResultQueryBlocks(IList<ContentAreaItem> existingItems)
        {
            List<ResultQueryBlock> list = new List<ResultQueryBlock>();
            var repository = ServiceLocator.Current.GetInstance<IContentRepository>();

            foreach (ContentAreaItem cai in existingItems)
            {
                var contentReference = new ContentReference(cai.ContentLink.ID);
                ResultQueryBlock rqb = repository.Get<ResultQueryBlock>(contentReference);

                if (rqb != null)
                {
                    list.Add(rqb);
                }
            }

            return list;
        }
        */

        private List<ResultQueryBlock> GetExistingQueryBlocks(CompareResultsBlock block)
        {
            List<ResultQueryBlock> values = new List<ResultQueryBlock>();
            if (block.PropertyQueries != null)
            {
                foreach (var item in block.PropertyQueries.Items)
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

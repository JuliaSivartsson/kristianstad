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

namespace Kristianstad.Controllers.Compare
{
    public class CategoryController : PageController<CategoryPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;

        public ActionResult Index(CategoryPage currentPage)
        {
            var model = new CategoryPageModel(currentPage);

            if (PageEditing.PageIsInEditMode)
            {

                // Get existing queries in category page (if found)
                IEnumerable<ResultQueryBlock> existingQueries = null;
                var categoryPage = _contentLoader.Service.GetAncestors(currentPage.ContentLink).OfType<CategoryPage>().FirstOrDefault();
                if (categoryPage != null)
                {
                    existingQueries = categoryPage.ResultQueries != null ? categoryPage.ResultQueries.Items.OfType<ResultQueryBlock>() : null;
                }

                // Get all web service queries
                var webServiceQueries = CompareServiceFactory.Instance.GetWebServicePropertyQueries();

                // Set queries to view model
                model.ResultQueryGroups = webServiceQueries.Select(g => new ResultQueryGroupModel()
                {
                    Name = g.Title,
                    SourceName = g.SourceName,
                    SourceId = g.SourceId,
                    InfoReadAt = g.InfoReadAt,
                    ResultQueries = g.Queries.Select(q => new ResultQueryModel()
                    {
                        SourceName = q.SourceName,
                        SourceId = q.SourceId,
                        Name = q.Title,
                        InfoReadAt = q.InfoReadAt,
                        Use = existingQueries != null && existingQueries.Any(eq => eq.SourceInfo.SourceName == q.SourceName && eq.SourceInfo.SourceId == q.SourceId)
                    }).ToList()
                }).ToList();
            }
            
            return View(model);
        }

        public ActionResult SaveResultQueries(List<ResultQueryGroupModel> resultQueryGroups)
        {
            resultQueryGroups = resultQueryGroups;
            return null;
        }
    }
}

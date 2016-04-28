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
using Kristianstad.Models.Pages;
using Kristianstad.Business.Compare;
using Kristianstad.ViewModels.Compare;

namespace Kristianstad.Controllers.Compare
{
    /*
    public class AddOrganisationalUnitsBlockController : BlockController<AddOrganisationalUnitsBlock>
    {
        private readonly Injected<IContentLoader> contentLoader; // private IContentLoader contentLoader;

        public override ActionResult Index(AddOrganisationalUnitsBlock currentBlock)
        {
            //var category = Request.RequestContext.GetCustomRouteData<Category>("category");

            var organisationalUnits = FindPages(currentBlock); //, category);
            organisationalUnits = Sort(organisationalUnits, currentBlock.SortOrder);
            
            var model = new OrganisationalUnitListModel() // (currentBlock)
            {
                OrganisationalUnits = organisationalUnits
                //Heading = string.Empty //category != null ? "Category tags for post: "+category.Name : string.Empty
            };

            return PartialView(model);
        }

        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitListModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;
            PreviewTextLength = 200;

            var model = new OrganisationalUnitPageModel(pd)
            {
                Categories = CategoryHelper.GetCategoryViewModels(pd),
                PreviewText = GetPreviewText(pd),
                //ShowIntroduction = organisationalUnitListModel.ShowIntroduction,
                //ShowPublishDate = organisationalUnitListModel.ShowPublishDate
            };

            return PartialView("Preview", model);
        }
        

    }
    */
}

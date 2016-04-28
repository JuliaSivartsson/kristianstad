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
using Kristianstad.Models.Pages;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Business.Compare;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiCore.ViewModels.Pages;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitController : PageController<OrganisationalUnitPage>
    {
        private readonly Injected<IContentLoader> _contentLoader;

        public int PreviewTextLength { get; set; }
        
        public ActionResult Preview(PageData currentPage, OrganisationalUnitListModel organisationalUnitModel)
        {
            var pd = (OrganisationalUnitPage)currentPage;
            PreviewTextLength = 200;

            var model = new OrganisationalUnitPageModel(pd)
            {
                Categories = CategoryHelper.GetCategoryViewModels(pd),
                PreviewText = GetPreviewText(pd),

                // ShowIntroduction = organisationalUnitModel.ShowIntroduction,
                // ShowPublishDate = organisationalUnitModel.ShowPublishDate
            };

            return PartialView("Preview", model);
        }

        public ActionResult Full(OrganisationalUnitPage currentPage)
        {
            PreviewTextLength = 200;

            var model = new OrganisationalUnitPageModel(currentPage)
            {
                Category = currentPage.Category,
                Categories = CategoryHelper.GetCategoryViewModels(currentPage),
                PreviewText = GetPreviewText(currentPage),
                // MainBody = currentPage.MainBody,
                StartPublish = currentPage.StartPublish
            };

            var editHints = ViewData.GetEditHints<OrganisationalUnitPageModel, OrganisationalUnitPage>();
            editHints.AddConnection(m => m.Category, p => p.Category);
            editHints.AddFullRefreshFor(p => p.Category);
            editHints.AddFullRefreshFor(p => p.StartPublish);
            
           

            return PartialView("Full", model);
        }

        public ActionResult Index(OrganisationalUnitPage currentPage)
        {
            //var model = new PageViewModel<OrganisationalUnitPage>(currentPage);
           
            var model = new OrganisationalUnitPageModel(currentPage); //currentPage; // PageViewModel.Create(currentPage);
            //if (currentPage.OrganisationalUnitBlock != null)
            //{
            //    model.OrganisationalUnitBlock = _contentLoader.Service.Get<OrganisationalUnitBlock>(currentPage.OrganisationalUnitBlock);
            //}

            // Connect the view models logotype property to the start page's to make it editable
            var editHints = ViewData.GetEditHints<OrganisationalUnitPage, OrganisationalUnitPage>();
            editHints.AddConnection(m => m.Category, p => p.Category);
            editHints.AddConnection(m => m.StartPublish, p => p.StartPublish);
            
            return View(model);
        }

        
        protected string GetPreviewText(OrganisationalUnitPage page)
        {
            if (PreviewTextLength <= 0)
            {
                return string.Empty;
            }

            string previewText = String.Empty;

            /*
            if (page.MainBody != null)
            {
                previewText = page.MainBody.ToHtmlString();
            }
            */

            if (String.IsNullOrEmpty(previewText))
            {
                return string.Empty;
            }

            //If the MainBody contains DynamicContents, replace those with an empty string
            StringBuilder regexPattern = new StringBuilder(@"<span[\s\W\w]*?classid=""");
            regexPattern.Append(DynamicContentFactory.Instance.DynamicContentId.ToString());
            regexPattern.Append(@"""[\s\W\w]*?</span>");
            previewText = Regex.Replace(previewText, regexPattern.ToString(), string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return TextIndexer.StripHtml(previewText, PreviewTextLength);
        }

    
    }
}

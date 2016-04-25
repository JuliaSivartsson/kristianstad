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
    public class OrganisationalUnitListBlockController : BlockController<OrganisationalUnitListBlock>
    {
        public int PreviewTextLength { get; set; }

        private readonly Injected<IContentLoader> contentLoader; // private IContentLoader contentLoader;

        public override ActionResult Index(OrganisationalUnitListBlock currentBlock)
        {
            //var category = Request.RequestContext.GetCustomRouteData<Category>("category");

            var organisationalUnits = FindPages(currentBlock); //, category);
            organisationalUnits = Sort(organisationalUnits, currentBlock.SortOrder);

            /*
            if(currentBlock.Count > 0)
            {
                organisationalUnits = organisationalUnits.Take(currentBlock.Count);
            }
            */

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

        private PageData GetCompareStartPage(PageData currentPage)
        {
            if (currentPage.PageTypeName == typeof(CompareStartPage).GetPageType().Name)
            {
                return currentPage;
            }
            if (currentPage.ParentLink != null)
            {
                return GetCompareStartPage(contentLoader.Service.Get<PageData>(currentPage.ContentLink)); //DataFactory.Instance.GetPage(currentPage.ParentLink));
            }
            return null;
        }
        private IEnumerable<PageData> FindPages(OrganisationalUnitListBlock currentBlock) //, Category categoryParameter)
        {
            IEnumerable<PageData> pages = null;

            var pageRouteHelper = ServiceLocator.Current.GetInstance<PageRouteHelper>();
            PageData currentPage = pageRouteHelper.Page ?? contentLoader.Service.Get<PageData>(ContentReference.StartPage);

            //var ancestorPage = contentLoader.Service.Get<PageData>(currentPage.ParentLink);

            var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
            var category = CategoryHelper.FindCompareCategory(categoryRepository, currentPage.Name);

            if (category != null && currentPage is CategoryPage)
            {
                var ouPages = contentLoader.Service.GetChildren<OrganisationalUnitPage>(currentPage.ContentLink).Where(o => o.Category.Contains(category.ID)).ToList();
                if (ouPages != null && ouPages.Count > 0)
                {
                    pages = ouPages;
                }

                //var listRoot = currentBlock.Root ?? currentPage.ContentLink.ToPageReference();
                /*var compareStartPage = contentLoader.Service.Get<CompareStartPage>(currentPage.ParentLink); //GetCompareStartPage(currentPage);


                if (compareStartPage != null)
                {
                    var ouFolderPage = contentLoader.Service.GetChildren<OrganisationalUnitFolderPage>(compareStartPage.ContentLink).FirstOrDefault();
                    if (ouFolderPage != null)
                    {

                    }
                    //contentLoader.Service.GetChildren<PageData>(compareStartPage.ContentLink.ToPageReference());
                    //PageReference ouPage = CompareInitialization.GetOrganisationalUnitsPageRef(compareStartPage, contentRepository);

                }*/
            }

            return pages ?? new List<PageData>();
        }

        private IEnumerable<PageData> Sort(IEnumerable<PageData> pages, FilterSortOrder sortOrder)
        {
            var asCollection = new PageDataCollection(pages);
            var sortFilter = new FilterSort(sortOrder);
            sortFilter.Sort(asCollection);
            return asCollection;
        }


    }
}

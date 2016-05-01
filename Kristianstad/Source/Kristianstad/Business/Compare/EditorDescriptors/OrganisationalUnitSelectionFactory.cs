using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Cms.Shell.UI.ObjectEditing;
using EPiServer;
using EPiServer.ServiceLocation;
using Kristianstad.CompareDomain;
using Kristianstad.CompareDomain.Models;

namespace Kristianstad.Business.Compare.EditorDescriptors
{
    public class OrganisationalUnitSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            if (metadata is ContentDataMetadata)
            {
                var contentMetadata = metadata as ContentDataMetadata;
                var ownerContent = contentMetadata.OwnerContent as IContent;

                if (ownerContent is CategoryPage)
                {
                    var categoryPage = ownerContent as CategoryPage;
                    if (categoryPage.ContentLink.ID > 0)
                    {
                        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
                        var categoryOUPages = contentLoader.GetChildren<PageData>(categoryPage.ContentLink, LanguageSelector.AutoDetect(true)).OfType<OrganisationalUnitPage>();

                        // Get organisational unit info from web service(s)
                        List<OrganisationalUnit> organisationalUnits = CompareServiceFactory.Instance.GetWebServiceOrganisationalUnits();
                        var organisationalUnitsNotInCategory = organisationalUnits != null ? organisationalUnits.Where(x => !categoryOUPages.Any(x2 => x2.SourceInfo != null && (x.SourceName == x2.SourceInfo.SourceName && x.SourceId == x2.SourceInfo.SourceId))) : null;

                        return OrganisationalUnitHelper.GetSelectItems(organisationalUnitsNotInCategory);
                    }
                }
            }

            return Enumerable.Empty<SelectItem>();
        }
    }
}
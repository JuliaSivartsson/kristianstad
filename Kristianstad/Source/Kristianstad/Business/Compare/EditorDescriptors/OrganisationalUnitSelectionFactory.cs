using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using Kristianstad.Models.Pages;
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
                    var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
                    var categoryPage = ownerContent as CategoryPage;
                    var categoryOUPages = contentLoader.GetChildren<PageData>(categoryPage.ContentLink).OfType<OrganisationalUnitPage>();

                    // Get organisational unit info from web service(s)
                    List<OrganisationalUnit> organisationalUnits = CompareServiceFactory.Instance.GetWebServiceOrganisationalUnits();
                    IEnumerable<OrganisationalUnit> organisationalUnitsToAdd = organisationalUnits.Where(x => !categoryOUPages.Any(x2 => x.WebServiceName == x2.WebServiceName && x.OrganisationalUnitId == x2.OrganisationalUnitId));

                    return OrganisationalUnitHelper.GetSelectItems(organisationalUnitsToAdd);
                }
            }

            return Enumerable.Empty<SelectItem>();
        }
    }
}
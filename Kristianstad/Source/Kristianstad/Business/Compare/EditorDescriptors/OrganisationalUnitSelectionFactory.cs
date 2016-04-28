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
                    var childOUs = contentLoader.GetChildren<PageData>(categoryPage.ContentLink).OfType<OrganisationalUnitPage>();

                    List<SelectItem> items = new List<SelectItem>();
                    foreach (OrganisationalUnitPage ou in childOUs)
                    {
                        items.Add(new SelectItem
                        {
                            Text = ou.Name,
                            Value = ou.Name
                        });
                    }

                    return items;
                }
            }

            return Enumerable.Empty<SelectItem>();
        }
    }
}
using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.Business.Compare
{
    [Component]
    public class OrganisationalUnitsPaneNavigationComponent : ComponentDefinitionBase
    {
        public OrganisationalUnitsPaneNavigationComponent()
            : base("epi-cms.component.SharedBlocks")
        {
            Categories = new[] { "cms", "content" };
            Title = "Organisational units";
            SortOrder = 1000;
            PlugInAreas = new[] { PlugInArea.AssetsDefaultGroup };
            Settings.Add(new Setting("repositoryKey", OrganisationalUnitsPaneDescriptor.RepositoryKey));
            LanguagePath = "/components/organisational-units";
        }
    }
}
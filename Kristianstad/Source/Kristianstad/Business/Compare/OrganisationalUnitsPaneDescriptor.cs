using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using Kristianstad.Business.Models.Blocks.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.Business.Compare
{
    [ServiceConfiguration(typeof(IContentRepositoryDescriptor))]
    public class OrganisationalUnitsPaneDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey { get { return "organisational-units"; } }

        public override string Key { get { return RepositoryKey; } }

        public override string Name { get { return "Organisational units"; } }

        public override IEnumerable<Type> ContainedTypes
        {
            get { return new[] { typeof(OrganisationalUnitBlock), typeof(ContentFolder) }; }
        }

        public override IEnumerable<Type> CreatableTypes
        {
            get { return new List<Type> { typeof(OrganisationalUnitBlock) }; }
        }

        public override IEnumerable<ContentReference> Roots
        {
            get { return Enumerable.Empty<ContentReference>(); }
        }

        public override IEnumerable<Type> MainNavigationTypes
        {
            get { return new[] { typeof(ContentFolder) }; }
        }
    }
}
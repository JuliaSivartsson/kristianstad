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
    public class ContactsPaneDescriptor : ContentRepositoryDescriptorBase
    {
        public static string RepositoryKey { get { return "contacts"; } }

        public override string Key { get { return RepositoryKey; } }

        public override string Name { get { return "Contacts"; } }

        public override IEnumerable<Type> ContainedTypes
        {
            get { return new[] { typeof(ContactBlock), typeof(ContentFolder) }; }
        }

        public override IEnumerable<Type> CreatableTypes
        {
            get { return new List<Type> { typeof(ContactBlock) }; }
        }

        public override IEnumerable<ContentReference> Roots
        {
            get
            {
                return Enumerable.Empty<ContentReference>();
            }
        }

        public override IEnumerable<Type> MainNavigationTypes
        {
            get { return new[] { typeof(ContentFolder) }; }
        }

        public bool EnableContextualContent { get { return true; } }
    }
}
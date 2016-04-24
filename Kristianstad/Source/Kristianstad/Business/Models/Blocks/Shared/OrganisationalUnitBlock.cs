using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Kristianstad.Business.Models.Blocks.Shared
{
    [ContentType(
        DisplayName = "OrganisationalUnitBlock",
        GroupName = "Compare",
        GUID = "c6fcb305-51b8-49fd-9c16-daec05b39b19",
        Description = "",
        AvailableInEditMode = true)]

    public class OrganisationalUnitBlock : ContentData, IContent //, IResourceable
    {
        private PropertyDataCollection _properties = new PropertyDataCollection();
        //private bool _isModified = false;

        public virtual string ContentAssetIdInternal { get; set; }

        public string Name { get; set; }
        public ContentReference ContentLink { get; set; }
        public ContentReference ParentLink { get; set; }
        public Guid ContentGuid { get; set; }
        public int ContentTypeID { get; set; }
        public bool IsDeleted { get; set; }
        
        public virtual string Body { get; set; }
        
        public PropertyDataCollection Property
        {
            get { return _properties; }
        }

        public bool IsNull
        {
            get { return _properties.Count == 0; }
        }

        /*
        protected override bool IsModified
        {
            get
            {
                return base.IsModified || _isModified;
            }
        }

        public Guid ContentAssetsID
        {
            get
            {
                Guid assetId;
                if (Guid.TryParse(ContentAssetIdInternal, out assetId))
                    return assetId;
                return Guid.Empty;
            }
            set
            {
                ContentAssetIdInternal = value.ToString();
                ThrowIfReadOnly();
                _isModified = true;
            }
        }
        */
    }
}
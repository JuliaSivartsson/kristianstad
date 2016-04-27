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
        public virtual string Body { get; set; }
        
        // IContent implementation
        public string Name { get; set; }
        public ContentReference ContentLink { get; set; }
        public ContentReference ParentLink { get; set; }
        public Guid ContentGuid { get; set; }
        public int ContentTypeID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
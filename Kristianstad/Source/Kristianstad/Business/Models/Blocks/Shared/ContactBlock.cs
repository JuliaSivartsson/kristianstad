using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Kristianstad.Business.Models.Blocks.Shared
{
    [ContentType(
        DisplayName = "ContactBlock",
        GroupName = "Compare",
        GUID = "37f711d1-d58b-4765-851e-258bf7352177",
        Description = "",
        AvailableInEditMode = true)]

    public class ContactBlock : ContentData, IContent //, IResourceable
    {
        public virtual string Email { get; set; }
        public virtual string Phonenumber { get; set; }
        public virtual string Title { get; set; }

        // IContent implementation
        public string Name { get; set; }
        public ContentReference ContentLink { get; set; }
        public ContentReference ParentLink { get; set; }
        public Guid ContentGuid { get; set; }
        public int ContentTypeID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
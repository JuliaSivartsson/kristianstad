using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Kristianstad.Business.Models.Blocks.Compare
{
    [ContentType(
        DisplayName = "ContactBlock", 
        GUID = "edc3071e-ad00-462a-ad67-f2663cfa240c",
        Description = "")]
    public class ContactBlock : BlockData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 1,
            Name = "Name")]
        public virtual string Name { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 2,
            Name = "Email")]
        public virtual string Email { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 3,
            Name = "Phone number")]
        public virtual string Phone { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 4,
            Name = "Title")]
        public virtual string Title { get; set; }


        #region IInitializableContent

        /// <summary>
        /// Sets the default property values on the content data.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

        }

        #endregion
    }
}
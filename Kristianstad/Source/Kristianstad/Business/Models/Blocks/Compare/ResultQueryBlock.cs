using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.Web.Mvc;

namespace Kristianstad.Business.Models.Blocks.Compare
{
    [ContentType(
        DisplayName = "PropertyQuery",
        GUID = "98781a85-4a2c-4e86-839c-f61d0f175f14",
        Description = "")]
    public class ResultQueryBlock : BlockData
    {
        [Display(
            GroupName = SystemTabNames.Content,
            Order = 1,
            Name = "Title")]
        public virtual string Title { get; set; }

        [Display(
            GroupName = SystemTabNames.Content,
            Order = 2,
            Name = "SourceInfo")]
        public virtual SourceInfoBlock SourceInfo { get; set; }

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
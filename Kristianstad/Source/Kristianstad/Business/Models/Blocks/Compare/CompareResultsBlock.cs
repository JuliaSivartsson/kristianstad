using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel;
using EPiServer.Filters;
using EPiCore.Content.Models.Misc;

namespace Kristianstad.Business.Models.Blocks.Compare
{

    [ContentType(
        GUID = "6359bfb8-ecd6-46c5-88e0-30b409611285",
        GroupName = "Compare",
        DisplayName = "Compare results block")]
    public class CompareResultsBlock : BlockData
    {

        [Display(GroupName = SystemTabNames.Content, Order = 2)]
        [AllowedTypes(new Type[] { typeof(ResultQueryBlock) })]
        public virtual ContentArea PropertyQueries { get; set; }


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
using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Compare;
using System.Collections.Generic;

namespace Kristianstad.Models.Pages.Compare
{
    [ContentType(
        GroupName = "Compare",
        DisplayName = "CompareResultPage", 
        GUID = "a6f2e517-1d09-4c54-8976-324227ccf0c6", 
        Description = "The page where compare results are presented")]
    public class CompareResultPage : ContentPage
    {
        [Display(GroupName = SystemTabNames.Content, Order = 1)]
        public virtual string Description { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 2)]
        [AllowedTypes(new Type[] { typeof(ResultQueryBlock) })]
        public virtual ContentArea PropertyQueries { get; set; }
    }
}
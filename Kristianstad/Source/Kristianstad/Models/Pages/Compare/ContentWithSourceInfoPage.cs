using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;

namespace Kristianstad.Models.Pages.Compare
{
    [ContentType(DisplayName = "ContentWithSourceInfoPage", GUID = "f7889656-1458-45f6-9be1-5b934d1f071d", Description = "")]
    public class ContentWithSourceInfoPage : ContentPage
    {
        public virtual string WebServiceName { get; set; }

        public virtual string EntityId { get; set; }

    }
}
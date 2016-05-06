using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Kristianstad.ViewModels.Compare
{
    [ContentType(DisplayName = "DistanceViewModel", GUID = "ca769587-8a94-41ea-851a-879c24d785ba", Description = "")]
    public class DistanceFromAddressModel : BlockData
    {
                public virtual string MeasureFromAddress { get; set; }
    }
}
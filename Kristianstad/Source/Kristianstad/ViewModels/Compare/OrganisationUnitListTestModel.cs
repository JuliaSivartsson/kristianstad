using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Kristianstad.ViewModels.Compare
{
    [ContentType(DisplayName = "OrganisationUnitListTestModel", GUID = "4704db53-8d0a-459e-aac7-7d8328ad8b78", Description = "")]
    public class OrganisationUnitListTestModel : BlockData
    {
        public virtual string MeasureFromAddress { get; set; }
    }
}
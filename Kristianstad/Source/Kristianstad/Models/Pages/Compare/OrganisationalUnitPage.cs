﻿using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiServer.Web;
using EPiCore.Content.Models.Blocks;
using Kristianstad.Business.Models.Blocks.Compare;

namespace Kristianstad.Models.Pages
{
    [ContentType(
        GroupName = "Compare",
        GUID = "56f6be6a-25b6-4459-a154-8a68b3af08f3",
        DisplayName = "Organisational unit page",
        Description = "A organisational unit page for the comparison service")]
    [AvailableContentTypes(
     Availability.Specific,
     Include = new System.Type[] { })]

    public class OrganisationalUnitPage : ContentPage
    {
        [Display(GroupName = SystemTabNames.Content)]
        public virtual string ShortDescription { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string LongDescription { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Address { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Telephone { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Contact { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string Email { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual string OrganisationalForm { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual double? Latitude { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual double? Longitude { get; set; }

        /*
        [UIHint(UIHint.Block)]
        [AllowedTypes(AllowedTypes = new System.Type[] { typeof(OrganisationalUnitBlock) })]
        public virtual ContentReference OrganisationalUnitBlock { get; set; }
        */

        [AllowedTypes(AllowedTypes = new System.Type[] { typeof(ContactBlock) })]
        public virtual ContentArea Contacts { get; set; }

        [Display(GroupName = SystemTabNames.Content)]
        public virtual ContentArea RightContentArea { get; set; }

    }
}
using EPiCore.ViewModels.Pages;
using EPiServer.Core;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.CompareDomain.Models;
using Kristianstad.HtmlHelpers;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitModel : SourceInfoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public ContentReference TitleImage { get; set; }

        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public bool Use { get; set; }
        public bool UseBefore { get; set; }

        public bool NameAlreadyExistsInCategory { get; set; }

        public OrganisationalUnitModel(OrganisationalUnit organisationalUnit)
        {
            Title = organisationalUnit.Name;
            
            SourceName = organisationalUnit.SourceName;
            SourceId = organisationalUnit.SourceId;
            Name = organisationalUnit.Name;
            InfoReadAt = organisationalUnit.InfoReadAt;
        }

        public OrganisationalUnitModel(OrganisationalUnitPage organisationalUnitPage)
        {
            Id = organisationalUnitPage.ContentLink.ID;
            Title = organisationalUnitPage.Name;
            Link = CompareHelper.GetExternalUrl(organisationalUnitPage.ContentLink);
            TitleImage = organisationalUnitPage.TitleImage;

            Address = organisationalUnitPage.Address;
            Telephone = organisationalUnitPage.Telephone;
            Email = organisationalUnitPage.Email;

            SourceName = organisationalUnitPage.SourceInfo.SourceName;
            SourceId = organisationalUnitPage.SourceInfo.SourceId;
            Name = organisationalUnitPage.SourceInfo.Name;
            InfoReadAt = organisationalUnitPage.SourceInfo.InfoReadAt;
        }

        public OrganisationalUnit ToDomainModel()
        {
            return new OrganisationalUnit()
            {
                SourceName = SourceName,
                SourceId = SourceId,
                Name = Name,
                InfoReadAt = InfoReadAt
            };
        }
    }
}
using EPiCore.ViewModels.Pages;
using EPiServer.Core;
using Kristianstad.Business.Models.Blocks.Compare;
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

        public string Adress { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }


        public bool Use { get; set; }
        public bool UseBefore { get; set; }

        public bool NameAlreadyExistsInCategory { get; set; }
    }
}
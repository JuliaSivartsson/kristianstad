﻿using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitListModel
    {
        public OrganisationalUnitListModel()
        {
            // Empty
        }

        /*
        public OrganisationalUnitListModel(OrganisationalUnitListBlock block)
        {
            //Heading = block.Heading;
            //ShowIntroduction = block.IncludeIntroduction;
            //ShowPublishDate = block.IncludePublishDate;
        }
        */
        /*
        public string Heading { get; set; }
        public bool ShowIntroduction { get; set; }
        public bool ShowPublishDate { get; set; }
        */

        public IEnumerable<PageData> OrganisationalUnits { get; set; }
    }
}
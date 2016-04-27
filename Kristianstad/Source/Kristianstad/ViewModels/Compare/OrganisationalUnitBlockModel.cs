using System.Collections.Generic;
using EPiServer.Core;
using Kristianstad.Business.Models.Blocks.Shared;

namespace Kristianstad.ViewModels.Compare
{
    public class OrganisationalUnitBlockModel
    {
        public OrganisationalUnitBlockModel(OrganisationalUnitBlock currentBlock)
        {
            CurrentBlock = currentBlock;
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

        public OrganisationalUnitBlock CurrentBlock { get; set; }
        public PageData CurrentPage { get; set; }
    }
}
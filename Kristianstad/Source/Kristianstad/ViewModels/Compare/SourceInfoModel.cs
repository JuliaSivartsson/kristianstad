using EPiCore.ViewModels.Pages;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class SourceInfoModel
    {
        public string SourceName { get; set; }

        public string SourceId { get; set; }

        public string Name { get; set; }

        public DateTime? InfoReadAt { get; set; }

    }
}
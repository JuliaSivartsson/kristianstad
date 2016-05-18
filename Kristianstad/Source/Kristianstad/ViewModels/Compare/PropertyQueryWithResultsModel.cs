using EPiCore.ViewModels.Pages;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.CompareDomain.Models;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class PropertyQueryWithResultsModel
    {
        public PropertyQueryModel Query { get; set; }

        public List<PropertyQueryResult> Results { get; set; }
    }
}
using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CompareListModel" /> class. Defines elements to be used to present a category list.
    /// </summary>
    public class CompareListModel
    {
        public CompareListModel()
        {
            OrganisationalUnits = new List<CompareModel>();
        }

        public List<CompareModel> OrganisationalUnits { get; set; }
    }
}
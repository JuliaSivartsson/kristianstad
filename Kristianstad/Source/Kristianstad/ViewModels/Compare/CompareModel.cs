using System.Collections.Generic;
using EPiServer.Core;

namespace Kristianstad.ViewModels.Compare
{
    /// <summary>
    /// The <see cref="CompareModel" /> class. Defines elements to be used to present a category list.
    /// </summary>
    public class CompareModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int ID { get; set; }
    }
}
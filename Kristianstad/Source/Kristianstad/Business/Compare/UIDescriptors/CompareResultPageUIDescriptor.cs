using EPiServer.Shell;
using Kristianstad.Models.Pages.Compare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.Business.Compare.UIDescriptors
{
    [UIDescriptorRegistration]
    public class CompareResultPageUIDescriptor : UIDescriptor<CompareResultPage>
    {
        public CompareResultPageUIDescriptor()
            : base(ContentTypeCssClassNames.Container)
        {
            IconClass = "epi-iconSearch";
        }
    }
}
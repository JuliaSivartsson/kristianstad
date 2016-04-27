using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using EPiServer.ServiceLocation;
using EPiServer.DataAbstraction;
using System.Text;
using System;
using System.Text.RegularExpressions;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Models.Pages;
using Kristianstad.Business.Compare;

namespace Kristianstad.Controllers.Compare
{
    
    public class ContactBlockController : BlockController<ContactBlock>
    {
        // private readonly Injected<IContentLoader> contentLoader;
        
        public override ActionResult Index(ContactBlock currentBlock)
        {
            return PartialView(currentBlock);
        }

        public ActionResult Preview(ContactBlock currentBlock)
        {
            return PartialView(currentBlock);
        }
    }

}

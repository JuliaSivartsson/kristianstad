// <copyright file="HeaderKrController.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Controllers.Common
{
    using System.Web.Mvc;
    using EPiCore.Controllers;
    using EPiServer;
    using EPiServer.Core;
    using ViewModels.Common;

    /// <summary>
    /// The <see cref="HeaderKrController" /> class. Controller for rendering the header of the current page.
    /// </summary>
    public class HeaderKrController : BaseController
    {
        /// <summary>
        /// Renders the header of the current page.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The rendered footer.</returns>
        [ChildActionOnly]
        public PartialViewResult Index(PageData currentPage)
        {
            var model = new HeaderViewModel
            {
                Adjust = PagePropertyService.GetSettingsPageProperty<Url>("Adjust"),
                EServices = PagePropertyService.GetSettingsPageProperty<Url>("EServices"),
                Contact = PagePropertyService.GetSettingsPageProperty<Url>("Contact"),
            };

            return PartialView("_Header", model);
        }
    }
}
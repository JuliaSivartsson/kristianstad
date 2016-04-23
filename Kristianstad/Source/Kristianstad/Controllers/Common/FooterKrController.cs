// <copyright file="FooterKrController.cs" company="Sigma AB">
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
    /// The <see cref="FooterKrController" /> class. Controller for rendering the footer of the current page.
    /// </summary>
    public class FooterKrController : BaseController
    {
        /// <summary>
        /// Renders the footer of the current page.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <returns>The rendered footer.</returns>
        [ChildActionOnly]
        public PartialViewResult Index(PageData currentPage)
        {
            var model = new FooterViewModel
            {
                LinkListArea = PagePropertyService.GetSettingsPageProperty<ContentArea>("LinkListArea"),
                Facebook = PagePropertyService.GetSettingsPageProperty<Url>("Facebook"),
                Twitter = PagePropertyService.GetSettingsPageProperty<Url>("Twitter"),
                LinkedIn = PagePropertyService.GetSettingsPageProperty<Url>("LinkedIn"),
                Flickr = PagePropertyService.GetSettingsPageProperty<Url>("Flickr"),
                YouTube = PagePropertyService.GetSettingsPageProperty<Url>("YouTube"),
                ContactDetails = PagePropertyService.GetSettingsPageProperty<XhtmlString>("ContactDetails")
            };

            return PartialView("_Footer", model);
        }
    }
}
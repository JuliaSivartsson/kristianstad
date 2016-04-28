// <copyright file="FooterIconSelectionFactory.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.UI.Factories
{
    using System.Collections.Generic;
    using EPiServer.Framework.Localization;
    using EPiServer.ServiceLocation;
    using EPiServer.Shell.ObjectEditing;

    /// <summary>
    /// The <see cref="FooterIconSelectionFactory"/> class. Produces items representing icons.
    /// </summary>
    [SelectionFactoryRegistration]
    public class FooterIconSelectionFactory : ISelectionFactory
    {
        private readonly Injected<LocalizationService> _localizationService;

        /// <summary>
        /// Gets the selections.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The selections.</returns>
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata) // TODO: Should this meta data be used!?
        {
            var selectItems = new List<SelectItem>
            {
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/binoculars"),
                    Value = "binoculars-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/crossroad"),
                    Value = "crossroad-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/ballon"),
                    Value = "ballon-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/camera"),
                    Value = "camera-icon"
                }
            };

            return selectItems;
        }
    }
}
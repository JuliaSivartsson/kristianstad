// <copyright file="MegaMenuIconSelectionFactory.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.UI.Factories
{
    using System.Collections.Generic;
    using EPiServer.Framework.Localization;
    using EPiServer.ServiceLocation;
    using EPiServer.Shell.ObjectEditing;

    /// <summary>
    /// The <see cref="MegaMenuIconSelectionFactory"/> class.
    /// Produces items representing icons to be used by tabs/sections in the Mega Menu.
    /// </summary>
    [SelectionFactoryRegistration]
    public class MegaMenuIconSelectionFactory : ISelectionFactory
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
                    Text = _localizationService.Service.GetString("/icons/family"),
                    Value = "family-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/heart"),
                    Value = "heart-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/camera"),
                    Value = "camera-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/environmentalhouse"),
                    Value = "environmental-house-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/truck"),
                    Value = "truck-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/handshake"),
                    Value = "handshake-icon"
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/icons/club"),
                    Value = "club-icon"
                }
            };

            return selectItems;
        }
    }
}